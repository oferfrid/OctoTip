/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 06/10/2011
 * Time: 17:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Threading;

using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipManager
{
	/// <summary>
	/// Description of RobotWorker.
	/// </summary>
	public class RobotWorker
	{

		public  const string LOG_NAME = "OctoTipManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		
		
		public RobotWorkerStatus _Status = RobotWorkerStatus.Stopped;
		
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		private volatile bool _ShouldStop = false;
		private volatile bool _ShouldPause = false;
		private RobotWrapper Robot;
		
		
		public bool ShouldStop
		{
			get { return _ShouldStop;}
		}
		
		public bool ShouldPause
		{
			get { return _ShouldPause;}
		}
		
		public RobotWorker()
		{
			Robot = new RobotWrapper();
			Robot.StatusChangeEvent += OnRobot_StatusChangeEvent;
		}
		
		public void StartReadingQueue()
		{
			bool Paused=false;
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,"Initilasing..."));
			while (!_ShouldStop)
			{
				
				if (!_ShouldPause && !Paused)
				{
					
					RobotJob RJ =  MainForm.FormRobotJobsQueue.GetNextRobotJob();
					if(RJ!=null)
					{
						MainForm.FormRobotJobsQueueHestoryDictionary.Add(RJ.UniqueID,OctoTip.OctoTipLib.RobotJob.Status.Running);
						RJ.CreateScript();
						OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,RJ,"Running...."));
						OctoTip.OctoTipLib.RobotJob.Status STS = Robot.RunScript(RJ.ScriptFilePath);
						//running ended
						MainForm.FormRobotJobsQueueHestoryDictionary[RJ.UniqueID]=STS;
						switch (STS)
						{
							case OctoTip.OctoTipLib.RobotJob.Status.Finished:
								OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RJ,"Job terminated Successfuly "));
								break;
							case OctoTip.OctoTipLib.RobotJob.Status.TerminatedByUser:
								OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RJ,"Job Terminated By the User "));
								break;
							case OctoTip.OctoTipLib.RobotJob.Status.Failed:
								OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RJ,"Job Failed"));
								break;
						}
						
						OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,"Waiting...."));
					}
					
				}
				if (!Paused && _ShouldPause)
				{
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,null,"Paused...."));
					Paused = true;
					Robot.RequestPause();
				}
				if (Paused && !_ShouldPause)
				{
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,null,"Job Resumed...."));
					Paused = false;
					Robot.RequestResume();
				}
				
				Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["QueueSumplelingRate"]));
				
			}
			_ShouldStop = false;
			_ShouldPause = false;
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopped,null,"Stoped...."));
		}
		
		
		public void   RequestStop()
		{
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopped,null,"Paused...."));
			_ShouldStop = true;
			_ShouldPause = false;
			
			Robot.RequestStop();
		}
		public void RequestPause()
		{
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,null,"Paused...."));
			_ShouldPause  = true;
			myLogger.Add("B4 asking RW Pause");
			Robot.RequestPause();
			myLogger.Add("after asking RW Pause");
		}
		public void RequestResume()
		{
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,null,"Paused...."));
			_ShouldPause  = false;
			myLogger.Add("B4 asking RW Resume");
			Robot.RequestResume();
			myLogger.Add("after asking RW Resume");
			
		}
		
		
		// The event. Note that by using the generic EventHandler<T> event type
		// we do not need to declare a separate delegate type.
		public event EventHandler<RobotWorkerStatusChangeEventArgs> StatusChanged;
		//The event-invoking method that derived classes can override.
		public virtual void OnStatusChanged(RobotWorkerStatusChangeEventArgs e)
		{
			this._Status = e.RobotWorkerStatus;
			// Make a temporary copy of the event to avoid possibility of
			// a race condition if the last subscriber unsubscribes
			// immediately after the null check and before the event is raised.
			EventHandler<RobotWorkerStatusChangeEventArgs> handler = StatusChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		
		
		private void OnRobot_StatusChangeEvent(object sender, RobotWrapperEventArgs e)
		{
			switch (e.ScriptStatus)
			{
				case OctoTip.OctoTipLib.RobotJob.Status.RuntimeError:
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,null,"Job Runtime Error"));
					break;
				case OctoTip.OctoTipLib.RobotJob.Status.Paused:
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,null,"Job Paused"));
					break;
				case OctoTip.OctoTipLib.RobotJob.Status.Running:
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,null,"Job Resumed"));
					break;
					
					
					
			}
			
		}
		
		
		
		public RobotWorkerStatus Status
		{
			get {return _Status;}
		}
		
		public enum RobotWorkerStatus
		{Stopped,WaitingForQueuedItems,RunningJob,FinishRunningJob,Paused}
	}
	
	public class RobotWorkerStatusChangeEventArgs : EventArgs
	{
		
		private RobotWorker.RobotWorkerStatus _RobotWorkerStatus;
		private RobotJob _CurentJob;
		private string _Message;
		
		public RobotWorkerStatusChangeEventArgs(RobotWorker.RobotWorkerStatus RobotWorkerStatus,RobotJob CurentJob,string Message)
		{
			_RobotWorkerStatus = RobotWorkerStatus;
			_CurentJob=CurentJob;
			_Message = Message;
		}
		public RobotWorker.RobotWorkerStatus RobotWorkerStatus
		{
			get { return _RobotWorkerStatus; }
		}
		public RobotJob CurentJob
		{
			get { return _CurentJob; }
		}
		public string Messege
		{
			get { return _Message; }
		}
	}
}
