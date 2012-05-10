using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTSWikiImport
{
	public partial class WikiGet : Form
	{
		public WikiGet()
		{
			InitializeComponent();
			this.AcceptButton = this.fetchButton;
		}

		private delegate void NotifyDownloadComplete();

		private System.Threading.Thread worker;

		private void onClick(object sender, EventArgs e)
		{
			this.worker = new System.Threading.Thread(this.downloadJumpBridgeData);

			//Disable all controls while downloading page data.
			this.fetchButton.Text = "Working";
			this.fetchButton.Enabled = false;
			this.userTextBox.Enabled = false;
			this.passTextBox.Enabled = false;

			this.worker.SetApartmentState(System.Threading.ApartmentState.STA);
			this.worker.Start();
		}

		private void onDownload()
		{
			this.worker.Join();
			this.worker = null;

			//Re-enable controls to allow another run.
			this.userTextBox.Enabled = true;
			this.passTextBox.Enabled = true;
			this.fetchButton.Enabled = true;
			this.fetchButton.Text = "Fetch";
		}

		private void downloadJumpBridgeData()
		{
			// Connect to the login page and download the form to grab the auth key needed to login.
			System.Net.HttpWebRequest loginPage = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://goonfleet.com/");
			loginPage.Proxy = null;
			System.Net.HttpWebResponse loginPageResponse = (System.Net.HttpWebResponse)loginPage.GetResponse();

			System.IO.StreamReader loginPageReader = new System.IO.StreamReader(loginPageResponse.GetResponseStream());
			String pageData = loginPageReader.ReadToEnd();
			int authKeyIndex = pageData.IndexOf("auth_key"); // Need to grab the random identifier from the login page used in the auth process
			int authKeyValueIndex = pageData.IndexOf("'", authKeyIndex + 10) + 1; // Length of the string "auth_key' ", + 1 to increment past the first apostrophe
			String authKey = pageData.Substring(authKeyValueIndex, pageData.IndexOf("'", authKeyValueIndex) - authKeyValueIndex); // Grab the value enclosed in apostrophes

			// Using the auth key and user credentials, build the HTTP POST request as expected by Goonfleet.com
			System.Net.HttpWebRequest loginRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://goonfleet.com/index.php?app=core&module=global&section=login&do=process");
			loginRequest.Proxy = null;
			loginRequest.CookieContainer = new System.Net.CookieContainer();
			loginRequest.Method = "POST";
			loginRequest.Referer = "https://goonfleet.com/";
			loginRequest.ContentType = "application/x-www-form-urlencoded";

			ASCIIEncoding encoding = new ASCIIEncoding();
			String postData = "auth_key=" + authKey + "&referer=" + System.Uri.EscapeDataString("https://goonfleet.com/") + "&username=" + System.Uri.EscapeDataString(this.userTextBox.Text) + "&password=" + System.Uri.EscapeDataString(this.passTextBox.Text);
			byte[] data = encoding.GetBytes(postData);

			// And send the data to the server
			System.IO.Stream loginRequestStream = loginRequest.GetRequestStream();
			loginRequestStream.Write(data, 0, data.Length);
			loginRequestStream.Close();

			// Grabbing the response for debugging - anything other than a 200/OK will throw an error anyway.
			System.Net.HttpWebResponse loginResponse = (System.Net.HttpWebResponse)loginRequest.GetResponse();

			// Turns out status codes aren't used to indicate unsuccessful login attempts - will need to parse this page for the error message. Later.
			System.IO.StreamReader loginResponseReader = new System.IO.StreamReader(loginResponse.GetResponseStream());
			String loginResponseString = loginResponseReader.ReadToEnd();

			// Build the request to download the jump bridge list.
			System.Net.HttpWebRequest jumpBridgePageRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://wiki.goonfleet.com/Jump_bridge");
			jumpBridgePageRequest.Proxy = null;
			jumpBridgePageRequest.CookieContainer = loginRequest.CookieContainer; // Use the cookie returned during the login process to allow access to the wiki.

			// And download the page.
			System.Net.HttpWebResponse jumpBridgePageResponse = (System.Net.HttpWebResponse)jumpBridgePageRequest.GetResponse();
			System.IO.StreamReader jumpBridgeDataReader = new System.IO.StreamReader(jumpBridgePageResponse.GetResponseStream());

			// For now, the jump bridge info is contained in a table. Lets hope this doesn't change.
			HtmlAgilityPack.HtmlDocument jumpBridgePageDocument = new HtmlAgilityPack.HtmlDocument();
			jumpBridgePageDocument.LoadHtml(jumpBridgeDataReader.ReadToEnd());

			String jumpBridgeText = "";

			try
			{
				// Parse each table
				foreach (HtmlAgilityPack.HtmlNode tableNode in jumpBridgePageDocument.DocumentNode.SelectNodes(".//table"))
				{
					try
					{
						// This loop is not actually needed.
						foreach (HtmlAgilityPack.HtmlNode tableRow in tableNode.SelectNodes(".//tr"))
						{
							try
							{
								// Grab every cell and strip out the plain text - then build a tab seperated string out of the values.
								foreach (HtmlAgilityPack.HtmlNode tableData in tableRow.SelectNodes(".//td"))
								{
									jumpBridgeText += tableData.InnerText;

									// Avoiding unnecesary whitespace at the start/end of a line.
									if (!tableData.InnerText.Contains("\n"))
									{
										jumpBridgeText += "\t";
									}
								}
							}
							catch (NullReferenceException)
							{
								continue;
							}
						}
					}
					catch (NullReferenceException)
					{
						continue;
					}
				}
			}
			catch (NullReferenceException)
			{
				// Goggles
			}

			// Finally, dump the parsed string on the clipboard so GTS can import the data.
			Clipboard.SetText(jumpBridgeText);

			this.BeginInvoke(new NotifyDownloadComplete(this.onDownload));
		}
	}
}
