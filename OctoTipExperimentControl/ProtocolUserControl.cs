/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/10/2011
 * Time: 10:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipExperiments.Core;

namespace OctoTip.OctoTipExperimentControl
{
	/// <summary>
	/// Description of ProtocolUserControl.
	/// </summary>
	public partial class ProtocolUserControl : UserControl
	{
		Protocol UserControlProtocol;
		Type UserControlProtocolType;
		Protocol.ProtocolStatus  UserProtocolState = Protocol.ProtocolStatus.Stopped;
		
		public ProtocolUserControl()
		{
			InitializeComponent();
		}
		
		public ProtocolUserControl(Type ProtocolType):this()
		{
			this.UserControlProtocolType  =ProtocolType;
			UserControlProtocol = ProtocolHostProvider.GetProtocol(ProtocolType);
			
			UserControlProtocol.StatusChanged += HandleProtocolStatusChanged;
			UserControlProtocol.DisplayedDataChange += HandleDisplayedDataChange;
		}
		
		
		#region Handeling events
		
		void CheckBoxStartPauseCheckedChanged(object sender, EventArgs e)
		{
			
			
			if (this.checkBoxStartPause.Checked)
			{
				
				this.checkBoxStartPause.Text = "Pause";
				if(UserProtocolState ==  Protocol.ProtocolStatus.Paused)
				{
					UserControlProtocol.RequestResume();
				}
				else
				{
					Thread workerThread = new Thread(UserControlProtocol.DoWork);
					workerThread.Start();
					
				}
				
				UserProtocolState = Protocol.ProtocolStatus.Started;
				
			}
			else
			{
				//PauseProtocol.
				
				this.checkBoxStartPause.Text = "Resume";
				UserControlProtocol.RequestPause();
				UserProtocolState = Protocol.ProtocolStatus.Paused;
				
			}
		}
		
		
		
		
		
		void ButtonStopClick(object sender, EventArgs e)
		{
			checkBoxStartPause.Checked = false;
			this.checkBoxStartPause.Text = "Start";
			UserControlProtocol.RequestStop();
			UserProtocolState = Protocol.ProtocolStatus.Stopped;
		}
		
		
		
		#endregion


		void ProtocolUserControlLoad(object sender, EventArgs e)
		{
			this.textBoxData.Text = UserControlProtocolType.Name + Environment.NewLine;
			foreach (Type t in ProtocolHostProvider.GetProtocolStates(UserControlProtocolType))
			{
				this.textBoxData.Text+= "," + t.Name + "(" ;
				foreach (Type ts in ProtocolHostProvider.GetStateNextStates(t))
				{
					this.textBoxData.Text+=ts.Name + ",";
				}
				this.textBoxData.Text+= ")";
			}
			this.textBoxData.Text+= Environment.NewLine;
		}
		
		
		private void HandleProtocolStatusChanged(object sender, ProtocolStatusChangeEventArgs e)
		{
			switch (e.NewStatus)
			{
				case Protocol.ProtocolStatus.Stopped:
					
						break;
					
				case Protocol.ProtocolStatus.Stoping:
						  
						break;
						case Protocol.ProtocolStatus.Started:
						  
						break;
						case Protocol.ProtocolStatus.Starting:
						  
						break;
						case Protocol.ProtocolStatus.Paused:
						  
						break;
					case Protocol.ProtocolStatus.Pausing:
						  
						break;					
					
			}
			
			
			MethodInvoker action = delegate
			{
				textBoxStatus.Text =e.NewStatus + ">" +e.Messege;
			};
			textBoxData.BeginInvoke(action);
			
		}
		
		private void HandleDisplayedDataChange(object sender, ProtocolDisplayedDataChangeEventArgs e)
		{
			MethodInvoker action = delegate
			{ textBoxData.Text =e.Messege; };
			textBoxData.BeginInvoke(action);
		}
		
	}
}
