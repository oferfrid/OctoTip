/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 06/10/2011
 * Time: 17:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using OctoTip.Lib;

namespace OctoTip.Manager
{
	/// <summary>
	/// Description of RobotWorker.
	/// </summary>
	public class RobotWorker
	{

		private Thread RunningThread;
		
		public  const string LOG_NAME = "OctoTipManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		
		
		public RobotWorkerStatus _Status = RobotWorkerStatus.Stopped;

		int QueueSumplelingRate ;

		
		RobotJobsQueue WorkerRobotJobsQueue
		{
			get{return MainForm.FormRobotJobsQueue;}
		}
		
		Dictionary<Guid, OctoTip.Lib.RobotJob.Status> WorkerRobotJobsQueueHestoryDictionary
		{
			get {return MainForm.FormRobotJobsQueueHestoryDictionary;}
		}
		
		
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		
		private volatile bool _ShouldPause = false;
		private volatile bool _ShouldStop = false;
		
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
			QueueSumplelingRate = Convert.ToInt32(ConfigurationManager.AppSettings["QueueSumplelingRate"]);
			if (QueueSumplelingRate == 0)
			{
				throw new NullReferenceException("AppSettings QueueSumplelingRate is null");
			}
			
			
			Robot = new RobotWrapper();
			Robot.RobotJobStatusChanged += OnRobot_RobotJobStatusChanged;
			//start StartReadingQueue loop in diferent Thread
			
			RunningThread = new Thread(StartReadingQueue);
		}
		
		~RobotWorker()
		{
			
			RunningThread.Abort();
			RunningThread=null;
		}
		
		
		private void StartReadingQueue()
		{
			while (!_ShouldStop)
			{
				if (_Status != RobotWorkerStatus.WaitingForQueuedItems)
				{
				OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,"Waiting..."));
				}
				
				if(_ShouldPause && !_ShouldStop)
				{
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,null,"Paused..."));
					while (_ShouldPause && !_ShouldStop)
					{
						Thread.Sleep(QueueSumplelingRate);
					}
				}
				//not paused or stoped read job from Q
				
				RobotJob RJ =  WorkerRobotJobsQueue.GetNextRobotJob();
				if(RJ!=null)
				{
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,RJ,"Running...."));
					
					
					Robot.RunScript(RJ);
					
					
					switch (RJ.JobStatus)
					{
						case OctoTip.Lib.RobotJob.Status.Finished:
							OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RJ,"Job terminated Successfuly "));
							break;
						case OctoTip.Lib.RobotJob.Status.TerminatedByUser:
							OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopped,RJ,"Job Terminated By the User "));
							_ShouldStop = true;
							break;
						case OctoTip.Lib.RobotJob.Status.Failed:
							OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopped,RJ,"Job Failed"));
							_ShouldStop = true;
							break;
					}
				}
				
				
				if (_ShouldPause)
				{
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,null,"Paused..."));
					while(_ShouldPause)
					{
						System.Threading.Thread.Sleep(QueueSumplelingRate);
					}
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,"Waiting..."));
				}
				
				Thread.Sleep(QueueSumplelingRate);
			}
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopped,null,"Stopped..."));
		}
		
		
		public void  RequestStop()
		{
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopping,null,"Stopping..."));
			_ShouldStop = true;
			_ShouldPause = false;			
			Robot.RequestStop();
		}
		public void RequestPause()
		{
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Pausing,null,"Pausing..."));
			_ShouldPause  = true;
			Robot.RequestPause();
		}
		public void RequestStart()
		{
			switch(this.Status)
			{
				case( RobotWorkerStatus.Paused):
						
						OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,null,"Resume..."));
					_ShouldPause  = false;
					Robot.RequestResume();
					break;
					
				case( RobotWorkerStatus.Stopped):
						
						_ShouldPause  = false;
					try
					{
						RunningThread.Start();
					}
					catch(System.Threading.ThreadStateException)
					{
						RunningThread = null;
						RunningThread = new Thread(StartReadingQueue);
						RunningThread.Start();
					}
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,"Waiting..."));
					break;
					

			}
			
			
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
		
		
		private void OnRobot_RobotJobStatusChanged(object sender,RobotJobStatusChangedEventArgs  e)
		{
			switch (e.ScriptStatus)
			{
				case OctoTip.Lib.RobotJob.Status.RuntimeError:
					WorkerRobotJobsQueueHestoryDictionary[e.Job.UniqueID]=OctoTip.Lib.RobotJob.Status.RuntimeError;
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,e.Job,"Job Runtime Error"));
					break;
				case OctoTip.Lib.RobotJob.Status.Paused:
					WorkerRobotJobsQueueHestoryDictionary[e.Job.UniqueID]=OctoTip.Lib.RobotJob.Status.Paused;
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,e.Job,"Job Paused"));
					break;
				case OctoTip.Lib.RobotJob.Status.Running:
					WorkerRobotJobsQueueHestoryDictionary[e.Job.UniqueID]=OctoTip.Lib.RobotJob.Status.Running;
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,e.Job,"Job Runing From Worker"));
					break;
					
			}
			
		}
		
		
		
		public RobotWorkerStatus Status
		{
			get {return _Status;}
		}
		
		public enum RobotWorkerStatus
		{
			Stopping,
			Stopped,
			WaitingForQueuedItems,
			RunningJob,
			FinishRunningJob,
			Pausing,
			Paused
		}
	}
	
	public class RobotWorkerStatusChangeEventArgs : EventArgs
	{
		
		private RobotWorker.RobotWorkerStatus _RobotWorkerStatus;
		private RobotJob _CurrentJob;
		private string _Message;
		
		public RobotWorkerStatusChangeEventArgs(RobotWorker.RobotWorkerStatus RobotWorkerStatus,RobotJob CurrentJob,string Message)
		{
			_RobotWorkerStatus = RobotWorkerStatus;
			_CurrentJob=CurrentJob;
			_Message = Message;
		}
		public RobotWorker.RobotWorkerStatus RobotWorkerStatus
		{
			get { return _RobotWorkerStatus; }
		}
		public RobotJob CurrentJob
		{
			get { return _CurrentJob; }
		}
		public string Messege
		{
			get { return _Message; }
		}
	}
}
