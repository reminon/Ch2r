using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace directx
{
	/// <summary>
	/// Summary description for dxcreate.
	/// </summary>
	public class dxcreate
	{
		protected Control target = null;
		protected Device device = null;

		public dxcreate(Control renderTarget)
		{
			this.target = renderTarget;
			this.target.ClientSize = renderTarget.ClientSize;
			this.target.Text = renderTarget.Text;
			this.InitializeGraphics();
		}

		private bool InitializeGraphics()
		{
			try
			{
				// Now let's setup our D3D stuff
				PresentParameters presentParams = new PresentParameters();
				presentParams.Windowed=true;
				presentParams.SwapEffect = SwapEffect.Discard;
				device = new Device(0, DeviceType.Hardware, this.target, CreateFlags.SoftwareVertexProcessing, presentParams);
				return true;
			}
			catch (DirectXException)
			{ 
				return false; 
			}
		}

		public void Render()
		{
			if (device == null) 
				return;

			//Clear the backbuffer to a blue color 
			device.Clear(ClearFlags.Target, System.Drawing.Color.Blue, 1.0f, 0);
			//Begin the scene
			device.BeginScene();
			
			// Rendering of scene objects can happen here
    
			//End the scene
			device.EndScene();
			device.Present();
		}
	}
}
