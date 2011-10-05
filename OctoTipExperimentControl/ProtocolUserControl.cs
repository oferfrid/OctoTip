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
using Microsoft.Msagl.Drawing;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipExperiments.Core;
using OctoTip.OctoTipExperiments.Core.Interfaces;

namespace OctoTip.OctoTipExperimentControl
{
	/// <summary>
	/// Description of ProtocolUserControl.
	/// </summary>
	public partial class ProtocolUserControl : UserControl
	{
		Protocol UserControlProtocol;
		Type UserControlProtocolType;
		
		ProtocolParameters UserControlProtocolParameters;
		
		Graph graph ;
		Thread ProtocolworkerThread;
		
		
		public ProtocolUserControl()
		{
			InitializeComponent();
		}
		
		public ProtocolUserControl(Type ProtocolType):this()
		{
			this.UserControlProtocolType  =ProtocolType;
		}
		
		public ProtocolUserControl(Protocol UserControlProtocol):this()
		{
			this.UserControlProtocolType  =UserControlProtocol.GetType();
			this.UserControlProtocol  =UserControlProtocol;
			UserControlProtocolParameters = this.UserControlProtocol.ProtocolParameters;
			
			ActivateUserControlProtocol();
		}
		
		private void ActivateUserControlProtocol()
		{
			this.EditParametersbutton.BackColor = System.Drawing.SystemColors.Control;
			this.buttonStop.Enabled = false;
			this.checkBoxStartPause.Enabled = true;
		}
		
		
		#region Handeling events
		
		void CheckBoxStartPauseCheckedChanged(object sender, EventArgs e)
		{
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
						InitUserControlProtocol();
						ProtocolworkerThread = new Thread(UserControlProtocol.DoWork);
					}
					else
					{
						
						ProtocolworkerThread.Abort();
						InitUserControlProtocol();
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
		
		
		private void InitUserControlProtocol()
		{
			
			if (UserControlProtocol!=null)
			{
				//remove the courent Protocol from the List;
				
				((MainForm)this.ParentForm).RemoveProtocol(this.UserControlProtocol);
				this.UserControlProtocol = null;
			}
			
			UserControlProtocol = ProtocolProvider.GetProtocol(UserControlProtocolType,UserControlProtocolParameters);
			UserControlProtocol.StatusChanged += HandleProtocolStatusChanged;
			UserControlProtocol.DisplayedDataChange += HandleDisplayedDataChange;
			UserControlProtocol.StateStatusChange += HandleStateStatusChange;
			
			((MainForm)this.ParentForm).AddProtocol(this.UserControlProtocol);
			
			ActivateUserControlProtocol();
			
			
			
		}
		
		void ButtonStopClick(object sender, EventArgs e)
		{

			UserControlProtocol.RequestStop();
			//checkBoxStartPause.Checked = false;
		}
		
		#endregion

		void ProtocolUserControlLoad(object sender, EventArgs e)
		{
			DrowProtocolStates();
		}
		
		
		private void DrowProtocolStates()
		{
			graph = new Graph("graph");
			foreach (Type t in ProtocolProvider.GetProtocolStates(UserControlProtocolType))
			{
				string NodeFrom = ProtocolProvider.GetNodeDesplayName(t);
				foreach (Type ts in ProtocolProvider.GetStateNextStates(t))
				{
					voidUpdateEdgeNodesAttr(graph.AddEdge(NodeFrom,ProtocolProvider.GetNodeDesplayName(ts)));	
				}
			}

			graph.Attr.LayerDirection =LayerDirection.LR;
			
			ProtocolStatesViewer.Graph = graph;
			
			
			
		}
		
		private void voidUpdateEdgeNodesAttr(Edge E)
		{
			E.SourceNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse ;
			E.TargetNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse ;
		}
		private void HandleProtocolStatusChanged(object sender, ProtocolStatusChangeEventArgs e)
		{
			
			
			bool buttonStopEnabled = false;
			bool checkBoxStartPauseEnabled = true;
			string checkBoxStartPauseText = "start";
			
			
			
			switch (e.NewStatus)
			{
				case Protocol.ProtocolStatus.Stopped:
					buttonStopEnabled = false;
					checkBoxStartPauseEnabled = true;
					checkBoxStartPauseText = "Start";
					
					
					
					break;
				case Protocol.ProtocolStatus.Stoping:
					buttonStopEnabled = false;
					checkBoxStartPauseEnabled = false;
					checkBoxStartPauseText = "Start";
					
					
					break;
				case Protocol.ProtocolStatus.Started:
					buttonStopEnabled = true;
					checkBoxStartPauseEnabled = true;
					checkBoxStartPauseText = "Pause";
					
					
					break;
				case Protocol.ProtocolStatus.Starting:
					buttonStopEnabled = false;
					checkBoxStartPauseEnabled = false;
					checkBoxStartPauseText = "Pause";
					
					
					break;
				case Protocol.ProtocolStatus.Paused:
					buttonStopEnabled = true;
					checkBoxStartPauseEnabled = true;
					checkBoxStartPauseText = "Resturt";
					
					
					break;
				case Protocol.ProtocolStatus.Pausing:
					buttonStopEnabled = false;
					checkBoxStartPauseEnabled = false;
					checkBoxStartPauseText = "Resturt";
					
					
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
		private void HandleStateStatusChange(object sender, ProtocolStateStatusChangeEventArgs e)
		{
			Node N;
			if(e.PreviuseState!=null)
			{
			 N = graph.FindNode(ProtocolProvider.GetNodeDesplayName(e.PreviuseState));
			N.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Transparent;
			}
			
			 N = graph.FindNode(ProtocolProvider.GetNodeDesplayName(e.CurentState));
			N.Attr.FillColor = Microsoft.Msagl.Drawing.Color.MediumSeaGreen;
			
			MethodInvoker action = delegate
			{ ProtocolStatesViewer.Refresh(); };
			ProtocolStatesViewer.BeginInvoke(action);
		}
		
		void EditParametersbuttonClick(object sender, EventArgs e)
		{
			ProtocolParametersForm PPF;
			if (UserControlProtocolParameters==null)
			{
				PPF = new ProtocolParametersForm(this,UserControlProtocolType);
			}
			else
			{
				PPF = new ProtocolParametersForm(this,UserControlProtocolParameters);
			}
			PPF.ShowDialog();
		}
		
		public void SetNewUserControlProtocolParameters(ProtocolParameters ProtocolParameters)
		{
			this.UserControlProtocolParameters = ProtocolParameters;
			InitUserControlProtocol();
		}
	}
}
