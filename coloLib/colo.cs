using Halo2;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Halo2Plugin
{
	/// <summary>
	/// Items stored in colo tag.
	/// </summary>
	public struct ColoItem
	{
		string name;
		float red;
		float green;
		float blue;
		int zero;

		public void Read(ref BinaryReader br)
		{
			name = new string(br.ReadChars(32));
			name = name.Substring(0, name.IndexOf("\0"));
			red = br.ReadSingle();
			green = br.ReadSingle();
			blue = br.ReadSingle();
			zero = br.ReadInt32();
		}

		public string Name
		{
			get {
				return name;
			}
			set {
				name = value.Substring(0, 31);
			}
		}

		/// <summary>
		/// Get/set the color stored in this item.
		/// </summary>
		public Color Color
		{
			get {
				return Color.FromArgb((int)(red * 255), (int)(green * 255), (int)(blue * 255));
			}
			set {
				red = value.R / 255;
				green = value.G / 255;
				blue = value.B / 255;
			}
		}
	}

	/// <summary>
	/// Control to show the colo tag.
	/// </summary>
	public class ColoLib : System.Windows.Forms.UserControl
	{
		// Remember members
		FileStream stream = null;
		long offset = 0;
		long secmagic = 0;
		long size = 0;
		string tag = "";

		// Other meta members
		Reflexive colors = new Reflexive();
		ColoItem[] items;

		// Controls
		private System.Windows.Forms.ListBox namesLB;
		private System.Windows.Forms.GroupBox detailsGB;
		private System.Windows.Forms.TextBox nameTB;
		private System.Windows.Forms.Label nameL;
		private System.Windows.Forms.Label colorL;
		private System.Windows.Forms.Label previewL;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ColoLib()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.namesLB = new System.Windows.Forms.ListBox();
			this.detailsGB = new System.Windows.Forms.GroupBox();
			this.previewL = new System.Windows.Forms.Label();
			this.colorL = new System.Windows.Forms.Label();
			this.nameTB = new System.Windows.Forms.TextBox();
			this.nameL = new System.Windows.Forms.Label();
			this.detailsGB.SuspendLayout();
			this.SuspendLayout();
			// 
			// namesLB
			// 
			this.namesLB.Dock = System.Windows.Forms.DockStyle.Left;
			this.namesLB.Location = new System.Drawing.Point(0, 0);
			this.namesLB.Name = "namesLB";
			this.namesLB.Size = new System.Drawing.Size(192, 264);
			this.namesLB.TabIndex = 0;
			this.namesLB.SelectedIndexChanged += new System.EventHandler(this.namesLB_SelectedIndexChanged);
			// 
			// detailsGB
			// 
			this.detailsGB.Controls.Add(this.previewL);
			this.detailsGB.Controls.Add(this.colorL);
			this.detailsGB.Controls.Add(this.nameTB);
			this.detailsGB.Controls.Add(this.nameL);
			this.detailsGB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.detailsGB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.detailsGB.Location = new System.Drawing.Point(192, 0);
			this.detailsGB.Name = "detailsGB";
			this.detailsGB.Size = new System.Drawing.Size(200, 272);
			this.detailsGB.TabIndex = 1;
			this.detailsGB.TabStop = false;
			this.detailsGB.Text = "Details";
			// 
			// previewL
			// 
			this.previewL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.previewL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.previewL.Location = new System.Drawing.Point(8, 104);
			this.previewL.Name = "previewL";
			this.previewL.Size = new System.Drawing.Size(184, 80);
			this.previewL.TabIndex = 3;
			this.previewL.Text = "Preview";
			this.previewL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorL
			// 
			this.colorL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.colorL.Location = new System.Drawing.Point(8, 80);
			this.colorL.Name = "colorL";
			this.colorL.Size = new System.Drawing.Size(64, 16);
			this.colorL.TabIndex = 2;
			this.colorL.Text = "Color:";
			// 
			// nameTB
			// 
			this.nameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.nameTB.Location = new System.Drawing.Point(8, 40);
			this.nameTB.Name = "nameTB";
			this.nameTB.Size = new System.Drawing.Size(184, 20);
			this.nameTB.TabIndex = 1;
			this.nameTB.Text = "";
			// 
			// nameL
			// 
			this.nameL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nameL.Location = new System.Drawing.Point(8, 24);
			this.nameL.Name = "nameL";
			this.nameL.Size = new System.Drawing.Size(48, 16);
			this.nameL.TabIndex = 0;
			this.nameL.Text = "Name:";
			// 
			// ColoLib
			// 
			this.Controls.Add(this.detailsGB);
			this.Controls.Add(this.namesLB);
			this.Name = "ColoLib";
			this.Size = new System.Drawing.Size(392, 272);
			this.detailsGB.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Load the plugin.
		/// </summary>
		public void LoadPlugin(FileStream stream, long offset, long secmagic, long size, string tag)
		{
			this.stream = stream;
			this.offset = offset;
			this.secmagic = secmagic;
			this.tag = tag;
			this.size = size;

			ReadData();
		}

		/// <summary>
		/// Read data from meta.
		/// </summary>
		void ReadData()
		{
			if (size == 0) return;

			BinaryReader br = new BinaryReader(stream);
			colors.ReadReflexive(ref br, (uint)secmagic, offset, SeekOrigin.Begin);

			br.BaseStream.Position = colors.Offset;

			items = new ColoItem[colors.Count];
			items.Initialize();

			// Clear list view
			namesLB.Items.Clear();
			
			for (int x = 0; x < colors.Count; x++) {
				items[x].Read(ref br);
				namesLB.Items.Add(items[x].Name);
			}

			namesLB.SelectedIndex = 0;
		}

		private void namesLB_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int idx = namesLB.SelectedIndex;
			nameTB.Text = items[idx].Name;
			previewL.BackColor = items[idx].Color;
			previewL.ForeColor = SwapColor(items[idx].Color);
		}

		private Color SwapColor(Color color)
		{
			int red, green, blue;
			red = 255 - color.R;
			green = 255 - color.G;
			blue = 255 - color.B;
			return Color.FromArgb(red, green, blue);
		}
	}
}
