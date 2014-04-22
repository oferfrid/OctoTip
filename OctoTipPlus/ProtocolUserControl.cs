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
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore;
using OctoTip.Lib.Logging;

namespace OctoTip.OctoTipPlus
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
		
		public const string LOG_NAME = "OctoTipExperimentManager";
		
		private int OldHeight;
		
		
		public ProtocolUserControl()
		{
			InitializeComponent();
			(ProtocolStatesViewer as Microsoft.Msagl.Drawing.IViewer).MouseDown += new EventHandler<Microsoft.Msagl.Drawing.MsaglMouseEventArgs>(ProtocolStatesViewerMouseDown);
			(ProtocolStatesViewer as Microsoft.Msagl.Drawing.IViewer).MouseUp += new EventHandler<Microsoft.Msagl.Drawing.MsaglMouseEventArgs>(ProtocolStatesViewerMouseUp);
			
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
		
		public Protocol.Statuses ProtocolStatus
		{
			get 
			{
				if (UserControlProtocol==null)
				{
				return  Protocol.Statuses.Stopped;
				}
				else
				{
					return UserControlProtocol.Status;
				}
			}
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
			string Titel ;
			string Sendor  = UserControlProtocolParameters.Name;
			string Message = e.NewStatus + ">" + e.Message;
			
			//((MainForm)this.ParentForm).Notify(new Logging.LoggingEntery(this
			
			bool buttonStopEnabled ;
			bool buttonStartEnabled ;
			bool buttonPauseEnabled ;
			System.Drawing.Color ProtocolBackColor;
			
			switch (e.NewStatus)
			{
				case (Protocol.Statuses.Stoping):
					buttonStopEnabled  = false;
					buttonStartEnabled =false;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.White;
					break;
				case (Protocol.Statuses.Pausing):
					buttonStopEnabled  = true;
					buttonStartEnabled =false;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.White;
					break;
				case (Protocol.Statuses.Starting):
					buttonStopEnabled  = true;
					buttonStartEnabled =false;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.White;
					break;
				case (Protocol.Statuses.Stopped):
					buttonStopEnabled  = false;
					buttonStartEnabled =true;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.Red;
					break;
				case (Protocol.Statuses.Started):
					buttonStopEnabled  = true;
					buttonStartEnabled =false;
					buttonPauseEnabled=true ;
					ProtocolBackColor = System.Drawing.Color.LightGreen;
					break;
				case (Protocol.Statuses.Paused):
					buttonStopEnabled  = true;
					buttonStartEnabled =true;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.Yellow;
					break;
				case (Protocol.Statuses.Error):
					buttonStopEnabled  = false;
					buttonStartEnabled =true;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.DarkRed;
					// notify
					Titel = string.Format("Error in {0} ({1})",this.labelProtocolType.Text,this.labelProtocolName.Text);
					Message = string.Format("Error in {0} ({1}\n {2})",this.labelProtocolType.Text,this.labelProtocolName.Text,e.Message);
					Log.LogEntery(new LoggingEntery("Protocol","ProtocolUserControl",Titel,Message,LoggingEntery.EnteryTypes.Error));
					break;
				case (Protocol.Statuses.FatalError):
					buttonStopEnabled  = false;
					buttonStartEnabled =true;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.Black;
					// notify
					Titel = string.Format("Error in {0} ({1})",this.labelProtocolType.Text,this.labelProtocolName.Text);
					Message = string.Format("Error in {0} ({1}\n {2})",this.labelProtocolType.Text,this.labelProtocolName.Text,e.Message);
					Log.LogEntery(new LoggingEntery("Protocol","ProtocolUserControl",Titel,Message,LoggingEntery.EnteryTypes.Critical));
					break;
				case (Protocol.Statuses.RuntimeError):
					buttonStopEnabled  = true;
					buttonStartEnabled =false;
					buttonPauseEnabled=false ;
					ProtocolBackColor = System.Drawing.Color.Yellow;
					Titel = string.Format("Run time error {0} ({1})",this.labelProtocolType.Text,this.labelProtocolName.Text);
					Message = string.Format("Run time error in {0} ({1}\n {2})",this.labelProtocolType.Text,this.labelProtocolName.Text,e.Message);
					Log.LogEntery(new LoggingEntery("Protocol","ProtocolUserControl",Titel,Message,LoggingEntery.EnteryTypes.Critical));
					
					break;
				default:
					buttonStopEnabled  = true;
					buttonStartEnabled =true;
					buttonPauseEnabled=true ;
					ProtocolBackColor = System.Drawing.Color.White;
					break;
			}
			
			string Title	= string.Format("{0}:{1}>{2}",this.Name, e.NewStatus ,e.Message);
			Log.LogEntery(new LoggingEntery("Protocol","ProtocolUserControl",Title,LoggingEntery.EnteryTypes.Informational));
			
			if			(this.ParentForm !=null)
			{
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
			
			((MainForm)this.ParentForm).RefreshProtocolUserControls();
			
		}
		
		private void HandleDisplayedDataChange(object sender, ProtocolDisplayedDataChangeEventArgs e)
		{
			MethodInvoker action = delegate
			{ textBoxProtocolData.Text =e.Message; };
			textBoxProtocolData.BeginInvoke(action);
		}
		
		private void HandleStateDisplayedDataChange(object sender, StateDisplayedDataChangeEventArgs e)
		{
			MethodInvoker action = delegate
			{ textBoxStateData.Text = e.Message ;};
			textBoxStateData.BeginInvoke(action);
		}
		
		private void HandleStateStatusChange(object sender, StateStatusChangeEventArgs e)
		{
			Node N;
			
			MethodInvoker action = delegate
			{
				DrawProtocolStates();
				switch (e.StateStatus)
				{
					case State.Statuses.Started:
						N = graph.FindNode(ProtocolProvider.GetStateDesplayName(e.CurrentState));
						N.Attr.FillColor = Microsoft.Msagl.Drawing.Color.MediumSeaGreen;
						break;
					case State.Statuses.FatalError:
						N = graph.FindNode(ProtocolProvider.GetStateDesplayName(e.CurrentState));
						N.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
						break;
					case State.Statuses.RuntimeError:
						N = graph.FindNode(ProtocolProvider.GetStateDesplayName(e.CurrentState));
						N.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
						break;
						//TODO:Add all Statuses
				}
				
				ProtocolStatesViewer.Refresh(); };
			ProtocolStatesViewer.BeginInvoke(action);
			
			string Title	= string.Format("{0}:{1}\n{2}",ProtocolProvider.GetStateDesplayName(e.CurrentState),  e.StateStatus,e.Message);
			Log.LogEntery(new LoggingEntery("Protocol","ProtocolUserControl",Title,LoggingEntery.EnteryTypes.Informational));
			
			
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
			PPF.ShowDialog();
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
					string DescriptionAttribute = ProtocolProvider.GetStateDescription(ProtocolProvider.GetStatePlugInByDesplayName(SelectedNode.Id,UserControlProtocolType));
					ProtocolStatesViewer.SetToolTip(new ToolTip(),DescriptionAttribute);
				}

				
			}

			//here you can use e.Attr.Id to get back to your data
			//this.gViewer.SetToolTip(toolTip1, String.Format("node {0}", (selectedObject as Node).Attr.Id));
			ProtocolStatesViewer.Invalidate();
		}
		
		
		
		void ClosebuttonClick(object sender, EventArgs e)
		{
			if (this.UserControlProtocol !=null)
			{
				if (this.UserControlProtocol.Status != Protocol.Statuses.Stopped &&
				    this.UserControlProtocol.Status != Protocol.Statuses.Error)
				{
					DialogResult result;
					result = MessageBox.Show("Protocol is in running state, Are you sure you want to close?", this.Text, MessageBoxButtons.YesNo,MessageBoxIcon.Hand);
					if (result == DialogResult.Yes)
					{
						//close protocol and remove from list
						((MainForm)this.ParentForm).RemoveProtocol(this.UserControlProtocol);
						this.Height = 0;
						this.Hide();
						((MainForm)this.ParentForm).RefreshProtocolUserControls();
						
					}
					
				}
				else
				{
					((MainForm)this.ParentForm).RemoveProtocol(this.UserControlProtocol);
					this.Height = 0;
					this.Hide();
					((MainForm)this.ParentForm).RefreshProtocolUserControls();
				}
			}
			else
			{
				this.Height = 0;
				this.Hide();
				((MainForm)this.ParentForm).RefreshProtocolUserControls();
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
		
		void ProtocolStatesViewerMouseDown(object sender, Microsoft.Msagl.Drawing.MsaglMouseEventArgs e)
		{
			
			if (e.RightButtonIsPressed && !e.Handled)
			{
				//Microsoft.Msagl.Point  m_MouseRightButtonDownPoint = (ProtocolStatesViewer as Microsoft.Msagl.Drawing.IViewer).ScreenToSource(e);

				if (UserControlProtocol == null || UserControlProtocol.Status == Protocol.Statuses.EndedSuccessfully ||
				    UserControlProtocol.Status == Protocol.Statuses.Stopped)
				{
					ProtocolViewercontextMenuStrip.Show(this, new System.Drawing.Point(e.X, e.Y));
				}
			}
		}
		
		void ProtocolStatesViewerMouseUp(object sender, Microsoft.Msagl.Drawing.MsaglMouseEventArgs e)
		{
//			  object obj = ProtocolStatesViewer.GetObjectAt(e.X, e.Y);
			//            Microsoft.Msagl.Drawing.Node node = null;
			//            Microsoft.Msagl.Drawing.Edge edge = null;
			//            Microsoft.Msagl.GraphViewerGdi.DNode dnode = obj as Microsoft.Msagl.GraphViewerGdi.DNode;
			//            Microsoft.Msagl.GraphViewerGdi.DEdge dedge = obj as Microsoft.Msagl.GraphViewerGdi.DEdge;
			//            Microsoft.Msagl.GraphViewerGdi.DLabel dl = obj as Microsoft.Msagl.GraphViewerGdi.DLabel;
			//            if (dnode!=null)
			//                node = dnode.DrawingNode;
			//            else if (dedge!=null)
			//                edge = dedge.DrawingEdge;
			//            else if (dl!=null) {
			//                if (dl.Parent is Microsoft.Msagl.GraphViewerGdi.DNode)
			//                    node = (dl.Parent as Microsoft.Msagl.GraphViewerGdi.DNode).DrawingNode;
			//                else if (dl.Parent is Microsoft.Msagl.GraphViewerGdi.DEdge)
			//                    edge = (dl.Parent as Microsoft.Msagl.GraphViewerGdi.DEdge).DrawingEdge;
			//            }
			//            if (node != null) {
			//                ShowEditorDelegate(node);
			//            } else if (edge != null) {
			//                ShowEditorDelegate(edge);
			//            } else {
			//                CloseEditorDelegate();
			//            }

		}
	}
}




