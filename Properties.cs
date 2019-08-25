using Halo2;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Globalization;

namespace Ch2r
{
	/// <summary>
	/// Summary description for Properties.
	/// </summary>
	public class Properties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl propertiesTabControl;
		private System.Windows.Forms.TabPage generalTabPage;
		private System.Windows.Forms.TabPage dependenciesTabPage;
		private System.Windows.Forms.GroupBox indexEntryGroupBox;
		private System.Windows.Forms.Label identLabel;
		private System.Windows.Forms.Label offsetLabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label filenameLabel;
		private System.Windows.Forms.Label tagLabel;
		private System.Windows.Forms.ComboBox tagsComboBox;
		private System.Windows.Forms.TextBox filenameTextBox;
		private System.Windows.Forms.TextBox identTextBox;
		private System.Windows.Forms.TabPage hexViewTabPage;
		private Ch2r.SmartHexViewer smartHexViewer;
		private System.Windows.Forms.TextBox offsetTextBox;
		private System.Windows.Forms.TextBox sizeTextBox;
		private System.Windows.Forms.TextBox metaOffsetTextBox;
		private System.Windows.Forms.Label sizeLabel;
		private System.Windows.Forms.Label metaOffsetLabel;
		private System.Windows.Forms.GroupBox helpGroupBox;
		private System.Windows.Forms.Label helpLabel;
		private System.Windows.Forms.Button extractButton;
		private System.Windows.Forms.Button injectButton;
		private System.Windows.Forms.ListView dependenciesListView;
		private System.Windows.Forms.ColumnHeader tagColumn;
		private System.Windows.Forms.ColumnHeader filenameColumn;
		private System.Windows.Forms.ContextMenu dependencyContextMenu;
		private System.Windows.Forms.MenuItem changeMenuItem;
		private System.Windows.Forms.SaveFileDialog metaSaveFileDialog;
		private System.Windows.Forms.OpenFileDialog metaOpenFileDialog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Properties()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Skin it
			Global.Skin.SkinControl(this);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Properties));
			this.propertiesTabControl = new System.Windows.Forms.TabControl();
			this.generalTabPage = new System.Windows.Forms.TabPage();
			this.helpGroupBox = new System.Windows.Forms.GroupBox();
			this.helpLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.injectButton = new System.Windows.Forms.Button();
			this.extractButton = new System.Windows.Forms.Button();
			this.sizeTextBox = new System.Windows.Forms.TextBox();
			this.metaOffsetTextBox = new System.Windows.Forms.TextBox();
			this.sizeLabel = new System.Windows.Forms.Label();
			this.metaOffsetLabel = new System.Windows.Forms.Label();
			this.indexEntryGroupBox = new System.Windows.Forms.GroupBox();
			this.offsetTextBox = new System.Windows.Forms.TextBox();
			this.identTextBox = new System.Windows.Forms.TextBox();
			this.filenameTextBox = new System.Windows.Forms.TextBox();
			this.tagsComboBox = new System.Windows.Forms.ComboBox();
			this.tagLabel = new System.Windows.Forms.Label();
			this.filenameLabel = new System.Windows.Forms.Label();
			this.offsetLabel = new System.Windows.Forms.Label();
			this.identLabel = new System.Windows.Forms.Label();
			this.hexViewTabPage = new System.Windows.Forms.TabPage();
			this.smartHexViewer = new Ch2r.SmartHexViewer();
			this.dependenciesTabPage = new System.Windows.Forms.TabPage();
			this.dependenciesListView = new System.Windows.Forms.ListView();
			this.tagColumn = new System.Windows.Forms.ColumnHeader();
			this.filenameColumn = new System.Windows.Forms.ColumnHeader();
			this.dependencyContextMenu = new System.Windows.Forms.ContextMenu();
			this.changeMenuItem = new System.Windows.Forms.MenuItem();
			this.metaSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.metaOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.propertiesTabControl.SuspendLayout();
			this.generalTabPage.SuspendLayout();
			this.helpGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.indexEntryGroupBox.SuspendLayout();
			this.hexViewTabPage.SuspendLayout();
			this.dependenciesTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertiesTabControl
			// 
			this.propertiesTabControl.Controls.Add(this.generalTabPage);
			this.propertiesTabControl.Controls.Add(this.hexViewTabPage);
			this.propertiesTabControl.Controls.Add(this.dependenciesTabPage);
			this.propertiesTabControl.Location = new System.Drawing.Point(8, 8);
			this.propertiesTabControl.Name = "propertiesTabControl";
			this.propertiesTabControl.SelectedIndex = 0;
			this.propertiesTabControl.Size = new System.Drawing.Size(720, 368);
			this.propertiesTabControl.TabIndex = 1;
			this.propertiesTabControl.SelectedIndexChanged += new System.EventHandler(this.propertiesTabControl_SelectedIndexChanged);
			// 
			// generalTabPage
			// 
			this.generalTabPage.Controls.Add(this.helpGroupBox);
			this.generalTabPage.Controls.Add(this.groupBox1);
			this.generalTabPage.Controls.Add(this.indexEntryGroupBox);
			this.generalTabPage.Location = new System.Drawing.Point(4, 22);
			this.generalTabPage.Name = "generalTabPage";
			this.generalTabPage.Size = new System.Drawing.Size(712, 342);
			this.generalTabPage.TabIndex = 0;
			this.generalTabPage.Text = "General";
			// 
			// helpGroupBox
			// 
			this.helpGroupBox.Controls.Add(this.helpLabel);
			this.helpGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.helpGroupBox.Location = new System.Drawing.Point(8, 240);
			this.helpGroupBox.Name = "helpGroupBox";
			this.helpGroupBox.Size = new System.Drawing.Size(696, 96);
			this.helpGroupBox.TabIndex = 4;
			this.helpGroupBox.TabStop = false;
			this.helpGroupBox.Text = "Help";
			// 
			// helpLabel
			// 
			this.helpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.helpLabel.Location = new System.Drawing.Point(8, 16);
			this.helpLabel.Name = "helpLabel";
			this.helpLabel.Size = new System.Drawing.Size(680, 72);
			this.helpLabel.TabIndex = 0;
			this.helpLabel.Text = @"This window allows you to view properties of the selected item. On this page you can change the tag type for this item, as well as Inject and Extract meta data. On the next page you can view a hex dump of the meta data, and on the third you can change the dependencies this item has. Dependencies are other items that this item uses, for example a weapon item (weap) will have a projectile item (proj) as a dependency, so the weapon will know what to shot. Swapping these dependencies can lead to interesting results ;)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.injectButton);
			this.groupBox1.Controls.Add(this.extractButton);
			this.groupBox1.Controls.Add(this.sizeTextBox);
			this.groupBox1.Controls.Add(this.metaOffsetTextBox);
			this.groupBox1.Controls.Add(this.sizeLabel);
			this.groupBox1.Controls.Add(this.metaOffsetLabel);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 152);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(696, 80);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Meta Data";
			// 
			// injectButton
			// 
			this.injectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.injectButton.Location = new System.Drawing.Point(152, 44);
			this.injectButton.Name = "injectButton";
			this.injectButton.Size = new System.Drawing.Size(56, 24);
			this.injectButton.TabIndex = 13;
			this.injectButton.Text = "Inject";
			this.injectButton.Click += new System.EventHandler(this.injectButton_Click);
			// 
			// extractButton
			// 
			this.extractButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.extractButton.Location = new System.Drawing.Point(152, 20);
			this.extractButton.Name = "extractButton";
			this.extractButton.Size = new System.Drawing.Size(56, 24);
			this.extractButton.TabIndex = 12;
			this.extractButton.Text = "Extract";
			this.extractButton.Click += new System.EventHandler(this.extractButton_Click);
			// 
			// sizeTextBox
			// 
			this.sizeTextBox.Location = new System.Drawing.Point(64, 46);
			this.sizeTextBox.Name = "sizeTextBox";
			this.sizeTextBox.ReadOnly = true;
			this.sizeTextBox.Size = new System.Drawing.Size(64, 20);
			this.sizeTextBox.TabIndex = 11;
			this.sizeTextBox.Text = "";
			// 
			// metaOffsetTextBox
			// 
			this.metaOffsetTextBox.Location = new System.Drawing.Point(64, 24);
			this.metaOffsetTextBox.Name = "metaOffsetTextBox";
			this.metaOffsetTextBox.ReadOnly = true;
			this.metaOffsetTextBox.Size = new System.Drawing.Size(64, 20);
			this.metaOffsetTextBox.TabIndex = 10;
			this.metaOffsetTextBox.Text = "";
			// 
			// sizeLabel
			// 
			this.sizeLabel.Location = new System.Drawing.Point(8, 48);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(56, 16);
			this.sizeLabel.TabIndex = 9;
			this.sizeLabel.Text = "Size: ";
			// 
			// metaOffsetLabel
			// 
			this.metaOffsetLabel.Location = new System.Drawing.Point(8, 26);
			this.metaOffsetLabel.Name = "metaOffsetLabel";
			this.metaOffsetLabel.Size = new System.Drawing.Size(56, 16);
			this.metaOffsetLabel.TabIndex = 8;
			this.metaOffsetLabel.Text = "Offset:";
			// 
			// indexEntryGroupBox
			// 
			this.indexEntryGroupBox.Controls.Add(this.offsetTextBox);
			this.indexEntryGroupBox.Controls.Add(this.identTextBox);
			this.indexEntryGroupBox.Controls.Add(this.filenameTextBox);
			this.indexEntryGroupBox.Controls.Add(this.tagsComboBox);
			this.indexEntryGroupBox.Controls.Add(this.tagLabel);
			this.indexEntryGroupBox.Controls.Add(this.filenameLabel);
			this.indexEntryGroupBox.Controls.Add(this.offsetLabel);
			this.indexEntryGroupBox.Controls.Add(this.identLabel);
			this.indexEntryGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.indexEntryGroupBox.Location = new System.Drawing.Point(8, 8);
			this.indexEntryGroupBox.Name = "indexEntryGroupBox";
			this.indexEntryGroupBox.Size = new System.Drawing.Size(696, 136);
			this.indexEntryGroupBox.TabIndex = 2;
			this.indexEntryGroupBox.TabStop = false;
			this.indexEntryGroupBox.Text = "Index Entry";
			// 
			// offsetTextBox
			// 
			this.offsetTextBox.Location = new System.Drawing.Point(64, 80);
			this.offsetTextBox.Name = "offsetTextBox";
			this.offsetTextBox.ReadOnly = true;
			this.offsetTextBox.Size = new System.Drawing.Size(64, 20);
			this.offsetTextBox.TabIndex = 7;
			this.offsetTextBox.Text = "";
			// 
			// identTextBox
			// 
			this.identTextBox.Location = new System.Drawing.Point(64, 56);
			this.identTextBox.Name = "identTextBox";
			this.identTextBox.ReadOnly = true;
			this.identTextBox.Size = new System.Drawing.Size(64, 20);
			this.identTextBox.TabIndex = 6;
			this.identTextBox.Text = "";
			// 
			// filenameTextBox
			// 
			this.filenameTextBox.Location = new System.Drawing.Point(64, 104);
			this.filenameTextBox.Name = "filenameTextBox";
			this.filenameTextBox.ReadOnly = true;
			this.filenameTextBox.Size = new System.Drawing.Size(624, 20);
			this.filenameTextBox.TabIndex = 5;
			this.filenameTextBox.Text = "";
			// 
			// tagsComboBox
			// 
			this.tagsComboBox.Items.AddRange(new object[] {
																											"<fx>",
																											"adlg",
																											"ant!",
																											"bipd",
																											"bitm",
																											"bloc",
																											"bsdt",
																											"char",
																											"clwd",
																											"coll",
																											"colo",
																											"cont",
																											"crea",
																											"ctrl",
																											"deca",
																											"DECR",
																											"effe",
																											"egor",
																											"eqip",
																											"fog ",
																											"foot",
																											"fpch",
																											"garb",
																											"gldf",
																											"goof",
																											"hlmt",
																											"hudg",
																											"itmc",
																											"jmad",
																											"jpt!",
																											"lens",
																											"ligh",
																											"lsnd",
																											"ltmp",
																											"mach",
																											"matg",
																											"mdlg",
																											"MGS2",
																											"mode",
																											"mulg",
																											"nhdt",
																											"phmo",
																											"phys",
																											"pmov",
																											"pphy",
																											"proj",
																											"prt3",
																											"PRTM",
																											"sbsp",
																											"scen",
																											"scnr",
																											"sfx+",
																											"shad",
																											"sily",
																											"skin",
																											"sky ",
																											"sncl",
																											"snd!",
																											"snde",
																											"snmx",
																											"spas",
																											"spk!",
																											"ssce",
																											"stem",
																											"styl",
																											"tdtl",
																											"trak",
																											"udlg",
																											"ugh!",
																											"unic",
																											"vehc",
																											"vehi",
																											"vrtx",
																											"weap",
																											"wgit",
																											"wgtz",
																											"wigl"});
			this.tagsComboBox.Location = new System.Drawing.Point(48, 24);
			this.tagsComboBox.Name = "tagsComboBox";
			this.tagsComboBox.Size = new System.Drawing.Size(64, 21);
			this.tagsComboBox.TabIndex = 4;
			// 
			// tagLabel
			// 
			this.tagLabel.Location = new System.Drawing.Point(8, 26);
			this.tagLabel.Name = "tagLabel";
			this.tagLabel.Size = new System.Drawing.Size(40, 16);
			this.tagLabel.TabIndex = 3;
			this.tagLabel.Text = "Tag: ";
			// 
			// filenameLabel
			// 
			this.filenameLabel.Location = new System.Drawing.Point(8, 106);
			this.filenameLabel.Name = "filenameLabel";
			this.filenameLabel.Size = new System.Drawing.Size(56, 16);
			this.filenameLabel.TabIndex = 2;
			this.filenameLabel.Text = "Filename: ";
			// 
			// offsetLabel
			// 
			this.offsetLabel.Location = new System.Drawing.Point(8, 82);
			this.offsetLabel.Name = "offsetLabel";
			this.offsetLabel.Size = new System.Drawing.Size(56, 16);
			this.offsetLabel.TabIndex = 1;
			this.offsetLabel.Text = "Offset: ";
			// 
			// identLabel
			// 
			this.identLabel.Location = new System.Drawing.Point(8, 58);
			this.identLabel.Name = "identLabel";
			this.identLabel.Size = new System.Drawing.Size(56, 16);
			this.identLabel.TabIndex = 0;
			this.identLabel.Text = "Ident: ";
			// 
			// hexViewTabPage
			// 
			this.hexViewTabPage.Controls.Add(this.smartHexViewer);
			this.hexViewTabPage.Location = new System.Drawing.Point(4, 22);
			this.hexViewTabPage.Name = "hexViewTabPage";
			this.hexViewTabPage.Size = new System.Drawing.Size(712, 342);
			this.hexViewTabPage.TabIndex = 2;
			this.hexViewTabPage.Text = "Hex View";
			// 
			// smartHexViewer
			// 
			this.smartHexViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartHexViewer.Location = new System.Drawing.Point(0, 0);
			this.smartHexViewer.Name = "smartHexViewer";
			this.smartHexViewer.Size = new System.Drawing.Size(712, 342);
			this.smartHexViewer.TabIndex = 0;
			// 
			// dependenciesTabPage
			// 
			this.dependenciesTabPage.Controls.Add(this.dependenciesListView);
			this.dependenciesTabPage.Location = new System.Drawing.Point(4, 22);
			this.dependenciesTabPage.Name = "dependenciesTabPage";
			this.dependenciesTabPage.Size = new System.Drawing.Size(712, 342);
			this.dependenciesTabPage.TabIndex = 1;
			this.dependenciesTabPage.Text = "Dependencies";
			// 
			// dependenciesListView
			// 
			this.dependenciesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																																													 this.tagColumn,
																																													 this.filenameColumn});
			this.dependenciesListView.ContextMenu = this.dependencyContextMenu;
			this.dependenciesListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dependenciesListView.FullRowSelect = true;
			this.dependenciesListView.Location = new System.Drawing.Point(0, 0);
			this.dependenciesListView.Name = "dependenciesListView";
			this.dependenciesListView.Size = new System.Drawing.Size(712, 342);
			this.dependenciesListView.TabIndex = 0;
			this.dependenciesListView.View = System.Windows.Forms.View.Details;
			this.dependenciesListView.DoubleClick += new System.EventHandler(this.dependenciesListView_DoubleClick);
			// 
			// tagColumn
			// 
			this.tagColumn.Text = "Tag";
			// 
			// filenameColumn
			// 
			this.filenameColumn.Text = "Filename";
			this.filenameColumn.Width = 625;
			// 
			// dependencyContextMenu
			// 
			this.dependencyContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																													this.changeMenuItem});
			// 
			// changeMenuItem
			// 
			this.changeMenuItem.Index = 0;
			this.changeMenuItem.Text = "Change...";
			// 
			// Properties
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.ClientSize = new System.Drawing.Size(738, 384);
			this.Controls.Add(this.propertiesTabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Properties";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Properties";
			this.propertiesTabControl.ResumeLayout(false);
			this.generalTabPage.ResumeLayout(false);
			this.helpGroupBox.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.indexEntryGroupBox.ResumeLayout(false);
			this.hexViewTabPage.ResumeLayout(false);
			this.dependenciesTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private MapIndexItem item;
		private FileStream stream;
		private uint secMagic;
		private Map halo2map;

		public void ShowMeta(MapIndexItem thisItem, Map map)
		{
			item = thisItem;
			halo2map = map;

			stream = halo2map.stream;
			secMagic =halo2map.SecondaryMagic;
			tagsComboBox.SelectedItem = item.Tag;
			identTextBox.Text = item.Ident.ToString("X8");
			offsetTextBox.Text = item.Offset.ToString("X8");
			filenameTextBox.Text = item.Filename;
			metaOffsetTextBox.Text = (item.MetaOffset - secMagic).ToString("X8");
			sizeTextBox.Text = item.Size.ToString();
		}

		private void propertiesTabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (propertiesTabControl.SelectedIndex == 1)
			{
				smartHexViewer.Clear();
				smartHexViewer.ShowHex(item.Filename, item.GetMeta(stream, secMagic), true);
			}
			else if (propertiesTabControl.SelectedIndex == 2)
			{
				ScanDependencies();
			}
		}

		private void ScanDependencies()
		{
			// Open binary reader and go to meta
			BinaryReader br = new BinaryReader(stream);
			br.BaseStream.Seek(item.MetaOffset - secMagic, SeekOrigin.Begin);

			// Clear current listview
			dependenciesListView.Items.Clear();

			// Find them!
			string possibletag = "";
			string nexttag     = "";
			uint possibleident = 0xFFFFFFFF;
			long endposition   = br.BaseStream.Position + item.Size;

			while (br.BaseStream.Position < endposition) 
			{
				try 
				{
					possibletag = MapIndexTag.ReadTag(ref br);
					if (Map.IsTag(Reverse(possibletag))) 
					{
						nexttag = MapIndexTag.ReadTag(ref br);
						if (Map.IsTag(Reverse(nexttag))) 
						{
							possibletag = nexttag;
						} 
						else 
						{
							br.BaseStream.Position -= 4;
						}
						possibleident = br.ReadUInt32();
						ListViewItem lvitem = new ListViewItem(new string[]{Reverse(possibletag), halo2map.Index.ItemByIdent(possibleident).Filename});
						lvitem.Tag = (br.BaseStream.Position - 8).ToString("X8") + " " + possibleident.ToString("X8");
						dependenciesListView.Items.Add(lvitem);
					}
				} 
				catch {}
			}
		}

		private static string Reverse(string tag)
		{
			return tag.Substring(3, 1) + tag.Substring(2, 1) + tag.Substring(1, 1) + tag.Substring(0, 1);
		}

		private void extractButton_Click(object sender, System.EventArgs e)
		{
			metaSaveFileDialog.FileName = item.Filename.Substring(item.Filename.LastIndexOf("\\") + 1) + "." + item.Tag + ".meta";
			if (metaSaveFileDialog.ShowDialog() == DialogResult.OK) 
			{
				if (!item.SaveMeta(halo2map.stream, metaSaveFileDialog.FileName, halo2map.SecondaryMagic, halo2map.Index.Item(0).MetaOffset)) 
				{
					MessageBox.Show("Failed saving meta");
				}
			}
		}

		private void injectButton_Click(object sender, System.EventArgs e)
		{
			// Ask for new meta file
			if (metaOpenFileDialog.ShowDialog() == DialogResult.OK) 
			{
				FileStream metafile = new FileStream(metaOpenFileDialog.FileName, FileMode.Open, FileAccess.Read);

				// Check is size isnt too big
				if (metafile.Length > (long)item.Size) 
				{
					MessageBox.Show("Injected meta is bigger then current, aborting!");
					return;
				}

				if (!item.InjectMeta(halo2map.stream, metafile, halo2map.SecondaryMagic)) 
				{
					MessageBox.Show("Failed injecting meta! Inject to something bigger");
				}

				// Fix reflexives
				bool reflexivefixed = item.FixReflexives(metaOpenFileDialog.FileName + ".xml", halo2map.stream, halo2map.SecondaryMagic);

				// Done, close file
				metafile.Close();
				if (reflexivefixed) 
				{
					MessageBox.Show("Meta injected\n\nReflexives fixed.", "Meta");
				} 
				else 
				{
					MessageBox.Show("Meta injected\n\nReflexives fixing failed.", "Meta");
				}
			}
		}

		public void RefreshCurrentItem()
		{
			ScanDependencies();
		}

		private void dependenciesListView_DoubleClick(object sender, System.EventArgs e)
		{
			if (dependenciesListView.SelectedItems.Count > 0) {
				ListViewItem lvitem = dependenciesListView.SelectedItems[0];
				SwapForm swapForm = new SwapForm(halo2map);
				swapForm.Type = SwapType.Reflexive;
				swapForm.metaform = this;
				swapForm._original = item;
				string tag = (string)lvitem.Tag;
				swapForm.offset = long.Parse(tag.Substring(1, tag.IndexOf(" ")), NumberStyles.HexNumber);

				swapForm.Show();

				swapForm.LoadTags(lvitem.Text, lvitem.SubItems[1].Text);
			}
		}
	}
}
