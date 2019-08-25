using Halo2;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ch2r
{
	public class SwapForm : System.Windows.Forms.Form
	{
		private SwapType swaptype;
		public SwapType Type
		{
			get {
				return swaptype;
			}
			set {
				swaptype = value;
				swapButton.Enabled = value == SwapType.Item;
			}
		}
		
		private struct SwapItem
		{
			public MapIndexItem _item;

			public override string ToString()
			{
				return _item.Filename;
			}
		}

		public MapExplorer mapform = null;
		public Properties metaform = null;
		private Map map;
		public MapIndexItem _original;
		public long offset;

		#region Controls
		private System.Windows.Forms.ComboBox tagsComboBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button swapButton;
		private System.Windows.Forms.Button changeButton;
		private System.Windows.Forms.Label filenameL;
		private System.Windows.Forms.ComboBox itemsComboBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public SwapForm(Map map)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Global.Skin.SkinControl(this);

			this.map = map;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SwapForm));
			this.tagsComboBox = new System.Windows.Forms.ComboBox();
			this.itemsComboBox = new System.Windows.Forms.ComboBox();
			this.swapButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.changeButton = new System.Windows.Forms.Button();
			this.filenameL = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tagsComboBox
			// 
			this.tagsComboBox.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.tagsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tagsComboBox.Location = new System.Drawing.Point(8, 16);
			this.tagsComboBox.Name = "tagsComboBox";
			this.tagsComboBox.Size = new System.Drawing.Size(64, 21);
			this.tagsComboBox.TabIndex = 0;
			this.tagsComboBox.SelectedIndexChanged += new System.EventHandler(this.tagsComboBox_SelectedIndexChanged);
			// 
			// itemsComboBox
			// 
			this.itemsComboBox.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.itemsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.itemsComboBox.Location = new System.Drawing.Point(80, 16);
			this.itemsComboBox.Name = "itemsComboBox";
			this.itemsComboBox.Size = new System.Drawing.Size(480, 21);
			this.itemsComboBox.TabIndex = 1;
			this.itemsComboBox.SelectedIndexChanged += new System.EventHandler(this.itemsComboBox_SelectedIndexChanged);
			// 
			// swapButton
			// 
			this.swapButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.swapButton.Location = new System.Drawing.Point(136, 88);
			this.swapButton.Name = "swapButton";
			this.swapButton.Size = new System.Drawing.Size(88, 24);
			this.swapButton.TabIndex = 2;
			this.swapButton.Text = "&Swap";
			this.swapButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(344, 88);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(88, 24);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// changeButton
			// 
			this.changeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.changeButton.Location = new System.Drawing.Point(240, 88);
			this.changeButton.Name = "changeButton";
			this.changeButton.Size = new System.Drawing.Size(88, 24);
			this.changeButton.TabIndex = 4;
			this.changeButton.Text = "&Change";
			this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
			// 
			// filenameL
			// 
			this.filenameL.Location = new System.Drawing.Point(8, 40);
			this.filenameL.Name = "filenameL";
			this.filenameL.Size = new System.Drawing.Size(552, 32);
			this.filenameL.TabIndex = 5;
			// 
			// SwapForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.ClientSize = new System.Drawing.Size(570, 128);
			this.Controls.Add(this.filenameL);
			this.Controls.Add(this.changeButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.swapButton);
			this.Controls.Add(this.itemsComboBox);
			this.Controls.Add(this.tagsComboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SwapForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SwapForm";
			this.ResumeLayout(false);

		}
		#endregion

		public void LoadTags(string currentTag, string filename)
		{
			// Clear current tags and item
			tagsComboBox.Items.Clear();
			itemsComboBox.Items.Clear();

			// Load tags
			for (int x = 0; x < Map.tags.Length; x++) {
				tagsComboBox.Items.Add(Map.tags[x][0]);
			}

			tagsComboBox.Text = currentTag;

			CurrentFilename(filename);

			Text = "Swapping [" + currentTag + "] " + filename.Substring(filename.LastIndexOf("\\") + 1);
		}

		public void LoadTag(string tag)
		{
			SwapItem nulled;
			MapIndexItem empty = new MapIndexItem();
			empty.Filename = "Nulled Out";
			empty.Ident = 0xFFFFFFFF;
			nulled._item = empty;

			itemsComboBox.Items.Clear();

			if (Type == SwapType.Reflexive) {
				// Show Nulled Out in begin
				itemsComboBox.Items.Add(nulled);
			}

			MapIndexItem[] items = map.Index.ItemArray;
			for (int i = 0; i < items.Length; i++) {
				if (items[i].Tag == tag) {
					SwapItem si;
					si._item = items[i];
					itemsComboBox.Items.Add(si);
				}
			}

			if (Type == SwapType.Reflexive) {
				// Show Nulled Out at the end
				itemsComboBox.Items.Add(nulled);
			}

			itemsComboBox.SelectedIndex = 0;
		}

		public void CurrentFilename(string filename)
		{
			itemsComboBox.Text = filename;
		}

		private void tagsComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadTag(tagsComboBox.Text);
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			map.SwapItem(_original, ((SwapItem)itemsComboBox.SelectedItem)._item, Type, tagsComboBox.Text, true);
			RefreshOwner();

			Close();
		}

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void changeButton_Click(object sender, System.EventArgs e)
		{
			MapIndexItem original = _original;
			if (Type == SwapType.Reflexive) {
				original.Offset = offset;
			}
			map.SwapItem(original, ((SwapItem)itemsComboBox.SelectedItem)._item, Type, tagsComboBox.Text, false);
			RefreshOwner();
			Close();
		}

		private void itemsComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			filenameL.Text = itemsComboBox.Text;
		}

		private void RefreshOwner()
		{
			if (Type == SwapType.Item) {
				if (mapform != null)
					mapform.RefreshCurrentItem();
			} else {
				if (metaform != null)
					metaform.RefreshCurrentItem();
			}
		}
	}
}
