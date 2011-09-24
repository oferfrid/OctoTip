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
using System.Windows.Forms;
using System.Configuration;

using System.ServiceModel;
using System.ServiceModel.Description;

using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipManager
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public const string LOG_NAME = "OctoTipManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		
		
		public RobotJobsQueue RJQ;
		ServiceHost host = null;
		Uri baseAddress = new Uri("http://localhost:"+ ConfigurationManager.AppSettings["ListeningPort"] +"/hello");
		
		
		public MainForm()
		{
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			txtLog.ScrollBars = ScrollBars.Both; // use scroll bars; no text wrapping
			txtLog.MaxLength = myLogger.MaxChars + 100;
			// Add update callback delegate
			myLogger.OnLogUpdate += new LogString.LogUpdateDelegate(this.LogUpdate);
			
			
			OctoTip.OctoTipLib.RobotJob RP = new OctoTip.OctoTipLib.RobotJob(@"C:\Users\Public\Documents\Learn\BioLab\programing\OctoTip\SampleData\" + "NewScript1.esc");
			
			RP.RobotJobParameters = new List<RobotJobParameter>();
			RP.RobotJobParameters.Add(new RobotJobParameter("rr",RobotJobParameterType.String,"Fdsgs"));
			RP.RobotJobParameters.Add(new RobotJobParameter("N",RobotJobParameterType.Number,444));
			RP.TestJob();
			RJQ = new OctoTipLib.RobotJobsQueue();
			RJQ.InsertRobotJob(RP);
			
			OctoTip.OctoTipLib.RobotJob RP1 = new OctoTip.OctoTipLib.RobotJob(@"C:\Users\Public\Documents\Learn\BioLab\programing\OctoTip\SampleData\" + "NewScript2.esc",0.7);
			RJQ.InsertRobotJob(RP1);
			
			OctoTip.OctoTipLib.RobotJob RP2 = new OctoTip.OctoTipLib.RobotJob(@"C:\Users\Public\Documents\Learn\BioLab\programing\OctoTip\SampleData\" + "NewScript2.esc",0.1);
			RJQ.InsertRobotJob(RP2);
			
			BindRobotJobsQueue();
			
		}
		
		private void BindRobotJobsQueue()
		{

			dataGridViewRobotJobsQueue.AutoGenerateColumns = false;
			dataGridViewRobotJobsQueue.DataSource = RJQ;


			//dataGridViewRobotJobsQueue.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
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
			column.DataPropertyName = "ScriptFilePath";
			column.Name = "Script File Path";
			column.DefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
			dataGridViewRobotJobsQueue.Columns.Add(column);
			//TODO: AutoResizeColumns
			//dataGridViewRobotJobsQueue.AutoResizeColumns(  DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
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

				// Open the ServiceHost to start listening for messages. Since
				// no endpoints are explicitly configured, the runtime will create
				// one endpoint per base address for each service contract implemented
				// by the service.
				host.Open();
				myLogger.Add("Lisener Opened at: " + baseAddress.ToString());
				checkBoxServerState.Text = "Stop Server";
				
			}
			else
			{
				host.Close();
				myLogger.Add("Lisener Closed");
				checkBoxServerState.Text = "Start Server";
			}
		}
		
		
		
		void ClearLogButtonClick(object sender, EventArgs e)
		{
			 myLogger.Clear();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			 txtLog.Text = myLogger.Log;
		}
		// Updates that come from a different thread can not directly change the
        // TextBox component. This must be done through Invoke().
        private delegate void UpdateDelegate();
        private void LogUpdate()
        {
            Invoke(new UpdateDelegate(
                delegate
                {
                    txtLog.Text = myLogger.Log;
                })
            );
        }
	}
	
	
}
