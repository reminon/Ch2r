using Halo2;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.Win32;

namespace Ch2r
{
	public class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Remember window state.
		/// </summary>
		private PersistWindowState pws;

		#region Controls
		public XPExplorerBar.XPListView mapsXPListView;
		private XPExplorerBar.TaskPane mapsTaskPane;
		private XPExplorerBar.Expando mapExpando;
		private XPExplorerBar.TaskItem openTaskItem;
		private System.Windows.Forms.ImageList bigIconsImageList;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private XPExplorerBar.TaskItem exploreMapTaskItem;
		private XPExplorerBar.TaskItem signMapTaskItem;
		private XPExplorerBar.TaskItem closeMapTaskItem;
		private XPExplorerBar.Expando actionsExpando;
		private XPExplorerBar.Expando propertiesExpando;
		private XPExplorerBar.TaskItem primaryMagicTaskItem;
		private XPExplorerBar.TaskItem secondaryMagicTaskItem;
		private XPExplorerBar.TaskItem checksumTaskItem;
		private XPExplorerBar.TaskItem mapTypeTaskItem;
		private XPExplorerBar.Expando settingsExpando;
		private XPExplorerBar.TaskItem configurationTaskItem;
		private XPExplorerBar.TaskItem aboutExpando;
		private System.Windows.Forms.ComboBox recentComboBox;
		private System.Windows.Forms.ImageList smalliconsIL;
		private System.Windows.Forms.MenuItem fileMenu;
		private System.Windows.Forms.MenuItem openMI;
		private System.Windows.Forms.MenuItem helpMenu;
		private System.Windows.Forms.MenuItem aboutMI;
		private System.Windows.Forms.MenuItem toolsMenu;
		private System.Windows.Forms.MenuItem configMI;
		private System.Windows.Forms.MenuItem exitMI;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem closeMI;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.ContextMenu mapsCM;
		private System.Windows.Forms.MenuItem mapexplorerMI;
		private System.Windows.Forms.MenuItem mapresignMI;
		private System.Windows.Forms.MenuItem mapcloseMI;
		private System.ComponentModel.IContainer components;
		#endregion

		public MainForm()
		{
			try {
				//
				// Required for Windows Form Designer support
				//
				InitializeComponent();
			} catch (Exception exp) {
				MessageBox.Show("Error initializing components.\n\n" + exp.Message);
			}

			Text = "Ch2r " + Global.Version;

			Global.Skin.SkinControl(this);

			Global.ResizeTaskItems(ref mapsTaskPane);

			pws = new PersistWindowState();
			pws.Parent = this;
			pws.RegistryPath = @"Software\Ch2r\Position\MainForm";
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
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.mapsTaskPane = new XPExplorerBar.TaskPane();
			this.mapExpando = new XPExplorerBar.Expando();
			this.openTaskItem = new XPExplorerBar.TaskItem();
			this.recentComboBox = new System.Windows.Forms.ComboBox();
			this.actionsExpando = new XPExplorerBar.Expando();
			this.exploreMapTaskItem = new XPExplorerBar.TaskItem();
			this.smalliconsIL = new System.Windows.Forms.ImageList(this.components);
			this.signMapTaskItem = new XPExplorerBar.TaskItem();
			this.closeMapTaskItem = new XPExplorerBar.TaskItem();
			this.propertiesExpando = new XPExplorerBar.Expando();
			this.primaryMagicTaskItem = new XPExplorerBar.TaskItem();
			this.secondaryMagicTaskItem = new XPExplorerBar.TaskItem();
			this.checksumTaskItem = new XPExplorerBar.TaskItem();
			this.mapTypeTaskItem = new XPExplorerBar.TaskItem();
			this.settingsExpando = new XPExplorerBar.Expando();
			this.configurationTaskItem = new XPExplorerBar.TaskItem();
			this.aboutExpando = new XPExplorerBar.TaskItem();
			this.mapsXPListView = new XPExplorerBar.XPListView();
			this.bigIconsImageList = new System.Windows.Forms.ImageList(this.components);
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.fileMenu = new System.Windows.Forms.MenuItem();
			this.openMI = new System.Windows.Forms.MenuItem();
			this.helpMenu = new System.Windows.Forms.MenuItem();
			this.aboutMI = new System.Windows.Forms.MenuItem();
			this.toolsMenu = new System.Windows.Forms.MenuItem();
			this.configMI = new System.Windows.Forms.MenuItem();
			this.exitMI = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.closeMI = new System.Windows.Forms.MenuItem();
			this.mapsCM = new System.Windows.Forms.ContextMenu();
			this.mapexplorerMI = new System.Windows.Forms.MenuItem();
			this.mapresignMI = new System.Windows.Forms.MenuItem();
			this.mapcloseMI = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.mapsTaskPane)).BeginInit();
			this.mapsTaskPane.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mapExpando)).BeginInit();
			this.mapExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.actionsExpando)).BeginInit();
			this.actionsExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.propertiesExpando)).BeginInit();
			this.propertiesExpando.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.settingsExpando)).BeginInit();
			this.settingsExpando.SuspendLayout();
			this.SuspendLayout();
			// 
			// mapsTaskPane
			// 
			this.mapsTaskPane.AutoScrollMargin = new System.Drawing.Size(12, 12);
			this.mapsTaskPane.CustomSettings.GradientEndColor = System.Drawing.Color.DarkRed;
			this.mapsTaskPane.CustomSettings.GradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.mapsTaskPane.Dock = System.Windows.Forms.DockStyle.Left;
			this.mapsTaskPane.Expandos.AddRange(new XPExplorerBar.Expando[] {
																																				this.mapExpando,
																																				this.actionsExpando,
																																				this.propertiesExpando,
																																				this.settingsExpando});
			this.mapsTaskPane.Location = new System.Drawing.Point(0, 0);
			this.mapsTaskPane.Name = "mapsTaskPane";
			this.mapsTaskPane.Size = new System.Drawing.Size(184, 421);
			this.mapsTaskPane.TabIndex = 0;
			this.mapsTaskPane.Text = "taskPane";
			// 
			// mapExpando
			// 
			this.mapExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mapExpando.Animate = true;
			this.mapExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.mapExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.mapExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.mapExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.mapExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.mapExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.mapExpando.CustomHeaderSettings.TitleGradient = true;
			this.mapExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.mapExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.mapExpando.ExpandedHeight = 80;
			this.mapExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.mapExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																					this.openTaskItem,
																																					this.recentComboBox});
			this.mapExpando.Location = new System.Drawing.Point(12, 12);
			this.mapExpando.Name = "mapExpando";
			this.mapExpando.TabIndex = 0;
			this.mapExpando.Text = "Maps";
			// 
			// openTaskItem
			// 
			this.openTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.openTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.openTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.openTaskItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.openTaskItem.Image = null;
			this.openTaskItem.Location = new System.Drawing.Point(8, 32);
			this.openTaskItem.Name = "openTaskItem";
			this.openTaskItem.Size = new System.Drawing.Size(144, 16);
			this.openTaskItem.TabIndex = 0;
			this.openTaskItem.Text = "Open Map";
			this.openTaskItem.Click += new System.EventHandler(this.openTaskItem_Click);
			// 
			// recentComboBox
			// 
			this.recentComboBox.Location = new System.Drawing.Point(8, 48);
			this.recentComboBox.Name = "recentComboBox";
			this.recentComboBox.Size = new System.Drawing.Size(144, 21);
			this.recentComboBox.TabIndex = 1;
			this.recentComboBox.Text = "Recent";
			this.recentComboBox.SelectedIndexChanged += new System.EventHandler(this.recentComboBox_SelectedIndexChanged);
			// 
			// actionsExpando
			// 
			this.actionsExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.actionsExpando.Animate = true;
			this.actionsExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.actionsExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.actionsExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.actionsExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.actionsExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.actionsExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.actionsExpando.CustomHeaderSettings.TitleGradient = true;
			this.actionsExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.actionsExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.actionsExpando.ExpandedHeight = 87;
			this.actionsExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.actionsExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																							this.exploreMapTaskItem,
																																							this.signMapTaskItem,
																																							this.closeMapTaskItem});
			this.actionsExpando.Location = new System.Drawing.Point(12, 104);
			this.actionsExpando.Name = "actionsExpando";
			this.actionsExpando.TabIndex = 1;
			this.actionsExpando.Text = "Actions";
			this.actionsExpando.Visible = false;
			// 
			// exploreMapTaskItem
			// 
			this.exploreMapTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.exploreMapTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.exploreMapTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.exploreMapTaskItem.Image = ((System.Drawing.Image)(resources.GetObject("exploreMapTaskItem.Image")));
			this.exploreMapTaskItem.ImageIndex = 3;
			this.exploreMapTaskItem.ImageList = this.smalliconsIL;
			this.exploreMapTaskItem.Location = new System.Drawing.Point(8, 32);
			this.exploreMapTaskItem.Name = "exploreMapTaskItem";
			this.exploreMapTaskItem.Size = new System.Drawing.Size(144, 16);
			this.exploreMapTaskItem.TabIndex = 1;
			this.exploreMapTaskItem.Text = "Explore Map";
			this.exploreMapTaskItem.Click += new System.EventHandler(this.exploreMapTaskItem_Click);
			// 
			// smalliconsIL
			// 
			this.smalliconsIL.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.smalliconsIL.ImageSize = new System.Drawing.Size(16, 16);
			this.smalliconsIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smalliconsIL.ImageStream")));
			this.smalliconsIL.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// signMapTaskItem
			// 
			this.signMapTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.signMapTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.signMapTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.signMapTaskItem.Image = ((System.Drawing.Image)(resources.GetObject("signMapTaskItem.Image")));
			this.signMapTaskItem.ImageIndex = 4;
			this.signMapTaskItem.ImageList = this.smalliconsIL;
			this.signMapTaskItem.Location = new System.Drawing.Point(8, 48);
			this.signMapTaskItem.Name = "signMapTaskItem";
			this.signMapTaskItem.Size = new System.Drawing.Size(144, 16);
			this.signMapTaskItem.TabIndex = 2;
			this.signMapTaskItem.Text = "Re-Sign Map";
			this.signMapTaskItem.Click += new System.EventHandler(this.signMapTaskItem_Click);
			// 
			// closeMapTaskItem
			// 
			this.closeMapTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.closeMapTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.closeMapTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.closeMapTaskItem.Image = ((System.Drawing.Image)(resources.GetObject("closeMapTaskItem.Image")));
			this.closeMapTaskItem.ImageIndex = 2;
			this.closeMapTaskItem.ImageList = this.smalliconsIL;
			this.closeMapTaskItem.Location = new System.Drawing.Point(8, 64);
			this.closeMapTaskItem.Name = "closeMapTaskItem";
			this.closeMapTaskItem.Size = new System.Drawing.Size(144, 16);
			this.closeMapTaskItem.TabIndex = 3;
			this.closeMapTaskItem.Text = "Close Map";
			this.closeMapTaskItem.Click += new System.EventHandler(this.closeMapTaskItem_Click);
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
			this.propertiesExpando.ExpandedHeight = 120;
			this.propertiesExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.propertiesExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																								 this.primaryMagicTaskItem,
																																								 this.secondaryMagicTaskItem,
																																								 this.checksumTaskItem,
																																								 this.mapTypeTaskItem});
			this.propertiesExpando.Location = new System.Drawing.Point(12, 203);
			this.propertiesExpando.Name = "propertiesExpando";
			this.propertiesExpando.TabIndex = 2;
			this.propertiesExpando.Text = "Properties";
			this.propertiesExpando.Visible = false;
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
			this.primaryMagicTaskItem.Text = "Magic 1:           ";
			this.primaryMagicTaskItem.Click += new System.EventHandler(this.primaryMagicTaskItem_Click);
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
			this.secondaryMagicTaskItem.Click += new System.EventHandler(this.secondaryMagicTaskItem_Click);
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
			this.checksumTaskItem.Click += new System.EventHandler(this.checksumTaskItem_Click);
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
			this.mapTypeTaskItem.Click += new System.EventHandler(this.mapTypeTaskItem_Click);
			// 
			// settingsExpando
			// 
			this.settingsExpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.settingsExpando.Animate = true;
			this.settingsExpando.Collapsed = true;
			this.settingsExpando.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.settingsExpando.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.DarkRed;
			this.settingsExpando.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.settingsExpando.CustomHeaderSettings.NormalTitleColor = System.Drawing.Color.White;
			this.settingsExpando.CustomHeaderSettings.NormalTitleHotColor = System.Drawing.Color.Black;
			this.settingsExpando.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.RosyBrown;
			this.settingsExpando.CustomHeaderSettings.TitleGradient = true;
			this.settingsExpando.CustomSettings.NormalBackColor = System.Drawing.Color.RosyBrown;
			this.settingsExpando.CustomSettings.NormalBorderColor = System.Drawing.Color.Black;
			this.settingsExpando.ExpandedHeight = 69;
			this.settingsExpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.settingsExpando.Items.AddRange(new System.Windows.Forms.Control[] {
																																							 this.configurationTaskItem,
																																							 this.aboutExpando});
			this.settingsExpando.Location = new System.Drawing.Point(12, 335);
			this.settingsExpando.Name = "settingsExpando";
			this.settingsExpando.TabIndex = 3;
			this.settingsExpando.Text = "Settings";
			// 
			// configurationTaskItem
			// 
			this.configurationTaskItem.BackColor = System.Drawing.Color.Transparent;
			this.configurationTaskItem.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.configurationTaskItem.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.configurationTaskItem.Image = ((System.Drawing.Image)(resources.GetObject("configurationTaskItem.Image")));
			this.configurationTaskItem.ImageIndex = 0;
			this.configurationTaskItem.ImageList = this.smalliconsIL;
			this.configurationTaskItem.Location = new System.Drawing.Point(8, 32);
			this.configurationTaskItem.Name = "configurationTaskItem";
			this.configurationTaskItem.Size = new System.Drawing.Size(144, 16);
			this.configurationTaskItem.TabIndex = 0;
			this.configurationTaskItem.Text = "Configuration";
			this.configurationTaskItem.Click += new System.EventHandler(this.configurationTaskItem_Click);
			// 
			// aboutExpando
			// 
			this.aboutExpando.BackColor = System.Drawing.Color.Transparent;
			this.aboutExpando.CustomSettings.HotLinkColor = System.Drawing.Color.Black;
			this.aboutExpando.CustomSettings.LinkColor = System.Drawing.Color.Black;
			this.aboutExpando.Image = ((System.Drawing.Image)(resources.GetObject("aboutExpando.Image")));
			this.aboutExpando.ImageIndex = 1;
			this.aboutExpando.ImageList = this.smalliconsIL;
			this.aboutExpando.Location = new System.Drawing.Point(8, 48);
			this.aboutExpando.Name = "aboutExpando";
			this.aboutExpando.Size = new System.Drawing.Size(144, 16);
			this.aboutExpando.TabIndex = 1;
			this.aboutExpando.Text = "About";
			// 
			// mapsXPListView
			// 
			this.mapsXPListView.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.mapsXPListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mapsXPListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapsXPListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mapsXPListView.ForeColor = System.Drawing.Color.White;
			this.mapsXPListView.LargeImageList = this.bigIconsImageList;
			this.mapsXPListView.Location = new System.Drawing.Point(184, 0);
			this.mapsXPListView.Name = "mapsXPListView";
			this.mapsXPListView.Size = new System.Drawing.Size(442, 421);
			this.mapsXPListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.mapsXPListView.TabIndex = 1;
			this.mapsXPListView.DoubleClick += new System.EventHandler(this.mapsXPListView_ItemClicked);
			this.mapsXPListView.SelectedIndexChanged += new System.EventHandler(this.mapsXPListView_SelectedIndexChanged);
			// 
			// bigIconsImageList
			// 
			this.bigIconsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.bigIconsImageList.ImageSize = new System.Drawing.Size(50, 50);
			this.bigIconsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("bigIconsImageList.ImageStream")));
			this.bigIconsImageList.TransparentColor = System.Drawing.Color.White;
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "map";
			this.openFileDialog.Filter = "Halo 2 Map|*.map";
			this.openFileDialog.Title = "Open map...";
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																						 this.fileMenu,
																																						 this.toolsMenu,
																																						 this.helpMenu});
			// 
			// fileMenu
			// 
			this.fileMenu.Index = 0;
			this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																						 this.openMI,
																																						 this.closeMI,
																																						 this.menuItem6,
																																						 this.exitMI});
			this.fileMenu.Text = "&File";
			// 
			// openMI
			// 
			this.openMI.Index = 0;
			this.openMI.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.openMI.Text = "&Open";
			this.openMI.Click += new System.EventHandler(this.openTaskItem_Click);
			// 
			// helpMenu
			// 
			this.helpMenu.Index = 2;
			this.helpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																						 this.aboutMI});
			this.helpMenu.Text = "&Help";
			// 
			// aboutMI
			// 
			this.aboutMI.Index = 0;
			this.aboutMI.Text = "&About Ch2r...";
			this.aboutMI.Click += new System.EventHandler(this.aboutMI_Click);
			// 
			// toolsMenu
			// 
			this.toolsMenu.Index = 1;
			this.toolsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.configMI});
			this.toolsMenu.Text = "&Tools";
			// 
			// configMI
			// 
			this.configMI.Index = 0;
			this.configMI.Text = "&Configuration";
			this.configMI.Click += new System.EventHandler(this.configurationTaskItem_Click);
			// 
			// exitMI
			// 
			this.exitMI.Index = 3;
			this.exitMI.Text = "E&xit";
			this.exitMI.Click += new System.EventHandler(this.exitMI_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.Text = "-";
			// 
			// closeMI
			// 
			this.closeMI.Enabled = false;
			this.closeMI.Index = 1;
			this.closeMI.Text = "&Close";
			this.closeMI.Click += new System.EventHandler(this.closeMapTaskItem_Click);
			// 
			// mapsCM
			// 
			this.mapsCM.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																					 this.mapexplorerMI,
																																					 this.mapresignMI,
																																					 this.mapcloseMI});
			// 
			// mapexplorerMI
			// 
			this.mapexplorerMI.Index = 0;
			this.mapexplorerMI.Text = "&Explorer";
			this.mapexplorerMI.Click += new System.EventHandler(this.exploreMapTaskItem_Click);
			// 
			// mapresignMI
			// 
			this.mapresignMI.Index = 1;
			this.mapresignMI.Text = "&Re-Sign";
			this.mapresignMI.Click += new System.EventHandler(this.signMapTaskItem_Click);
			// 
			// mapcloseMI
			// 
			this.mapcloseMI.Index = 2;
			this.mapcloseMI.Text = "&Close";
			this.mapcloseMI.Click += new System.EventHandler(this.closeMapTaskItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(626, 421);
			this.Controls.Add(this.mapsXPListView);
			this.Controls.Add(this.mapsTaskPane);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "Ch2r";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Closed += new System.EventHandler(this.MainForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.mapsTaskPane)).EndInit();
			this.mapsTaskPane.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mapExpando)).EndInit();
			this.mapExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.actionsExpando)).EndInit();
			this.actionsExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.propertiesExpando)).EndInit();
			this.propertiesExpando.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.settingsExpando)).EndInit();
			this.settingsExpando.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		const int SIGN_START = 0x800;
		const int SIG_POS = 0x2d0;
		const int INIT_SUM = 0x00000000;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			// Enable Windows XP Styles
			Application.EnableVisualStyles();
			Application.DoEvents();

			// Start program
			MainForm form = new MainForm();
			Global.mainForm = form;
			Application.Run(form);
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			// Check for updates
			if (Global.Update.UpdateFound) {
				DialogResult result = MessageBox.Show("Newer version found on server\nYou want to upgrade?", "Update", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes) {
					if (Global.Update.RunUpdate())
						return;
				}
			}

			try {
				// Read recent files
				RefreshRecentList();

				// Open last opened maps
				LoadLastFiles();
			} catch (Exception exp) {
				MessageBox.Show("Error loading recent files.\n\n" + exp.Message);
			}

			// Check if a file was opened
			string[] commands = Environment.GetCommandLineArgs();

			if (commands.Length == 2) {
				string command = commands[1];
				if (command.EndsWith(".map")) {
					OpenMap(command, false);
				} else if (command.EndsWith(".ch2r")) {
					// Copy plugins
					try {
						string dest = Application.StartupPath + "\\Plugins\\" + command.Substring(command.LastIndexOf("\\") + 1);
						if (command != dest) {
							File.Copy(command, dest);
						}
					} catch {
						MessageBox.Show("Installing plugin failed.");
					}
				}
			}
		}

		private void MainForm_Closed(object sender, System.EventArgs e)
		{
			// Remember opened maps
			SaveLastFiles();
		}

		#region Recent File Code
		/// <summary>
		/// Read recent files from Registry.
		/// </summary>
		private void RefreshRecentList()
		{
			// Clear items
			recentComboBox.Items.Clear();

			// Check registry key
			RegistryKey ourKey = Registry.CurrentUser.OpenSubKey("Software\\Ch2r\\Recent Files");
			if (ourKey == null) {
				ourKey = Registry.CurrentUser.CreateSubKey("Software\\Ch2r\\Recent Files");
			}

			if (ourKey.ValueCount == 0) {
				// Disable it
				recentComboBox.Enabled = false;
				return;
			} else {
				recentComboBox.Enabled = true;
			}

			// Add recent files to combo box
			foreach(string recentFile in ourKey.GetValueNames())
			{
				RecentFileStruct newRecent = new RecentFileStruct();
				newRecent.filename = (string)ourKey.GetValue(recentFile);
				newRecent.mapName = recentFile;
				recentComboBox.Items.Add(newRecent);
			}
		}

		private void LoadLastFiles()
		{
			RegistryKey ourKey = Registry.CurrentUser.OpenSubKey("Software\\Ch2r\\Last Files");
			
			if (ourKey == null) {
				ourKey = Registry.CurrentUser.CreateSubKey("Software\\Ch2r\\Last Files");
			}

			foreach (string lastFile in ourKey.GetValueNames()) {
				try {
					OpenMap((string)ourKey.GetValue(lastFile), true);
				} catch {}
			}
		}

		private void SaveLastFiles()
		{
			RegistryKey ourKey = Registry.CurrentUser.OpenSubKey("Software\\Ch2r\\Last Files", true);
			
			if (ourKey == null) {
				ourKey = Registry.CurrentUser.CreateSubKey("Software\\Ch2r\\Last Files");
			}

			for (int x = 0; x < mapsXPListView.Items.Count; x++) {
				Map map = (Map)mapsXPListView.Items[x].Tag;
				ourKey.SetValue(x.ToString(), map.Filename);
			}
		}

		private struct RecentFileStruct
		{
			public string mapName;
			public string filename;

			public override string ToString()
			{
				return mapName;
			}
		}

		private void AddRecentFile(string filename)
		{
			RegistryKey ourKey = Registry.CurrentUser.OpenSubKey("Software\\Ch2r\\Recent Files", true);
			string[] filenames = new string[10];
			string[] mapNames = new string[10];
			ourKey.GetValueNames().CopyTo(mapNames, 0);
			int oldpos = 1;

			for (int i = 0; i < 10; i++)
			{
				if ((filenames[i] == null) || (mapNames[i] == null)) { break; }
				try {filenames[i] = ourKey.GetValue(mapNames[i]).ToString();} 
				catch {}
				ourKey.DeleteValue(mapNames[i], true);
				if (filenames[i] == filename){oldpos = i;}
			}

			ourKey.SetValue(filename.Substring(filename.LastIndexOf("\\") + 1), filename);

			int pos = 1;
			for (int i = 0; i < 10; i++)
			{
				if (filenames[i] != null & ((i + 1) != oldpos))
				{
					pos++;
					if (pos < 10) 
					{
						ourKey.SetValue(mapNames[i], filenames[i]);
					}
				}
			}

			ourKey.Flush();
			RefreshRecentList();
		}
		
		private void recentComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			RecentFileStruct recentFile = (RecentFileStruct)recentComboBox.SelectedItem;
			OpenMap(recentFile.filename, false);
		}
		#endregion

		#region Maps Expando Code & Map Open Code
		private void openTaskItem_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				OpenMap(openFileDialog.FileName, false);
			}
		}

		private void OpenMap(string filename, bool silent)
		{
			try {

				try {
					// See if map is already opened
					foreach (ListViewItem item in mapsXPListView.Items) {
						Map map = (Map)item.Tag;
						if (map.Filename == filename) {
							if (!silent)
								ExploreMap(map);
							return;
						}
					}
				} catch (Exception ex) {
					MessageBox.Show("Error in trying to find if map was opened.\n\n" + ex.Message);
				}

				Map halo2map = new Map();
				int result = 0;

				result = halo2map.Open(filename, silent);
				if (result == 0) 
				{
					// Failed to access file, an error message should have already been shown
					// so we do nothing
				} 
				else if (result == 1) 
				{
					// Map was opened fine, we can add it to the list
					mapsXPListView.Items.Add(halo2map.GetListViewItem());

					// Open needed shared files
					try {
						switch (halo2map.Header.MapType) {
							case "Single Player":
								OpenSharedMaps(3);
								break;
							case "Multi Player":
							case "Single Player Shared":
								OpenSharedMaps(2);
								break;
							case "Shared":
								OpenSharedMaps(1);
								break;
						}
					} catch (Exception ex) {
						MessageBox.Show("Error occured in opening shared files.\n\n" + ex.Message);
					}
					
					AddRecentFile(filename);
				} 
				else if (result == 2) 
				{
					// Map was opened fine, but failed processing, file is either not a map file
					// or it is corrupted, so we alert the user to this
					MessageBox.Show("This is not a valid Halo 2 Map File", "Invalid File");
				}
			} catch (Exception ex) {
				MessageBox.Show("An unknown error has occured.\n\n" + ex.Message);
			}
		}

		private void ExploreMap(Map map)
		{
				MapExplorer newExplorer = new MapExplorer(map);
				newExplorer.CreateFileList();
				newExplorer.ShowFiles("");
				newExplorer.Show();
		}

		/// <summary>
		/// Opens all shared maps from the shared path.
		/// </summary>
		public void OpenSharedMaps(int level)
		{
			if (level > 0) {
				OpenMap(Global.SharedPath + "mainmenu.map", true);
				if (level > 1) {
					OpenMap(Global.SharedPath + "shared.map", true);
					if (level > 2) 
						OpenMap(Global.SharedPath + "single_player_shared.map", true);
				}
			}
		}
		#endregion

		#region Map Signing Function
		private bool signMap(int blockSize, FileStream map)
		{
			SigningProgress progressForm = new SigningProgress();
			progressForm.Show();
			progressForm.Text = "Signing " + map.Name;
			progressForm.signProgressBar.Minimum = 0;
			progressForm.signProgressBar.Maximum = (int)(map.Length / blockSize) + 1;
			progressForm.signProgressBar.Value = 0;
			Application.DoEvents();

			uint sum;

			if ((blockSize % 4) != 0) 
			{
				Exception ex = new System.Exception("Block size must be divisible by 4");
				throw ex;
			}

			BinaryReader br = new BinaryReader(map);
	
			// initialize xor sum
			sum = INIT_SUM;
	
			// go to start of hash
			map.Seek(SIGN_START, SeekOrigin.Begin);
	
			byte[] buffer = new byte[blockSize];

			int bytesRead;
			while (!(map.Position == map.Length))
			{
				Application.DoEvents();
				bytesRead = map.Read(buffer, 0, blockSize);

				if (bytesRead == 0)
				{
					break;
				}

				MemoryStream ms = new MemoryStream(buffer, 0, bytesRead, false, false);
				BinaryReader msBR = new BinaryReader(ms);

				while (!(ms.Position == ms.Length))
				{
					sum = sum ^ msBR.ReadUInt32();
				}

				progressForm.signProgressBar.Value++;
			}

			map.Seek(SIG_POS, SeekOrigin.Begin);

			BinaryWriter mapBW = new BinaryWriter(map);
			mapBW.Write(sum);

			progressForm.Close();
			return true;
		}
		#endregion

		#region Map List View Events
		private void mapsXPListView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (mapsXPListView.SelectedItems.Count > 0) {
				Map halo2map = (Map)mapsXPListView.SelectedItems[0].Tag;
				actionsExpando.Visible = true;
				propertiesExpando.Visible = true;
				closeMI.Enabled = true;
				mapsXPListView.ContextMenu = mapsCM;

				primaryMagicTaskItem.Text = "Magic 1: 0x" + halo2map.Magic.ToString("X8");
				secondaryMagicTaskItem.Text = "Magic 2: 0x" + halo2map.SecondaryMagic.ToString("X8");
				checksumTaskItem.Text = "Checksum: 0x" + halo2map.Signature.ToString("X8");
				mapTypeTaskItem.Text = "Map Type:\n" + halo2map.Header.MapType;
			} else {
				// Nothing selected
				propertiesExpando.Visible = false;
				actionsExpando.Visible = false;
				closeMI.Enabled = false;
				mapsXPListView.ContextMenu = null;
			}
		}

		private void mapsXPListView_ItemClicked(object sender, System.EventArgs e)
		{
			if (mapsXPListView.SelectedItems.Count > 0)
			{
				Map halo2map = (Map)mapsXPListView.SelectedItems[0].Tag;
				ExploreMap(halo2map);
			}
		}
		#endregion

		#region Actions Expando Code
		private void exploreMapTaskItem_Click(object sender, System.EventArgs e)
		{
			mapsXPListView_ItemClicked(sender, e);
		}

		private void signMapTaskItem_Click(object sender, System.EventArgs e)
		{
			Map halo2map = (Map)mapsXPListView.SelectedItems[0].Tag;
			signMap(4096, halo2map.stream);

			halo2map.Header.RefreshSig(halo2map.stream);
			checksumTaskItem.Text = "Checksum: 0x" + halo2map.Signature.ToString("X8");
		}

		private void closeMapTaskItem_Click(object sender, System.EventArgs e)
		{
			((Map)mapsXPListView.SelectedItems[0].Tag).Close();
			mapsXPListView.Items.Remove(mapsXPListView.SelectedItems[0]);
		}
		#endregion

		#region Properties Expando Code
		private void primaryMagicTaskItem_Click(object sender, System.EventArgs e)
		{
			Map halo2map = (Map)mapsXPListView.SelectedItems[0].Tag;
			Clipboard.SetDataObject(halo2map.Magic.ToString("X8"));
		}

		private void secondaryMagicTaskItem_Click(object sender, System.EventArgs e)
		{
			Map halo2map = (Map)mapsXPListView.SelectedItems[0].Tag;
			Clipboard.SetDataObject(halo2map.SecondaryMagic.ToString("X8"));
		}

		private void checksumTaskItem_Click(object sender, System.EventArgs e)
		{
			Map halo2map = (Map)mapsXPListView.SelectedItems[0].Tag;
			Clipboard.SetDataObject(halo2map.Signature.ToString("X8"));
		}

		private void mapTypeTaskItem_Click(object sender, System.EventArgs e)
		{
			Map halo2map = (Map)mapsXPListView.SelectedItems[0].Tag;
			Clipboard.SetDataObject(halo2map.Header.MapType);
		}
		#endregion

		#region Settings Expando Code
		private void configurationTaskItem_Click(object sender, System.EventArgs e)
		{
			Global.cfg.ShowDialog();
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

		private void aboutMI_Click(object sender, System.EventArgs e)
		{
		
		}

		private void exitMI_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}
	}
}
