/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/04/2014
 * Time: 11:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using OctoTip.Lib.ExperimentsCore;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib;
using OctoTip.Lib.Logging;

namespace OctoTip.OctoTipPlus
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			MainFormLog = Log.Instance;
			MainFormLog.LogEnteryCreated += Notify;
			RJQ = RobotJobsQueue.Instance;
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			InitAvailableLoggers();
			InitRobot();
			UpdateRobotJobsQueue();
			
		}
		
		
		#region Protocols
		private void AddAvailableProtocols()
		{	//
			List<Assembly> UncompitbleTypes = ProtocolProvider.GetUncompitbleProtocolPlugIns();
			if (UncompitbleTypes.Count>0)
			{
				string Massege = string.Empty;
				foreach(Assembly A in UncompitbleTypes)
				{
					Massege +=string.Format("The suplied dll {0} is not compatible with the current version, and was not loaded\n",A.GetName());
				}
				
				MessageBox.Show(Massege,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
			
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			
			List<Type> ProtocolsData =  ProtocolProvider.GetAvalbleProtocolPlugIns();
			
			ToolStripItem[] ToolStripProtocols =new ToolStripItem[ProtocolsData.Count];
			
			
			// remove all items but the first 3. (refresh btn etc..)
			for (int i=3;i<(this.ProtocolsToolStrip.Items.Count);i++)
			{
				this.ProtocolsToolStrip.Items.RemoveAt(i);
			}
			
			
			foreach(Type ProtocolData in ProtocolsData)
			{
				ToolStripButton BTN = new ToolStripButton(
					((ProtocolAttribute)ProtocolData.GetCustomAttributes(typeof(ProtocolAttribute), true)[0]).ShortName, ((System.Drawing.Image)(resources.GetObject("Protocol1.Image"))));
				BTN.ImageTransparentColor = System.Drawing.Color.Magenta;
				BTN.Size = new System.Drawing.Size(88, 20);
				BTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
				BTN.ToolTipText = ((ProtocolAttribute)ProtocolData.GetCustomAttributes(typeof(ProtocolAttribute), true)[0]).Description;
				BTN.Click += new System.EventHandler(this.AvailableProtocolClick);
				BTN.Tag = ProtocolData;
				
				this.ProtocolsToolStrip.Items.Add(BTN);
			}
		}
		
		
		void AvailableProtocolClick(object sender, EventArgs e)
		{
			Type ProtocolType;
			if (!(((ToolStripButton)sender).Tag==null))
			{
				ProtocolType = (Type)((ToolStripButton)sender).Tag;
				AddProtocolUserControl(ProtocolType);
			}
			
		}
		
		private void AddProtocolUserControl(Type ProtocolType)
		{
			ProtocolUserControl P = new ProtocolUserControl(ProtocolType);
			this.ProtocolPanel.Controls.Add(P);
			RefreshProtocolUserControls();
		}
		
		private void AddProtocolUserControl(Protocol NewProtocol)
		{
			ProtocolUserControl P = new ProtocolUserControl(NewProtocol);
			this.ProtocolPanel.Controls.Add(P);
			RefreshProtocolUserControls();
		}
		
		public void RefreshProtocolUserControls()
		{
			int ProtocolsCount = 0;
			int RuningProtocols = 0;
			
			foreach(Control UC in ProtocolPanel.Controls)
			{
				ProtocolUserControl PUC = (ProtocolUserControl)UC;
				if (PUC.Visible)
				{
					ProtocolsCount++;
					if (PUC.ProtocolStatus != Protocol.Statuses.Stopped && PUC.ProtocolStatus != Protocol.Statuses.EndedSuccessfully)
					{
						RuningProtocols++;
					}
				}
			}
			
			for(int i=1;i<ProtocolPanel.Controls.Count;i++)
			{
				Control LastProtocolUserControl = ProtocolPanel.Controls[i-1];
				ProtocolPanel.Controls[i].Location =   new Point(LastProtocolUserControl.Left , LastProtocolUserControl.Bottom);
			}

						
			//TODO: update toolStripStatus
			
			ProtocolsCountToolStripStatusLabel.Text = string.Format("Active Protocols: {0:0}" ,ProtocolsCount);
			RuningProtocolsToolStripStatusLabel.Text = string.Format("Runing Protocols: {0:0}" ,RuningProtocols);
		}
		
		private List<Protocol>  Protocols = new List<Protocol>();
		
		
		public void AddProtocol(Protocol Protocol2Add)
		{
			Protocols.Add(Protocol2Add);
			//TODO: update toolStripStatus
			//toolStripStatusLabelProtocolCount.Text = "Active Protocols:" + Protocols.Count;
		}
		public void RemoveProtocol(Protocol Protocol2Remove)
		{
			Protocols.Remove(Protocol2Remove);
			//TODO: update toolStripStatus
			//toolStripStatusLabelProtocolCount.Text = "Active Protocols:" + Protocols.Count;
		}
		
		void ToolStripButtonRefreshProtocolClick(object sender, EventArgs e)
		{
			AddAvailableProtocols();
		}
		#endregion
		
		#region Robot
		
		private RobotWorker FormRobotWorker ;
		
		
		public RobotJobsQueue RJQ;
		BindingSource BS = new BindingSource();
		
		static  private volatile RobotJob RuningJob;
		
		private void InitRobot()
		{
			
			//init worker object
			FormRobotWorker = new RobotWorker();
			
			FormRobotWorker.StatusChanged += FormRobotWorker_StatusChanged;
			BindRobotJobsQueue();
		}
		
		
		void ButtonRobotStartClick(object sender, EventArgs e)
		{
			FormRobotWorker.RequestStart();
		}
		
		private void BindRobotJobsQueue()
		{
			dataGridViewRobotJobsQueue.Columns.Clear();
			DataGridViewColumn column;
			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "Priority";
			column.Name = "Priority";
			column.DefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
			dataGridViewRobotJobsQueue.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "JobStatus";
			column.Name = "JobStatus";
			column.DefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
			dataGridViewRobotJobsQueue.Columns.Add(column);
			
			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "ScriptName";
			column.Name = "Script Name";
			column.DefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
			dataGridViewRobotJobsQueue.Columns.Add(column);
			
			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "RobotJobDisplayParameters";
			column.Name = "Parameters";
			column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			dataGridViewRobotJobsQueue.Columns.Add(column);
			
			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "UniqueID";
			column.Name = "Unique ID";
			column.DefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
			dataGridViewRobotJobsQueue.Columns.Add(column);
		}
		
		private void  UpdateRobotJobsQueue()
		{
			if (CheckForIllegalCrossThreadCalls && (RJQ.Count==0))
			{
				dataGridViewRobotJobsQueue.DataSource = null;
			}
			else
			{
				BS.DataSource =RJQ ;
				dataGridViewRobotJobsQueue.AutoGenerateColumns = false;
				BS.Filter = "JobStatus = 'Queued'";
				BS.Sort = "JobStatus ASC , Priority ASC";
				dataGridViewRobotJobsQueue.DataSource = BS;				
			}
		
		}
		
		
		void FormRobotWorker_StatusChanged(object sender, RobotWorkerStatusChangeEventArgs e)
		{
			
			string Message;
			if(e.RobotWorkerStatus==RobotWorker.RobotWorkerStatus.RunningJob && e.CurrentJob!=null)
			{
				Message  = string.Format("{0}-{1}({2}), parameters:{3}) ,{4}" , e.RobotWorkerStatus,e.CurrentJob.ScriptName,e.CurrentJob.UniqueID,e.CurrentJob.RobotJobDisplayParameters,e.Message);
			}
			else
			{
				
				Message  = string.Format("{0} - {1}" , e.RobotWorkerStatus,e.Message);
			}
			Notify(new LoggingEntery("OctoTipPlus Appilcation","RobotWorker","RunningJob",Message,LoggingEntery.EnteryTypes.Informational));
			
			
			if (e.CurrentJob == null)
			{
				RuningJob = null;
				UpdateRunningJob();
			}
			else
			{
				
				RuningJob = e.CurrentJob;
				UpdateRunningJob();
			}
			
			bool buttonPauseEnabled ;
			bool buttonStartEnabled ;
			bool buttonStopEnabled;
			
			string textBoxRuningJobStatusText = string.Empty;
			string textRobotWorkerStatusText = string.Empty;
			
			textRobotWorkerStatusText = e.RobotWorkerStatus.ToString();
			
			switch(e.RobotWorkerStatus)
			{
				case(RobotWorker.RobotWorkerStatus.Stopped):
					buttonPauseEnabled = false;
					buttonStartEnabled = true;
					buttonStopEnabled = false;
					break;
				case(RobotWorker.RobotWorkerStatus.Paused):
					buttonPauseEnabled = false;
					buttonStartEnabled = true;
					buttonStopEnabled = true;
					break;
				case(RobotWorker.RobotWorkerStatus.Stopping ):
					buttonPauseEnabled = false;
					buttonStartEnabled = false;
					buttonStopEnabled = false;
					break;
				case( RobotWorker.RobotWorkerStatus.Pausing):
					buttonPauseEnabled = false;
					buttonStartEnabled = false;
					buttonStopEnabled = false;
					break;
				default:
					buttonPauseEnabled = true;
					buttonStartEnabled = false;
					buttonStopEnabled = true;
					break;
					
			}
			
			if (!this.IsDisposed)
			{
				MethodInvoker buttonPauseInvoker = delegate
				{
					buttonRobotPause.Enabled = buttonPauseEnabled ;
				};
				buttonRobotPause.BeginInvoke(buttonPauseInvoker);
				
				MethodInvoker buttonStartInvoker = delegate
				{
					buttonRobotStart.Enabled = buttonStartEnabled ;
				};
				buttonRobotStart.BeginInvoke(buttonStartInvoker);
				
				MethodInvoker buttonStopInvoker = delegate
				{
					buttonRobotStop.Enabled = buttonStopEnabled ;
				};
				buttonRobotStop.BeginInvoke(buttonStopInvoker);
				
				MethodInvoker textBoxRuningJobStatusInvoker = delegate
				{
					RunningJobStatus.Text = textBoxRuningJobStatusText ;
				};
				RunningJobStatus.BeginInvoke(textBoxRuningJobStatusInvoker);
				
				MethodInvoker textRobotWorkerStatusInvoker = delegate
				{
					RobotStatuslabel.Text = textRobotWorkerStatusText ;
				};
				RunningJobStatus.BeginInvoke(textRobotWorkerStatusInvoker);
			}
			
		}
		
		private void UpdateRunningJob()
		{
			string RunningJobNameText;
			string RunningJobStatusText;
			if (RuningJob!=null)
			{
				RunningJobNameText = RuningJob.ScriptName;
				RunningJobStatusText = RuningJob.JobStatus.ToString();
			}
			else
			{
				RunningJobNameText = string.Empty;
				RunningJobStatusText = string.Empty;
			}
			
			if(!this.IsDisposed)
			{
				MethodInvoker UpdateRunningJobName = delegate
				{
					RunningJobName.Text = RunningJobNameText ;
				};
				RunningJobName.BeginInvoke(UpdateRunningJobName);
				
				MethodInvoker UpdateRunningJobStatus = delegate
				{
					RunningJobStatus.Text = RunningJobStatusText ;
				};
				RunningJobStatus.BeginInvoke(UpdateRunningJobStatus);
			}
			
		}
		
		void RefreshToolStripButtonClick(object sender, EventArgs e)
		{
			UpdateRobotJobsQueue();
		}
		
		#endregion
		
		
		
		#region Exception handling
		private Log MainFormLog;
		
		private List<Logger> AvailableLoggers = new List<Logger>();
		
		private void InitAvailableLoggers()
		{
			
			AvailableLoggers.Add(new EventLogLogger());
			AvailableLoggers.Add(new DebugLogger());
			AvailableLoggers.Add(new GoogleSpreadsheetLogger());
			
			
			ActiveLoggersCheckedListBox.DataSource = AvailableLoggers;
			ActiveLoggersCheckedListBox.DisplayMember = "LoggerName";
			ActiveLoggersCheckedListBox.ValueMember = "ThisLogger";
			
			ActiveLoggersCheckedListBox.Refresh();

		}
		
		public void Notify(LoggingEntery LE)
		{
			//get selected notifyer
			
			System.Windows.Forms.CheckedListBox.CheckedItemCollection SelectedLoggers =  ActiveLoggersCheckedListBox.CheckedItems;
			
			foreach (Logger L in SelectedLoggers)
			{
				L.Log(LE);
			}
		}
		
		public void Notify(object sender, LogEnteryCreatedEventArgs e)
		{
			Notify(e.LE);
		}
		

		void CreateErrorButtonClick(object sender, EventArgs e)
		{
			
			Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation",this.Name,"Test Error","Test 1234",LoggingEntery.EnteryTypes.Critical));
		}
		#endregion
		
	}
}
