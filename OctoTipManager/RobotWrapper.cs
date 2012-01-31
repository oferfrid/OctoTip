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
using OctoTip.Lib;

namespace OctoTip.Manager
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
		
		int RobotSamplingRate;
		
		
		public RobotWrapper()
		{
			Evo = new EVOAPILib.SystemClass();
			
			RobotSamplingRate = Convert.ToInt32(ConfigurationManager.AppSettings["RobotSamplelingRate"]);
			if (RobotSamplingRate == 0)
			{
				throw new NullReferenceException("AppSettings RobotSamplelingRate is null");
			}
			
		}
		
		public event EventHandler<RobotJobStatusChangedEventArgs> RobotJobStatusChanged;
		
		protected virtual void OnRobotJobStatusChanged(RobotJobStatusChangedEventArgs e)
		{
			EventHandler<RobotJobStatusChangedEventArgs> handler = RobotJobStatusChanged;

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
			}
			catch(Exception e)
			{
				throw e;
			}
			
			
			try
			{
				RobotStatus = Evo.GetStatus();
				// initialazing
				if (RobotStatus == SC_Status.STATUS_NOTINITIALIZED)
				{
					Evo.Initialize();
				}
			}
			catch(Exception e)
			{
				Evo.Logoff();
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
				throw e;
			}
		}
		
		
		public void RunScript(RobotJob Job)
		{
			int ScriptID;
			Job.CreateScript();


			Logon();
			
			
			
			try
			{
				 ScriptID = Evo.PrepareScript(Job.ScriptFilePath);
				Evo.StartScript(ScriptID, 0, 0);

				Job.JobStatus = RobotJob.Status.Running;
				OnRobotJobStatusChanged( new RobotJobStatusChangedEventArgs(Job));
			}
			catch(Exception e)
			{
				Job.JobStatus = RobotJob.Status.Failed;
				OnRobotJobStatusChanged( new RobotJobStatusChangedEventArgs(Job));
				Logoff();
				throw e;
			}
			
			//Loop while the robot is preforming the job
			
			try
			{
				SC_ScriptStatus ScriptStatusEx = Evo.GetScriptStatusEx(ScriptID);
				SC_ScriptStatus ScriptStatus   = Evo.GetScriptStatus(ScriptID);
				
				while(ScriptStatus == SC_ScriptStatus.SS_BUSY && !_ShouldStop)
				{
					System.Threading.Thread.Sleep(RobotSamplingRate);
					
					// Handeling pause request
					if (_ShouldPause)
					{
						Evo.Pause();
						Job.JobStatus=RobotJob.Status.Paused;
						OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
						
						while (_ShouldPause)
						{
							System.Threading.Thread.Sleep(RobotSamplingRate);
						}
						
						if (!_ShouldStop)
						{
							Evo.Resume();
							Job.JobStatus = RobotJob.Status.Running;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
						}
					}
					
					//Chack for runtime error and wait
					if(Evo.GetScriptStatusEx(ScriptID) == SC_ScriptStatus.SS_ERROR)
					{
						
						Job.JobStatus = RobotJob.Status.RuntimeError;
						OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
						
						while(Evo.GetScriptStatusEx(ScriptID) == SC_ScriptStatus.SS_ERROR &&
						      !_ShouldStop)
						{
							System.Threading.Thread.Sleep(RobotSamplingRate);
						}
						
						if (!_ShouldStop)
						{
							Job.JobStatus = RobotJob.Status.Running;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
						}
						
						
					}
					
					
					
					ScriptStatus = Evo.GetScriptStatus(ScriptID);
				}
				//or ended of _souldStop or
				
				if (_ShouldStop)
				{
					Evo.Stop();
					Job.JobStatus = RobotJob.Status.TerminatedByUser;
					OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
				}
				else
				{
					ScriptStatusEx = Evo.GetScriptStatusEx(ScriptID);
					
					// determain script termination status
					switch (ScriptStatusEx)
					{
						case SC_ScriptStatus.SS_IDLE:
							Job.JobStatus = RobotJob.Status.Finished;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
							break;
						case SC_ScriptStatus.SS_STOPPED:
							Job.JobStatus = RobotJob.Status.TerminatedByUser;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
							break;
						case SC_ScriptStatus.SS_ABORTED:
							Job.JobStatus = RobotJob.Status.TerminatedByUser;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
							break;
						case SC_ScriptStatus.SS_STATUS_ERROR:
							Job.JobStatus = RobotJob.Status.Failed;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(Job));
							break;
					}
				}
			}
			catch(Exception e)
			{
				Job.JobStatus = RobotJob.Status.Failed;
				OnRobotJobStatusChanged( new RobotJobStatusChangedEventArgs(Job));
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
			_ShouldPause = false;
			
		}
	}



	public class RobotJobStatusChangedEventArgs : EventArgs
	{
		private RobotJob _Job;
		
		public RobotJobStatusChangedEventArgs(RobotJob Job)
		{
			this._Job = Job;
		}
		
		public RobotJob.Status ScriptStatus
		{
			get { return _Job.JobStatus; }
			//set { _ScriptStatus = value;}
		}
		public RobotJob Job
		{
			get { return _Job; }
			//	set { _ScriptStatus = value;}
		}
	}
}
