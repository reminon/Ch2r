using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Ch2r
{
	/// <summary>
	/// Summary description for HexViewer.
	/// </summary>
	public class SmartHexViewer : System.Windows.Forms.UserControl
	{
		public System.Windows.Forms.RichTextBox hexRichTextBox;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SmartHexViewer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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

		private string fullLine, bottomLine;
		public bool running;
		public bool stopped;
		const char h = '\u2500';
		const char v = '\u2502';
		const char uh = '\u2534';
		int startPos;
		byte[] lastData;

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.hexRichTextBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// hexRichTextBox
			// 
			this.hexRichTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.hexRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hexRichTextBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.hexRichTextBox.Location = new System.Drawing.Point(0, 0);
			this.hexRichTextBox.Name = "hexRichTextBox";
			this.hexRichTextBox.ReadOnly = true;
			this.hexRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.hexRichTextBox.Size = new System.Drawing.Size(728, 424);
			this.hexRichTextBox.TabIndex = 0;
			this.hexRichTextBox.Text = "";
			// 
			// SmartHexViewer
			// 
			this.Controls.Add(this.hexRichTextBox);
			this.Name = "SmartHexViewer";
			this.Size = new System.Drawing.Size(728, 424);
			this.Load += new System.EventHandler(this.SmartHexViewer_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public void Clear()
		{
			hexRichTextBox.Text = "";
			SmartHexViewer_Load(null, null);
		}

		public void ShowHex(string name, byte[] data, bool wordSplit)
		{
			lastData = data;
			if (stopped) {return;}

			int rows, offset, lastRowCount;
			byte thisByte;
			System.Text.StringBuilder outputText = new System.Text.StringBuilder();

			running = true;
			rows = data.Length / 16;
			lastRowCount = data.Length % 16;
			if(lastRowCount != 0) {rows++;}

			hexRichTextBox.AppendText(name + "\n\n");
			startPos = hexRichTextBox.TextLength;

			for (int row = 0; row < rows; row++)
			{
				if(!running) {return;}

				offset = row * 16;
				outputText.Append("0x" + offset.ToString("X8") + " " + v + " ");

				#region Generate hex representation of bytes
				for (int curByte = 0; curByte < 16; curByte++)
				{
					if (!running) {return;}

					if ((offset + curByte) < data.Length)
					{
						outputText.Append(data[offset + curByte].ToString("X2"));
					} 
					else 
					{
						outputText.Append("  ");
					}

					if ((wordSplit && (curByte == 3)) || (curByte == 7) || (curByte == 11) || (curByte == 15))
					{
						outputText.Append(" " + v + " ");
					} else
					{
						outputText.Append(" ");
					}
				}
				#endregion

				#region Generate character representation of bytes
				for(int curByte = 0; curByte < 16; curByte++)
				{
					if(!running) {return;}
					if(offset + curByte < data.Length)
					{
						thisByte = data[offset + curByte];
						if(thisByte < 0x20)
						{
							outputText.Append(".");
						}
						else
						{
							outputText.Append(new string((char)thisByte, 1));
						}
					}
				}
				#endregion

				outputText.Append("\n");
			}

			hexRichTextBox.AppendText(outputText.ToString());
			hexRichTextBox.AppendText(bottomLine + "\n");

			Application.DoEvents();
		}

		#region Highlight Functions
		public void HighlightByte(uint offset, Color color)
		{
			hexRichTextBox.Select((int)GetByteHexPos(offset), 2);
			hexRichTextBox.SelectionColor = color;
			hexRichTextBox.Select((int)GetByteCharPos(offset), 1);
			hexRichTextBox.SelectionColor = color;
			Application.DoEvents();
		}

		public void HighlightRange(uint offset, uint length, Color highlightColor)
		{
			for(uint i = 0; i < length; i++)
			{
				HighlightByte(offset + i, highlightColor);
			}
		}

		public void HighlightWord(uint offset, Color highlightColor)
		{
			HighlightRange(offset, 4, highlightColor);
		}
		#endregion

		#region Byte Getting Functions
		public byte GetByte(uint offset)
		{
			return lastData[offset];
		}

		public byte[] GetBytes(uint offset, uint length)
		{
			byte[] bytes = new byte[length];

			for (uint i = 0; i < length; i++)
			{
				bytes[i] = GetByte(offset + i);
			}

			return bytes;
		}

		public byte[] GetWord(uint offset)
		{
			return GetBytes(offset, 4);
		}

		public uint GetUInt32(uint offset)
		{
			byte[] uint32Bytes = GetWord(offset);
			uint uint32 = (uint)uint32Bytes[0] + ((uint)uint32Bytes[1] * 0x100) + ((uint)uint32Bytes[2] * 0x10000) + ((uint)uint32Bytes[3] * 0x1000000);

			return uint32;
		}
		#endregion

		#region Byte Position Getting Functions
		public uint GetByteHexPos(uint offset)
		{
			uint pos;

			pos = (uint)startPos;
			pos += (offset / 16) * 86;
			pos += 13;

			pos += 3 * (offset % 16);
			pos += 2 * ((offset % 16) / 4);

			return pos;
		}

		public uint GetByteCharPos(uint offset)
		{
			uint pos;

			pos = (uint)startPos;
			pos += (offset / 16) * 86;
			pos += 69;
			pos += offset % 16;

			return pos;
		}
		#endregion

		private void SmartHexViewer_Load(object sender, System.EventArgs e)
		{
			stopped = false;
			fullLine = new string(h, 86);
			bottomLine = new string(h, 11) + uh + new string(h, 13) + uh + new string(h, 13) + uh + new string(h, 13) + uh + new string(h, 13) + uh + new string(h, 18);
			hexRichTextBox.Text = "  Offset      0  1  2  3    4  5  6  7    8  9  a  b    c  d  e  f\n";
			hexRichTextBox.Text += fullLine + "\n";
		}
	}
}
