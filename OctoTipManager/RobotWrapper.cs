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
		private RobotJob.Status CheckScriptStatus(RobotJob Job,int ScriptID)
		{
			SC_ScriptStatus   ScriptStatusEx = SC_ScriptStatus.SS_UNKNOWN;
			SC_ScriptStatus   ScriptStatus   = SC_ScriptStatus.SS_UNKNOWN;
			RobotJob.Status  STS = RobotJob.Status.RuntimeError;
			DateTime               StartTime = DateTime.Now;
			TimeSpan                      TS = DateTime.Now - StartTime;

			try
			{				
				ScriptStatusEx = Evo.GetScriptStatusEx(ScriptID);
				ScriptStatus   = Evo.GetScriptStatus(ScriptID);
				while(ScriptStatus == SC_ScriptStatus.SS_BUSY && !_ShouldStop)
				{		
					System.Threading.Thread.Sleep(100);
					// Handeling pause request
					if (_ShouldPause)
					{
						//myLogger.Add("Robot B4 Pauseed");
						Evo.Pause();
						//myLogger.Add("Robot Pauseed");
						OnStatusChangeEvent(new RobotWrapperEventArgs(Job,RobotJob.Status.Paused));
						while (_ShouldPause)
						{
							System.Threading.Thread.Sleep(100);
						}
						if (!_ShouldStop)
						{
						//myLogger.Add("Robot B4 Resume");
						Evo.Resume();
						//myLogger.Add("Robot Resume");
						OnStatusChangeEvent(new RobotWrapperEventArgs(Job,RobotJob.Status.Running));
						}
					}
					ScriptStatus = Evo.GetScriptStatus(ScriptID);
					if(Evo.GetScriptStatusEx(ScriptID) == SC_ScriptStatus.SS_ERROR)
					{
						OnStatusChangeEvent(new RobotWrapperEventArgs(Job,RobotJob.Status.RuntimeError));
						while(Evo.GetScriptStatusEx(ScriptID) == SC_ScriptStatus.SS_ERROR)
						{
							System.Threading.Thread.Sleep(100);
						}
					}
						
				}				
				
				ScriptStatusEx = Evo.GetScriptStatusEx(ScriptID);
				
				// determain script termination status
				switch (ScriptStatusEx)
				{				
					case SC_ScriptStatus.SS_IDLE:
						STS = RobotJob.Status.Finished;
					break;
					case SC_ScriptStatus.SS_BUSY:
						STS = RobotJob.Status.Running;
					break;	
					case SC_ScriptStatus.SS_STOPPED:
						STS = RobotJob.Status.TerminatedByUser;
					break;
					case SC_ScriptStatus.SS_ABORTED:
						STS = RobotJob.Status.TerminatedByUser;
					break;
					case SC_ScriptStatus.SS_STATUS_ERROR:
						STS = RobotJob.Status.Failed;
					break;
					default:
						STS = RobotJob.Status.RuntimeError;
					break;
				}
				
				// Handling Stop request
				if (_ShouldStop)
				{
					Evo.Stop();
					STS = RobotJob.Status.TerminatedByUser;
				}
				
			}
			catch (Exception e)
			{
				myLogger.Add("Run Script Error");
				myLogger.Add(e.ToString());
				STS = RobotJob.Status.Failed;
				
				throw e;
			}
			finally
			{
				OnStatusChangeEvent(new RobotWrapperEventArgs(Job,STS));
				
			}
			return STS;
		}
		
		/// <summary>
		/// Runs a script on the Robot.
		/// </summary>
		/// <param name="ScriptName">Name of script</param>
		public RobotJob.Status RunScript(object _Job)
		{		
			RobotJob Job =(RobotJob)_Job;
			RobotJob.Status STS = RobotJob.Status.Finished;
			
			try
			{
				Logon();
				int ScriptID = Evo.PrepareScript(Job.ScriptFilePath);
				Evo.StartScript(ScriptID, 0, 0);
				CheckScriptStatus(Job,ScriptID);
			}
			catch(Exception e)
			{
				myLogger.Add("Run Script Error");
				myLogger.Add(e.ToString());
				STS = RobotJob.Status.Failed;
				OnStatusChangeEvent(new RobotWrapperEventArgs(Job,STS));
				throw e;
			}
			finally
			{
				Logoff();
			}
			
			return STS;
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
			_ShouldPause=false;
						
		}				
	}
	
	
	
	public class RobotWrapperEventArgs : EventArgs
	{
		private OctoTip.OctoTipLib.RobotJob.Status _ScriptStatus;
		private OctoTip.OctoTipLib.RobotJob _Job;
		
		public RobotWrapperEventArgs(RobotJob Job,OctoTip.OctoTipLib.RobotJob.Status ScriptStatus)
		{
			this._ScriptStatus = ScriptStatus;
			this._Job = Job;
		}
		
		public OctoTip.OctoTipLib.RobotJob.Status ScriptStatus
		{
			get { return _ScriptStatus; }
			//set { _ScriptStatus = value;}
		}
		public OctoTip.OctoTipLib.RobotJob Job
		{
			get { return _Job; }
		//	set { _ScriptStatus = value;}
		}
	}
}
