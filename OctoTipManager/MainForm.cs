﻿/*
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
using System.Windows.Forms;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml.Serialization;

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
		
		
		static public RobotJobsQueue RJQ;
		ServiceHost host = null;
		Uri baseAddress = new Uri("http://localhost:"+ ConfigurationManager.AppSettings["ListeningPort"] +"/RobotJobsQueueListener");
		
		
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
			
			BindRobotJobsQueue();
			
			RJQ = new RobotJobsQueue();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			txtLog.Text = myLogger.Log;
		}
		
		
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
					BindRobotJobsQueue();
				})
			      );
		}

		
		
		private void BindRobotJobsQueue()
		{

			BindingSource BS = new BindingSource();
			BS.DataSource =RJQ;
			
			
			
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
			column.DataPropertyName = "ScriptFilePath";
			column.Name = "Script File Path";
			column.DefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
			dataGridViewRobotJobsQueue.Columns.Add(column);
			//TODO: AutoResizeColumns
			//dataGridViewRobotJobsQueue.AutoResizeColumns(  DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
		}
		
		
		private void WriteRobotJobQueue2File(string fileName)
		{
			XmlSerializer writer =	new System.Xml.Serialization.XmlSerializer(typeof(RobotJobsQueue));
			System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
			writer.Serialize(file,RJQ );
			file.Close();
		}
		
				private void LoadRobotJobQueueFile(string fileName)
		{
			XmlSerializer reader =	new XmlSerializer(typeof(RobotJobsQueue));
			System.IO.StreamReader file = new System.IO.StreamReader(fileName);
			//RobotJobsQueue S = new RobotJobsQueue();
			RJQ = (RobotJobsQueue)reader.Deserialize(file);
			BindRobotJobsQueue();
		}
		
		#endregion
		
		

		
		
		#region Event handeling
		
		void ToolStripButtonRefreshQueueClick(object sender, EventArgs e)
		{
			BindRobotJobsQueue();
		}
		
		void ToolStripButtonRemoveJobClick(object sender, EventArgs e)
		{
			
			
			foreach (DataGridViewRow Row in dataGridViewRobotJobsQueue.SelectedRows)
			{
				RJQ.Remove((RobotJob)Row.DataBoundItem);
			}
			BindRobotJobsQueue();
			
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
				
				
				//debug me
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
		void EToolStripMenuItemExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
		{
			WriteRobotJobQueue2File("temp.xml"	);
		}
		
		
		void LoadToolStripMenuItemClick(object sender, EventArgs e)
		{
			LoadRobotJobQueueFile("temp.xml");
		}
		#endregion
		

		
		
		
		
		
	}
	
	
}
