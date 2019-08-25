using System;
using System.Drawing;
using System.Windows.Forms;
using ToolBarControl;
using XPExplorerBar;

namespace Ch2r
{
	public struct SkinSettings
	{
		public Color background;
		public Color text;
		public Color listtext;
		public Color gradientstart;
		public Color gradientend;

		public void Default()
		{
			background = SystemColors.Control;
			text = SystemColors.ControlText;
			listtext = SystemColors.Highlight;
			gradientstart = Color.Empty;
			gradientend = Color.Empty;
		}
	}

	public class Skin
	{
		public SkinSettings current = new SkinSettings();

		public Skin()
		{
		}

		/// <summary>
		/// Check all the controls.
		/// </summary>
		public void SkinControl(Control ctrl)
		{
			SetControl(ctrl);
			for (int i = 0; i < ctrl.Controls.Count; i++) {
				SkinControl(ctrl.Controls[i]);
			}
		}

		/// <summary>
		/// Use skin colors to set the Control.
		/// </summary>
		private void SetControl(Control ctrl)
		{
			ctrl.ForeColor = current.text;
			if (ctrl is Form) {
				ctrl.BackColor = current.background;
			} else if (ctrl is TaskPane) {
				((TaskPane)ctrl).CustomSettings.GradientStartColor = current.gradientstart;
				((TaskPane)ctrl).CustomSettings.GradientEndColor = current.gradientend;
			} else if (ctrl is Expando) {
				Expando tmp = (Expando)ctrl;

				tmp.CustomSettings.NormalBackColor = Color.Empty;
				tmp.CustomSettings.NormalBorderColor = Color.Empty;
				tmp.CustomHeaderSettings.NormalBackColor = Color.Empty;
				tmp.CustomHeaderSettings.NormalBorderColor = Color.Empty;
				tmp.CustomHeaderSettings.NormalGradientEndColor = Color.Empty;
				tmp.CustomHeaderSettings.NormalGradientStartColor = Color.Empty;
				tmp.CustomHeaderSettings.NormalTitleColor = Color.Empty;
			} else if (ctrl is XPListView) {
				ctrl.BackColor = Color.White;
				ctrl.ForeColor = current.listtext;
				((XPListView)ctrl).BorderStyle = BorderStyle.None;
			} else if (ctrl is TextBox || ctrl is ComboBox) {
				ctrl.BackColor = Color.White;
			} else if (ctrl is ToolBarControl.ToolBar) {
				ctrl.BackColor = SystemColors.Control;
			}
		}

		public void MakeChristmas()
		{
			current.background = Color.FromArgb(187, 0, 0);
		}
	}
}
