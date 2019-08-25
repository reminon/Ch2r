using BitmControl;
using Halo2;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ToolBarControl;
using XPExplorerBar;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;
using Buffer = Microsoft.DirectX.DirectSound.SecondaryBuffer;
using Microsoft.DirectX.AudioVideoPlayback;
//using Microsoft.Samples.DirectX.UtilityToolkit;

namespace Ch2r
{
	/// <summary>
	/// Summary description for MapExplorer.
	/// </summary>
	public class MapExplorer : System.Windows.Forms.Form
	{
		/// <summary>
		/// Currently opened map.
		/// </summary>
		public Map halo2map;

		public string filePath;

		/// <summary>
		/// Remember window state.
		/// </summary>
		private PersistWindowState pws;

		/// <summary>
		/// Used by search to remember last path.
		/// </summary>
		private string lastPath = null;

		/// <summary>
		/// Our bitmap control to preview/extract/inject.
		/// </summary>
		private BitmControl.H2Bitmap bitm = new BitmControl.H2Bitmap();

		bool dropdown = false;

		#region Controls
		private XPExplorerBar.TaskPane mapsTaskPane;
		private XPExplorerBar.Expando browseExpando;
		private XPExplorerBar.TaskItem parentTaskItem;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList iconsImageList;
		private System.Windows.Forms.ContextMenu itemContextMenu;
		private System.Windows.Forms.MenuItem propertiesMenuItem;
		private System.Windows.Forms.Panel addressPanel;
		private System.Windows.Forms.TextBox addressTB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MenuItem openMI;
		private XPExplorerBar.Expando propertiesExpando;
		private XPExplorerBar.Expando mapdetailsExpando;
		private XPExplorerBar.TaskItem primaryMagicTaskItem;
		private XPExplorerBar.TaskItem secondaryMagicTaskItem;
		private XPExplorerBar.TaskItem checksumTaskItem;
		private XPExplorerBar.TaskItem mapTypeTaskItem;
		private XPExplorerBar.Expando folderdetailsExpando;
		private XPExplorerBar.TaskItem nameTI;
		private System.Windows.Forms.PictureBox bitmPB;
		private System.Windows.Forms.CheckBox findCB;
		private XPExplorerBar.Expando toolsExpando;
		private XPExplorerBar.Expando findExpando;
		private System.Windows.Forms.CheckBox calculatorCB;
		private XPExplorerBar.Expando calcExpando;
		private XPExplorerBar.XPTextBox valueOneTextBox;
		private XPExplorerBar.XPTextBox valueTwoTextBox;
		private System.Windows.Forms.Label value1Label;
		private System.Windows.Forms.Label value2Label;
		private XPExplorerBar.XPTextBox resultTextBox;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button subtractButton;
		private System.Windows.Forms.Label resultLabel;
		private System.Windows.Forms.ContextMenu calculatorContextMenu;
		private System.Windows.Forms.MenuItem insertMenuItem;
		private System.Windows.Forms.MenuItem secMagicMenuItem;
		private System.Windows.Forms.MenuItem formatMenuItem;
		private System.Windows.Forms.MenuItem addSpacesMenuItem;
		private System.Windows.Forms.MenuItem endianSwapMenuItem;
		private System.Windows.Forms.MenuItem convertMenuItem;
		private System.Windows.Forms.MenuItem fromDecimalMenuItem;
		private System.Windows.Forms.MenuItem dToBinary;
		private System.Windows.Forms.MenuItem fromBinaryMenuItem;
		private System.Windows.Forms.MenuItem bToHexMenuItem;
		private System.Windows.Forms.MenuItem checkSumMenuItem;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox searchasCB;
		private System.Windows.Forms.Button searchB;
		private System.Windows.Forms.TextBox searchforTB;
		private System.Windows.Forms.ImageList smalliconsIL;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox lookinCB;
		private XPExplorerBar.Expando finddetailsExpando;
		private System.Windows.Forms.Label findresultL;
		private System.Windows.Forms.MenuItem openfolderMI;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem copyIdentMenuItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem copyMetaOffsetMenuItem;
		private System.Windows.Forms.MenuItem copyMetaValueMenuItem;
		private System.Windows.Forms.MenuItem magicMenuItem;
		private System.Windows.Forms.MenuItem removeSpacesMenuItem;
		private XPExplorerBar.XPListView contentsXPListView;
		private System.Windows.Forms.MenuItem fromHexMenuItem;
		private System.Windows.Forms.MenuItem hToDecimal;
		private System.Windows.Forms.MenuItem hToBinaryMenuItem;
		private System.Windows.Forms.MenuItem dToHexMenuItem;
		private System.Windows.Forms.MenuItem bToDecimal;
		private XPExplorerBar.Expando multidetailsExpando;
		private System.Windows.Forms.Label multiitemsL;
		private System.Windows.Forms.Label iteminfoL;
		private XPExplorerBar.TaskItem extractMapTaskItem;
		private System.Windows.Forms.FolderBrowserDialog extractFolderBrowserDialog;
		private System.Windows.Forms.SaveFileDialog bitmSFD;
		private System.Windows.Forms.CheckBox soundOptionsCheckBox;
		private XPExplorerBar.Expando soundOptionsExpando;
		private System.Windows.Forms.ComboBox optionComboBox;
		private System.Windows.Forms.Label soundOptionDetailsLabel;
		private System.Windows.Forms.Button extractSoundButton;
		private System.Windows.Forms.Button playButton;
		private System.Windows.Forms.ColumnHeader filenameCH;
		private System.Windows.Forms.ColumnHeader sizeCH;
		private System.Windows.Forms.ColumnHeader typeCH;
		private System.Windows.Forms.TrackBar bitmTB;
		private ToolBarControl.ToolBar mainTB;
		private System.Windows.Forms.TreeView foldersTV;
		private System.Windows.Forms.Panel foldersPanel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Splitter foldersS;
		private System.Windows.Forms.ImageList tbiconsIL;
		private System.Windows.Forms.TextBox filterTB;
		private System.Windows.Forms.SaveFileDialog sndSFD;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem shortcutsMI;
		private System.Windows.Forms.MenuItem findMI;
		private System.Windows.Forms.MenuItem itemswapMI;
		private string curPath; // For find

		#endregion

		public MapExplorer(Map map)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Skin it
			Global.Skin.SkinControl(this);

			// Save stuff
			halo2map = map;
			filePath = map.Filename;
			Text = map.Header.Name + " - " + filePath;

			// Set everything that the designer fucks up
			parentTaskItem.ImageList = iconsImageList;
			toolsExpando.Collapsed = true;
			Global.ResizeTaskItems(ref mapsTaskPane);
			searchasCB.SelectedIndex = 0;
			mainTB.Buttons.ButtonClick += new ToolBarControl.ToolbarButtons.ButtonClickEventHandler(Buttons_ButtonClick);
			MakeToolbar();

			ToolTip bitmTT = new ToolTip();
			bitmTT.SetToolTip(bitmPB, "Double-click to save image as png");

			pws = new PersistWindowState();
			pws.Parent = this;
			pws.RegistryPath = @"Software\Ch2r\Position\MapExplorer";
			pws.LoadStateEvent += new PersistWindowState.WindowStateDelegate(LoadState);
			pws.SaveStateEvent += new PersistWindowState.WindowStateDelegate(SaveState);
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MapExplorer));
			this.mapsTaskPane = new XPExplorerBar.TaskPane();
			this.browseExpando = new XPExplorerBar.Expando();
			this.parentTaskItem = new XPExplorerBar.TaskItem();
			this.toolsExpando = new XPExplorerBar.Expando();
			this.findCB = new System.Windows.Forms.CheckBox();
			this.calculatorCB = new System.Windows.Forms.CheckBox();
			this.extractMapTaskItem = new XPExplorerBar.TaskItem();
			this.soundOptionsCheckBox = new System.Windows.Forms.CheckBox();
			this.calcExpando = new XPExplorerBar.Expando();
			this.valueOneTextBox = new XPExplorerBar.XPTextBox();
			this.calculatorContextMenu = new System.Windows.Forms.ContextMenu();
			this.insertMenuItem = new System.Windows.Forms.MenuItem();
			this.magicMenuItem = new System.Windows.Forms.MenuItem();
			this.secMagicMenuItem = new System.Windows.Forms.MenuItem();
			this.checkSumMenuItem = new System.Windows.Forms.MenuItem();
			this.formatMenuItem = new System.Windows.Forms.MenuItem();
			this.removeSpacesMenuItem = new System.Windows.Forms.MenuItem();
			this.addSpacesMenuItem = new System.Windows.Forms.MenuItem();
			this.endianSwapMenuItem = new System.Windows.Forms.MenuItem();
			this.convertMenuItem = new System.Windows.Forms.MenuItem();
			this.fromHexMenuItem = new System.Windows.Forms.MenuItem();
			this.hToDecimal = new System.Windows.Forms.MenuItem();
			this.hToBinaryMenuItem = new System.Windows.Forms.MenuItem();
			this.fromDecimalMenuItem = new System.Windows.Forms.MenuItem();
			this.dToHexMenuItem = new System.Windows.Forms.MenuItem();
			this.dToBinary = new System.Windows.Forms.MenuItem();
			this.fromBinaryMenuItem = new System.Windows.Forms.MenuItem();
			this.bToDecimal = new System.Windows.Forms.MenuItem();
			this.bToHexMenuItem = new System.Windows.Forms.MenuItem();
			this.valueTwoTextBox = new XPExplorerBar.XPTextBox();
			this.value1Label = new System.Windows.Forms.Label();
			this.value2Label = new System.Windows.Forms.Label();
			this.resultTextBox = new XPExplorerBar.XPTextBox();
			this.addButton = new System.Windows.Forms.Button();
			this.subtractButton = new System.Windows.Forms.Button();
			this.resultLabel = new System.Windows.Forms.Label();
			this.findExpando = new XPExplorerBar.Expando();
			this.label2 = new System.Windows.Forms.Label();
			this.searchforTB = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.searchasCB = new System.Windows.Forms.ComboBox();
			this.searchB = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.lookinCB = new System.Windows.Forms.ComboBox();
			this.soundOptionsExpando = new XPExplorerBar.Expando();
			this.optionComboBox = new System.Windows.Forms.ComboBox();
			this.soundOptionDetailsLabel = new System.Windows.Forms.Label();
			this.extractSoundButton = new System.Windows.Forms.Button();
			this.playButton = new System.Windows.Forms.Button();
			this.propertiesExpando = new XPExplorerBar.Expando();
			this.bitmPB = new System.Windows.Forms.PictureBox();
			this.iteminfoL = new System.Windows.Forms.Label();
			this.bitmTB = new System.Windows.Forms.TrackBar();
			this.mapdetailsExpando = new XPExplorerBar.Expando();
			this.primaryMagicTaskItem = new XPExplorerBar.TaskItem();
			this.secondaryMagicTaskItem = new XPExplorerBar.TaskItem();
			this.checksumTaskItem = new XPExplorerBar.TaskItem();
			this.mapTypeTaskItem = new XPExplorerBar.TaskItem();
			this.folderdetailsExpando = new XPExplorerBar.Expando();
			this.nameTI = new XPExplorerBar.TaskItem();
			this.finddetailsExpando = new XPExplorerBar.Expando();
			this.findresultL = new System.Windows.Forms.Label();
			this.multidetailsExpando = new XPExplorerBar.Expando();
			this.multiitemsL = new System.Windows.Forms.Label();
			this.iconsImageList = new System.Windows.Forms.ImageList(this.components);
			this.contentsXPListView = new XPExplorerBar.XPListView();
			this.filenameCH = new System.Windows.Forms.ColumnHeader();
			this.sizeCH = new System.Windows.Forms.ColumnHeader();
			this.typeCH = new System.Windows.Forms.ColumnHeader();
			this.smalliconsIL = new System.Windows.Forms.ImageList(this.components);
			this.itemContextMenu = new System.Windows.Forms.ContextMenu();
			this.openMI = new System.Windows.Forms.MenuItem();
			this.openfolderMI = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.copyIdentMenuItem = new System.Windows.Forms.MenuItem();
			this.copyMetaOffsetMenuItem = new System.Windows.Forms.MenuItem();
			this.copyMetaValueMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.propertiesMenuItem = new System.Windows.Forms.MenuItem();
			this.addressPanel = new System.Windows.Forms.Panel();
			this.filterTB = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.addressTB = new System.Windows.Forms.TextBox();
			this.mainTB = new ToolBarControl.ToolBar();
			this.extractFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.bitmSFD = new System.Windows.Forms.SaveFileDialog();
			this.foldersTV = new System.Windows.Forms.TreeView();
			this.foldersPanel = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.foldersS = new System.Windows.Forms.Splitter();
			this.tbiconsIL = new System.Windows.Forms.ImageList(this.components);
			this.sndSFD = new System.Windows.Forms.SaveFileDialog();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.shortcutsMI = new System.Windows.Forms.MenuItem();
			this.findMI = new System.Windows.Forms.MenuItem();
			this.itemswapMI = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.mapsTaskPane)).BeginInit();
			this.mapsTaskPane.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.browseExpando)).BeginInit();
			this.browseExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolsExpando)).BeginInit();
			this.toolsExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.calcExpando)).BeginInit();
			this.calcExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.findExpando)).BeginInit();
			this.findExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.soundOptionsExpando)).BeginInit();
			this.soundOptionsExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.propertiesExpando)).BeginInit();
			this.propertiesExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bitmTB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mapdetailsExpando)).BeginInit();
			this.mapdetailsExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.folderdetailsExpando)).BeginInit();
			this.folderdetailsExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.finddetailsExpando)).BeginInit();
			this.finddetailsExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.multidetailsExpando)).BeginInit();
			this.multidetailsExpando.SuspendLayout();
			this.addressPanel.SuspendLayout();
			this.foldersPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mapsTaskPane
			// 
			this.mapsTaskPane.AutoScrollMargin = new System.Drawing.Size(0, 12);
			this.mapsTaskPane.CustomSettings.GradientEndColor = System.Drawing.Color.DarkRed;
			this.mapsTaskPane.CustomSettings.GradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.mapsTaskPane.Dock = System.Windows.Forms.DockStyle.Left;
			this.mapsTaskPane.Expandos.AddRange(new XPExplorerBar.Expando[] {
																																				this.browseExpando,
																																				this.toolsExpando,
																																				this.calcExpando,
																																				this.findExpando,
																																				this.soundOptionsExpando,
																																				this.propertiesExpando,
																																				this.mapdetailsExpando,
																																				this.folderdetailsExpando,
																																				this.finddetailsExpando,
																																				this.multidetailsExpando});
			this.mapsTaskPane.Location = new System.Drawing.Point(0, 25);
			this.mapsTaskPane.Name = "mapsTaskPane";
			this.mapsTaskPane.Size = new System.Drawing.Size(184, 381);
			this.mapsTaskPane.TabIndex = 1;
			this.mapsTaskPane.Text = "taskPane";
			// 
			// browseExpando
			// 
			this.browseExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.browseExpando.Animate = true;
			this.browseExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.browseExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.browseExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.browseExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.browseExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.browseExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.browseExpando.CustomHeaderSettings.TitleGradient = true;
			this.browseExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.browseExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.browseExpando.ExpandedHeight = 54;
			this.browseExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.browseExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																						 this.parentTaskItem});
			this.browseExpando.Location = new System.Drawing.Point(12, 12);
			this.browseExpando.Name = "browseExpando";
			this.browseExpando.TabIndex = 0;
			this.browseExpando.Text = "Other Places";
			// 
			// parentTaskItem
			// 
			this.parentTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.parentTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.parentTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.parentTaskItem.Image = null;
			this.parentTaskItem.Location = new System.Drawing.Point(8, 32);
			this.parentTaskItem.Name = "parentTaskItem";
			this.parentTaskItem.Size = new System.Drawing.Size(144, 16);
			this.parentTaskItem.TabIndex = 0;
			this.parentTaskItem.Text = "Parent Folder";
			this.parentTaskItem.Click += new System.EventHandler(this.parentTaskItem_Click);
			// 
			// toolsExpando
			// 
			this.toolsExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.toolsExpando.Animate = true;
			this.toolsExpando.Collapsed = true;
			this.toolsExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.toolsExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.toolsExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.toolsExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.toolsExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.toolsExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.toolsExpando.CustomHeaderSettings.TitleGradient = true;
			this.toolsExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.toolsExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.toolsExpando.ExpandedHeight = 125;
			this.toolsExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.toolsExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																						this.findCB,
																																						this.calculatorCB,
																																						this.extractMapTaskItem,
																																						this.soundOptionsCheckBox});
			this.toolsExpando.Location = new System.Drawing.Point(12, 78);
			this.toolsExpando.Name = "toolsExpando";
			this.toolsExpando.TabIndex = 5;
			this.toolsExpando.Text = "Tools";
			// 
			// findCB
			// 
			this.findCB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.findCB.Location = new System.Drawing.Point(8, 56);
			this.findCB.Name = "findCB";
			this.findCB.Size = new System.Drawing.Size(144, 16);
			this.findCB.TabIndex = 0;
			this.findCB.Text = "Find Items";
			this.findCB.CheckedChanged += new System.EventHandler(this.findCB_CheckedChanged);
			// 
			// calculatorCB
			// 
			this.calculatorCB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.calculatorCB.Location = new System.Drawing.Point(8, 32);
			this.calculatorCB.Name = "calculatorCB";
			this.calculatorCB.Size = new System.Drawing.Size(144, 16);
			this.calculatorCB.TabIndex = 1;
			this.calculatorCB.Text = "Calculator";
			this.calculatorCB.CheckedChanged += new System.EventHandler(this.calculatorCB_CheckedChanged);
			// 
			// extractMapTaskItem
			// 
			this.extractMapTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.extractMapTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.extractMapTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.extractMapTaskItem.Image = null;
			this.extractMapTaskItem.Location = new System.Drawing.Point(8, 104);
			this.extractMapTaskItem.Name = "extractMapTaskItem";
			this.extractMapTaskItem.Size = new System.Drawing.Size(144, 16);
			this.extractMapTaskItem.TabIndex = 2;
			this.extractMapTaskItem.Text = "Extract Map";
			this.extractMapTaskItem.Click += new System.EventHandler(this.extractMapTaskItem_Click);
			// 
			// soundOptionsCheckBox
			// 
			this.soundOptionsCheckBox.Enabled = false;
			this.soundOptionsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.soundOptionsCheckBox.Location = new System.Drawing.Point(8, 80);
			this.soundOptionsCheckBox.Name = "soundOptionsCheckBox";
			this.soundOptionsCheckBox.Size = new System.Drawing.Size(144, 16);
			this.soundOptionsCheckBox.TabIndex = 3;
			this.soundOptionsCheckBox.Text = "Sound Options";
			this.soundOptionsCheckBox.CheckedChanged += new System.EventHandler(this.soundOptionsCheckBox_CheckedChanged);
			// 
			// calcExpando
			// 
			this.calcExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.calcExpando.Animate = true;
			this.calcExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.calcExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.calcExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.calcExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.calcExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.calcExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.calcExpando.CustomHeaderSettings.TitleGradient = true;
			this.calcExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.calcExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.calcExpando.ExpandedHeight = 150;
			this.calcExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.calcExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																					 this.valueOneTextBox,
																																					 this.valueTwoTextBox,
																																					 this.value1Label,
																																					 this.value2Label,
																																					 this.resultTextBox,
																																					 this.addButton,
																																					 this.subtractButton,
																																					 this.resultLabel});
			this.calcExpando.Location = new System.Drawing.Point(12, 113);
			this.calcExpando.Name = "calcExpando";
			this.calcExpando.TabIndex = 7;
			this.calcExpando.Text = "Calculator";
			this.calcExpando.Visible = false;
			// 
			// valueOneTextBox
			// 
			this.valueOneTextBox.BackColor = System.Drawing.Color.RosyBrown;
			this.valueOneTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.valueOneTextBox.ContextMenu = this.calculatorContextMenu;
			this.valueOneTextBox.Location = new System.Drawing.Point(56, 32);
			this.valueOneTextBox.Name = "valueOneTextBox";
			this.valueOneTextBox.Size = new System.Drawing.Size(96, 21);
			this.valueOneTextBox.TabIndex = 0;
			this.valueOneTextBox.Text = "";
			// 
			// calculatorContextMenu
			// 
			this.calculatorContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																													this.insertMenuItem,
																																													this.formatMenuItem,
																																													this.convertMenuItem});
			// 
			// insertMenuItem
			// 
			this.insertMenuItem.Index = 0;
			this.insertMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.magicMenuItem,
																																									 this.secMagicMenuItem,
																																									 this.checkSumMenuItem});
			this.insertMenuItem.Text = "Insert";
			// 
			// magicMenuItem
			// 
			this.magicMenuItem.Index = 0;
			this.magicMenuItem.Text = "Magic";
			this.magicMenuItem.Click += new System.EventHandler(this.magicMenuItem_Click);
			// 
			// secMagicMenuItem
			// 
			this.secMagicMenuItem.Index = 1;
			this.secMagicMenuItem.Text = "Secondary Magic";
			this.secMagicMenuItem.Click += new System.EventHandler(this.secMagicMenuItem_Click);
			// 
			// checkSumMenuItem
			// 
			this.checkSumMenuItem.Index = 2;
			this.checkSumMenuItem.Text = "Checksum";
			this.checkSumMenuItem.Click += new System.EventHandler(this.checkSumMenuItem_Click);
			// 
			// formatMenuItem
			// 
			this.formatMenuItem.Index = 1;
			this.formatMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.removeSpacesMenuItem,
																																									 this.addSpacesMenuItem,
																																									 this.endianSwapMenuItem});
			this.formatMenuItem.Text = "Format";
			// 
			// removeSpacesMenuItem
			// 
			this.removeSpacesMenuItem.Index = 0;
			this.removeSpacesMenuItem.Text = "Remove Spaces";
			this.removeSpacesMenuItem.Click += new System.EventHandler(this.removeSpacesMenuItem_Click);
			// 
			// addSpacesMenuItem
			// 
			this.addSpacesMenuItem.Index = 1;
			this.addSpacesMenuItem.Text = "Add Spaces";
			this.addSpacesMenuItem.Click += new System.EventHandler(this.addSpacesMenuItem_Click);
			// 
			// endianSwapMenuItem
			// 
			this.endianSwapMenuItem.Index = 2;
			this.endianSwapMenuItem.Text = "Endian Swap";
			this.endianSwapMenuItem.Click += new System.EventHandler(this.endianSwapMenuItem_Click);
			// 
			// convertMenuItem
			// 
			this.convertMenuItem.Index = 2;
			this.convertMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.fromHexMenuItem,
																																										this.fromDecimalMenuItem,
																																										this.fromBinaryMenuItem});
			this.convertMenuItem.Text = "Convert";
			// 
			// fromHexMenuItem
			// 
			this.fromHexMenuItem.Index = 0;
			this.fromHexMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.hToDecimal,
																																										this.hToBinaryMenuItem});
			this.fromHexMenuItem.Text = "Hexadecimal";
			// 
			// hToDecimal
			// 
			this.hToDecimal.Index = 0;
			this.hToDecimal.Text = "To Decimal";
			this.hToDecimal.Click += new System.EventHandler(this.hToDecimalMenuItem_Click);
			// 
			// hToBinaryMenuItem
			// 
			this.hToBinaryMenuItem.Index = 1;
			this.hToBinaryMenuItem.Text = "To Binary";
			this.hToBinaryMenuItem.Click += new System.EventHandler(this.hToBinaryMenuItem_Click);
			// 
			// fromDecimalMenuItem
			// 
			this.fromDecimalMenuItem.Index = 1;
			this.fromDecimalMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																												this.dToHexMenuItem,
																																												this.dToBinary});
			this.fromDecimalMenuItem.Text = "Decimal";
			// 
			// dToHexMenuItem
			// 
			this.dToHexMenuItem.Index = 0;
			this.dToHexMenuItem.Text = "To Hexadecimal";
			this.dToHexMenuItem.Click += new System.EventHandler(this.dToHexMenuItem_Click);
			// 
			// dToBinary
			// 
			this.dToBinary.Index = 1;
			this.dToBinary.Text = "To Binary";
			this.dToBinary.Click += new System.EventHandler(this.dToBinary_Click);
			// 
			// fromBinaryMenuItem
			// 
			this.fromBinaryMenuItem.Index = 2;
			this.fromBinaryMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																											 this.bToDecimal,
																																											 this.bToHexMenuItem});
			this.fromBinaryMenuItem.Text = "Binary";
			// 
			// bToDecimal
			// 
			this.bToDecimal.Index = 0;
			this.bToDecimal.Text = "To Decimal";
			this.bToDecimal.Click += new System.EventHandler(this.bToDecimal_Click);
			// 
			// bToHexMenuItem
			// 
			this.bToHexMenuItem.Index = 1;
			this.bToHexMenuItem.Text = "To Hexadecimal";
			this.bToHexMenuItem.Click += new System.EventHandler(this.bToHexMenuItem_Click);
			// 
			// valueTwoTextBox
			// 
			this.valueTwoTextBox.BackColor = System.Drawing.Color.RosyBrown;
			this.valueTwoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.valueTwoTextBox.ContextMenu = this.calculatorContextMenu;
			this.valueTwoTextBox.Location = new System.Drawing.Point(56, 54);
			this.valueTwoTextBox.Name = "valueTwoTextBox";
			this.valueTwoTextBox.Size = new System.Drawing.Size(96, 21);
			this.valueTwoTextBox.TabIndex = 1;
			this.valueTwoTextBox.Text = "";
			// 
			// value1Label
			// 
			this.value1Label.Location = new System.Drawing.Point(8, 34);
			this.value1Label.Name = "value1Label";
			this.value1Label.Size = new System.Drawing.Size(48, 16);
			this.value1Label.TabIndex = 2;
			this.value1Label.Text = "Value 1:";
			// 
			// value2Label
			// 
			this.value2Label.Location = new System.Drawing.Point(8, 56);
			this.value2Label.Name = "value2Label";
			this.value2Label.Size = new System.Drawing.Size(48, 16);
			this.value2Label.TabIndex = 3;
			this.value2Label.Text = "Value 2:";
			// 
			// resultTextBox
			// 
			this.resultTextBox.BackColor = System.Drawing.Color.RosyBrown;
			this.resultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.resultTextBox.ContextMenu = this.calculatorContextMenu;
			this.resultTextBox.Location = new System.Drawing.Point(56, 88);
			this.resultTextBox.Name = "resultTextBox";
			this.resultTextBox.Size = new System.Drawing.Size(96, 21);
			this.resultTextBox.TabIndex = 4;
			this.resultTextBox.Text = "";
			// 
			// addButton
			// 
			this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.addButton.Location = new System.Drawing.Point(16, 120);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(48, 24);
			this.addButton.TabIndex = 5;
			this.addButton.Text = "Add";
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// subtractButton
			// 
			this.subtractButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.subtractButton.Location = new System.Drawing.Point(80, 120);
			this.subtractButton.Name = "subtractButton";
			this.subtractButton.Size = new System.Drawing.Size(64, 24);
			this.subtractButton.TabIndex = 6;
			this.subtractButton.Text = "Subtract";
			this.subtractButton.Click += new System.EventHandler(this.subtractButton_Click);
			// 
			// resultLabel
			// 
			this.resultLabel.Location = new System.Drawing.Point(8, 90);
			this.resultLabel.Name = "resultLabel";
			this.resultLabel.Size = new System.Drawing.Size(48, 16);
			this.resultLabel.TabIndex = 7;
			this.resultLabel.Text = "Result:";
			// 
			// findExpando
			// 
			this.findExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.findExpando.Animate = true;
			this.findExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.findExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.findExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.findExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.findExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.findExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.findExpando.CustomHeaderSettings.TitleGradient = true;
			this.findExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.findExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.findExpando.ExpandedHeight = 204;
			this.findExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.findExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																					 this.label2,
																																					 this.searchforTB,
																																					 this.label3,
																																					 this.searchasCB,
																																					 this.searchB,
																																					 this.label4,
																																					 this.lookinCB});
			this.findExpando.Location = new System.Drawing.Point(12, 275);
			this.findExpando.Name = "findExpando";
			this.findExpando.TabIndex = 6;
			this.findExpando.Text = "Find";
			this.findExpando.TitleImage = ((System.Drawing.Image)(resources.GetObject("findExpando.TitleImage")));
			this.findExpando.Visible = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Search For:";
			// 
			// searchforTB
			// 
			this.searchforTB.BackColor = System.Drawing.Color.RosyBrown;
			this.searchforTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.searchforTB.Location = new System.Drawing.Point(8, 56);
			this.searchforTB.Name = "searchforTB";
			this.searchforTB.Size = new System.Drawing.Size(144, 21);
			this.searchforTB.TabIndex = 1;
			this.searchforTB.Text = "";
			this.searchforTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchforTB_KeyPress);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Search As:";
			// 
			// searchasCB
			// 
			this.searchasCB.BackColor = System.Drawing.Color.RosyBrown;
			this.searchasCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.searchasCB.Items.AddRange(new object[] {
																										"File Name",
																										"Ident",
																										"Meta Offset",
																										"Offset",
																										"Value"});
			this.searchasCB.Location = new System.Drawing.Point(8, 104);
			this.searchasCB.Name = "searchasCB";
			this.searchasCB.Size = new System.Drawing.Size(144, 21);
			this.searchasCB.TabIndex = 3;
			// 
			// searchB
			// 
			this.searchB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchB.Location = new System.Drawing.Point(80, 176);
			this.searchB.Name = "searchB";
			this.searchB.Size = new System.Drawing.Size(72, 24);
			this.searchB.TabIndex = 4;
			this.searchB.Text = "&Search";
			this.searchB.Click += new System.EventHandler(this.searchB_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Look In:";
			// 
			// lookinCB
			// 
			this.lookinCB.BackColor = System.Drawing.Color.RosyBrown;
			this.lookinCB.DropDownWidth = 400;
			this.lookinCB.Location = new System.Drawing.Point(8, 152);
			this.lookinCB.Name = "lookinCB";
			this.lookinCB.Size = new System.Drawing.Size(144, 21);
			this.lookinCB.TabIndex = 6;
			// 
			// soundOptionsExpando
			// 
			this.soundOptionsExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.soundOptionsExpando.Animate = true;
			this.soundOptionsExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.soundOptionsExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.soundOptionsExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.soundOptionsExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.soundOptionsExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.soundOptionsExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.soundOptionsExpando.CustomHeaderSettings.TitleGradient = true;
			this.soundOptionsExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.soundOptionsExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.soundOptionsExpando.ExpandedHeight = 145;
			this.soundOptionsExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.soundOptionsExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																									 this.optionComboBox,
																																									 this.soundOptionDetailsLabel,
																																									 this.extractSoundButton,
																																									 this.playButton});
			this.soundOptionsExpando.Location = new System.Drawing.Point(12, 491);
			this.soundOptionsExpando.Name = "soundOptionsExpando";
			this.soundOptionsExpando.TabIndex = 10;
			this.soundOptionsExpando.Text = "Sound Options";
			this.soundOptionsExpando.Visible = false;
			// 
			// optionComboBox
			// 
			this.optionComboBox.Location = new System.Drawing.Point(8, 32);
			this.optionComboBox.Name = "optionComboBox";
			this.optionComboBox.Size = new System.Drawing.Size(144, 21);
			this.optionComboBox.TabIndex = 0;
			this.optionComboBox.SelectedIndexChanged += new System.EventHandler(this.optionComboBox_SelectedIndexChanged);
			// 
			// soundOptionDetailsLabel
			// 
			this.soundOptionDetailsLabel.Location = new System.Drawing.Point(8, 64);
			this.soundOptionDetailsLabel.Name = "soundOptionDetailsLabel";
			this.soundOptionDetailsLabel.Size = new System.Drawing.Size(144, 48);
			this.soundOptionDetailsLabel.TabIndex = 1;
			// 
			// extractSoundButton
			// 
			this.extractSoundButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.extractSoundButton.Location = new System.Drawing.Point(88, 112);
			this.extractSoundButton.Name = "extractSoundButton";
			this.extractSoundButton.Size = new System.Drawing.Size(56, 24);
			this.extractSoundButton.TabIndex = 2;
			this.extractSoundButton.Text = "Extract";
			this.extractSoundButton.Click += new System.EventHandler(this.extractSoundButton_Click);
			// 
			// playButton
			// 
			this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.playButton.Location = new System.Drawing.Point(16, 112);
			this.playButton.Name = "playButton";
			this.playButton.Size = new System.Drawing.Size(56, 24);
			this.playButton.TabIndex = 3;
			this.playButton.Text = "Play";
			this.playButton.Click += new System.EventHandler(this.playButton_Click);
			// 
			// propertiesExpando
			// 
			this.propertiesExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertiesExpando.Animate = true;
			this.propertiesExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.propertiesExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.propertiesExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.propertiesExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.propertiesExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.propertiesExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.propertiesExpando.CustomHeaderSettings.TitleGradient = true;
			this.propertiesExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.propertiesExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.propertiesExpando.ExpandedHeight = 193;
			this.propertiesExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.propertiesExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																								 this.bitmPB,
																																								 this.iteminfoL,
																																								 this.bitmTB});
			this.propertiesExpando.Location = new System.Drawing.Point(12, 648);
			this.propertiesExpando.Name = "propertiesExpando";
			this.propertiesExpando.TabIndex = 1;
			this.propertiesExpando.Text = "Details";
			this.propertiesExpando.Visible = false;
			// 
			// bitmPB
			// 
			this.bitmPB.Location = new System.Drawing.Point(16, 32);
			this.bitmPB.Name = "bitmPB";
			this.bitmPB.Size = new System.Drawing.Size(128, 128);
			this.bitmPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.bitmPB.TabIndex = 4;
			this.bitmPB.TabStop = false;
			this.bitmPB.Visible = false;
			this.bitmPB.DoubleClick += new System.EventHandler(this.bitmPB_DoubleClick);
			// 
			// iteminfoL
			// 
			this.iteminfoL.Location = new System.Drawing.Point(8, 184);
			this.iteminfoL.Name = "iteminfoL";
			this.iteminfoL.Size = new System.Drawing.Size(144, 16);
			this.iteminfoL.TabIndex = 5;
			this.iteminfoL.Text = "Blabla";
			// 
			// bitmTB
			// 
			this.bitmTB.AutoSize = false;
			this.bitmTB.Location = new System.Drawing.Point(8, 160);
			this.bitmTB.Name = "bitmTB";
			this.bitmTB.Size = new System.Drawing.Size(144, 16);
			this.bitmTB.TabIndex = 6;
			this.bitmTB.Visible = false;
			this.bitmTB.Scroll += new System.EventHandler(this.bitmTB_Scroll);
			// 
			// mapdetailsExpando
			// 
			this.mapdetailsExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mapdetailsExpando.Animate = true;
			this.mapdetailsExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.mapdetailsExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.mapdetailsExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.mapdetailsExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.mapdetailsExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.mapdetailsExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.mapdetailsExpando.CustomHeaderSettings.TitleGradient = true;
			this.mapdetailsExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.mapdetailsExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.mapdetailsExpando.ExpandedHeight = 120;
			this.mapdetailsExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.mapdetailsExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																								 this.primaryMagicTaskItem,
																																								 this.secondaryMagicTaskItem,
																																								 this.checksumTaskItem,
																																								 this.mapTypeTaskItem});
			this.mapdetailsExpando.Location = new System.Drawing.Point(12, 853);
			this.mapdetailsExpando.Name = "mapdetailsExpando";
			this.mapdetailsExpando.TabIndex = 3;
			this.mapdetailsExpando.Text = "Details";
			this.mapdetailsExpando.Visible = false;
			// 
			// primaryMagicTaskItem
			// 
			this.primaryMagicTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.primaryMagicTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.primaryMagicTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.primaryMagicTaskItem.Image = null;
			this.primaryMagicTaskItem.Location = new System.Drawing.Point(8, 32);
			this.primaryMagicTaskItem.Name = "primaryMagicTaskItem";
			this.primaryMagicTaskItem.Size = new System.Drawing.Size(144, 16);
			this.primaryMagicTaskItem.TabIndex = 2;
			this.primaryMagicTaskItem.Text = "Magic 1:";
			// 
			// secondaryMagicTaskItem
			// 
			this.secondaryMagicTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.secondaryMagicTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.secondaryMagicTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.secondaryMagicTaskItem.Image = null;
			this.secondaryMagicTaskItem.Location = new System.Drawing.Point(8, 48);
			this.secondaryMagicTaskItem.Name = "secondaryMagicTaskItem";
			this.secondaryMagicTaskItem.Size = new System.Drawing.Size(144, 16);
			this.secondaryMagicTaskItem.TabIndex = 3;
			this.secondaryMagicTaskItem.Text = "Magic 2:";
			// 
			// checksumTaskItem
			// 
			this.checksumTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.checksumTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.checksumTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.checksumTaskItem.Image = null;
			this.checksumTaskItem.Location = new System.Drawing.Point(8, 64);
			this.checksumTaskItem.Name = "checksumTaskItem";
			this.checksumTaskItem.Size = new System.Drawing.Size(144, 16);
			this.checksumTaskItem.TabIndex = 4;
			this.checksumTaskItem.Text = "Checksum:";
			// 
			// mapTypeTaskItem
			// 
			this.mapTypeTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.mapTypeTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.mapTypeTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.mapTypeTaskItem.Image = null;
			this.mapTypeTaskItem.Location = new System.Drawing.Point(8, 80);
			this.mapTypeTaskItem.Name = "mapTypeTaskItem";
			this.mapTypeTaskItem.Size = new System.Drawing.Size(144, 32);
			this.mapTypeTaskItem.TabIndex = 5;
			this.mapTypeTaskItem.Text = "Map Type:";
			// 
			// folderdetailsExpando
			// 
			this.folderdetailsExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.folderdetailsExpando.Animate = true;
			this.folderdetailsExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.folderdetailsExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.folderdetailsExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.folderdetailsExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.folderdetailsExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.folderdetailsExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.folderdetailsExpando.CustomHeaderSettings.TitleGradient = true;
			this.folderdetailsExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.folderdetailsExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.folderdetailsExpando.ExpandedHeight = 54;
			this.folderdetailsExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.folderdetailsExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																										this.nameTI});
			this.folderdetailsExpando.Location = new System.Drawing.Point(12, 985);
			this.folderdetailsExpando.Name = "folderdetailsExpando";
			this.folderdetailsExpando.TabIndex = 4;
			this.folderdetailsExpando.Text = "Details";
			this.folderdetailsExpando.Visible = false;
			// 
			// nameTI
			// 
			this.nameTI.BackColor = System.Drawing.Color.Transparent;
			this.nameTI.CustomSettings.FontDecoration = System.Drawing.FontStyle.Bold;
			this.nameTI.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.nameTI.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.nameTI.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nameTI.Image = null;
			this.nameTI.Location = new System.Drawing.Point(8, 32);
			this.nameTI.Name = "nameTI";
			this.nameTI.Size = new System.Drawing.Size(144, 16);
			this.nameTI.TabIndex = 0;
			// 
			// finddetailsExpando
			// 
			this.finddetailsExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.finddetailsExpando.Animate = true;
			this.finddetailsExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.finddetailsExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.finddetailsExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.finddetailsExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.finddetailsExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.finddetailsExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.finddetailsExpando.CustomHeaderSettings.TitleGradient = true;
			this.finddetailsExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.finddetailsExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.finddetailsExpando.ExpandedHeight = 68;
			this.finddetailsExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.finddetailsExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																									this.findresultL});
			this.finddetailsExpando.Location = new System.Drawing.Point(12, 1051);
			this.finddetailsExpando.Name = "finddetailsExpando";
			this.finddetailsExpando.TabIndex = 8;
			this.finddetailsExpando.Text = "Details";
			this.finddetailsExpando.Visible = false;
			// 
			// findresultL
			// 
			this.findresultL.Location = new System.Drawing.Point(8, 32);
			this.findresultL.Name = "findresultL";
			this.findresultL.Size = new System.Drawing.Size(144, 32);
			this.findresultL.TabIndex = 0;
			this.findresultL.Text = "Result:";
			// 
			// multidetailsExpando
			// 
			this.multidetailsExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.multidetailsExpando.Animate = true;
			this.multidetailsExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.multidetailsExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.multidetailsExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.multidetailsExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.multidetailsExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.multidetailsExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.multidetailsExpando.CustomHeaderSettings.TitleGradient = true;
			this.multidetailsExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.multidetailsExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.multidetailsExpando.ExpandedHeight = 78;
			this.multidetailsExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.multidetailsExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																									 this.multiitemsL});
			this.multidetailsExpando.Location = new System.Drawing.Point(12, 1131);
			this.multidetailsExpando.Name = "multidetailsExpando";
			this.multidetailsExpando.TabIndex = 9;
			this.multidetailsExpando.Text = "Details";
			this.multidetailsExpando.Visible = false;
			// 
			// multiitemsL
			// 
			this.multiitemsL.Location = new System.Drawing.Point(8, 32);
			this.multiitemsL.Name = "multiitemsL";
			this.multiitemsL.Size = new System.Drawing.Size(144, 42);
			this.multiitemsL.TabIndex = 0;
			this.multiitemsL.Text = "0 files selected.";
			// 
			// iconsImageList
			// 
			this.iconsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.iconsImageList.ImageSize = new System.Drawing.Size(50, 50);
			this.iconsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconsImageList.ImageStream")));
			this.iconsImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// contentsXPListView
			// 
			this.contentsXPListView.AllowDrop = true;
			this.contentsXPListView.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.contentsXPListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.contentsXPListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																																												 this.filenameCH,
																																												 this.sizeCH,
																																												 this.typeCH});
			this.contentsXPListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentsXPListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.contentsXPListView.ForeColor = System.Drawing.Color.White;
			this.contentsXPListView.LargeImageList = this.iconsImageList;
			this.contentsXPListView.Location = new System.Drawing.Point(429, 25);
			this.contentsXPListView.Name = "contentsXPListView";
			this.contentsXPListView.Size = new System.Drawing.Size(189, 381);
			this.contentsXPListView.SmallImageList = this.smalliconsIL;
			this.contentsXPListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.contentsXPListView.TabIndex = 2;
			this.contentsXPListView.Resize += new System.EventHandler(this.contentsXPListView_Resize);
			this.contentsXPListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.contentsXPListView_MouseDown);
			this.contentsXPListView.ItemActivate += new System.EventHandler(this.contentsXPListView_ItemClicked);
			this.contentsXPListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.contentsXPListView_DragDrop);
			this.contentsXPListView.SelectedIndexChanged += new System.EventHandler(this.contentsXPListView_SelectedIndexChanged);
			this.contentsXPListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.contentsXPListView_MouseUp);
			this.contentsXPListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.contentsXPListView_MouseMove);
			this.contentsXPListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.contentsXPListView_DragEnter);
			this.contentsXPListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.contentsXPListView_KeyUp);
			// 
			// filenameCH
			// 
			this.filenameCH.Text = "File Name";
			this.filenameCH.Width = 400;
			// 
			// sizeCH
			// 
			this.sizeCH.Text = "Size";
			this.sizeCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.sizeCH.Width = 70;
			// 
			// typeCH
			// 
			this.typeCH.Text = "Type";
			this.typeCH.Width = 140;
			// 
			// smalliconsIL
			// 
			this.smalliconsIL.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.smalliconsIL.ImageSize = new System.Drawing.Size(16, 16);
			this.smalliconsIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smalliconsIL.ImageStream")));
			this.smalliconsIL.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// itemContextMenu
			// 
			this.itemContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.openMI,
																																										this.openfolderMI,
																																										this.itemswapMI,
																																										this.menuItem3,
																																										this.copyIdentMenuItem,
																																										this.copyMetaOffsetMenuItem,
																																										this.copyMetaValueMenuItem,
																																										this.menuItem1,
																																										this.propertiesMenuItem});
			this.itemContextMenu.Popup += new System.EventHandler(this.itemContextMenu_Popup);
			// 
			// openMI
			// 
			this.openMI.Index = 0;
			this.openMI.Text = "&Open";
			this.openMI.Click += new System.EventHandler(this.openMI_Click);
			// 
			// openfolderMI
			// 
			this.openfolderMI.Index = 1;
			this.openfolderMI.Text = "Open containing &Folder";
			this.openfolderMI.Visible = false;
			this.openfolderMI.Click += new System.EventHandler(this.openfolderMI_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "-";
			// 
			// copyIdentMenuItem
			// 
			this.copyIdentMenuItem.Index = 4;
			this.copyIdentMenuItem.Text = "Copy Ident to Calculator";
			this.copyIdentMenuItem.Click += new System.EventHandler(this.copyIdentMenuItem_Click);
			// 
			// copyMetaOffsetMenuItem
			// 
			this.copyMetaOffsetMenuItem.Index = 5;
			this.copyMetaOffsetMenuItem.Text = "Copy Meta Offset to Calculator";
			this.copyMetaOffsetMenuItem.Click += new System.EventHandler(this.copyMetaOffsetMenuItem_Click);
			// 
			// copyMetaValueMenuItem
			// 
			this.copyMetaValueMenuItem.Index = 6;
			this.copyMetaValueMenuItem.Text = "Copy Meta Value to Calculator";
			this.copyMetaValueMenuItem.Click += new System.EventHandler(this.copyMetaValueMenuItem_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 7;
			this.menuItem1.Text = "-";
			// 
			// propertiesMenuItem
			// 
			this.propertiesMenuItem.Index = 8;
			this.propertiesMenuItem.Text = "&Properties";
			this.propertiesMenuItem.Click += new System.EventHandler(this.propertiesMenuItem_Click);
			// 
			// addressPanel
			// 
			this.addressPanel.Controls.Add(this.filterTB);
			this.addressPanel.Controls.Add(this.label1);
			this.addressPanel.Controls.Add(this.addressTB);
			this.addressPanel.Controls.Add(this.mainTB);
			this.addressPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.addressPanel.Location = new System.Drawing.Point(0, 0);
			this.addressPanel.Name = "addressPanel";
			this.addressPanel.Size = new System.Drawing.Size(618, 25);
			this.addressPanel.TabIndex = 3;
			// 
			// filterTB
			// 
			this.filterTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.filterTB.BackColor = System.Drawing.Color.RosyBrown;
			this.filterTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.filterTB.Location = new System.Drawing.Point(556, 3);
			this.filterTB.Name = "filterTB";
			this.filterTB.Size = new System.Drawing.Size(56, 20);
			this.filterTB.TabIndex = 2;
			this.filterTB.Text = "*.*";
			this.filterTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.filterTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.filterTB_KeyPress);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(80, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Address";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// addressTB
			// 
			this.addressTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.addressTB.BackColor = System.Drawing.Color.RosyBrown;
			this.addressTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.addressTB.Location = new System.Drawing.Point(136, 3);
			this.addressTB.Name = "addressTB";
			this.addressTB.Size = new System.Drawing.Size(412, 20);
			this.addressTB.TabIndex = 0;
			this.addressTB.Text = "";
			this.addressTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressTB_KeyPress);
			// 
			// mainTB
			// 
			this.mainTB.BackColor = System.Drawing.Color.LightGray;
			this.mainTB.ButtonImageHeight = 16;
			this.mainTB.ButtonImageWidth = 16;
			this.mainTB.ButtonShadeBorderColor = System.Drawing.Color.DarkBlue;
			this.mainTB.ButtonShadeColor = System.Drawing.Color.FromArgb(((System.Byte)(50)), ((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(255)));
			this.mainTB.ButtonShadeColorOpacity = 50;
			this.mainTB.ImageShadowColor = System.Drawing.Color.Black;
			this.mainTB.Location = new System.Drawing.Point(1, 1);
			this.mainTB.Name = "mainTB";
			this.mainTB.SeparatorColor = System.Drawing.Color.DarkGray;
			this.mainTB.SeparatorWidth = 5;
			this.mainTB.Size = new System.Drawing.Size(64, 24);
			this.mainTB.TabIndex = 4;
			this.mainTB.Visible = false;
			// 
			// bitmSFD
			// 
			this.bitmSFD.DefaultExt = "png";
			this.bitmSFD.Filter = "Bitmap (*.png)|*.png";
			this.bitmSFD.Title = "Save bitmap to...";
			// 
			// foldersTV
			// 
			this.foldersTV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.foldersTV.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.foldersTV.HideSelection = false;
			this.foldersTV.ImageList = this.smalliconsIL;
			this.foldersTV.Location = new System.Drawing.Point(0, 20);
			this.foldersTV.Name = "foldersTV";
			this.foldersTV.ShowLines = false;
			this.foldersTV.Size = new System.Drawing.Size(240, 361);
			this.foldersTV.Sorted = true;
			this.foldersTV.TabIndex = 5;
			this.foldersTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.foldersLV_AfterSelect);
			// 
			// foldersPanel
			// 
			this.foldersPanel.Controls.Add(this.foldersTV);
			this.foldersPanel.Controls.Add(this.label5);
			this.foldersPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.foldersPanel.Location = new System.Drawing.Point(184, 25);
			this.foldersPanel.Name = "foldersPanel";
			this.foldersPanel.Size = new System.Drawing.Size(240, 381);
			this.foldersPanel.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(8, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 24);
			this.label5.TabIndex = 6;
			this.label5.Text = "Folders";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// foldersS
			// 
			this.foldersS.Location = new System.Drawing.Point(424, 25);
			this.foldersS.Name = "foldersS";
			this.foldersS.Size = new System.Drawing.Size(5, 381);
			this.foldersS.TabIndex = 7;
			this.foldersS.TabStop = false;
			// 
			// tbiconsIL
			// 
			this.tbiconsIL.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.tbiconsIL.ImageSize = new System.Drawing.Size(16, 16);
			this.tbiconsIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbiconsIL.ImageStream")));
			this.tbiconsIL.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// sndSFD
			// 
			this.sndSFD.Title = "Save sound to...";
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																						 this.shortcutsMI});
			// 
			// shortcutsMI
			// 
			this.shortcutsMI.Index = 0;
			this.shortcutsMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								this.findMI});
			this.shortcutsMI.Text = "Shortcuts";
			this.shortcutsMI.Visible = false;
			// 
			// findMI
			// 
			this.findMI.Index = 0;
			this.findMI.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
			this.findMI.Text = "&Find";
			this.findMI.Click += new System.EventHandler(this.findMI_Click);
			// 
			// itemswapMI
			// 
			this.itemswapMI.Index = 2;
			this.itemswapMI.Text = "&Swap";
			this.itemswapMI.Visible = false;
			this.itemswapMI.Click += new System.EventHandler(this.itemswapMI_Click);
			// 
			// MapExplorer
			// 
			this.AllowDrop = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.ClientSize = new System.Drawing.Size(618, 406);
			this.Controls.Add(this.contentsXPListView);
			this.Controls.Add(this.foldersS);
			this.Controls.Add(this.foldersPanel);
			this.Controls.Add(this.mapsTaskPane);
			this.Controls.Add(this.addressPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "MapExplorer";
			this.Text = "MapExplorer";
			((System.ComponentModel.ISupportInitialize)(this.mapsTaskPane)).EndInit();
			this.mapsTaskPane.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.browseExpando)).EndInit();
			this.browseExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.toolsExpando)).EndInit();
			this.toolsExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.calcExpando)).EndInit();
			this.calcExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.findExpando)).EndInit();
			this.findExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.soundOptionsExpando)).EndInit();
			this.soundOptionsExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.propertiesExpando)).EndInit();
			this.propertiesExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bitmTB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mapdetailsExpando)).EndInit();
			this.mapdetailsExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.folderdetailsExpando)).EndInit();
			this.folderdetailsExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.finddetailsExpando)).EndInit();
			this.finddetailsExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.multidetailsExpando)).EndInit();
			this.multidetailsExpando.ResumeLayout(false);
			this.addressPanel.ResumeLayout(false);
			this.foldersPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region File List Code
		Hashtable root = new Hashtable();
		Hashtable curDirectory;

		public void CreateFileList()
		{
			string[] steps;
			string step;
			Hashtable currentDir;
			TreeNode currentNode;
			string path;

			// Clear current treeview
			foldersTV.Nodes.Clear();

			foldersTV.Nodes.Add(FolderNode(halo2map.Header.Name, ""));

			foreach (MapIndexItem item in halo2map.Index.ItemArray)
			{
				steps = item.Filename.Split(@"\".ToCharArray());
				currentDir = root;
				currentNode = foldersTV.Nodes[0];
				path = "";
				
				for (int i = 0; i < (steps.Length - 1); i++)
				{
					step = steps[i];
					path = path + step + "\\";
					if (!currentDir.ContainsKey(step))
					{
						currentDir.Add(step, new Hashtable());
						currentNode.Nodes.Add(FolderNode(step, path));
					}
					currentDir = (Hashtable)currentDir[step];
					currentNode = FindNode(currentNode, path);
				}

				currentDir.Add(steps[steps.GetUpperBound(0)] + "." + item.Tag, item);
			}

			foldersTV.Nodes[0].Expand();
		}

		private TreeNode FolderNode(string text, string path)
		{
			TreeNode node = new TreeNode(text, GetFolderIndex(path), GetOpenFolderIndex(path));
			node.Tag = path;
			return node;
		}

		private TreeNode FindNode(TreeNode root, string path)
		{
			for (int i = 0; i < root.Nodes.Count; i++) {
				if ((string)root.Nodes[i].Tag == path) {
					return root.Nodes[i];
				}
				if (path.StartsWith((string)root.Nodes[i].Tag)) {
					return FindNode(root.Nodes[i], path);
				}
			}
			return root;
		}

		public void ShowFiles(string path)
		{
			// Reset find view
			lastPath = null;
			openfolderMI.Visible = false;

			if (path.EndsWith("\\")) {
				path = path.Substring(0, path.Length - 1);
			}
			addressTB.Text = path;
			curPath = path;

			// Update root folder
			if (path == "") {
				ShowProperties(null, "");

				browseExpando.Visible = false;

				foldersTV.SelectedNode = foldersTV.Nodes[0];
			} else {
				if (path.IndexOf("\\") == -1) {
					parentTaskItem.Text = halo2map.Header.Name;
				} else if (path.IndexOf("\\") == path.LastIndexOf("\\")) {
					parentTaskItem.Text = path.Substring(0, path.IndexOf("\\"));
				} else {
					string tmp = path.Substring(0, path.LastIndexOf("\\"));
					parentTaskItem.Text = tmp.Substring(tmp.LastIndexOf("\\") + 1);
					parentTaskItem.ImageList = smalliconsIL;
				}
				foldersTV.SelectedNode = FindNode(foldersTV.Nodes[0], path + "\\");
				parentTaskItem.ImageList = smalliconsIL;
				parentTaskItem.ImageIndex = GetFolderIndex(parentTaskItem.Text);

				ShowProperties(curDirectory, path.Substring(path.LastIndexOf("\\") + 1));

				browseExpando.Visible = true;
			}

			contentsXPListView.Items.Clear();

			string[] steps;
			steps = path.Split(@"\".ToCharArray());

			curDirectory = root;

			if (path != "")
			{
				foreach (string step in steps)
				{
					if (!curDirectory.ContainsKey(step))
					{
						Exception ex = new Exception("Path not found\n" + path);
						throw ex;
					}
					curDirectory = (Hashtable)curDirectory[step];
				}
			}

			int iconIndex;
			bool additem;
			Regex regex = new Regex(Filter(filterTB.Text), RegexOptions.IgnoreCase);

			foreach (string element in curDirectory.Keys)
			{
				object thisObject = curDirectory[element];

				ListViewItem newListItem = new ListViewItem(element);
				additem = true;

				if (thisObject.GetType() == typeof(Hashtable)) {
					iconIndex = GetFolderIndex(element);
					newListItem.SubItems.Add("");
					newListItem.SubItems.Add("Folder");
				} else if (thisObject.GetType() == typeof(MapIndexItem)) {
					iconIndex = GetImageIndex((MapIndexItem)thisObject);
					MapIndexItem item = (MapIndexItem)thisObject;
					newListItem.SubItems.Add(Global.ConvertSize(item.Size));
					newListItem.SubItems.Add(Map.TagName(item.Tag));

					if (!regex.IsMatch(element))
						additem = false;
				} else {
					iconIndex = 0;
				}
				newListItem.ImageIndex = iconIndex;
				newListItem.Tag = thisObject;

				if (additem)
					contentsXPListView.Items.Add(newListItem);
			}

			contentsXPListView.View = View.Details;
			BuildLookIn();
		}

		private int GetFolderIndex(string path)
		{
			int result = 0;
			if (path.EndsWith("\\"))
				path = path.TrimEnd('\\');
			path = path.Substring(path.LastIndexOf("\\") + 1);

			if (path.IndexOf("bitmap") > -1)
				result = 12;
			else if (path.IndexOf("sound") > -1)
				result = 13;
			else if (path == halo2map.Header.Name || path == "")
				result = -1;

			return result;
		}

		private int GetOpenFolderIndex(string path)
		{
			int result = 15;
			if (path.EndsWith("\\"))
				path = path.TrimEnd('\\');
			path = path.Substring(path.LastIndexOf("\\") + 1);

			if (path.IndexOf("bitmap") > -1)
				result = 12;
			else if (path.IndexOf("sound") > -1)
				result = 13;
			else if (path == halo2map.Header.Name || path == "")
				result = -1;

			return result;
		}

		/// <summary>
		/// Gives the image index based on the tag for the item.
		/// </summary>
		private int GetImageIndex(MapIndexItem item)
		{
			switch (item.Tag)
			{
				case "bitm":
					return 2;
				case "snd!":
				case "lsnd":
				case "sfx+":
				case "sncl":
				case "snmx":
					return 3;
				case "mode":
					return 4;
				case "vehi":
					return 5;
				case "weap":
					return 6;
				case "proj":
					return 7;
				case "jpt!":
					return 8;
				case "ugh!":
					return 9;
				case "colo":
					return 10;
				case "ligh":
					return 11;
				case "unic":
					return 14;
				case "trak":
					return 16;
				default:
					return 1;
			}
		}
		#endregion

		#region Properties + Info
		public void ShowProperties(object obj, string text)
		{
			// Hide properties window
			propertiesExpando.Visible = false;
			mapdetailsExpando.Visible = false;
			folderdetailsExpando.Visible = false;
			finddetailsExpando.Visible = false;
			multidetailsExpando.Visible = false;

			if (obj == null) {
				if (lastPath == null) {
					// Root folder
					primaryMagicTaskItem.Text = "Magic 1: 0x" + halo2map.Magic.ToString("X8");
					secondaryMagicTaskItem.Text = "Magic 2: 0x" + halo2map.SecondaryMagic.ToString("X8");
					checksumTaskItem.Text = "Checksum: 0x" + halo2map.Signature.ToString("X8");
					mapTypeTaskItem.Text = "Map Type:\n" + halo2map.Header.MapType;

					mapdetailsExpando.Visible = true;
				} else {
					// Find Results
					finddetailsExpando.Visible = true;
				}
			} else if (obj is MapIndexItem) {
				// Meta Item
				ItemInformation((MapIndexItem)obj);

				propertiesExpando.Visible = true;
			} else if (obj is Hashtable) {
				// Folder
				nameTI.Text = text;

				folderdetailsExpando.Visible = true;
			} else if (obj is XPListView) {
				XPListView lv = (XPListView)obj;
				long totalsize = 0;

				foreach (ListViewItem item in lv.SelectedItems) {
					if (item.Tag is MapIndexItem) {
						totalsize += ((MapIndexItem)item.Tag).Size;
					}
				}

				if (totalsize > 0) {
					multiitemsL.Text = "" + lv.SelectedItems.Count + " items selected.\n\nTotal File Size: " + Global.ConvertSize(totalsize);
					multiitemsL.Height = 42;
					multidetailsExpando.ExpandedHeight = 78;
				} else {
					multiitemsL.Text = "" + lv.SelectedItems.Count + " items selected.";
					multiitemsL.Height = 16;
					multidetailsExpando.ExpandedHeight = 54;
				}

				multidetailsExpando.Visible = true;
			}
		}

		private void ItemInformation(MapIndexItem item) 
		{
			// Set default stuff
			iteminfoL.Top = 32;
			bitmPB.Visible = false;

			// Set default information
			string info = Map.TagName(item.Tag) + " [" + item.Tag + "]";
			info += "\n\nIdent: 0x" + item.Ident.ToString("X8");

			// Special Tags
			if (item.Tag != "snd!") {
				soundOptionsExpando.Visible = false;
				soundOptionsCheckBox.Enabled = false;
			}
			switch (item.Tag) {
				case "bitm":
					bitmPB.Visible = false;
					bitmTB.Visible = false;
					bitm.Read(
							halo2map.stream, 
							(int)(item.MetaOffset - halo2map.SecondaryMagic), 
							(long)halo2map.SecondaryMagic, 
							item.Filename, 
							Global.SharedStream(MapTypes.Shared), 
							Global.SharedStream(MapTypes.MainMenu), 
							Global.SharedStream(MapTypes.SinglePlayerShared)
						);
					int result = 3;
					try {
						result = bitm.ShowPreview(0);
						info += "\nDimensions: " + bitm.BitmapWidth(0) + " x " + bitm.BitmapHeight(0);
						info += "\nFormat: " + bitm.BitmapFormat(0);
						info += "\nLocation: " + bitm.BitmapLocation(0);
						if (result < 2) {
							bitmPB.Image = bitm.b;
							bitmPB.Visible = true;
							FixImage();
							bitmTB.Value = 0;
							if (bitm.BitmapCount() > 1) {
								bitmTB.Visible = true;
								bitmTB.Maximum = bitm.BitmapCount() - 1;
								iteminfoL.Top = 184;
								info += "\nCount: " + bitm.BitmapCount();
							} else {
								iteminfoL.Top = 168;
							}
						}
					} catch (Exception exp) {
						info += "\nError: " + exp.Message;
					}
					break;
				case "snd!":
					soundOptionsCheckBox.Enabled = true;
					soundOptionsExpando.Visible = soundOptionsCheckBox.Checked;
					SoundMeta thisSound = new SoundMeta();
					thisSound.ReadMeta(halo2map.stream, item.MetaOffset - halo2map.SecondaryMagic);

					info += "\nType: ";
					switch (thisSound.format)
					{
						case 0:
							info += "ADPCM 22050 mono";
							break;
						case 1:
							info += "ADPCM 44100 stereo";
							break;
						case 2:
							info += "WMA";
							break;
					}

					info += "\nChunks: " + thisSound.chunkCount.ToString();
					info += "\nIndex: " + thisSound.index.ToString();

					info += "\n\nOptions: " + halo2map.sounds.soundOptions[thisSound.index].options.ToString();
					info += "\nStart Index: " + halo2map.sounds.soundOptions[thisSound.index].index.ToString();

					optionComboBox.Items.Clear();
					for (int i = 0; i < halo2map.sounds.soundOptions[thisSound.index].options; i++)
					{
						halo2map.sounds.soundPieces[halo2map.sounds.soundOptions[thisSound.index].index + i].format = thisSound.format;
						halo2map.sounds.soundPieces[halo2map.sounds.soundOptions[thisSound.index].index + i].listIndex = i + 1;
						optionComboBox.Items.Add(halo2map.sounds.soundPieces[halo2map.sounds.soundOptions[thisSound.index].index + i]);
					}

					if (optionComboBox.Items.Count > 0)
						optionComboBox.SelectedIndex = 0;
					else
						optionComboBox.SelectedIndex = -1;
					break;
			}

			info += "\n\nSize: " + Global.ConvertSize(item.Size);

			Graphics g = this.CreateGraphics();

			SizeF size = g.MeasureString(info, iteminfoL.Font, iteminfoL.Width);

			iteminfoL.Text = info;
			iteminfoL.Height = (int)size.Height;

			propertiesExpando.ExpandedHeight = iteminfoL.Top + iteminfoL.Height + 8;
		}
		#endregion

		#region Find Code
		/// <summary>
		/// User pressed Search button.
		/// </summary>
		private void searchB_Click(object sender, System.EventArgs e)
		{
			Search();
		}

		/// <summary>
		/// User pressed key in Search for textbox.
		/// </summary>
		private void searchforTB_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)0xd) {
				Search();
			}
		}

		/// <summary>
		/// Search for the items matching the users parameters.
		/// </summary>
		public void Search()
		{
			string search = searchforTB.Text.ToLower();
			string searchpath = lookinCB.Text.Replace(halo2map.Header.Name, "").ToLower();
			int searchas = searchasCB.SelectedIndex;

			// Remember last path
			lastPath = curPath;

			// Check searchpath
			if (searchpath != "" && !searchpath.EndsWith("\\"))
				searchpath += "\\";

			// Check input
			if (searchas == -1 || search == "")
				return;

			// Clear current stuff
			contentsXPListView.Items.Clear();
			addressTB.Text = "";

			// Set view for find results
			contentsXPListView.View = View.Details;
			openfolderMI.Visible = true;

			// Search all items
			if (searchas == 0) {
				SearchFolder(root, search, searchas);
			} else {
				// Fix search string
				if (search.IndexOfAny(new char[]{'a','b','c','d','e','f'}) > -1) {
					if (!search.StartsWith("0x"))
						search = "0x" + search;
				}
			}
			
			int count = 0;
			try {
				count = SearchItems(searchpath, search, searchas);
			} catch (Exception exp) {
				MessageBox.Show("Error when searching for items.\n\n" + exp.Message);
			}

			// Resize column
			contentsXPListView.Columns[0].Width = -2;

			if (count == 0) {
				findresultL.Text = "Result:\nNothing Found.";
			} else {
				findresultL.Text = "Result:\n" + count + " Item(s) Found.";
			}

			ShowProperties(null, "");
		}

		/// <summary>
		/// Check if item matches our search.
		/// </summary>
		public bool SearchMatches(MapIndexItem item, string search, int searchas)
		{
			bool result = false;

			switch (searchas) {
				case 0: // File Name
					string path = item.Filename + "." + item.Tag;
					result = MatchFilter(path.ToLower(), search);
					break;
				case 1: // Ident
					result = item.Ident == SearchValue(search);
					break;
				case 2: // Meta Offset
					uint metaoffset = item.MetaOffset - halo2map.SecondaryMagic;
					result = metaoffset == SearchValue(search);
					break;
				case 3: // Offset
					result = item.Offset == SearchValue(search);
					break;
				case 4: // Value
					result = item.MetaOffset == SearchValue(search);
					break;
			}

			return result;
		}

		public long SearchValue(string search)
		{
			long result = 0;

			try {
				if (search.StartsWith("0x"))
					result = long.Parse(search.Substring(2), NumberStyles.HexNumber);
				else
					result = long.Parse(search);
			} catch {}

			return result;
		}

		/// <summary>
		/// Search all items in the map.
		/// </summary>
		public int SearchItems(string path, string search, int searchas)
		{
			int searchcount = 0;
			int searchfound = 0;

			foreach (MapIndexItem item in halo2map.Index.ItemArray) {
				searchcount++;
				if (item.Filename.StartsWith(path)) {
					if (SearchMatches(item, search, searchas)) {
						searchfound++;
						int iconIndex = GetImageIndex(item);
						ListViewItem newListItem = new ListViewItem(item.Filename + "." + item.Tag, iconIndex);
						newListItem.Tag = item;
						newListItem.SubItems.Add(Global.ConvertSize(item.Size));
						newListItem.SubItems.Add(Map.TagName(item.Tag));
						contentsXPListView.Items.Add(newListItem);
					}
				}

				// Let the program breath every 100 items
				if (searchfound % 100 == 0)
					Application.DoEvents();

				// Don't show more then 1000 items
				if (searchfound >= 1000)
					break;
			}

			return searchfound;
		}

		/// <summary>
		/// Recursive search the folders.
		/// </summary>
		public void SearchFolder(Hashtable folder, string search, int searchas)
		{
		}

		/// <summary>
		/// Adds the needed paths to the Look In combobox.
		/// </summary>
		private void BuildLookIn()
		{
			lookinCB.Items.Clear();

			// Add map
			lookinCB.Items.Add(halo2map.Header.Name);

			// Add current folder
			if (curDirectory != root) {
				lookinCB.Items.Add(curPath);
			}

			// Select last path
			lookinCB.SelectedIndex = lookinCB.Items.Count - 1;
		}
		#endregion

		#region Save/Load User Settings

		private void LoadState(object sender, RegistryKey key)
		{
			// get additional state information from registry
			//m_data = (int)key.GetValue("m_data", m_data);
		}

		private void SaveState(object sender, RegistryKey key)
		{
			// save additional state information to registry
			//key.SetValue("m_data", m_data);
		}
		
		#endregion

		private void bitmTB_Scroll(object sender, System.EventArgs e)
		{
			MapIndexItem item = (MapIndexItem)contentsXPListView.SelectedItems[0].Tag;

			// Set default information
			string info = Map.TagName(item.Tag) + " [" + item.Tag + "]";
			info += "\n\nIdent: 0x" + item.Ident.ToString("X8");

			// Get the bitmap
			bitm.ShowPreview(bitmTB.Value);
			bitmPB.Image = bitm.b;
			FixImage();

			// Get information
			info += "\nDimensions: " + bitm.BitmapWidth(bitmTB.Value) + " x " + bitm.BitmapHeight(bitmTB.Value);
			info += "\nFormat: " + bitm.BitmapFormat(bitmTB.Value);
			info += "\nLocation: " + bitm.BitmapLocation(bitmTB.Value);
			info += "\nCount: " + bitm.BitmapCount();

			// Get rest of default information
			info += "\n\nSize: " + Global.ConvertSize(item.Size);
		}

		/// <summary>
		/// Will try to make the image viewable on screen.
		/// </summary>
		private void FixImage()
		{
			bitmPB.Location = new Point(16, 32);
			if (bitmPB.Image.Width > 128 || bitmPB.Image.Height > 128) {
				bitmPB.SizeMode = PictureBoxSizeMode.StretchImage;
				int width = 128;
				int height = 128;
				if (bitmPB.Image.Width > 128) {
					height = (int)((128.0f / bitmPB.Image.Width) * bitmPB.Image.Height);
					if (height > 128)
						height = 128;
					bitmPB.Location = new Point(16, 32 + (128 - height) / 2);
				} else {
					width = (int)((128.0f / bitmPB.Image.Height) * bitmPB.Image.Width);
					bitmPB.Location = new Point(16 + (128 - width) / 2, 32);
				}
				bitmPB.Width = width;
				bitmPB.Height = height;
			} else {
				bitmPB.Width = 128;
				bitmPB.Height = 128;
				bitmPB.SizeMode = PictureBoxSizeMode.CenterImage;
			}
		}

		/// <summary>
		/// User selected another item.
		/// </summary>
		private void contentsXPListView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			object obj = null;
			string text = "";
			int count = contentsXPListView.SelectedItems.Count;

			// Enable/Disable popup
			if (count > 0)
				contentsXPListView.ContextMenu = itemContextMenu;
			else
				contentsXPListView.ContextMenu = null;

			// Show correct information
			if (count > 1) {
				obj = contentsXPListView;
			} else if (count == 1) {
				ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
				obj = clickedItem.Tag;
				text = clickedItem.Text;
			} else if (curDirectory != root) {
				obj = curDirectory;
				text = curPath.Substring(curPath.LastIndexOf("\\") + 1);
			}
			
			ShowProperties(obj, text);
		}

		private void contentsXPListView_ItemClicked(object sender, System.EventArgs e)
		{
			if (contentsXPListView.SelectedItems.Count == 0) { return; }
			ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
			object thisObject = clickedItem.Tag;
			
			if (thisObject is Hashtable) {
				if (curPath == "") { ShowFiles(clickedItem.Text); }
				else { ShowFiles(curPath + "\\" + clickedItem.Text); }
			}
			else if (thisObject is MapIndexItem) { openMI_Click(null, null); }
			else { MessageBox.Show("Error"); }
		}

		private void parentTaskItem_Click(object sender, System.EventArgs e)
		{
			GoUp();
		}

		/// <summary>
		/// Move one level up in the map.
		/// </summary>
		public void GoUp()
		{			
			if (curPath.IndexOf("\\") > -1) {
				ShowFiles(curPath.Substring(0, curPath.LastIndexOf("\\")));
			} else {
				ShowFiles("");
			}
		}

		private void MapExplorer_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
				case (char)0x8:
					if (lastPath == null) {
						parentTaskItem_Click(null, null);
					} else {
						ShowFiles(lastPath);
					}
					e.Handled = true;
					break;
				case (char)0xd:
					if (Control.ModifierKeys == Keys.Alt)
					{ propertiesMenuItem_Click(null, null); }
					e.Handled = true;
					break;
				case 'f':
					if (Control.ModifierKeys == Keys.Control) {
						Find();
						e.Handled = true;
					}
					break;
			}
		}

		private void propertiesMenuItem_Click(object sender, System.EventArgs e)
		{
			ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
			object thisObject = clickedItem.Tag;
			
			if (thisObject.GetType() == typeof(Hashtable)) 
			{
			} 
			else if (thisObject.GetType() == typeof(MapIndexItem)) 
			{
				// Item was clicked
				MapIndexItem item = (MapIndexItem)thisObject;

				Properties propWindow = new Properties();
				propWindow.ShowMeta(item, halo2map);
				propWindow.Show();
			}
		}

		private void addressTB_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)0xd) {
				try {
					ShowFiles(addressTB.Text);
				} catch (Exception ex) {
					MessageBox.Show(ex.Message);
				}
				e.Handled = true;
			}
		}

		private void openMI_Click(object sender, System.EventArgs e)
		{
			ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
			object thisObject = clickedItem.Tag;
			
			if (thisObject is Hashtable) {
				// Folder was opened
				if (curPath == "") {
					ShowFiles(clickedItem.Text);
				} else { 
					ShowFiles(curPath + "\\" + clickedItem.Text);
				}
			} else if (thisObject is MapIndexItem) {
				// Item was clicked
				MapIndexItem item = (MapIndexItem)thisObject;

				EditMeta newEdit = new EditMeta(new EditedHandler(SetEdited), halo2map, item);
				if (item.Tag == "bitm") {
					H2Bitmap plugin = new H2Bitmap();
					plugin.Read(
						halo2map.stream, 
						(int)(item.MetaOffset - halo2map.SecondaryMagic), 
						(long)halo2map.SecondaryMagic, 
						item.Filename, 
						Global.SharedStream(MapTypes.Shared), 
						Global.SharedStream(MapTypes.MainMenu), 
						Global.SharedStream(MapTypes.SinglePlayerShared)
					);
					newEdit.PluginControl = plugin;
				}
				newEdit.ReadPlugin(item.Tag);
				newEdit.Show();
			}
		}

		public void SetEdited()
		{
		}

		private void findCB_CheckedChanged(object sender, System.EventArgs e)
		{
			findExpando.Visible = findCB.Checked;

			if (findExpando.Visible) {
				searchforTB.Focus();
			}
		}

		private void calculatorCB_CheckedChanged(object sender, System.EventArgs e)
		{
			calcExpando.Visible = calculatorCB.Checked;
		}

		#region Calculator Code
		void addButton_Click(object sender, System.EventArgs e)
		{
			try {resultTextBox.Text = hexCalc(true).ToString("X8");} 
			catch {}
		}

		void subtractButton_Click(object sender, System.EventArgs e)
		{
			try {resultTextBox.Text = hexCalc(false).ToString("X8");} 
			catch {}
		}

		private uint hexCalc(bool add)
		{
			uint value1 = 0;
			uint value2 = 0;

			try
			{
				// Convert hex strings to unsigned integers
				value1 = System.Convert.ToUInt32(valueOneTextBox.Text, 16);
				value2 = System.Convert.ToUInt32(valueTwoTextBox.Text, 16);
			}

			catch (System.OverflowException) 
			{
				// Show overflow message and return
				System.Windows.Forms.MessageBox.Show("Value entered is too high", "Illegal value:");
				return 0;
			}

			uint result = 0;

			if (add)
			{
				result = value1 + value2;
			} 
			else 
			{
				result = value1 - value2;
			}

			return result;
		}

		private void magicMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			source.Text = halo2map.Magic.ToString("X8");
		}

		private void secMagicMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			source.Text = halo2map.SecondaryMagic.ToString("X8");
		}

		private void checkSumMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			source.Text = halo2map.Signature.ToString("X8");
		}

		private void removeSpacesMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			source.Text = source.Text.Replace(" ", "");
		}

		private void addSpacesMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			string temp = "";
			for (int i = 0; i < source.Text.Length; i += 2)
			{
				if (i == source.Text.Length - 1)
				{ temp += source.Text.Substring(i, 1) + " "; }
				else { temp += source.Text.Substring(i, 2) + " "; }
			}
			source.Text = temp;
		}

		private void endianSwapMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			source.Text = ReverseEndian(Convert.ToInt32(source.Text, 16)).ToString("X8");
		}

		static int ReverseEndian(int x) 
		{
			return ((x<<24) | ((x & 0xff00)<<8) | ((x & 0xff0000)>>8) | (x>>24));
		}


		private void hToDecimalMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			source.Text = Convert.ToInt32(source.Text, 16).ToString();
		}

		private void hToBinaryMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			BitArray bits = new BitArray(new int[1] {Convert.ToInt32(source.Text, 16)} );
			
			source.Text = "";
			bool hitFirst = false;

			for (int i = bits.Length - 1; i >= 0; i--)
			{
				if (bits[i]) { source.Text += "1"; hitFirst = true; }
				else if (hitFirst) { source.Text += "0"; }
			}
		}

		private void dToHexMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			source.Text = Convert.ToInt32(source.Text, 10).ToString("X8");
		}

		private void dToBinary_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			BitArray bits = new BitArray(new int[1] {Convert.ToInt32(source.Text, 10)} );
			
			source.Text = "";
			bool hitFirst = false;

			for (int i = bits.Length - 1; i >= 0; i--)
			{
				if (bits[i]) { source.Text += "1"; hitFirst = true; }
				else if (hitFirst) { source.Text += "0"; }
			}
		}

		private void bToDecimal_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			BitArray bits = new BitArray(source.Text.Length);

			int number = 0;

			for (int i = 0; i < source.Text.Length; i++)
			{
				if (source.Text[i] == (char)0x30) { bits[i] = false; }
				else if (source.Text[i] == (char)0x31) { bits[i] = true; }
				else { MessageBox.Show("Invalid character"); return; }
			}

			for (int i = bits.Length - 1; i >= 0; i--)
			{
				if (bits[i]) { number |= 0x1 << (bits.Length - 1 - i); }

			}

			source.Text = number.ToString();
		}

		private void bToHexMenuItem_Click(object sender, System.EventArgs e)
		{
			TextBox source = (TextBox)((MenuItem)sender).GetContextMenu().SourceControl;
			BitArray bits = new BitArray(source.Text.Length);

			int number = 0;

			for (int i = 0; i < source.Text.Length; i++)
			{
				if (source.Text[i] == (char)0x30) { bits[i] = false; }
				else if (source.Text[i] == (char)0x31) { bits[i] = true; }
				else { MessageBox.Show("Invalid character"); return; }
			}

			for (int i = bits.Length - 1; i >= 0; i--)
			{
				if (bits[i]) { number |= 0x1 << (bits.Length - 1 - i); }

			}

			source.Text = number.ToString("X8");
		}
		#endregion

		#region Context Menu Code
		private void openfolderMI_Click(object sender, System.EventArgs e)
		{
			ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
			object tag = clickedItem.Tag;

			if (tag is MapIndexItem) {
				MapIndexItem item = (MapIndexItem)tag;
				string path = item.Filename;

				ShowFiles(path.Substring(0, path.LastIndexOf("\\")));
			}
		}

		private void itemContextMenu_Popup(object sender, System.EventArgs e)
		{
			ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
			object tag = clickedItem.Tag;

			copyIdentMenuItem.Visible = (tag is MapIndexItem);
			copyMetaOffsetMenuItem.Visible = (tag is MapIndexItem);
			copyMetaValueMenuItem.Visible = (tag is MapIndexItem);
			menuItem1.Visible = (tag is MapIndexItem);
			itemswapMI.Visible = (tag is MapIndexItem);
		}

		private void copyIdentMenuItem_Click(object sender, System.EventArgs e)
		{
			ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
			object tag = clickedItem.Tag;

			if ((tag is MapIndexItem) && (GetEmptyValueBox() != null))
			{
				GetEmptyValueBox().Text = ((MapIndexItem)tag).Ident.ToString("X8");
			}
		}

		private TextBox GetEmptyValueBox()
		{
			if (valueOneTextBox.Text == "") { return valueOneTextBox; }
			if (valueTwoTextBox.Text == "") { return valueTwoTextBox; }
			return null;
		}

		private void copyMetaOffsetMenuItem_Click(object sender, System.EventArgs e)
		{
			ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
			object tag = clickedItem.Tag;

			if ((tag is MapIndexItem) && (GetEmptyValueBox() != null))
			{
				GetEmptyValueBox().Text = (((MapIndexItem)tag).MetaOffset - halo2map.SecondaryMagic).ToString("X8");
			}
		}

		private void copyMetaValueMenuItem_Click(object sender, System.EventArgs e)
		{
			ListViewItem clickedItem = contentsXPListView.SelectedItems[0];
			object tag = clickedItem.Tag;

			if ((tag is MapIndexItem) && (GetEmptyValueBox() != null))
			{
				GetEmptyValueBox().Text = ((MapIndexItem)tag).MetaOffset.ToString("X8");
			}
		}
		#endregion

		#region Drap-And-Drop Support
		private void contentsXPListView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// TODO: Add support for adding new items
		
			// Check if they want to drop a file
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true) {

				// See if we arent dragging this file ourself
				if (!Global.DraggedFiles(e)[0].StartsWith(Path.GetTempPath())) {
					MapIndexItem item = FindItem(e);

					if (item.Filename != "") {
						e.Effect = DragDropEffects.Copy;
					}
				}
			}
		}

		private void contentsXPListView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			MapIndexItem item = FindItem(e);
		
			// Inject the meta
			string file = Global.DraggedFiles(e)[0].Replace(".meta.xml", ".meta");
			FileStream meta = new FileStream(file, FileMode.Open, FileAccess.Read); 
			item.InjectMeta(halo2map.stream, meta, halo2map.SecondaryMagic);
			item.FixReflexives(file + ".xml", halo2map.stream, halo2map.SecondaryMagic);
			meta.Close();

			// Meta injected
			MessageBox.Show("Meta has been overwritten");
		}

		private void contentsXPListView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dropdown = true;
		}

		private void contentsXPListView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!dropdown) return;
			if (e.Button == MouseButtons.Right) return;

			// Extract meta
			if (contentsXPListView.SelectedItems.Count > 0) {
				ArrayList temp = new ArrayList();

				for (int i = 0; i < contentsXPListView.SelectedItems.Count; i++) {
					object tag = contentsXPListView.SelectedItems[i].Tag;

					if (tag is MapIndexItem) {
						MapIndexItem item = (MapIndexItem)tag;
						string path = item.Filename;
						path = Path.GetTempPath() + path.Substring(path.LastIndexOf("\\") + 1) + "." + item.Tag + ".meta";

						if (!item.SaveMeta(halo2map.stream, path, halo2map.SecondaryMagic, halo2map.Index.Item(0).MetaOffset)) {
							MessageBox.Show("Failed extracting meta.");
							return;
						}

						temp.Add(path);
						temp.Add(path + ".xml");
					}
				}

				if (temp.Count > 0) {
					string[] files = new string[temp.Count];
					for (int x = 0; x < temp.Count; x++) {
						files[x] = (string)temp[x];
					}
					DataObject dt = new DataObject(DataFormats.FileDrop, files);
					DoDragDrop(dt, DragDropEffects.Copy);
				}
			}
		}

		private void contentsXPListView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dropdown = false;
		}

		private MapIndexItem FindItem(System.Windows.Forms.DragEventArgs e)
		{
			// Check if the file already exists otherwise we cant continue
			string file = Global.DraggedFiles(e)[0];
			file = file.Substring(file.LastIndexOf("\\") + 1);

			if (file.EndsWith(".meta") || file.EndsWith(".meta.xml")) {
				file = file.Replace(".meta.xml", "").Replace(".meta", "");

				foreach (ListViewItem item in contentsXPListView.Items) {
					if (item.Text.Substring(item.Text.LastIndexOf("\\") + 1) == file) {
						return (MapIndexItem)item.Tag;
					}
				}
			}

			MapIndexItem notfound = new MapIndexItem();
			notfound.Filename = "";

			return notfound;
		}
		#endregion

		private void contentsXPListView_Resize(object sender, System.EventArgs e)
		{
			if (lastPath != null) {
				contentsXPListView.Columns[0].Width = -2;
			}
		}

		private void extractMapTaskItem_Click(object sender, System.EventArgs e)
		{
			if (extractFolderBrowserDialog.ShowDialog() == DialogResult.Cancel) { return; }
			string targetDir = extractFolderBrowserDialog.SelectedPath;

			ParseDirectory(root, targetDir);
		}

		private void ParseDirectory(Hashtable dir, string curPhysDir)
		{
			Application.DoEvents();
			string filename;
			foreach (string element in dir.Keys)
			{
				if (dir[element] is Hashtable)
				{
					if (!Directory.Exists(curPhysDir + "\\" + element))
					{
						Directory.CreateDirectory(curPhysDir + "\\" + element);
					}
					ParseDirectory((Hashtable)dir[element], curPhysDir + "\\" + element);
				}
				else if (dir[element] is MapIndexItem)
				{
					MapIndexItem item = (MapIndexItem)dir[element];
					filename = item.Filename.Substring(item.Filename.LastIndexOf("\\") + 1) + "." + item.Tag.Replace("<", "_").Replace(">", "_") + ".meta";

					if (!item.SaveMeta(halo2map.stream, curPhysDir + "\\" + filename, halo2map.SecondaryMagic, halo2map.Index.Item(0).MetaOffset)) 
					{
						MessageBox.Show("Failed saving meta\n" + curPhysDir + "\\" + filename);
					}
				}
			}
		}

		private void contentsXPListView_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode) {
				case Keys.Back:
					if (lastPath == null) {
						parentTaskItem_Click(null, null);
					} else {
						ShowFiles(lastPath);
					}
					e.Handled = true;
					break;
				case Keys.Enter:
					if (e.Alt) {
						propertiesMenuItem_Click(null, null);
					} else {
						contentsXPListView_ItemClicked(sender, null);
					}
					e.Handled = true;
					break;
			}
		}

		private void bitmPB_DoubleClick(object sender, System.EventArgs e)
		{
			// Get access to current selected item
			MapIndexItem item = (MapIndexItem)contentsXPListView.SelectedItems[0].Tag;

			// Get details about item
			int idx = bitmTB.Value;

			// Build suggested filename
			string path = item.Filename;
			path = path.Substring(path.LastIndexOf("\\") + 1);
			path += "." + idx + "." + bitmSFD.DefaultExt;

			bitmSFD.FileName = path;

			// Ask user where to save to
			if (bitmSFD.ShowDialog() == DialogResult.OK) {
				// Save image
				bitmPB.Image.Save(bitmSFD.FileName);

				MessageBox.Show("Bitmap saved to " + bitmSFD.FileName);
			}
		}

		private void soundOptionsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			soundOptionsExpando.Visible = soundOptionsCheckBox.Checked;
		}

		private void optionComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SoundPiece thisOption = (SoundPiece)optionComboBox.SelectedItem;
			string details;
			long totalSize = 0;

			details = "Pieces: " + thisOption.pieces;
			details += "\nStarting Index: " + thisOption.index;

			for (int i = 0; i < thisOption.pieces; i++) {
				totalSize += halo2map.sounds.soundPointers[thisOption.index + i].size;
			}

			details += "\nTotal Size: " + Global.ConvertSize(totalSize);

			soundOptionDetailsLabel.Text = details;
		}

		private void extractSoundButton_Click(object sender, System.EventArgs e)
		{
			SoundPiece thisOption = (SoundPiece)optionComboBox.SelectedItem;
			
			string filename = ((MapIndexItem)contentsXPListView.SelectedItems[0].Tag).Filename;
			filename = filename.Substring(filename.LastIndexOf("\\") + 1) + "." + thisOption.listIndex;
			if (thisOption.format == 2)
				filename += ".wma";
			else
				filename += ".wav";

			sndSFD.FileName = filename;
			if (sndSFD.ShowDialog() != DialogResult.OK)
				return;
			filename = sndSFD.FileName;

			SaveSound(thisOption, filename);
		}

		private void SaveSound(SoundPiece thisOption, string filename)
		{
			FileStream fs;
			BinaryWriter bw;
			SoundPointer sp;
			byte[] temp;
			uint curPos = 0;
			uint totalSize = 0;

			if (filename.IndexOf("\\") == -1)
				filename = Application.StartupPath + "\\" + filename;

			for (int i = 0; i < thisOption.pieces; i++)
			{
				totalSize += halo2map.sounds.soundPointers[thisOption.index + i].size;
			}

			try 
			{
				switch (thisOption.format)
				{
					case 0:
					case 1:
						if (!filename.EndsWith(".wav"))
							filename += ".wav";
						fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
						bw = new BinaryWriter(fs);

						temp = new byte[totalSize];
						for (int i = 0; i < thisOption.pieces; i++)
						{
							sp = halo2map.sounds.soundPointers[thisOption.index + i];
							GetData(sp.GetLocation(), sp.GetActualOffset(), sp.size).CopyTo(temp, curPos);
							curPos += sp.size;
						}

						WriteADPCM(bw, thisOption.format, temp);
						fs.Close();
						break;
					case 2:
						if (!filename.EndsWith(".wma"))
							filename += ".wma";
						fs = new FileStream(filename + ".wma", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
						bw = new BinaryWriter(fs);

						for (int i = 0; i < thisOption.pieces; i++)
						{
							sp = halo2map.sounds.soundPointers[thisOption.index + i];
							bw.Write(GetData(sp.GetLocation(), sp.GetActualOffset(), sp.size));
						}
						fs.Close();
						break;
				}
			} 
			catch (System.IO.IOException ex)
			{
				MessageBox.Show("Please wait for the previous sound to finish playing/extracting.\n\n" + ex.Message);
			}
		}

		private byte[] GetData(uint location, uint offset, uint length)
		{
			BinaryReader br;
			switch (location)
			{
				case 0:
					br = new BinaryReader(halo2map.stream);
					break;
				case 1:
					br = new BinaryReader(Global.SharedStream(MapTypes.MainMenu));
					break;
				case 2:
					br = new BinaryReader(Global.SharedStream(MapTypes.Shared));
					break;
				case 3:
					br = new BinaryReader(Global.SharedStream(MapTypes.SinglePlayerShared));
					break;
				default:
					return null;
			}

			br.BaseStream.Seek(offset, SeekOrigin.Begin);
			return br.ReadBytes((int)length);
		}

		public void WriteADPCM(BinaryWriter bw, uint format, byte[] data)
		{
			WAVE_Header waveHeader = new WAVE_Header();
			fmt_Chunk fmtChunk = new fmt_Chunk();
			data_Chunk dataChunk = new data_Chunk();

			// Sizes
			waveHeader.length = (uint)data.Length + 44;
			dataChunk.length = (uint)data.Length;

			// Sample Type
			switch (format)
			{
				case 0:
					fmtChunk.numberOfChannels = 1;
					fmtChunk.sampleRate = 0x5622;
					break;
				case 1:
					fmtChunk.numberOfChannels = 2;
					fmtChunk.sampleRate = 0xAC44;
					break;
			}

			fmtChunk.avgBytesPerSec = fmtChunk.numberOfChannels * fmtChunk.sampleRate * 4 / 8;
			fmtChunk.blockAlign = (ushort)(36 * fmtChunk.numberOfChannels);

			#region Lots of write statements
			bw.Write(waveHeader.RIFF.ToCharArray());
			bw.Write(waveHeader.length);
			bw.Write(waveHeader.WAVE.ToCharArray());
			bw.Write(fmtChunk.fmt.ToCharArray());
			bw.Write(fmtChunk.length);
			bw.Write(fmtChunk.compressionCode);
			bw.Write(fmtChunk.numberOfChannels);
			bw.Write(fmtChunk.sampleRate);
			bw.Write(fmtChunk.avgBytesPerSec);
			bw.Write(fmtChunk.blockAlign);
			bw.Write(fmtChunk.sigBitsPerSample);
			bw.Write(fmtChunk.extraFormatByteCount);
			bw.Write(fmtChunk.extraFormatData);
			bw.Write(dataChunk.data.ToCharArray());
			bw.Write(dataChunk.length);
			bw.Write(data);
			#endregion
		}

		Audio clip = null;

		private void playButton_Click(object sender, System.EventArgs e)
		{
			if (optionComboBox.SelectedItem == null) { return; }
			SoundPiece thisOption = (SoundPiece)optionComboBox.SelectedItem;

			if (clip != null) {
				clip.Stop();
				clip.Dispose();
			}

			try {
				SaveSound(thisOption, "temp");
				
				switch (thisOption.format)
				{
					case 0:
					case 1:
						clip = new Audio(Application.StartupPath + "\\temp.wav", true);
						break;
					case 2:
						clip = new Audio(Application.StartupPath + "\\temp.wma", true);
						break;
				}
			} catch (Exception ex) {
				MessageBox.Show("Error in playing sound.\n\n" + ex.Message);
			}
		}

		public class WAVE_Header
		{
			public string RIFF = "RIFF";
			public uint length;
			public string WAVE = "WAVE";
		}

		public class fmt_Chunk
		{
			public string fmt = "fmt ";
			public uint length = 0x14;
			public ushort compressionCode = 0x69;
			public ushort numberOfChannels;
			public uint sampleRate;
			public uint avgBytesPerSec;
			public ushort blockAlign;
			public ushort sigBitsPerSample = 0x4;
			public ushort extraFormatByteCount = 0x2;
			public ushort extraFormatData = 0x40;
		}

		public class data_Chunk
		{
			public string data = "data";
			public uint length;
		}

		/// <summary>
		/// View the Find expando.
		/// </summary>
		public void Find()
		{
			findCB.Checked = true;
			findExpando.Visible = true;
			searchforTB.Focus();
		}

		private void foldersLV_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			ShowFiles((string)foldersTV.SelectedNode.Tag);
		}

		/// <summary>
		/// Try to add all icons to the toolbar.
		/// </summary>
		public void MakeToolbar()
		{
			try {
				mainTB.Buttons.Clear();

				AddToolbarButton("Up", 0);
				AddToolbarSeperator();
				AddToolbarButton("Find", 1);

				mainTB.Visible = true;
			} catch {}
		}

		/// <summary>
		/// Add a button to our toolbar.
		/// </summary>
		/// <param name="name">Name to identify button.</param>
		/// <param name="imageindex">Index of image.</param>
		private void AddToolbarButton(string name, int imageindex)
		{
			ToolBarControl.ToolBarButton button = new ToolBarControl.ToolBarButton();

			button.Name = name;
			button.Image = tbiconsIL.Images[imageindex];

			mainTB.Buttons.Add(button);
		}

		private void AddToolbarSeperator()
		{
			ToolBarControl.ToolBarButton button = new ToolBarControl.ToolBarButton();

			button.ButtonType = ButtonTypes.Seperator;

			mainTB.Buttons.Add(button);
		}

		/// <summary>
		/// User clicked on a button on the toolbar.
		/// </summary>
		private void Buttons_ButtonClick(object sender, EventArgs e)
		{
			ToolBarControl.ToolBarButton button = (ToolBarControl.ToolBarButton)sender;

			switch (button.Name) {
				case "Up":
					GoUp();
					break;
				case "Find":
					Find();
					break;
			}
		}

		private void filterTB_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)0xd) {
				ShowFiles(curPath);
				e.Handled = true;
			}
		}

		/// <summary>
		/// Changes the user requested filter to a regex match.
		/// </summary>
		string Filter(string text)
		{
			string filter = "^" + text;
			filter = filter.Replace(".", "\\.").Replace("?", ".").Replace("*", ".*");
			if (!filter.EndsWith("*"))
				filter += "$";
			return filter;
		}

		bool MatchFilter(string path, string filter)
		{
			Regex regex = new Regex(Filter(filter), RegexOptions.IgnoreCase);
			return MatchFilter(path, filter, regex);
		}

		private void itemswapMI_Click(object sender, System.EventArgs e)
		{
			MapIndexItem item = (MapIndexItem)contentsXPListView.SelectedItems[0].Tag;
			SwapForm swapForm = new SwapForm(halo2map);
			swapForm.Type = SwapType.Item;
			swapForm.mapform = this;
			swapForm._original = (MapIndexItem)contentsXPListView.SelectedItems[0].Tag;

			swapForm.Show();

			swapForm.LoadTags(item.Tag, item.Filename);
		}

		private void findMI_Click(object sender, System.EventArgs e)
		{
			Find();
		}

		bool MatchFilter(string path, string filter, Regex regex)
		{
			string file = path.Substring(path.LastIndexOf("\\") + 1);

			return path.IndexOf(filter) > -1 || regex.IsMatch(path) || file.IndexOf(filter) > -1 || regex.IsMatch(file);
		}

		public void RefreshCurrentItem()
		{
			ListViewItem lvitem = contentsXPListView.SelectedItems[0];
			MapIndexItem item = halo2map.Index.ItemByIdent(((MapIndexItem)lvitem.Tag).Ident);
			lvitem.Tag = item;

			contentsXPListView_SelectedIndexChanged(null, null);
		}
	}
}
