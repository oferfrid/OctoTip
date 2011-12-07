/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 01/11/2011
 * Time: 19:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipExperimentControl
{
	/// <summary>
	/// Description of ProtocolLog.
	/// </summary>
	public partial class ProtocolLogForm : Form
	{
		
		private LogString myLogger ;
		private string ProtocolName;
		
		public ProtocolLogForm(string ProtocolName )
		{
			
			InitializeComponent();
			 myLogger = LogString.GetLogString(ProtocolName);
			this.ProtocolName = ProtocolName;
			
			
			
		}
		
		void ProtocolLogLoad(object sender, EventArgs e)
		{
			textBoxProtocolLog.MaxLength = myLogger.MaxChars + 100;
			textBoxProtocolLog.Text = myLogger.Log;
			// Add update callback delegate
			myLogger.OnLogUpdate += new LogString.LogUpdateDelegate(this.LogUpdate);
			Text = ProtocolName;
		}
		
				// Updates that come from a different thread can not directly change the
		// TextBox component. This must be done through Invoke().
		private delegate void UpdateDelegate();
		private void LogUpdate()
		{
			Invoke(new UpdateDelegate(
				delegate
				{
					textBoxProtocolLog.Text = myLogger.Log;
					//TODO: Quick-and-dirty solution for updating the Q
					//UpdateRobotJobsQueue();
				})
			      );
		}
		
		void ProtocolLogFormFormClosed(object sender, FormClosedEventArgs e)
		{
			myLogger.OnLogUpdate -= new LogString.LogUpdateDelegate(this.LogUpdate);
		}
	}
}
