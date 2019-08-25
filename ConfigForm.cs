using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Ch2r
{
	/// <summary>
	/// Summary description for ConfigForm.
	/// </summary>
	public class ConfigForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox sharedTB;
		private System.Windows.Forms.Button browseB;
		private System.Windows.Forms.FolderBrowserDialog sharedFBD;
		private System.Windows.Forms.Button okB;
		private System.Windows.Forms.Button cancelB;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConfigForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Skinning
			Global.Skin.SkinControl(this);

			Read();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ConfigForm));
			this.label1 = new System.Windows.Forms.Label();
			this.sharedTB = new System.Windows.Forms.TextBox();
			this.browseB = new System.Windows.Forms.Button();
			this.sharedFBD = new System.Windows.Forms.FolderBrowserDialog();
			this.okB = new System.Windows.Forms.Button();
			this.cancelB = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Shared Path:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// sharedTB
			// 
			this.sharedTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.sharedTB.Location = new System.Drawing.Point(80, 8);
			this.sharedTB.Name = "sharedTB";
			this.sharedTB.Size = new System.Drawing.Size(282, 20);
			this.sharedTB.TabIndex = 1;
			this.sharedTB.Text = "";
			// 
			// browseB
			// 
			this.browseB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browseB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.browseB.Location = new System.Drawing.Point(370, 6);
			this.browseB.Name = "browseB";
			this.browseB.Size = new System.Drawing.Size(32, 24);
			this.browseB.TabIndex = 2;
			this.browseB.Text = "...";
			this.browseB.Click += new System.EventHandler(this.browseB_Click);
			// 
			// sharedFBD
			// 
			this.sharedFBD.Description = "Path to Halo 2 Shared Maps...";
			// 
			// okB
			// 
			this.okB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okB.Location = new System.Drawing.Point(104, 56);
			this.okB.Name = "okB";
			this.okB.Size = new System.Drawing.Size(80, 24);
			this.okB.TabIndex = 3;
			this.okB.Text = "&OK";
			this.okB.Click += new System.EventHandler(this.okB_Click);
			// 
			// cancelB
			// 
			this.cancelB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelB.Location = new System.Drawing.Point(216, 56);
			this.cancelB.Name = "cancelB";
			this.cancelB.Size = new System.Drawing.Size(80, 24);
			this.cancelB.TabIndex = 4;
			this.cancelB.Text = "&Cancel";
			this.cancelB.Click += new System.EventHandler(this.cancelB_Click);
			// 
			// ConfigForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(410, 88);
			this.Controls.Add(this.cancelB);
			this.Controls.Add(this.okB);
			this.Controls.Add(this.browseB);
			this.Controls.Add(this.sharedTB);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConfigForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Configuration";
			this.Load += new System.EventHandler(this.ConfigForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Path of the shared maps.
		/// </summary>
		public string SharedPath
		{
			get {
				return sharedTB.Text;
			}
			set {
				if (!value.EndsWith("\\"))
					value += "\\";
				sharedTB.Text = value;
			}
		}

		/// <summary>
		/// Read configuration from Registry.
		/// </summary>
		public void Read()
		{
			RegistryKey ourKey = Registry.CurrentUser.OpenSubKey("Software\\Ch2r\\Config");
			
			if (ourKey != null) {
				SharedPath = (string)ourKey.GetValue("Shared Path", "");
			}
		}

		/// <summary>
		/// Save configuration to Registry.
		/// </summary>
		public void Save()
		{
			RegistryKey ourKey = Registry.CurrentUser.OpenSubKey("Software\\Ch2r\\Config", true);
			
			if (ourKey == null) {
				ourKey = Registry.CurrentUser.CreateSubKey("Software\\Ch2r\\Config");
			}

			ourKey.SetValue("Shared Path", SharedPath);
		}

		private void browseB_Click(object sender, System.EventArgs e)
		{
			DialogResult result = sharedFBD.ShowDialog();

			if (result == DialogResult.OK) {
				SharedPath = sharedFBD.SelectedPath;
			}
		}

		private void okB_Click(object sender, System.EventArgs e)
		{
			Save();
			Read();
			Close();
		}

		private void cancelB_Click(object sender, System.EventArgs e)
		{
			Read();
			Close();
		}

		private void ConfigForm_Load(object sender, System.EventArgs e)
		{
			Read();
		}
	}
}
