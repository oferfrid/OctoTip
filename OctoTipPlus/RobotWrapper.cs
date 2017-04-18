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
using OctoTip.Lib.Logging;

namespace OctoTip.OctoTipPlus
{
	/// <summary>
	/// Description of RobotWrapper.
	/// </summary>
	public class RobotWrapper
	{
		private EVOAPILib.System Evo;
		
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		private volatile bool _ShouldStop = false;
		private volatile bool _ShouldPause = false;
		
		int RobotSamplingRate;
		
		
		public RobotWrapper()
		{
			Evo = new EVOAPILib.SystemClass();
			
			Evo.ErrorEvent += delegate(DateTime StartTime, DateTime EndTime, string Device, string Macro, string Object, string Message, short Status, string ProcessName, int ProcessID, string MacroID)
			{
				string Title = string.Format("Error from Evo:StartTime={0} EndTime={1} Device={2} Macro={3} Object={4} Message={5} Status={6} ProcessName={7} ProcessID={8} MacroID={9}", StartTime,  EndTime,  Device,  Macro,  Object,  Message,  Status,  ProcessName,  ProcessID,  MacroID);
				if (Status==11)
				{
				Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation","RobotWrapper",Title,LoggingEntery.EnteryTypes.Debug));
				}
				else if (Status==5)
				{
				Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation","RobotWrapper",Title,LoggingEntery.EnteryTypes.Warning));
				}
				else
				{
					Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation","RobotWrapper",Title,LoggingEntery.EnteryTypes.Error));
				}
			};
			Evo.StatusChanged += delegate(SC_Status Status)
			{
				
				string Title = string.Format("Status From Evo:{0}" , Status);
				Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation","RobotWrapper",Title,LoggingEntery.EnteryTypes.Debug));
			};
			
//			Evo.UserPromptEvent += delegate(int ID, string Text, string Caption, int Choices, out int Answer)
//			{
//				myLogger.Add(string.Format("****UserPromptEvent: ID={0} Text={1} Caption={2} Choices={3}", ID,  Text,  Caption,  Choices));
//				Answer = 0;
//			};
			
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
				string Title = "RobotWrapper.Logon (UserName: " + UserName + ")";
				Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation","RobotWrapper",Title, LoggingEntery.EnteryTypes.Debug));
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
				Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation","RobotWrapper","RobotWrapper.Logoff",LoggingEntery.EnteryTypes.Debug));
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
		
		
		public void RunScript(Guid JobID,RobotJobsQueue q)
		{
			int ScriptID;
			q.WriteJobParameterFile(JobID);
			Logon();

			try
			{
				try
				{// 2014-04-14
					// ScriptID = Evo.PrepareScript(JobID.ScriptFilePath);
					ScriptID = Evo.PrepareScript(q.GetScriptFilePath(JobID));
				}
				catch (System.Runtime.InteropServices.COMException e)
				{
					if (e.Message.Contains("Previous script could not be unloaded"))
					{// 2014-04-14
						// ScriptID = Evo.PrepareScript(JobID.ScriptFilePath);
						ScriptID = Evo.PrepareScript(q.GetScriptFilePath(JobID));
					}
					else
					{
						throw e;
					}
					
				}
				Evo.StartScript(ScriptID, 0, 0);
				
				
				//myLogger.Add("After OctoTip.Manager.RobotWrapper.StartScript");
				q.setJobStatus(JobID,RobotJob.Status.Running);
				//Job.JobStatus = RobotJob.Status.Running;
				OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
			}
			catch(Exception e)
			{
				q.setJobStatus(JobID,RobotJob.Status.Failed);
				//Job.JobStatus = RobotJob.Status.Failed;
				OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
				Logoff();
				throw e;
			}
			
			//Loop while the robot is preforming the job
			
			try
			{
				SC_ScriptStatus ScriptStatusEx = Evo.GetScriptStatusEx(ScriptID);
				SC_ScriptStatus ScriptStatus   = Evo.GetScriptStatus(ScriptID);
				
				string Title = "ScriptStatusEx:" + ScriptStatusEx.ToString() + "ScriptStatus:" + ScriptStatus.ToString();
				Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation","RobotWrapper",Title,LoggingEntery.EnteryTypes.Debug));

				
				while(ScriptStatus == SC_ScriptStatus.SS_BUSY && !_ShouldStop)
				{
					System.Threading.Thread.Sleep(RobotSamplingRate);
					
					// Handeling pause request
					if (_ShouldPause)
					{
						Evo.Pause();
						q.setJobStatus(JobID,RobotJob.Status.Paused);
						//Job.JobStatus=RobotJob.Status.Paused;
						OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
						
						while (_ShouldPause)
						{
							System.Threading.Thread.Sleep(RobotSamplingRate);
						}
						
						if (!_ShouldStop)
						{
							Evo.Resume();
							q.setJobStatus(JobID,RobotJob.Status.Running);
							//Job.JobStatus = RobotJob.Status.Running;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
						}
					}
					
					//Chack for runtime error and wait
					if(Evo.GetScriptStatusEx(ScriptID) == SC_ScriptStatus.SS_ERROR)
					{
						q.setJobStatus(JobID,RobotJob.Status.RuntimeError);
						//Job.JobStatus = RobotJob.Status.RuntimeError;
						OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
						
						while(Evo.GetScriptStatusEx(ScriptID) == SC_ScriptStatus.SS_ERROR &&
						      !_ShouldStop)
						{
							System.Threading.Thread.Sleep(RobotSamplingRate);
						}
						
						if (!_ShouldStop)
						{
							q.setJobStatus(JobID,RobotJob.Status.Running);
							//Job.JobStatus = RobotJob.Status.Running;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
						}
						
						
					}
					
					
					
					ScriptStatus = Evo.GetScriptStatus(ScriptID);
				}
				//or ended of _souldStop or
				
				if (_ShouldStop)
				{
					Evo.Stop();
					q.setJobStatus(JobID,RobotJob.Status.TerminatedByUser);
					//Job.JobStatus = RobotJob.Status.TerminatedByUser;
					OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
				}
				else
				{
					ScriptStatusEx = Evo.GetScriptStatusEx(ScriptID);
					Log.LogEntery(new LoggingEntery("OctoTipPlus Appilcation","RobotWrapper","ScriptStatusEx:" + ScriptStatusEx.ToString(),LoggingEntery.EnteryTypes.Debug));

					// determain script termination status
					switch (ScriptStatusEx)
					{
						case SC_ScriptStatus.SS_IDLE:
							q.setJobStatus(JobID,RobotJob.Status.Finished);
							//Job.JobStatus = RobotJob.Status.Finished;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
							break;
						case SC_ScriptStatus.SS_STOPPED:
							q.setJobStatus(JobID,RobotJob.Status.TerminatedByUser);
							//Job.JobStatus = RobotJob.Status.TerminatedByUser;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
							break;
						case SC_ScriptStatus.SS_ABORTED:
							q.setJobStatus(JobID,RobotJob.Status.TerminatedByUser);
							//Job.JobStatus = RobotJob.Status.TerminatedByUser;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
							break;
						case SC_ScriptStatus.SS_STATUS_ERROR:
							q.setJobStatus(JobID,RobotJob.Status.Failed);
							//Job.JobStatus = RobotJob.Status.Failed;
							OnRobotJobStatusChanged(new RobotJobStatusChangedEventArgs(JobID));
							break;
					}
				}
			}
			catch(Exception e)
			{
				q.setJobStatus(JobID,RobotJob.Status.Failed);
				//Job.JobStatus = RobotJob.Status.Failed;
				OnRobotJobStatusChanged( new RobotJobStatusChangedEventArgs(JobID));
				throw e;
			}
			finally
			{
				Logoff();
			}
			
		}
		
		private void Evo_StatusChanged(object sender,SC_Status e)
		{
			
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
		private Guid _JobID;
		
		public RobotJobStatusChangedEventArgs(Guid JobID)
		{
			this._JobID = JobID;
		}
		
//		public RobotJob.Status ScriptStatus //2017-04-14
//		{
//			get { return _Job.JobStatus; }
//			//set { _ScriptStatus = value;}
//		}
		public Guid JobID
		{
			get { return _JobID; }
			//	set { _ScriptStatus = value;}
		}
	}
}
