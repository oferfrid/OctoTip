/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 25/09/2011
 * Time: 10:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using OctoTip.OctoTipExperiments.Core;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib;

//using System.Xml.Serialization;

namespace OctoTip.OctoTipExperimentControl
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public const string LOG_NAME = "OctoTipExperimentManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		
		private List<Protocol>  Protocols = new List<Protocol>();
		
		
		public void AddProtocol(Protocol Protocol2Add)
		{
			Protocols.Add(Protocol2Add);
			toolStripStatusLabelProtocolCount.Text = "Active Protocols:" + Protocols.Count;
		}
		public void RemoveProtocol(Protocol Protocol2Remove)
		{
			Protocols.Remove(Protocol2Remove);
			toolStripStatusLabelProtocolCount.Text = "Active Protocols:" + Protocols.Count;
		}
		
		
		
		
		public MainForm()
		{
			InitializeComponent();
			
			textBoxLog.ScrollBars = ScrollBars.Both; // use scroll bars; no text wrapping
			textBoxLog.MaxLength = myLogger.MaxChars + 100;
			// Add update callback delegate
			myLogger.OnLogUpdate += new LogString.LogUpdateDelegate(this.LogUpdate);
			
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			
				AddAvailableProtocols();
		}
		
		
		#region Private function
		
		
		// Updates that come from a different thread can not directly change the
		// TextBox component. This must be done through Invoke().
		private delegate void UpdateDelegate();
		private void LogUpdate()
		{
			

			Invoke(new UpdateDelegate(
				delegate
				{
					textBoxLog.Text = myLogger.Log;
					//TODO: Quick-and-dirty solution for updating the Q
					//UpdateRobotJobsQueue();
				})
			      );
			
		}
		
		private void AddAvailableProtocols()
		{	//
			List<Assembly> UncompitbleTypes = ProtocolProvider.GetUncompitbleProtocolPlugIns();
			if (UncompitbleTypes.Count>0)
			{
				string Massege = string.Empty;
				foreach(Assembly A in UncompitbleTypes)
				{
					Massege +=string.Format("The suplied dll {0} ({1}) is not compatible with the current version, and was not loaded",A.GetName(),A.Location);
				}
				
				MessageBox.Show(Massege,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
			
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			
			List<Type> ProtocolsData =  ProtocolProvider.GetAvalbleProtocolPlugIns();
			
			ToolStripItem[] ToolStripProtocols =new ToolStripItem[ProtocolsData.Count];
			
			this.ProtocoltoolStrip.Items.Clear();
			
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
				
				this.ProtocoltoolStrip.Items.Add(BTN);
			}
		}
		
		private void AddProtocolUserControl(Type ProtocolType)
		{
			ProtocolUserControl P	 = new ProtocolUserControl(ProtocolType);
			this.Protocolpanel.Controls.Add(P);
			RefreshProtocolUserControls();
		}
		
		private void AddProtocolUserControl(Protocol NewProtocol)
		{
			ProtocolUserControl P	 = new ProtocolUserControl(NewProtocol);
			this.Protocolpanel.Controls.Add(P);
			RefreshProtocolUserControls();
		}
		
		public void RefreshProtocolUserControls()
		{
			for(int i=1;i<Protocolpanel.Controls.Count;i++)
			{
				Control LastProtocolUserControl = Protocolpanel.Controls[i-1];
				Protocolpanel.Controls[i].Location =   new Point(LastProtocolUserControl.Left , LastProtocolUserControl.Bottom);
			}
			toolStripStatusLabelAllProtocolCount.Text = "All Protocol:" +Protocolpanel.Controls.Count;
			
		}
		
		private void WriteProtocols2File(string fileName)
		{
			XmlSerializer writer =	new XmlSerializer(typeof(List<Protocol>),ProtocolProvider.GetAvalbleTypes());
			System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
			writer.Serialize(file,Protocols );
			file.Close();
		}
		
		private void ReadProtocolsFromFile(string fileName)
		{
			XmlSerializer reader =	new XmlSerializer(typeof(List<Protocol>),ProtocolProvider.GetAvalbleTypes());
			System.IO.StreamReader file = new System.IO.StreamReader(fileName);
			List<Protocol> NewProtocols = (List<Protocol>)reader.Deserialize(file);
			file.Close();
			//Update Protocol List
			foreach(Protocol P in NewProtocols)
			{
				Protocols.Add(P);
				AddProtocolUserControl(P);
			}
			
		}
		
		#endregion
		
		#region Event Handeling
		void ToolStripButtonRefreshProtocolsClick(object sender, EventArgs e)
		{
			AddAvailableProtocols();
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
		
		void SaveProtocolsAsToolStripMenuItemClick(object sender, EventArgs e)
		{
			
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "XML File|*.XML";
			saveFileDialog1.Title = "Save Protocols File";
			saveFileDialog1.ShowDialog();

			// If the file name is not an empty string open it for saving.
			if(saveFileDialog1.FileName != "")
			{
				WriteProtocols2File(saveFileDialog1.FileName);
			}
			
		}
		void OpenToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenFileDialog  OpenFileDialog1 = new OpenFileDialog ();
			OpenFileDialog1.Filter = "XML File|*.XML";
			OpenFileDialog1.Title = "Load Protocols File";
			OpenFileDialog1.ShowDialog();

			// If the file name is not an empty string open it for saving.
			if(OpenFileDialog1.FileName != "")
			{
				ReadProtocolsFromFile(OpenFileDialog1.FileName);
			}
			
		}
		#endregion
		
		void ButtonClearLogClick(object sender, EventArgs e)
		{

			myLogger.Clear();
			
		}
		
	
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if(MessageBox.Show("Are you sure you want to exit",this.Text, MessageBoxButtons.YesNo,MessageBoxIcon.Question)!= DialogResult.Yes)
			{
				e.Cancel=true;
			}
		}
	}
}
