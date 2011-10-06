/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 27/09/2011
 * Time: 14:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using EVOAPILib;
using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipManager
{
	/// <summary>
	/// Description of RobotWrapper.
	/// </summary>
	public class RobotWrapper
	{
		private EVOAPILib.System Evo;
		int     ScriptID;
		
		public  const string LOG_NAME = "OctoTipManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		private volatile bool _ShouldStop = false;
		private volatile bool _ShouldPause = false;
		
			
		public RobotWrapper()
		{
			Evo = new EVOAPILib.SystemClass();
		}
		
		public event EventHandler<RobotWrapperEventArgs> StatusChangeEvent;
		
		protected virtual void OnStatusChangeEvent(RobotWrapperEventArgs e)
		{
			EventHandler<RobotWrapperEventArgs> handler = StatusChangeEvent;

			// Event will be null if there are no subscribers
			if (handler != null)
			{
				// Use the () operator to raise the event.
				handler(this, e);
			}
		}
		


		/// <summary>
		/// Logon Freedom Evo
		/// </summary>
		private void Logon()
		{			
			SC_Status RobotStatus;
			string UserName = ConfigurationManager.AppSettings["UserName"];
			string Password = ConfigurationManager.AppSettings["Password"];

	
			try
			{
				Evo.Logon(UserName,Password,0,0);
				RobotStatus = Evo.GetStatus();
				// initialazing
				if (RobotStatus == SC_Status.STATUS_NOTINITIALIZED)
				{
					Evo.Initialize();
				}
			}
			catch(Exception e)
			{
				myLogger.Add("Logon Error");
				myLogger.Add(e.ToString());
				throw e;
			}			
		}
		
		/// <summary>
		/// Logoff Freedom Evo
		/// </summary>
		private void Logoff()
		{
			try
			{
				Evo.Logoff();
			}
			catch(Exception e)
			{
				myLogger.Add("Logoff Error");
				myLogger.Add(e.ToString());
				throw e;
			}
		}
		
		/// <summary>
		/// Runs in a loop to check script execution status.
		/// Also checks if a pause or stop request arrived.
		/// </summary>
		private void CheckScriptStatus()
		{
			SC_ScriptStatus   ScriptStatusEx = SC_ScriptStatus.SS_UNKNOWN;
			SC_ScriptStatus   ScriptStatus   = SC_ScriptStatus.SS_UNKNOWN;
			ScriptStatuses  STS = ScriptStatuses.RuntimeError;
			DateTime               StartTime = DateTime.Now;
			TimeSpan                      TS = DateTime.Now - StartTime;

			try
			{				
				ScriptStatusEx = Evo.GetScriptStatusEx(ScriptID);
				ScriptStatus   = Evo.GetScriptStatus(ScriptID);
				while(ScriptStatus == SC_ScriptStatus.SS_BUSY && !_ShouldStop)
				{		
					
					System.Threading.Thread.Sleep(1000);
					// Handeling pause request
					if (_ShouldPause)
					{
						Evo.Pause();
						while (_ShouldPause)
						{
							System.Threading.Thread.Sleep(1000);
						}
						Evo.Resume();
					}
					ScriptStatus = Evo.GetScriptStatus(ScriptID);
				}				
				
				ScriptStatusEx = Evo.GetScriptStatusEx(ScriptID);
				
				// determain script termination status
				switch (ScriptStatusEx)
				{				
					case SC_ScriptStatus.SS_IDLE:
						STS = ScriptStatuses.Success;
					break;
					case SC_ScriptStatus.SS_BUSY:
						STS = ScriptStatuses.Running;
					break;	
					case SC_ScriptStatus.SS_STOPPED:
						STS = ScriptStatuses.TerminatedByUser;
					break;
					case SC_ScriptStatus.SS_ABORTED:
						STS = ScriptStatuses.TerminatedByUser;
					break;
					case SC_ScriptStatus.SS_STATUS_ERROR:
						STS = ScriptStatuses.Failed;
					break;
					default:
						STS = ScriptStatuses.RuntimeError;
					break;
				}
				
				// Handling Stop request
				if (_ShouldStop)
				{
					Evo.Stop();
					STS = ScriptStatuses.TerminatedByUser;
				}
				
			}
			catch (Exception e)
			{
				myLogger.Add("Run Script Error");
				myLogger.Add(e.ToString());
				STS = ScriptStatuses.Failed;
				
				throw e;
			}
			finally
			{
				OnStatusChangeEvent(new RobotWrapperEventArgs(STS));
			}
		}
		
		/// <summary>
		/// Runs a script on the Robot.
		/// </summary>
		/// <param name="ScriptName">Name of script</param>
		public void RunScript(object _ScriptName)
		{		
			string ScriptName =(string)_ScriptName;
			ScriptStatuses STS = ScriptStatuses.Success;
			
			try
			{
				Logon();
				ScriptID = Evo.PrepareScript(ScriptName);
				Evo.StartScript(ScriptID, 0, 0);
				CheckScriptStatus();
			}
			catch(Exception e)
			{
				myLogger.Add("Run Script Error");
				myLogger.Add(e.ToString());
				STS = ScriptStatuses.Failed;
				OnStatusChangeEvent(new RobotWrapperEventArgs(STS));
				throw e;
			}
			finally
			{
				Logoff();
			}
		}

		public bool ShouldStop
		{
			get { return _ShouldStop;}
		}
		
		public bool ShouldPause
		{
			get { return _ShouldPause;}
		}
		
		public void RequestPause()
		{
			_ShouldPause  = true;
		}
		
		public void RequestResume()
		{
			_ShouldPause  = false;		
		}		
		
		public void RequestStop()
		{
			_ShouldStop  = true;	
						
		}		
	
	}
	
	public enum ScriptStatuses
	{Success=0, Running=1, RuntimeError=2, Failed=3, TerminatedByUser = 4};
	
	public class RobotWrapperEventArgs : EventArgs
	{
		private ScriptStatuses ScriptTerm;
		public RobotWrapperEventArgs(ScriptStatuses _ScriptTerm)
		{
			ScriptTerm = _ScriptTerm;
		}
		
		public ScriptStatuses ScriptStatus
		{
			get { return ScriptTerm; }
			set { ScriptTerm = value;}
		}
	}
}
