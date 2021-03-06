﻿/*
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
			AddAvailableProtocols();
			
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if(MessageBox.Show("Are you sure you want to exit",this.Text, MessageBoxButtons.YesNo,MessageBoxIcon.Question)!= DialogResult.Yes)
			{
				e.Cancel=true;
			}
		}
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Close();
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
			int ProtocolsToolStripItemsCount = this.ProtocolsToolStrip.Items.Count;
			for (int i=ProtocolsToolStripItemsCount;i>3;i--)
			{
				this.ProtocolsToolStrip.Items.RemoveAt(i-1);
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

			
			ProtocolsCountToolStripStatusLabel.Text = string.Format("Active Protocols: {0:0}" ,ProtocolsCount);
			RuningProtocolsToolStripStatusLabel.Text = string.Format("Runing Protocols: {0:0}" ,RuningProtocols);
		}
		
		private List<Protocol>  Protocols = new List<Protocol>();
		
		
		public void AddProtocol(Protocol Protocol2Add)
		{
			Protocols.Add(Protocol2Add);
		}
		public void RemoveProtocol(Protocol Protocol2Remove)
		{
			Protocols.Remove(Protocol2Remove);
			
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
		
		void ButtonRobotPauseClick(object sender, EventArgs e)
		{
			FormRobotWorker.RequestPause();
		}
		
		void ButtonRobotStopClick(object sender, EventArgs e)
		{
			FormRobotWorker.RequestStop();
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
			Notify(new LoggingEntery("OctoTipPlus Appilcation","RobotWorker","RunningJob",Message,LoggingEntery.EnteryTypes.Debug));
			
			
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
			
			textRobotWorkerStatusText = RobotWorker.GetRobotWorkerStatusText(e.RobotWorkerStatus);
			
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
		
		void ActivateJobToolStripButtonClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow Row in dataGridViewRobotJobsQueue.SelectedRows)
			{
				
				RobotJob RJ = (RobotJob)Row.DataBoundItem;
				RJ.JobStatus = RobotJob.Status.Queued;
			}
		}
		
		void DeactivateJobToolStripButtonClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow Row in dataGridViewRobotJobsQueue.SelectedRows)
			{
				
				RobotJob RJ = (RobotJob)Row.DataBoundItem;
				RJ.JobStatus = RobotJob.Status.TerminatedByUser;
			}
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
			AvailableLoggers.Add(new EmailLogger());
			AvailableLoggers.Add(new FileLogger());
			
			
			
			
			
			
			try {
				
				ActiveLoggersCheckedListBox.DataSource = AvailableLoggers;
			} 
			catch (NullReferenceException)
			{
				
			}
			ActiveLoggersCheckedListBox.DisplayMember = "LoggerName";
			ActiveLoggersCheckedListBox.ValueMember = "ThisLogger";
			
			ActiveLoggersCheckedListBox.Refresh();
			
			for (int i = 0; i < ActiveLoggersCheckedListBox.Items.Count; i++)
            {
                Logger obj = (Logger)ActiveLoggersCheckedListBox.Items[i];
                ActiveLoggersCheckedListBox.SetItemChecked(i, obj.IsChecked);
            }

			
		}
		
		void ActiveLoggersCheckedListBoxMouseDown(object sender, MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Right )
			{
				//select the item under the mouse pointer
				ActiveLoggersCheckedListBox.SelectedIndex = ActiveLoggersCheckedListBox.IndexFromPoint( e.Location );
				if ( ActiveLoggersCheckedListBox.SelectedIndex != -1)
				{
					LoggersContextMenuStrip.Show();
				}
			}
			

		}
		
		public void Notify(LoggingEntery LE)
		{
			//print to ErrorExtendedRichTextBox
			Color TextColor;
			switch (LE.EnteryType)
			{
				case LoggingEntery.EnteryTypes.Debug:
				TextColor = Color.Green;
				break;
				case LoggingEntery.EnteryTypes.Informational:
					TextColor = Color.Black;
					break;
				case LoggingEntery.EnteryTypes.Warning:
					TextColor = Color.Yellow;
					break;
				case LoggingEntery.EnteryTypes.Error:
					TextColor = Color.DarkRed;
					break;
				case LoggingEntery.EnteryTypes.Critical:
					TextColor = Color.Red;
					break;
				default:
					TextColor = Color.Black;
					break;
			}
			
			MethodInvoker LastErrorTextBoxInvoker = delegate
			{
				string Message = string.Format("{0} {1} ({2})\n{3}\n*************\n",DateTime.Now,LE.Title,LE.EnteryType,LE.Message);
				ErrorExtendedRichTextBox.AppendText(Message,TextColor);
			};
			ErrorExtendedRichTextBox.BeginInvoke(LastErrorTextBoxInvoker);
			
			
			//get selected notifyer
			
			System.Windows.Forms.CheckedListBox.CheckedItemCollection SelectedLoggers =  ActiveLoggersCheckedListBox.CheckedItems;
			foreach (Logger L in SelectedLoggers)
			{
				if (L.LoggigLevel >= (int)LE.EnteryType)
				{
					try
					{
						L.Log(LE);
					}
					catch (Exception e)
					{
						System.Diagnostics.Debug.WriteLine(e.ToString());
						
					}
				}
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
		
		
		void LoggersContextMenuStripOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Logger ContextLogger = (Logger)ActiveLoggersCheckedListBox.SelectedItem;
			
			LoggersContextMenuStrip.Items.Clear();
			this.LoggersContextMenuStrip.Items.Add("Log Level");
			
			//add LogLoggingLevelComboBox
			ToolStripComboBox LoggerLoggingLevelComboBox;
			LoggerLoggingLevelComboBox =  new System.Windows.Forms.ToolStripComboBox();
			LoggerLoggingLevelComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			foreach (string name in Enum.GetNames(typeof(LoggingEntery.EnteryTypes)))
			{
				LoggerLoggingLevelComboBox.Items.Add(name);
			}
			
			LoggerLoggingLevelComboBox.SelectedIndex = ContextLogger.LoggigLevel;
			LoggerLoggingLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.LoggerLoggingLevelComboBoxIndexChanged);
			
			this.LoggersContextMenuStrip.Items.Add(LoggerLoggingLevelComboBox);
			
			//
			if (ContextLogger.ExtraData!=null)
			{
				ToolStripTextBox ExtraDataTextBox = new ToolStripTextBox();
				ExtraDataTextBox.Text = ContextLogger.ExtraData;
				ExtraDataTextBox.TextChanged += new System.EventHandler(this.LoggerExtraDataTextChanged);
				this.LoggersContextMenuStrip.Items.Add(ExtraDataTextBox);
				
			}
		}
		
		void LoggerExtraDataTextChanged(object sender, EventArgs e)
		{
			
			((Logger)ActiveLoggersCheckedListBox.SelectedItem).ExtraData = ((ToolStripTextBox)sender).Text;
		}
		void LoggerLoggingLevelComboBoxIndexChanged(object sender, EventArgs e)
		{
			
			((Logger)ActiveLoggersCheckedListBox.SelectedItem).LoggigLevel = ((ToolStripComboBox)sender).SelectedIndex;
		}
		
		void ErrorExtendedRichTextBoxDoubleClick(object sender, EventArgs e)
		{
			ShowLogForm ShowLogForm = new ShowLogForm(ErrorExtendedRichTextBox.Rtf);
			ShowLogForm.ShowDialog();
		}
		
		#endregion
		
		
		
		
		
		
	}
}
