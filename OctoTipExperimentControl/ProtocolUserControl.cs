/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 27/09/2011
 * Time: 08:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using OctoTip.OctoTipExperiments.Base;

namespace OctoTipExperimentControl
{
	/// <summary>
	/// Description of ProtocolUserControl.
	/// </summary>
	public partial class ProtocolUserControl : UserControl
	{
		
		
		public ProtocolUserControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public ProtocolUserControl(string UserControlProtocol)
		{
				InitializeComponent();
			this.textBox1.Text = UserControlProtocol;
		}
		
		void CheckBoxStartPauseCheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxStartPause.Checked)
			{
				// StartProtocol.
				this.checkBoxStartPause.Text = "Pause";
			}
			else
			{
			//PauseProtocol.
				this.checkBoxStartPause.Text = "Start";
			}
		}
	}
}
