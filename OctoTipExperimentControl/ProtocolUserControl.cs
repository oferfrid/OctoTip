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
using OctoTip.OctoTipLib;

//using Microsoft.Msagl;






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
		
		ProtocolLogForm PLog;
		
		public const string LOG_NAME = "OctoTipExperimentManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		private int OldHeight;
		
		
		public ProtocolUserControl()
		{
			InitializeComponent();
		}
		
		public ProtocolUserControl(Type ProtocolType):this()
		{
			this.UserControlProtocolType  =ProtocolType;
			this.labelProtocolType.Text = ((ProtocolAttribute)UserControlProtocolType.GetCustomAttributes(typeof(ProtocolAttribute), true)[0]).ShortName;
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
			this.buttonStart.Enabled = true;
			this.buttonPause.Enabled = false;
		}
		
		
		private void InitUserControlProtocol()
		{
			ProtocolStatesViewer.LayoutAlgorithmSettingsButtonVisible = true;
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
			UserControlProtocol.StateDisplayedDataChange += HandleStateDisplayedDataChange;
			((MainForm)this.ParentForm).AddProtocol(this.UserControlProtocol);
			
			ActivateUserControlProtocol();
		}
		
		#region Handeling events
		
		
		
		void ButtonStopClick(object sender, EventArgs e)
		{

			UserControlProtocol.RequestStop();
		}
		
		void ButtonStartClick(object sender, EventArgs e)
		{
			UserControlProtocol.RequestStart();
		}
		
		void ButtonPauseClick(object sender, EventArgs e)
		{
			UserControlProtocol.RequestPause();
		}
		
		
		
		
		
		private void HandleProtocolStatusChanged(object sender, ProtocolStatusChangeEventArgs e)
		{
			
			myLogger.Add(e.NewStatus + ">" + e.Messege);
			
			bool buttonStopEnabled ;
			bool buttonStartEnabled ;
			bool buttonPauseEnabled ;
			System.Drawing.Color ProtocolBackColor;
			
			switch (e.NewStatus)
			{
				case (Protocol.ProtocolStatus.Stoping):
					buttonStopEnabled  = false;
					buttonStartEnabled =false;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.White;
					break;
				case (Protocol.ProtocolStatus.Pausing):
					buttonStopEnabled  = true;
					buttonStartEnabled =false;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.White;
					break;
				case (Protocol.ProtocolStatus.Starting):
					buttonStopEnabled  = true;
					buttonStartEnabled =false;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.White;
					break;
				case (Protocol.ProtocolStatus.Stopped):
					buttonStopEnabled  = false;
					buttonStartEnabled =true;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.Red;
					break;
				case (Protocol.ProtocolStatus.Started):
					buttonStopEnabled  = true;
					buttonStartEnabled =false;
					buttonPauseEnabled=true ;
					ProtocolBackColor = System.Drawing.Color.LightGreen;
					break;
				case (Protocol.ProtocolStatus.Paused):
					buttonStopEnabled  = true;
					buttonStartEnabled =true;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.Yellow;
					break;
				case (Protocol.ProtocolStatus.Error):
					buttonStopEnabled  = false;
					buttonStartEnabled =true;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.DarkRed;
					break;
				case (Protocol.ProtocolStatus.RuntimeError):
					buttonStopEnabled  = true;
					buttonStartEnabled =false;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.Yellow;
					break;
				default:
					buttonStopEnabled  = true;
					buttonStartEnabled =true;
					buttonPauseEnabled=true ;
					ProtocolBackColor = System.Drawing.Color.White;
					break;
			}
			
			
			
			myLogger.Add(string.Format("{0}:{1}>{2}",this.Name, e.NewStatus ,e.Messege));
			
			
			
			
			MethodInvoker buttonStopaction = delegate
			{
				buttonStop.Enabled=buttonStopEnabled;
			};
			buttonStop.BeginInvoke(buttonStopaction);
			
			MethodInvoker buttonStartaction = delegate
			{
				buttonStart.Enabled=buttonStartEnabled;
			};
			buttonStart.BeginInvoke(buttonStartaction);
			
			MethodInvoker buttonPauseaction = delegate
			{
				buttonPause.Enabled=buttonPauseEnabled;
			};
			buttonPause.BeginInvoke(buttonPauseaction);
			
			
			MethodInvoker UserControlaction = delegate
			{
				this.BackColor=ProtocolBackColor;
			};
			this.BeginInvoke(UserControlaction);
			
		}
		
		private void HandleDisplayedDataChange(object sender, ProtocolDisplayedDataChangeEventArgs e)
		{
			MethodInvoker action = delegate
			{ textBoxProtocolData.Text =e.Messege; };
			textBoxProtocolData.BeginInvoke(action);
		}
		
		private void HandleStateDisplayedDataChange(object sender, ProtocolStateDisplayedDataChangeEventArgs e)
		{
			MethodInvoker action = delegate
			{ textBoxStateData.Text = e.Messege ;};
			textBoxStateData.BeginInvoke(action);
		}
		
		private void HandleStateStatusChange(object sender, ProtocolStateStatusChangeEventArgs e)
		{
			Node N;
			
			//if (e.StateStatus = State.Status.Active
			
			MethodInvoker action = delegate
			{
				DrawProtocolStates();
				switch (e.StateStatus)
				{
					case State.Status.Active:
						N = graph.FindNode(ProtocolProvider.GetStateDesplayName(e.CurrentState));
						N.Attr.FillColor = Microsoft.Msagl.Drawing.Color.MediumSeaGreen;
						break;
					case State.Status.Failed:
						N = graph.FindNode(ProtocolProvider.GetStateDesplayName(e.CurrentState));
						N.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
						break;
					case State.Status.RuntimeError:
						N = graph.FindNode(ProtocolProvider.GetStateDesplayName(e.CurrentState));
						N.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
						break;
				}
				
				ProtocolStatesViewer.Refresh(); };
			ProtocolStatesViewer.BeginInvoke(action);
			
			
			myLogger.Add( string.Format("{0}:{1}\n{2}",ProtocolProvider.GetStateDesplayName(e.CurrentState),  e.StateStatus,e.Messege));
			
			
			
			
			
		}
		
		
		#endregion

		void ProtocolUserControlLoad(object sender, EventArgs e)
		{
			DrawProtocolStates();
		}
		
		#region Private mathods
		
		private void DrawProtocolStates()
		{
			graph = new Graph("graph");
			foreach (Type t in ProtocolProvider.GetProtocolStates(UserControlProtocolType))
			{
				string NodeFrom = ProtocolProvider.GetStateDesplayName(t);
				foreach (Type ts in ProtocolProvider.GetStateNextStates(t))
				{
					UpdateEdgeNodesAttr(graph.AddEdge(NodeFrom,ProtocolProvider.GetStateDesplayName(ts)));
				}
			}
			
			
			//Node N = graph.FindNode("Grow 1");
			//N.Label.FontSize = 50;
			
			foreach (Node  N in  graph.NodeMap.Values )
			{
				//	N.Label.FontSize = 50;
			}
			
			graph.Attr.LayerDirection =LayerDirection.None;
			double AspectRatio = Convert.ToDouble(panel1.Width)/Convert.ToDouble(panel1.Height);
			graph.Attr.AspectRatio = AspectRatio;
			//graph.Attr.MinNodeHeight = 50;
			//graph.Attr.MinNodeWidth = 100;
			
			// aspect ratio is set

			graph.Attr.BackgroundColor = Microsoft.Msagl.Drawing.Color.White;
			
			//Microsoft.Msagl.GeometryGraph geomGraph = new Microsoft.Msagl.GeometryGraph();
			
			//	geomGraph.SimpleStretch=false;

			//	geomGraph.AspectRatio = 1;
			

			//	geomGraph.CalculateLayout();
			
			
			//ProtocolStatesViewer.Graph = graph;
			
			//	graph.GeometryGraph = geomGraph;

			//ProtocolStatesViewer.Graph = graph;

			//	ProtocolStatesViewer.NeedToCalculateLayout = false;
			ProtocolStatesViewer.Graph = graph;
			
			
		}
		
		private void UpdateEdgeNodesAttr(Edge E)
		{
			E.SourceNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse ;
			E.TargetNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse ;
		}
		
		#endregion
		
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
			try
			{
			PPF.ShowDialog();
			}
			catch(Exception ex)
			{
				myLogger.Add(ex.ToString());
			}
		}
		
		public void SetNewUserControlProtocolParameters(ProtocolParameters ProtocolParameters)
		{
			this.UserControlProtocolParameters = ProtocolParameters;
			InitUserControlProtocol();
		}
		public void UpdateUserControlProtocolName()
		{
			this.labelProtocolName.Text = UserControlProtocolParameters.Name;
			this.labelProtocolType.Text = ((ProtocolAttribute)UserControlProtocolType.GetCustomAttributes(typeof(ProtocolAttribute), true)[0]).ShortName;
			PLog = new ProtocolLogForm(UserControlProtocol.ProtocolParameters.Name);
		}
		
		
		
		void ProtocolStatesViewerSelectionChanged(object sender, EventArgs e)
		{
			object selectedObject = ProtocolStatesViewer.SelectedObject;
			
			if ( selectedObject!= null)
			{
				if (selectedObject is Edge)
				{
					Edge SelectedEdge = selectedObject as Edge;
				}
				else if (selectedObject is Node)
				{
					Node SelectedNode = selectedObject as Node;
					string DescriptionAttribute = ProtocolProvider.GetStateDescription(ProtocolProvider.GetStatePlugInByDesplayName(SelectedNode.Id));
					ProtocolStatesViewer.SetToolTip(new ToolTip(),DescriptionAttribute);
				}

				
			}

			//here you can use e.Attr.Id to get back to your data
			//this.gViewer.SetToolTip(toolTip1, String.Format("node {0}", (selectedObject as Node).Attr.Id));
			ProtocolStatesViewer.Invalidate();
		}
		
		void TextBoxProtocolDataDoubleClick(object sender, EventArgs e)
		{
			
			if (UserControlProtocolParameters!=null)
			{
				PLog.ShowDialog();
			}
			
		}
		
		void ClosebuttonClick(object sender, EventArgs e)
		{
			if (this.UserControlProtocol !=null)
			{
				if (this.UserControlProtocol.Status != Protocol.ProtocolStatus.Stopped &&
				    this.UserControlProtocol.Status != Protocol.ProtocolStatus.Error)
				{
					DialogResult result;
					result = MessageBox.Show("Protocol is in running state, Are you sure you want to close?", "OctoTip-Experiment Manager", MessageBoxButtons.YesNo);
					if (result == DialogResult.Yes)
					{
						//close protocol and remove from list
						((MainForm)this.ParentForm).RemoveProtocol(this.UserControlProtocol);
						this.Height = 0;
						((MainForm)this.ParentForm).RefreshProtocolUserControls();
						this.Hide();
					}
					
				}
				else
				{
					((MainForm)this.ParentForm).RemoveProtocol(this.UserControlProtocol);
						this.Height = 0;
						((MainForm)this.ParentForm).RefreshProtocolUserControls();
						this.Hide();
				}
			}
			else
			{
				this.Height = 0;
				((MainForm)this.ParentForm).RefreshProtocolUserControls();
				this.Hide();
			}
			
		}
		
		void MinimizebuttonClick(object sender, EventArgs e)
		{
			if (this.Height <= 25)
			{

			this.Height = OldHeight;
			
			((MainForm)this.ParentForm).RefreshProtocolUserControls();
			
				
			}
			else
			{
			OldHeight = this.Height;
			this.Height = 25;
			
			((MainForm)this.ParentForm).RefreshProtocolUserControls();
			
			}
		}
	}
}




