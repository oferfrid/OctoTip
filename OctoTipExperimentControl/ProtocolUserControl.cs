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
		Thread ProtocolworkerThread;
		
		
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
			
			System.Diagnostics.Debug.WriteLine(this.checkBoxStartPause.Checked);
			
			if (this.checkBoxStartPause.Checked)
			{
				if(UserControlProtocol.Status ==  Protocol.ProtocolStatus.Paused)
				{
					UserControlProtocol.RequestResume();
				}
				else
				{
					if(ProtocolworkerThread==null)
					{
					ProtocolworkerThread = new Thread(UserControlProtocol.DoWork);	
					}
					else
					{
					ProtocolworkerThread.Abort();
					ProtocolworkerThread = new Thread(UserControlProtocol.DoWork);
					}
					ProtocolworkerThread.Start();
				}
				
			}
			else
			{
				//PauseProtocol.
				if(UserControlProtocol.Status ==  Protocol.ProtocolStatus.Stoping || UserControlProtocol.Status ==  Protocol.ProtocolStatus.Stopped  )
				{
				}
				else
				{					
					UserControlProtocol.RequestPause();
				}
				
			}
		}
		
		
		
		
		
		void ButtonStopClick(object sender, EventArgs e)
		{

			UserControlProtocol.RequestStop();
			checkBoxStartPause.Checked = false;
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
			
			
			bool buttonStopEnabled = false;
			bool checkBoxStartPauseEnabled = true;
			string checkBoxStartPauseText = "start";
			bool checkBoxStartPauseChecked = false;
			
				
				switch (e.NewStatus)
			{
				case Protocol.ProtocolStatus.Stopped:
					buttonStopEnabled = false;
					checkBoxStartPauseEnabled = true;
					checkBoxStartPauseText = "Start";
					checkBoxStartPauseChecked = false;
			
					
					break;
				case Protocol.ProtocolStatus.Stoping:
					buttonStopEnabled = false;
					checkBoxStartPauseEnabled = false;
					checkBoxStartPauseText = "Start";
					checkBoxStartPauseChecked = true;
			
					break;
				case Protocol.ProtocolStatus.Started:
					buttonStopEnabled = true;
					checkBoxStartPauseEnabled = true;
					checkBoxStartPauseText = "Pause";
					checkBoxStartPauseChecked = true;
			
					break;
				case Protocol.ProtocolStatus.Starting:
					buttonStopEnabled = false;
					checkBoxStartPauseEnabled = false;
					checkBoxStartPauseText = "Pause";
					checkBoxStartPauseChecked = true;
			
					break;
				case Protocol.ProtocolStatus.Paused:
					buttonStopEnabled = true;
					checkBoxStartPauseEnabled = true;
					checkBoxStartPauseText = "Resturt";
					checkBoxStartPauseChecked = false;
			
					break;
				case Protocol.ProtocolStatus.Pausing:
					buttonStopEnabled = false;
					checkBoxStartPauseEnabled = false;
					checkBoxStartPauseText = "Resturt";
					checkBoxStartPauseChecked = false;
					checkBoxStartPauseChecked = true;
			
					break;
					
			}
			
			
			MethodInvoker textBoxStatusaction = delegate
			{
				textBoxStatus.Text =e.NewStatus + ">" +e.Messege;
			};
			textBoxData.BeginInvoke(textBoxStatusaction);
			
			
			MethodInvoker buttonStopaction = delegate
			{
				buttonStop.Enabled = buttonStopEnabled ;
				//buttonStop.Image = buttonStopImage;
			};
			buttonStop.BeginInvoke(buttonStopaction);
			
			
			MethodInvoker checkBoxStartPauseaction = delegate
			{
				checkBoxStartPause.Enabled = checkBoxStartPauseEnabled ;
				checkBoxStartPause.Text = checkBoxStartPauseText ;
				//checkBoxStartPause.Checked = checkBoxStartPauseChecked;
				//checkBoxStartPause.Image = checkBoxStartPauseImage;
				
			};
			checkBoxStartPause.BeginInvoke(checkBoxStartPauseaction);
		}
		
		private void HandleDisplayedDataChange(object sender, ProtocolDisplayedDataChangeEventArgs e)
		{
			MethodInvoker action = delegate
			{ textBoxData.Text =e.Messege; };
			textBoxData.BeginInvoke(action);
		}
		
	}
}
