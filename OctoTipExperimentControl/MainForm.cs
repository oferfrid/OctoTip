/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 25/09/2011
 * Time: 10:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using OctoTipExperimentControl;
using OctoTip.OctoTipExperiments.Core;
using OctoTip.OctoTipExperiments.Attributes;

namespace OctoTip.OctoTipExperimentControl
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
		
		
		
		void MainFormLoad(object sender, EventArgs e)
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			
			AddAvailableProtocols();
			
			
		}
		
		#region Private function
		private void AddAvailableProtocols()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			
			List<Type> ProtocolsData =  ProtocolHostProvider.ProtocolsData;
			
			ToolStripItem[] ToolStripProtocols =new ToolStripItem[ProtocolsData.Count];
			
			this.ProtocoltoolStrip.Items.Clear();
			
			foreach(Type ProtocolData in ProtocolsData)
            {
				ToolStripButton BTN = new ToolStripButton(
					ProtocolData.Name, ((System.Drawing.Image)(resources.GetObject("Protocol1.Image"))));
				BTN.ImageTransparentColor = System.Drawing.Color.Magenta;
				BTN.Size = new System.Drawing.Size(88, 20);
				BTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
				BTN.ToolTipText = ((ProtocolAttribute)ProtocolData.GetCustomAttributes(typeof(ProtocolAttribute), true)[0]).Description;
				BTN.Click += new System.EventHandler(this.AvailableProtocolClick);
				
				this.ProtocoltoolStrip.Items.Add(BTN);
            }
		}
		
		private void AddProtocolUserControl(string ProtocolName)
		{
		
				
				if (Protocolpanel.Controls.Count==0)
				{
					ProtocolUserControl P	 = new ProtocolUserControl(ProtocolName);
					this.Protocolpanel.Controls.Add(P);
				}
				else
				{
					ProtocolUserControl LastProtocolUserControl = (ProtocolUserControl)Protocolpanel.Controls[Protocolpanel.Controls.Count-1];
					ProtocolUserControl newProtocolUserControl = new ProtocolUserControl(ProtocolName);
					//newProtocolUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
					newProtocolUserControl.Location = new Point(LastProtocolUserControl.Left , LastProtocolUserControl.Bottom);
					Protocolpanel.Controls.Add(newProtocolUserControl);
				}
				
				
		}
		
		
		#endregion
		
		#region Event Handeling
		void ToolStripButtonRefreshProtocolsClick(object sender, EventArgs e)
		{
			AddAvailableProtocols();
		}
		
		
		
		
		
		
		void AvailableProtocolClick(object sender, EventArgs e)
		{
			
			
			AddProtocolUserControl(((ToolStripButton)sender).Text);
		}
		
		#endregion
		
		void Protocol1Click(object sender, EventArgs e)
		{
			
		}
	}
}
