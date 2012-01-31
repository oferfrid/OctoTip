/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 21/09/2011
 * Time: 11:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml.Serialization;
using OctoTip.Lib;

namespace OctoTip.Manager
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public const string LOG_NAME = "OctoTipManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		
		
		ServiceHost host = null;
		Uri baseAddress;
		
		private RobotWorker FormRobotWorker ;
		
		
		static public RobotJobsQueue FormRobotJobsQueue;
		static public Dictionary<Guid, OctoTip.Lib.RobotJob.Status> FormRobotJobsQueueHestoryDictionary;
		
		
		
		
		public MainForm()
		{
			InitializeComponent();
			
			
			FormRobotJobsQueue = new RobotJobsQueue();
			FormRobotJobsQueueHestoryDictionary =new Dictionary<Guid, RobotJob.Status>();
			
			string ListeningPort = ConfigurationManager.AppSettings["ListeningPort"];
			if (ListeningPort == null)
			{
				throw new System.NullReferenceException("AppSetting ListeningPort is null");
			}
			baseAddress = new Uri("http://localhost:"+ ListeningPort +"/RobotJobsQueueListener");
			
			txtLog.ScrollBars = ScrollBars.Both; // use scroll bars; no text wrapping
			txtLog.MaxLength = myLogger.MaxChars + 100;
			// Add update callback delegate
			myLogger.OnLogUpdate += new LogString.LogUpdateDelegate(this.LogUpdate);
			
			BindRobotJobsQueue();
			
			
			//init worker object
			FormRobotWorker = new RobotWorker();
			
			FormRobotWorker.StatusChanged += FormRobotWorker_StatusChanged;
		}
		
		
		void MainFormLoad(object sender, EventArgs e)
		{
			txtLog.Text = myLogger.Log;
		}
		
		#region Job List related
		
		
		
		#endregion
		
		#region Private mathods
		
		// Updates that come from a different thread can not directly change the
		// TextBox component. This must be done through Invoke().
		private delegate void UpdateDelegate();
		private void LogUpdate()
		{
			Invoke(new UpdateDelegate(
				delegate
				{
					txtLog.Text = myLogger.Log;
					//TODO: Quick-and-dirty solution for updating the Q
					//UpdateRobotJobsQueue();
				})
			      );
		}

		private void BindRobotJobsQueue()
		{

			BindingSource BS = new BindingSource();
			BS.DataSource =FormRobotJobsQueue ;
			
			dataGridViewRobotJobsQueue.AutoGenerateColumns = false;
			dataGridViewRobotJobsQueue.DataSource = BS;

			dataGridViewRobotJobsQueue.Columns.Clear();
			DataGridViewColumn column;
			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "Priority";
			column.Name = "Priority";
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
			//TODO: AutoResizeColumns
			//dataGridViewRobotJobsQueue.AutoResizeColumns(  DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
		}
		
		private void UpdateRobotJobsQueue()
		{
			WriteRobotJobQueue2File("RobotJobsQueueState.xml");
			
			if (FormRobotJobsQueue .Count>0)
			{
				BindingSource BS = new BindingSource();
				BS.DataSource =FormRobotJobsQueue ;
				dataGridViewRobotJobsQueue.AutoGenerateColumns = false;
				
				dataGridViewRobotJobsQueue.DataSource = BS;
			}
			else
			{
				dataGridViewRobotJobsQueue.DataSource = null;
			}
		}
		
		
		private void WriteRobotJobQueue2File(string fileName)
		{
			XmlSerializer writer =	new System.Xml.Serialization.XmlSerializer(typeof(RobotJobsQueue));
			System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
			writer.Serialize(file,FormRobotJobsQueue  );
			file.Close();
		}
		
		private void LoadRobotJobQueueFile(string fileName)
		{
			XmlSerializer reader =	new XmlSerializer(typeof(RobotJobsQueue));
			System.IO.StreamReader file = new System.IO.StreamReader(fileName);
			//RobotJobsQueue S = new RobotJobsQueue();
			FormRobotJobsQueue  = (RobotJobsQueue)reader.Deserialize(file);
			file.Close();
			//UpdateRobotJobsQueue();
		}
		
		#endregion
		
		
		
		
		
		#region Event handeling
		
		
		
		
		
		void ToolStripButtonRefreshQueueClick(object sender, EventArgs e)
		{
			UpdateRobotJobsQueue();
		}
		
		void ToolStripButtonRemoveJobClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow Row in dataGridViewRobotJobsQueue.SelectedRows)
			{
				RobotJob RJ = (RobotJob)Row.DataBoundItem;
				RJ.JobStatus = RobotJob.Status.TerminatedByUser;
				FormRobotJobsQueueHestoryDictionary[RJ.UniqueID] = RJ.JobStatus;
				FormRobotJobsQueue.Remove((RobotJob)RJ);
				UpdateRobotJobsQueue();
			}

			
		}
		void ClearLogButtonClick(object sender, EventArgs e)
		{
			myLogger.Clear();
		}
		
		
		void CheckBoxServerStateCheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxServerState.Checked)
			{
				// Create the ServiceHost.
				host = new ServiceHost(typeof(RobotJobsQueueService), baseAddress);
				// Enable metadata publishing.
				ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
				host.Description.Behaviors.Add(smb);
				
				ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();

				// if not found - add behavior with setting turned on
				if (debug == null)
				{
					host.Description.Behaviors.Add(
						new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
				}
				else
				{
					// make sure setting is turned ON
					if (!debug.IncludeExceptionDetailInFaults)
					{
						debug.IncludeExceptionDetailInFaults = true;
					}
				}
				

				// Open the ServiceHost to start listening for messages. Since
				// no endpoints are explicitly configured, the runtime will create
				// one endpoint per base address for each service contract implemented
				// by the service.
				host.Open();
				myLogger.Add("Listener Opened at: " + baseAddress.ToString());
				checkBoxServerState.Text = "Stop Listener";
				
			}
			else
			{
				host.Close();
				myLogger.Add("Listener Closed");
				checkBoxServerState.Text = "Start Listener";
			}
		}
		
		
		void EToolStripMenuItemExitClick(object sender, EventArgs e)
		{
			
			Application.Exit();
		}
		
		void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
		{
			
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "XML File|*.XML";
			saveFileDialog1.Title = "Save a Job File";
			saveFileDialog1.ShowDialog();

			// If the file name is not an empty string open it for saving.
			if(saveFileDialog1.FileName != "")
			{
				WriteRobotJobQueue2File(saveFileDialog1.FileName);
			}
			
			
		}
		
		void LoadToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenFileDialog  OpenFileDialog1 = new OpenFileDialog ();
			OpenFileDialog1.Filter = "XML File|*.XML";
			OpenFileDialog1.Title = "Load a Job File";
			OpenFileDialog1.ShowDialog();

			// If the file name is not an empty string open it for saving.
			if(OpenFileDialog1.FileName != "")
			{
				LoadRobotJobQueueFile(OpenFileDialog1.FileName);
			}
		}
		
		



		
		
		void ButtonPauseClick(object sender, EventArgs e)
		{
			FormRobotWorker.RequestPause();
		}
		

		void ButtonStartClick(object sender, EventArgs e)
		{
			FormRobotWorker.RequestStart();
		}
		
		void ButtonStopClick(object sender, EventArgs e)
		{
			
			FormRobotWorker.RequestStop();
			buttonStop.Enabled=false;
		}
		
		private string GetJobStatus(RobotJob Job)
		{
			string status = string.Format("{0}({1})>{2}",Job.ScriptName,Job.UniqueID,Job.JobStatus);
				return status;
		}
		

		void FormRobotWorker_StatusChanged(object sender, RobotWorkerStatusChangeEventArgs e)
		{
			if(e.RobotWorkerStatus==RobotWorker.RobotWorkerStatus.RunningJob)
			{
				if (e.CurrentJob == null)
				{
					myLogger.Add(string.Format("{0} ,{1}" , e.RobotWorkerStatus,e.Messege));
				}
				else
				{
					myLogger.Add(string.Format("{0}-{1}({2}), parameters:{3}) ,{4}" , e.RobotWorkerStatus,e.CurrentJob.ScriptName,e.CurrentJob.UniqueID,e.CurrentJob.RobotJobDisplayParameters,e.Messege));
				}
			}
			else
			{
				myLogger.Add(string.Format("{0} - {1}" , e.RobotWorkerStatus,e.Messege));
			}
			
			bool buttonPauseEnabled ;
			bool buttonStartEnabled ;
			bool buttonStopEnabled;
			
			string textBoxRuningJobStatusText = string.Empty;
			
			if(e.CurrentJob!=null)
			{
				textBoxRuningJobStatusText = GetJobStatus(e.CurrentJob);
				
				
				MainForm.FormRobotJobsQueueHestoryDictionary[e.CurrentJob.UniqueID] = e.CurrentJob.JobStatus;
				
			}
			
			
			
			
			
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
			MethodInvoker buttonPauseInvoker = delegate
			{
				buttonPause.Enabled = buttonPauseEnabled ;
			};
			buttonPause.BeginInvoke(buttonPauseInvoker);
			
			MethodInvoker buttonStartInvoker = delegate
			{
				buttonStart.Enabled = buttonStartEnabled ;
			};
			buttonPause.BeginInvoke(buttonStartInvoker);
			
			MethodInvoker buttonStopInvoker = delegate
			{
				buttonStop.Enabled = buttonStopEnabled ;
			};
			buttonPause.BeginInvoke(buttonStopInvoker);
			
			MethodInvoker textBoxRuningJobStatusInvoker = delegate
			{
				textBoxRuningJobStatus.Text = textBoxRuningJobStatusText ;
			};
			textBoxRuningJobStatus.BeginInvoke(textBoxRuningJobStatusInvoker);
			
			
			
			
		}
		



		
		#endregion
		
		void DataGridViewRobotJobsQueueDataError(object sender, DataGridViewDataErrorEventArgs anError)
		{
			//myLogger.Add("Error happened " + anError.Context.ToString());

			if (anError.Context == DataGridViewDataErrorContexts.Commit)
			{
				//myLogger.Add("Commit error");
			}
			if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
			{
				//myLogger.Add("Cell change");
			}
			if (anError.Context == DataGridViewDataErrorContexts.Parsing)
			{
				//myLogger.Add("parsing error");
			}
			if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
			{
				//myLogger.Add("leave control error");
			}

			if ((anError.Exception) is  System.Data.ConstraintException)
			{
				DataGridView view = (DataGridView)sender;
				view.Rows[anError.RowIndex].ErrorText = "an error";
				view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

				anError.ThrowException = false;
			}

		}
		
		
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			if(host.State != CommunicationState.Closed)
			{
				host.Close();
				myLogger.Add("Listener Closed");
			}
			if(FormRobotWorker.Status != RobotWorker.RobotWorkerStatus.Stopped)
			{
				FormRobotWorker.RequestStop();
			}
				
			
			
		}
	}
	
	
	
	
}



