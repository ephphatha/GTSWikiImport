namespace GTSWikiImport
{
	partial class WikiGet
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.userTextBox = new System.Windows.Forms.TextBox();
			this.passTextBox = new System.Windows.Forms.TextBox();
			this.userLabel = new System.Windows.Forms.Label();
			this.passLabel = new System.Windows.Forms.Label();
			this.fetchButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// userTextBox
			// 
			this.userTextBox.Location = new System.Drawing.Point(74, 12);
			this.userTextBox.Name = "userTextBox";
			this.userTextBox.Size = new System.Drawing.Size(100, 20);
			this.userTextBox.TabIndex = 0;
			// 
			// passTextBox
			// 
			this.passTextBox.Location = new System.Drawing.Point(74, 38);
			this.passTextBox.Name = "passTextBox";
			this.passTextBox.Size = new System.Drawing.Size(100, 20);
			this.passTextBox.TabIndex = 1;
			this.passTextBox.UseSystemPasswordChar = true;
			// 
			// userLabel
			// 
			this.userLabel.AutoSize = true;
			this.userLabel.Location = new System.Drawing.Point(10, 15);
			this.userLabel.Name = "userLabel";
			this.userLabel.Size = new System.Drawing.Size(58, 13);
			this.userLabel.TabIndex = 2;
			this.userLabel.Text = "Username:";
			this.userLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// passLabel
			// 
			this.passLabel.AutoSize = true;
			this.passLabel.Location = new System.Drawing.Point(12, 41);
			this.passLabel.Name = "passLabel";
			this.passLabel.Size = new System.Drawing.Size(56, 13);
			this.passLabel.TabIndex = 3;
			this.passLabel.Text = "Password:";
			this.passLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fetchButton
			// 
			this.fetchButton.Location = new System.Drawing.Point(74, 64);
			this.fetchButton.Name = "fetchButton";
			this.fetchButton.Size = new System.Drawing.Size(75, 23);
			this.fetchButton.TabIndex = 4;
			this.fetchButton.Text = "Fetch";
			this.fetchButton.UseVisualStyleBackColor = true;
			this.fetchButton.Click += new System.EventHandler(this.onClick);
			// 
			// WikiGet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(189, 94);
			this.Controls.Add(this.fetchButton);
			this.Controls.Add(this.passLabel);
			this.Controls.Add(this.userLabel);
			this.Controls.Add(this.passTextBox);
			this.Controls.Add(this.userTextBox);
			this.MinimumSize = new System.Drawing.Size(205, 132);
			this.Name = "WikiGet";
			this.Text = "WikiFetch";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox userTextBox;
		private System.Windows.Forms.TextBox passTextBox;
		private System.Windows.Forms.Label userLabel;
		private System.Windows.Forms.Label passLabel;
		private System.Windows.Forms.Button fetchButton;
	}
}

