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

using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipManager
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public RobotJobsQueue RJQ;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			
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
        
        
        //dataGridViewRobotJobsQueue.AutoResizeColumns(  DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
        

			
		}
		
		
	}
	
	
}
