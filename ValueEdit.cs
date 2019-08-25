using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ch2r
{
	/// <summary>
	/// Summary description for ValueEdit.
	/// </summary>
	public class ValueEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.TextBox valueTextBox;
		private System.Windows.Forms.Label descriptionLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ListViewItem itemToChange;

		event ValueChangedHandler OnChangeValue;

		public ValueEdit(ValueChangedHandler valueChange)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			OnChangeValue += valueChange;
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
			this.nameLabel = new System.Windows.Forms.Label();
			this.valueTextBox = new System.Windows.Forms.TextBox();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nameLabel.ForeColor = System.Drawing.Color.White;
			this.nameLabel.Location = new System.Drawing.Point(8, 8);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(224, 16);
			this.nameLabel.TabIndex = 0;
			this.nameLabel.Text = "Name Here";
			// 
			// valueTextBox
			// 
			this.valueTextBox.Location = new System.Drawing.Point(8, 24);
			this.valueTextBox.Name = "valueTextBox";
			this.valueTextBox.Size = new System.Drawing.Size(224, 20);
			this.valueTextBox.TabIndex = 1;
			this.valueTextBox.Text = "";
			this.valueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valueTextBox_KeyPress);
			// 
			// descriptionLabel
			// 
			this.descriptionLabel.ForeColor = System.Drawing.Color.White;
			this.descriptionLabel.Location = new System.Drawing.Point(8, 64);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new System.Drawing.Size(224, 56);
			this.descriptionLabel.TabIndex = 2;
			this.descriptionLabel.Text = "Description:";
			// 
			// okButton
			// 
			this.okButton.BackColor = System.Drawing.SystemColors.Control;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(56, 128);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(56, 24);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.BackColor = System.Drawing.SystemColors.Control;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(120, 128);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(56, 24);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// ValueEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(187)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.ClientSize = new System.Drawing.Size(240, 160);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.descriptionLabel);
			this.Controls.Add(this.valueTextBox);
			this.Controls.Add(this.nameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ValueEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ValueEdit";
			this.ResumeLayout(false);

		}
		#endregion

		public void Init(ListViewItem item, uint offset)
		{
			itemToChange = item;
			nameLabel.Text = itemToChange.SubItems[0].Text + " (" + itemToChange.SubItems[2].Text + ")";
			valueTextBox.Text = itemToChange.SubItems[1].Text;
			descriptionLabel.Text = "Description:\n" + itemToChange.SubItems[3].Text;
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			itemToChange.SubItems[1].Text = valueTextBox.Text;
			OnChangeValue((uint)itemToChange.Tag, itemToChange.SubItems[2].Text, valueTextBox.Text);
			this.Close();
		}

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void valueTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)0xd) 
			{
				okButton_Click(null, null);
				e.Handled = true;
			}
		}
	}
}
