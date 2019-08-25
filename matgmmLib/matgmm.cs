using Halo2;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Halo2Plugin
{
	public struct MatgmmItem
	{
		public int _value;
		public string name_eng;

		public void Read(ref BinaryReader br)
		{
			long pos = br.BaseStream.Position;

			_value = br.ReadInt32();
			br.ReadInt32();
			br.ReadInt32();
			name_eng = ReadUnicode(ref br, 64);

			br.BaseStream.Position = pos + 3172;
		}

		public static string ReadUnicode(ref BinaryReader br, int count)
		{
			UnicodeEncoding ue = new UnicodeEncoding();

			byte[] bytes = br.ReadBytes(count);
			
			return ue.GetString(bytes);
		}

		public override string ToString()
		{
			if (_value != -1)
				return name_eng;
			else
				return "Empty";
		}
	}

	public class MatgmmLib : System.Windows.Forms.UserControl
	{
		// Remember members
		FileStream stream = null;
		long offset = 0;
		long secmagic = 0;
		long size = 0;
		string tag = "";
		public Map map = null;

		// Other members
		Reflexive maps = new Reflexive();
		MatgmmItem[] items;

		// Controls
		private System.Windows.Forms.ListBox namesLB;
		private System.Windows.Forms.GroupBox detailsGB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox valueTB;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox nameengTB;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MatgmmLib()
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
			this.label1 = new System.Windows.Forms.Label();
			this.valueTB = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.nameengTB = new System.Windows.Forms.TextBox();
			this.detailsGB.SuspendLayout();
			this.SuspendLayout();
			// 
			// namesLB
			// 
			this.namesLB.Dock = System.Windows.Forms.DockStyle.Left;
			this.namesLB.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.namesLB.ItemHeight = 15;
			this.namesLB.Location = new System.Drawing.Point(0, 0);
			this.namesLB.Name = "namesLB";
			this.namesLB.Size = new System.Drawing.Size(208, 289);
			this.namesLB.TabIndex = 0;
			this.namesLB.SelectedIndexChanged += new System.EventHandler(this.namesLB_SelectedIndexChanged);
			// 
			// detailsGB
			// 
			this.detailsGB.Controls.Add(this.nameengTB);
			this.detailsGB.Controls.Add(this.label2);
			this.detailsGB.Controls.Add(this.pictureBox1);
			this.detailsGB.Controls.Add(this.valueTB);
			this.detailsGB.Controls.Add(this.label1);
			this.detailsGB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.detailsGB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.detailsGB.Location = new System.Drawing.Point(208, 0);
			this.detailsGB.Name = "detailsGB";
			this.detailsGB.Size = new System.Drawing.Size(320, 296);
			this.detailsGB.TabIndex = 1;
			this.detailsGB.TabStop = false;
			this.detailsGB.Text = "Details";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Value:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// valueTB
			// 
			this.valueTB.Location = new System.Drawing.Point(8, 32);
			this.valueTB.Name = "valueTB";
			this.valueTB.Size = new System.Drawing.Size(88, 20);
			this.valueTB.TabIndex = 1;
			this.valueTB.Text = "";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(184, 160);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 128);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Name (English)";
			// 
			// nameengTB
			// 
			this.nameengTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.nameengTB.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nameengTB.Location = new System.Drawing.Point(8, 80);
			this.nameengTB.Name = "nameengTB";
			this.nameengTB.Size = new System.Drawing.Size(304, 22);
			this.nameengTB.TabIndex = 4;
			this.nameengTB.Text = "";
			// 
			// MatgmmLib
			// 
			this.Controls.Add(this.detailsGB);
			this.Controls.Add(this.namesLB);
			this.Name = "MatgmmLib";
			this.Size = new System.Drawing.Size(528, 296);
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
			BinaryReader br = new BinaryReader(stream);

			maps.ReadReflexive(ref br, (uint)secmagic, offset + 0x3DCC, SeekOrigin.Begin);
			br.BaseStream.Position = maps.Offset;

			items = new MatgmmItem[maps.Count];
			items.Initialize();

			// Clear list view
			namesLB.Items.Clear();
			
			for (int x = 0; x < maps.Count; x++) {
				items[x].Read(ref br);
				namesLB.Items.Add(items[x]);
			}

			namesLB.SelectedIndex = 0;
		}

		private void namesLB_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowMap((MatgmmItem)namesLB.SelectedItem);
		}

		public void ShowMap(MatgmmItem item)
		{
			// Values
			valueTB.Text = item._value.ToString();

			// Names
			nameengTB.Text = item.name_eng;

			// Descriptions
		}
	}
}
