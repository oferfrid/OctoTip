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
using OctoTip.OctoTipPlus.Logging;

namespace OctoTip.OctoTipPlus
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			InitAvailableLoggers();

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
			for(int i=1;i<ProtocolPanel.Controls.Count;i++)
			{
				Control LastProtocolUserControl = ProtocolPanel.Controls[i-1];
				ProtocolPanel.Controls[i].Location =   new Point(LastProtocolUserControl.Left , LastProtocolUserControl.Bottom);
			}
			//TODO: update toolStripStatus
			//toolStripStatusLabelAllProtocolCount.Text = "All Protocol:" +ProtocolPanel.Controls.Count;
			
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
		
		
		static public RobotJobsQueue FormRobotJobsQueue;
		BindingSource BS = new BindingSource();
		
		static  private volatile RobotJob RuningJob;
		
		#endregion
		
		#region Exception handling
		
		private List<Logging.Logger> AvailableLoggers = new List<Logging.Logger>();
		
		private void InitAvailableLoggers()
		{
			
			AvailableLoggers.Add(new Logging.EventLogLogger());
			AvailableLoggers.Add(new Logging.DebugLogger());
			AvailableLoggers.Add(new Logging.GoogleSpreadsheetLogger());
			

			System.Diagnostics.Debug.WriteLine(	AvailableLoggers[0].LoggerName);
			
			ActiveLoggersCheckedListBox.DataSource = AvailableLoggers;
			ActiveLoggersCheckedListBox.DisplayMember = "LoggerName";
			ActiveLoggersCheckedListBox.ValueMember = "ThisLogger";
			
			ActiveLoggersCheckedListBox.Refresh();

		}
		
		public void Notify(LoggingEntery LE)
		{
			//get selected notifyer
			
			System.Windows.Forms.CheckedListBox.CheckedItemCollection SelectedLoggers =  ActiveLoggersCheckedListBox.CheckedItems;
			
			foreach (Logging.Logger L in SelectedLoggers)
			{
				L.Log(LE);
			}
		}
		
		public void Notify(string Subject,string Message)
		{
			System.Diagnostics.Debug.WriteLine(Subject + " " + Message);
			throw new NotImplementedException("dd");
		}
		void CreateErrorButtonClick(object sender, EventArgs e)
		{
			Notify(new LoggingEntery("OctoTipPlus Appilcation",this.Name,"Test Error","Test 1234",LoggingEntery.EnteryTypes.Critical));
		}
		#endregion
		
		
		
	}
}
