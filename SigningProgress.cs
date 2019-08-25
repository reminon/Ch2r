using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ch2r
{
	/// <summary>
	/// Summary description for SigningProgress.
	/// </summary>
	public class SigningProgress : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox signingGroupBox;
		public System.Windows.Forms.ProgressBar signProgressBar;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SigningProgress()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SigningProgress));
            this.signingGroupBox = new System.Windows.Forms.GroupBox();
            this.signProgressBar = new System.Windows.Forms.ProgressBar();
            this.signingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // signingGroupBox
            // 
            this.signingGroupBox.Controls.Add(this.signProgressBar);
            this.signingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signingGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.signingGroupBox.Location = new System.Drawing.Point(0, 0);
            this.signingGroupBox.Name = "signingGroupBox";
            this.signingGroupBox.Size = new System.Drawing.Size(344, 38);
            this.signingGroupBox.TabIndex = 0;
            this.signingGroupBox.TabStop = false;
            this.signingGroupBox.Text = "Signing:";
            // 
            // signProgressBar
            // 
            this.signProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signProgressBar.Location = new System.Drawing.Point(3, 16);
            this.signProgressBar.Name = "signProgressBar";
            this.signProgressBar.Size = new System.Drawing.Size(338, 19);
            this.signProgressBar.TabIndex = 0;
            // 
            // SigningProgress
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(344, 38);
            this.Controls.Add(this.signingGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(500, 72);
            this.MinimumSize = new System.Drawing.Size(216, 72);
            this.Name = "SigningProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SigningProgress";
            this.Load += new System.EventHandler(this.SigningProgress_Load);
            this.signingGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        private void SigningProgress_Load(object sender, EventArgs e)
        {

        }
	}
}
