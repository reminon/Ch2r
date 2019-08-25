using BitmControl;
using Halo2;
using Halo2Plugin;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Ch2r
{
	// For callbacks
	public delegate void EditedHandler();
	public delegate void SignedHandler();
	public delegate void ValueChangedHandler(uint offset, string valueType, string newValue);

	/// <summary>
	/// Summary description for Properties.
	/// </summary>
	public class EditMeta : System.Windows.Forms.Form
	{
		#region Control Declarations
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.TabControl metaTabControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button revertButton;
		#endregion

		#region Variable Declarations
		// Data arrays
		public byte[] editableData;
		public byte[] originalData;

		// Tag
		private string thisTag;

		// Variables that needed global scope
		private bool listViewExists;
		private int newTopPos;
		private bool stillInit;

		event EditedHandler OnEdit;
		FileStream fs;
		Map map;
		uint metaOffset;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.Label helpLabel;
		uint secMagic;
		UserControl plugincontrol = null;

		public UserControl PluginControl
		{
			get {
				return plugincontrol;
			}
			set {
				plugincontrol = value;
			}
		}
		#endregion

		#region Form Constructor
		public EditMeta(EditedHandler edit, Map map, MapIndexItem item)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Skin it
			Global.Skin.SkinControl(this);

			// Save needed members
			OnEdit += edit;
			fs = map.stream;
			secMagic = map.SecondaryMagic;
			metaOffset = item.MetaOffset;
			thisTag = item.Tag;
			this.map = map;

			// Get item meta
			byte[] metaData = item.GetMeta(fs, secMagic);

			// Save data to local arrays
			editableData = new byte[metaData.Length];
			originalData = new byte[metaData.Length];
			metaData.CopyTo(editableData, 0);
			metaData.CopyTo(originalData, 0);

			// Change caption
			string file = item.Filename + "." + item.Tag;
			file = file.Substring(file.LastIndexOf("\\") + 1);
			Text = "Edit Meta - " + file + " - " + map.Header.Name;
		}
		#endregion

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EditMeta));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.metaTabControl = new System.Windows.Forms.TabControl();
			this.revertButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.helpLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.BackColor = System.Drawing.SystemColors.Control;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(392, 344);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(56, 24);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.BackColor = System.Drawing.SystemColors.Control;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(456, 344);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(56, 24);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.BackColor = System.Drawing.SystemColors.Control;
			this.applyButton.Enabled = false;
			this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.applyButton.Location = new System.Drawing.Point(584, 344);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(56, 24);
			this.applyButton.TabIndex = 2;
			this.applyButton.Text = "Apply";
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// metaTabControl
			// 
			this.metaTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.metaTabControl.Location = new System.Drawing.Point(8, 8);
			this.metaTabControl.Name = "metaTabControl";
			this.metaTabControl.SelectedIndex = 0;
			this.metaTabControl.Size = new System.Drawing.Size(632, 328);
			this.metaTabControl.TabIndex = 3;
			// 
			// revertButton
			// 
			this.revertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.revertButton.BackColor = System.Drawing.SystemColors.Control;
			this.revertButton.Enabled = false;
			this.revertButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.revertButton.Location = new System.Drawing.Point(520, 344);
			this.revertButton.Name = "revertButton";
			this.revertButton.Size = new System.Drawing.Size(56, 24);
			this.revertButton.TabIndex = 4;
			this.revertButton.Text = "Revert";
			this.revertButton.Click += new System.EventHandler(this.revertButton_Click);
			// 
			// helpButton
			// 
			this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.helpButton.Location = new System.Drawing.Point(8, 344);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(64, 24);
			this.helpButton.TabIndex = 5;
			this.helpButton.Text = "Help >>";
			this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
			// 
			// helpLabel
			// 
			this.helpLabel.ForeColor = System.Drawing.Color.White;
			this.helpLabel.Location = new System.Drawing.Point(8, 384);
			this.helpLabel.Name = "helpLabel";
			this.helpLabel.Size = new System.Drawing.Size(632, 88);
			this.helpLabel.TabIndex = 6;
			this.helpLabel.Text = "Help:";
			// 
			// EditMeta
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.ClientSize = new System.Drawing.Size(650, 376);
			this.Controls.Add(this.helpLabel);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.revertButton);
			this.Controls.Add(this.metaTabControl);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EditMeta";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Meta";
			this.Load += new System.EventHandler(this.EditMeta_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region Read Plugin - a.k.a. "The Important Bit"
		public void ReadPlugin (string tag)
		{
			// Preparation
			metaTabControl.Controls.Clear();
			metaTabControl.TabPages.Clear();

			// Check for special tags
			switch (tag) {
				case "bitm":
					plugincontrol.Dock = DockStyle.Fill;
					plugincontrol.Visible = true;
					metaTabControl.TabPages.Add(new TabPage("Bitmap"));
					metaTabControl.TabPages[0].Controls.Add(plugincontrol);
					return;
				case "colo":
					ColoLib lib = new ColoLib();
					lib.LoadPlugin(fs, metaOffset - secMagic, secMagic, originalData.Length, thisTag);
					lib.Dock = DockStyle.Fill;
					metaTabControl.TabPages.Add(new TabPage("Color"));
					metaTabControl.TabPages[0].Controls.Add(lib);
					plugincontrol = lib;
					return;
				case "matg":
					if (map.Header.Name == "Main Menu") {
						MatgmmLib mlib = new MatgmmLib();
						mlib.map = map;
						mlib.LoadPlugin(fs, metaOffset - secMagic, secMagic, originalData.Length, thisTag);
						mlib.Dock = DockStyle.Fill;
						metaTabControl.TabPages.Add(new TabPage("Multiplayer Maps"));
						metaTabControl.TabPages[0].Controls.Add(mlib);
						plugincontrol = mlib;
						return;
					}
					break;
			}

			XmlDocument pluginXml = new XmlDocument();
			
			// Try loading the plugin, if this fails it uses NoPlugin to show an error message
			try { pluginXml.Load(Application.StartupPath + "\\Plugins\\" + tag + ".ch2r"); }
			catch { NoPlugin(tag); return; }

			// Node Lists
			XmlNodeList tabList = pluginXml.GetElementsByTagName("tab");
			XmlNodeList valueList;
			XmlNodeList bitmaskList;

			// General Controls
			TabPage newPage;
			ComboBox reflexiveBox;
			ListView valueView;
			ListViewItem valueItem;

			// Bitmask Controls
			GroupBox bitmaskContainer;
			CheckBox bitmaskCheckBox;

			// Variables
			BitArray bitmaskBits;
			uint offset, thisBitmask;
			short thisShort;
			int thisInteger;
			float thisFloat;
			string thisString;
			Halo2.Reflexive thisReflexive;
			ReflexiveListItem reflexiveListItem;
			ReflexiveBoxData reflexiveBoxData;
			int newInnerTopPos;
			uint size;

			stillInit = true;
			
			foreach (XmlElement tab in tabList)
			{
				// Create the tab
				newPage = new TabPage(tab.GetElementsByTagName("name")[0].InnerText);
				
				// Variables needed later
				newTopPos = 8;
				listViewExists = false;

				// Get a list of values for this tab and go through them
				valueList = tab.GetElementsByTagName("value");
				foreach (XmlElement metaValue in valueList)
				{
					offset = Convert.ToUInt32(metaValue.GetElementsByTagName("offset")[0].InnerText.Substring(2), 16);
					switch (metaValue.GetElementsByTagName("type")[0].InnerText)
					{
						case "bitmask32":
							thisBitmask = GetBitmask(offset);
							bitmaskBits = new BitArray(new int[] {(int)thisBitmask});

							bitmaskList = metaValue.GetElementsByTagName("bitmask");
							bitmaskContainer = NewGroupBox(metaValue.GetElementsByTagName("name")[0].InnerText, metaTabControl.Width - 24, 32 + (16 * bitmaskList.Count), 8, newTopPos);
							bitmaskContainer.Tag = offset;

							newInnerTopPos = 20;
							foreach (XmlElement bitmask in bitmaskList)
							{
								bitmaskCheckBox = NewCheckBox(bitmask.GetElementsByTagName("name")[0].InnerText, bitmaskContainer.Width - 32, 16, 16, newInnerTopPos);
								bitmaskCheckBox.Tag = int.Parse(bitmask.GetElementsByTagName("bit")[0].InnerText);

								if (bitmaskBits[32 - (int)bitmaskCheckBox.Tag])
								{
									bitmaskCheckBox.Checked = true;
								}
								newInnerTopPos += 16;
								bitmaskContainer.Controls.Add(bitmaskCheckBox);
							}

							newTopPos += bitmaskContainer.Height + 8;
							newPage.Controls.Add(bitmaskContainer);
							break;


						case "short":
							valueView = GetListView(newPage, tab);
							thisShort = GetShort(offset);

							// Create the new item
							valueItem = new ListViewItem(new string[4] {metaValue.GetElementsByTagName("name")[0].InnerText, thisShort.ToString(), metaValue.GetElementsByTagName("type")[0].InnerText, metaValue.GetElementsByTagName("desc")[0].InnerText});
							valueItem.Tag = offset;
							valueView.Items.Add(valueItem);
							break;


						case "integer":
							valueView = GetListView(newPage, tab);
							thisInteger = GetInteger(offset);

							// Create the new item
							valueItem = new ListViewItem(new string[4] {metaValue.GetElementsByTagName("name")[0].InnerText, thisInteger.ToString(), metaValue.GetElementsByTagName("type")[0].InnerText, metaValue.GetElementsByTagName("desc")[0].InnerText});
							valueItem.Tag = offset;
							valueView.Items.Add(valueItem);
							break;


						case "string256":
							valueView = GetListView(newPage, tab);
							thisString = GetString(offset, 256);

							// Create the new item
							valueItem = new ListViewItem(new string[4] {metaValue.GetElementsByTagName("name")[0].InnerText, thisString, metaValue.GetElementsByTagName("type")[0].InnerText, metaValue.GetElementsByTagName("desc")[0].InnerText});
							valueItem.Tag = offset;
							valueView.Items.Add(valueItem);
							break;


						case "float":
							valueView = GetListView(newPage, tab);
							thisFloat = GetFloat(offset);

							// Create the new item
							valueItem = new ListViewItem(new string[4] {metaValue.GetElementsByTagName("name")[0].InnerText, thisFloat.ToString(), metaValue.GetElementsByTagName("type")[0].InnerText, metaValue.GetElementsByTagName("desc")[0].InnerText});
							valueItem.Tag = offset;
							valueView.Items.Add(valueItem);
							break;


						case "reflexive":
							offset = Convert.ToUInt32(metaValue.GetElementsByTagName("offset")[0].InnerText.Substring(2), 16);
							size = Convert.ToUInt32(metaValue.GetElementsByTagName("size")[0].InnerText.Substring(2), 16);

							thisReflexive = new Halo2.Reflexive();
							thisReflexive.Count = BitConverter.ToUInt32(editableData, (int)offset);
							thisReflexive.Offset = (uint)(BitConverter.ToInt32(editableData, (int)offset + 4) - metaOffset);

							reflexiveBox = NewComboBox(metaValue.GetElementsByTagName("name")[0].InnerText, metaTabControl.Width - 24, 16, 8, newTopPos);
							newTopPos += 24;
							reflexiveBoxData = new ReflexiveBoxData();
							reflexiveBoxData.items = metaValue.GetElementsByTagName("item");
							reflexiveBoxData.target = GetListView(newPage, tab);
							reflexiveBoxData.targetParent = newPage;
							reflexiveBox.Tag = reflexiveBoxData;
							for (int i = 0; i < thisReflexive.Count; i++)
							{
								reflexiveListItem = new ReflexiveListItem();
								reflexiveListItem.offset = (uint)(thisReflexive.Offset + (i * size));
								
								foreach (XmlElement structItem in metaValue.GetElementsByTagName("item"))
								{
									if (structItem.GetElementsByTagName("name")[0].InnerText == metaValue.GetElementsByTagName("indexBy")[0].InnerText)
									{
										if (structItem.GetElementsByTagName("type")[0].InnerText == "string256")
										{ 
											reflexiveListItem.text = GetString(reflexiveListItem.offset + Convert.ToUInt32(structItem.GetElementsByTagName("offset")[0].InnerText.Substring(2), 16), 256);
										}
									}
								}

								if ((reflexiveListItem.text == "") || (reflexiveListItem.text == null))
								{
									reflexiveListItem.text = i.ToString();
								}
								reflexiveBox.Items.Add(reflexiveListItem);
							}

							newPage.Controls.Add(reflexiveBox);
							if(reflexiveBox.Items.Count != 0)
							{
								reflexiveBox.SelectedItem = reflexiveBox.Items[0];
								reflexive_IndexChanged(reflexiveBox, null);
							}
							break;
					}
				}

				metaTabControl.TabPages.Add(newPage);
			}
			stillInit = false;
		}
		#endregion

		private struct ReflexiveListItem
		{
			public string text;
			public uint offset;

			public override string ToString()
			{
				return text;
			}
		}

		private struct ReflexiveBoxData
		{
			public XmlNodeList items;
			public ListView target;
			public TabPage targetParent;
		}

		public void NoPlugin (string tag)
		{
			metaTabControl.Controls.Clear();
			metaTabControl.TabPages.Clear();
			TabPage newPage = new TabPage("Error");
			Label noPluginLabel = new Label();
			noPluginLabel.Text = "There was a problem loading the plugin for this tag.\nCheck the file " + tag + ".ch2r exists in the Plugins directory.\nIf it exists it might be a malformed XML file.";
			noPluginLabel.Dock = DockStyle.Fill;
			noPluginLabel.TextAlign = ContentAlignment.MiddleCenter;
			newPage.Controls.Add(noPluginLabel);
			metaTabControl.Controls.Add(newPage);
		}

		private void reflexive_IndexChanged(object sender, System.EventArgs e)
		{
			// Node Lists
			XmlNodeList bitmaskList;

			// General Controls
			TabPage newPage;
			ListView valueView;
			ListViewItem valueItem;

			// Bitmask Controls
			GroupBox bitmaskContainer;
			CheckBox bitmaskCheckBox;

			// Variables
			uint offset, thisBitmask;
			short thisShort;
			int thisInteger;
			float thisFloat;
			string thisString;
			int newInnerTopPos;

			ComboBox thisBox = (ComboBox)sender;
			ReflexiveBoxData thisData = (ReflexiveBoxData)thisBox.Tag;
			newPage = thisData.targetParent;
			if (thisBox.SelectedItem == null) {return;}
			offset = ((ReflexiveListItem)thisBox.SelectedItem).offset;
			uint innerOffset;

			valueView = thisData.target;
			valueView.Items.Clear();

			foreach (Control thisControl in newPage.Controls)
			{
				if (thisControl.GetType() == typeof(GroupBox)) {newPage.Controls.Remove(thisControl);}
			}

			newTopPos = valueView.Top + valueView.Height + 8;
			stillInit = true;

			foreach (XmlElement structItem in thisData.items)
			{
				innerOffset = Convert.ToUInt32(structItem.GetElementsByTagName("offset")[0].InnerText.Substring(2), 16);
				switch (structItem.GetElementsByTagName("type")[0].InnerText)
				{
					case "bitmask32":
						thisBitmask = GetBitmask(offset + innerOffset);

						bitmaskList = structItem.GetElementsByTagName("bitmask");
						bitmaskContainer = NewGroupBox(structItem.GetElementsByTagName("name")[0].InnerText, metaTabControl.Width - 24, 32 + (16 * bitmaskList.Count), 8, newTopPos);
						bitmaskContainer.Tag = offset + innerOffset;

						newInnerTopPos = 20;
						foreach (XmlElement bitmask in bitmaskList)
						{
							bitmaskCheckBox = NewCheckBox(bitmask.GetElementsByTagName("name")[0].InnerText, bitmaskContainer.Width - 32, 16, 16, newInnerTopPos);
							bitmaskCheckBox.Tag = int.Parse(bitmask.GetElementsByTagName("bit")[0].InnerText);

							if ((((int)0x1 << ((int)bitmaskCheckBox.Tag - 1)) & thisBitmask) == ((int)0x1 << ((int)bitmaskCheckBox.Tag - 1)))
								//if (((uint)Math.Pow(2, ((uint)bitmaskCheckBox.Tag - 1)) & thisBitmask) == (uint)Math.Pow(2, ((uint)bitmaskCheckBox.Tag - 1)))
							{
								bitmaskCheckBox.Checked = true;
							}
							newInnerTopPos += 16;
							bitmaskContainer.Controls.Add(bitmaskCheckBox);
						}

						newTopPos += bitmaskContainer.Height + 8;
						newPage.Controls.Add(bitmaskContainer);
						break;


					case "short":
						thisShort = GetShort(offset + innerOffset);

						// Create the new item
						valueItem = new ListViewItem(new string[4] {structItem.GetElementsByTagName("name")[0].InnerText, thisShort.ToString(), structItem.GetElementsByTagName("type")[0].InnerText, structItem.GetElementsByTagName("desc")[0].InnerText});
						valueItem.Tag = offset + innerOffset;
						valueView.Items.Add(valueItem);
						break;


					case "integer":
						thisInteger = GetInteger(offset + innerOffset);

						// Create the new item
						valueItem = new ListViewItem(new string[4] {structItem.GetElementsByTagName("name")[0].InnerText, thisInteger.ToString(), structItem.GetElementsByTagName("type")[0].InnerText, structItem.GetElementsByTagName("desc")[0].InnerText});
						valueItem.Tag = offset;
						valueView.Items.Add(valueItem);
						break;


					case "string256":
						thisString = GetString(offset + innerOffset, 256);

						// Create the new item
						valueItem = new ListViewItem(new string[4] {structItem.GetElementsByTagName("name")[0].InnerText, thisString, structItem.GetElementsByTagName("type")[0].InnerText, structItem.GetElementsByTagName("desc")[0].InnerText});
						valueItem.Tag = offset + innerOffset;
						valueView.Items.Add(valueItem);
						break;


					case "float":
						thisFloat = GetFloat(offset + innerOffset);

						// Create the new item
						valueItem = new ListViewItem(new string[4] {structItem.GetElementsByTagName("name")[0].InnerText, thisFloat.ToString(), structItem.GetElementsByTagName("type")[0].InnerText, structItem.GetElementsByTagName("desc")[0].InnerText});
						valueItem.Tag = offset + innerOffset;
						valueView.Items.Add(valueItem);
						break;
				}
			}
			stillInit = false;
		}

		#region Create Controls
		#region Group Box
		private GroupBox NewGroupBox(string text, int width, int height, int left, int top)
		{
			GroupBox newGroupBox = new GroupBox();
			newGroupBox.Text = text;
			newGroupBox.Name = text + "GroupBox";
			newGroupBox.Size = new System.Drawing.Size(width, height);
			newGroupBox.Location = new System.Drawing.Point(left, top);
			return newGroupBox;
		}
		#endregion

		#region Check Box
		private CheckBox NewCheckBox(string text, int width, int height, int left, int top)
		{
			CheckBox newCheckBox = new CheckBox();
			newCheckBox.Text = text;
			newCheckBox.Name = text.Replace(" ", "") + "CheckBox";
			newCheckBox.Size = new System.Drawing.Size(width, height);
			newCheckBox.Location = new System.Drawing.Point(left, top);
			newCheckBox.CheckedChanged += new System.EventHandler(this.bitmask_CheckedChanged);
			return newCheckBox;
		}
		#endregion

		#region Combo Box
		private ComboBox NewComboBox(string name, int width, int height, int left, int top)
		{
			ComboBox newComboBox = new ComboBox();
			newComboBox.Name = name + "ComboBox";
			newComboBox.Size = new System.Drawing.Size(width, height);
			newComboBox.Location = new System.Drawing.Point(left, top);
			newComboBox.SelectedIndexChanged += new System.EventHandler(this.reflexive_IndexChanged);
			return newComboBox;
		}
		#endregion

		#region List View
		private ListView NewListView(string name, int width, int height, int left, int top)
		{
			ListView newListView = new ListView();
			newListView.Name = name + "ListView";
			newListView.Size = new System.Drawing.Size(width, height);
			newListView.Location = new System.Drawing.Point(left, top);
			newListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			newListView.View = View.Details;
			newListView.MultiSelect = false;
			newListView.Columns.Add("Name", 150, HorizontalAlignment.Left);
			newListView.Columns.Add("Value", 50, HorizontalAlignment.Left);
			newListView.Columns.Add("Type", 75, HorizontalAlignment.Left);
			newListView.Columns.Add("Description", width - 300, HorizontalAlignment.Left);
			newListView.DoubleClick += new System.EventHandler(this.valueView_Clicked);
			return newListView;
		}
		#endregion
		#endregion

		#region "Get" Functions
		private ListView GetListView(TabPage newPage, XmlElement tab)
		{
			ListView valueView;
			// Create the ListView control if it doesn't exist
			if (!listViewExists) { newPage.Controls.Add(NewListView(newPage.Text, metaTabControl.Width - 24, (int.Parse(tab.GetElementsByTagName("listSize")[0].InnerText) * 16) + 24, 8, newTopPos)); newTopPos += newPage.Controls[newPage.Controls.Count - 1].Height + 8; listViewExists = true; }
			valueView = null;

			// Find the ListView control
			foreach (Control thisControl in newPage.Controls)
			{
				if (thisControl.GetType() == typeof(ListView))
				{valueView = (ListView)thisControl;}
			}

			return valueView;
		}

		private short GetShort(uint offset)
		{
			return BitConverter.ToInt16(editableData, (int)offset);
		}

		private float GetFloat(uint offset)
		{
			return BitConverter.ToSingle(editableData, (int)offset);
		}

		private int GetInteger(uint offset)
		{
			return BitConverter.ToInt32(editableData, (int)offset);
		}

		private uint GetBitmask(uint offset)
		{
			return BitConverter.ToUInt32(editableData, (int)offset);
		}

		private string GetString(uint offset, int length)
		{
			string tempStr = "";
			for (int i = 0; i < length; i++)
			{
				tempStr += (char)editableData[offset + i];
			}

			return tempStr;
		}
		#endregion

		#region Button Click Events
		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void applyButton_Click(object sender, System.EventArgs e)
		{
			// Apply changes
			ApplyChanges();
			applyButton.Enabled = false;
			revertButton.Enabled = false;
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			// Apply changes
			ApplyChanges();
			this.Close();
		}

		private void revertButton_Click(object sender, System.EventArgs e)
		{
			originalData.CopyTo(editableData, 0);
			Application.DoEvents();
			ReadPlugin(thisTag);
			revertButton.Enabled = false;
			applyButton.Enabled = false;
		}
		#endregion

		#region Value Changing
		private void ApplyChanges()
		{
			BinaryWriter br = new BinaryWriter(fs);
			fs.Seek(metaOffset - secMagic, SeekOrigin.Begin);
			br.Write(editableData);
			fs.Flush();
		}

		private void valueView_Clicked(object sender, System.EventArgs e)
		{
			ListViewItem clickedItem = ((ListView)sender).SelectedItems[0];
			ValueEdit changeValue = new ValueEdit(new ValueChangedHandler(ChangeValue));
			changeValue.Init(clickedItem, (uint)clickedItem.Tag);
			changeValue.Show();
		}

		public void ChangeValue(uint offset, string valueType, string newValue)
		{
			revertButton.Enabled = true;
			applyButton.Enabled = true;

			switch (valueType)
			{
				case "integer":
					BitConverter.GetBytes(int.Parse(newValue)).CopyTo(editableData, offset);
					break;
				case "float":
					BitConverter.GetBytes(float.Parse(newValue)).CopyTo(editableData, offset);
					break;
				case "short":
					BitConverter.GetBytes(short.Parse(newValue)).CopyTo(editableData, offset);
					break;
				case "string256":
					break;
			}
		}

		private void bitmask_CheckedChanged(object sender, System.EventArgs e)
		{
			if (stillInit) { return; }
			
			revertButton.Enabled = true;
			applyButton.Enabled = true;
			CheckBox checkedBox = (CheckBox)sender;
			
			uint maskOffset = (uint)FindParentGroupBox(checkedBox).Tag;
			BitArray mask = new BitArray(new int[] { BitConverter.ToInt32(editableData, (int)maskOffset) });

			mask[32 - (int)checkedBox.Tag] = checkedBox.Checked;

			mask.CopyTo(editableData, (int)maskOffset);
		}

		static public int ReverseEndian(int x) 
		{
			return ((x<<24) | ((x & 0xff00)<<8) | ((x & 0xff0000)>>8) | (x>>24));
		}

		private GroupBox FindParentGroupBox(CheckBox child)
		{
			foreach (TabPage page in metaTabControl.TabPages)
			{
				foreach (Control control in page.Controls)
				{
					if (control.GetType() == typeof(GroupBox))
					{
						if (control.Controls.GetChildIndex(child, false) > -1)
						{
							return (GroupBox)control;
						}
					}
				}
			}
			return null;
		}
		#endregion

		bool helpOpen = false;
		private void helpButton_Click(object sender, System.EventArgs e)
		{
			if (helpOpen)
			{
				this.Height = 400;
				helpButton.Text = "Help >>";
				helpOpen = false;
			}
			else
			{
				this.Height = 500;
				helpButton.Text = "Help <<";
				helpOpen = true;
			}
		}

		private void EditMeta_Load(object sender, System.EventArgs e)
		{
			helpLabel.Text = "Help:\nThis window allows you to edit the meta data of an item. The contents of this window depend on the plugin loaded for the tag type. ";
			helpLabel.Text += "There are certain data types that can be edited here. Bitmasks: these show up as a list of Check Boxes, tick the check box to set the bit. ";
			helpLabel.Text += "Floats/Integers/Shorts: These are decimal numbers, change them by double clicking on the value you want to change in the list box. This will show ";
			helpLabel.Text += "a window for you to enter the new value. Once you have changed a value you can either Apply the changes by clicking Apply or OK, or you can go back ";
			helpLabel.Text += "to the original settings by clicking Revert, or Cancel";
		}
	}
}
