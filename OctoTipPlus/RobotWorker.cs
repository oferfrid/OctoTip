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

namespace OctoTip.OctoTipPlus
{
	/// <summary>
	/// Description of RobotWorker.
	/// </summary>
	public class RobotWorker
	{

		private Thread RunningThread;
				
		public RobotWorkerStatus _Status = RobotWorkerStatus.Stopped;

		int QueueSumplelingRate ;

		
		RobotJobsQueue WorkerRobotJobsQueue
		{
			get
			{
				return RobotJobsQueue.Instance;
			}
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
			RunningThread = new Thread(_StartReadingQueue);
			RunningThread.IsBackground = true;
		}
		
		~RobotWorker()
		{
			
			RunningThread.Abort();
			RunningThread=null;
		}
		
		
		private void _StartReadingQueue()
		{
			try
			{
				StartReadingQueue();
			}
			catch(Exception e)
			{// 2017-04-14 replaced "null"
				OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopped,RobotJob.NULL_ID,"Exception:" + e.ToString()));
			}
			
		}
		
		private void StartReadingQueue()
		{
			
			while (!_ShouldStop)
			{
				if (_Status != RobotWorkerStatus.WaitingForQueuedItems)
				{// 2017-04-14 replaced "null"
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RobotJob.NULL_ID,"Waiting..."));
				}
				
				if(_ShouldPause && !_ShouldStop)
				{// 2017-04-14 replaced "null"
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,RobotJob.NULL_ID,"Paused..."));
					while (_ShouldPause && !_ShouldStop)
					{
						Thread.Sleep(QueueSumplelingRate);
					}
				}
				//not paused or stoped read job from Q
				
				Guid RJ =  WorkerRobotJobsQueue.GetNextRobotJob();//2017-04-13 canged RobotJob to Guid
				if(RJ!=RobotJob.NULL_ID)// 2017-04-14 replaced "null"
				{
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,RJ,"Running...."));

// To reconnect the robot, delete the 3 following lines and uncomment the comment region
					WorkerRobotJobsQueue.setJobStatus(RJ,RobotJob.Status.Finished);
				//RJ.JobStatus = RobotJob.Status.Finished;
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RJ,"Job terminated Successfuly "));
					Thread.Sleep(1000);
					/*
					Robot.RunScript(RJ,WorkerRobotJobsQueue);
					
					
					switch (WorkerRobotJobsQueue.GetJobStatus(RJ))
					{
						case OctoTip.Lib.RobotJob.Status.Finished:
							OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RJ,"Job terminated Successfuly "));
							break;
						case OctoTip.Lib.RobotJob.Status.TerminatedByUser:
							OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,RJ,"Job Terminated By the User "));
							_ShouldPause = true;
							break;
						case OctoTip.Lib.RobotJob.Status.Failed:
							OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,RJ,"Job Failed"));
							_ShouldPause = true;
							break;
					}*/
				}
				
				
				if (_ShouldPause)
				{// 2017-04-14 replaced "null"
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,RobotJob.NULL_ID,"Paused..."));
					while(_ShouldPause)
					{
						System.Threading.Thread.Sleep(QueueSumplelingRate);
					}// 2017-04-14 replaced "null"
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RobotJob.NULL_ID,"Waiting..."));
				}
				
				Thread.Sleep(QueueSumplelingRate);
			}// 2017-04-14 replaced "null"
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopped,RobotJob.NULL_ID,"Stopped..."));
		}
		
		
		public void  RequestStop()
		{// 2017-04-14 replaced "null"
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Stopping,RobotJob.NULL_ID,"Stopping..."));
			_ShouldStop = true;
			_ShouldPause = false;
			Robot.RequestStop();
		}
		public void RequestPause()
		{// 2017-04-14 replaced "null"
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Pausing,RobotJob.NULL_ID,"Pausing..."));
			_ShouldPause  = true;
			Robot.RequestPause();
		}
		public void RequestStart()
		{
			switch(this.Status)
			{
				case( RobotWorkerStatus.Paused):
					// 2017-04-14 replaced "null"
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,RobotJob.NULL_ID,"Resume..."));
					_ShouldPause  = false;
					Robot.RequestResume();
					break;
					
				case( RobotWorkerStatus.Stopped):
					
					_ShouldPause  = false;
					_ShouldStop = false;
					try
					{
						RunningThread.Start();
					}
					catch(System.Threading.ThreadStateException)
					{
						RunningThread = null;
						RunningThread = new Thread(_StartReadingQueue);
						RunningThread.IsBackground = true;
						RunningThread.Start();
					}// 2017-04-14 replaced "null"
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,RobotJob.NULL_ID,"Waiting..."));
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
			switch (WorkerRobotJobsQueue.GetJobStatus(e.JobID))
			{
				case OctoTip.Lib.RobotJob.Status.RuntimeError:
					//WorkerRobotJobsQueueHestoryDictionary[e.Job.UniqueID]=OctoTip.OctoTipPlus.Lib.RobotJob.Status.RuntimeError;
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,e.JobID,"Job Runtime Error"));
					break;
				case OctoTip.Lib.RobotJob.Status.Paused:
					//WorkerRobotJobsQueueHestoryDictionary[e.Job.UniqueID]=OctoTip.OctoTipPlus.Lib.RobotJob.Status.Paused;
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.Paused,e.JobID,"Job Paused"));
					break;
				case OctoTip.Lib.RobotJob.Status.Running:
					//WorkerRobotJobsQueueHestoryDictionary[e.Job.UniqueID]=OctoTip.OctoTipPlus.Lib.RobotJob.Status.Running;
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJob,e.JobID,"Job Runing From Worker"));
					break;
				case OctoTip.Lib.RobotJob.Status.Finished:
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.FinishRunningJob,e.JobID,"Job Runing From Worker"));
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
		static public string GetRobotWorkerStatusText(RobotWorkerStatus RWS)
		{
			//TODO: fix
			switch(RWS)
			{
				case RobotWorkerStatus.Stopping:
					return "Stopping";
				case RobotWorkerStatus.Stopped:
					return "Stopped";
				case RobotWorkerStatus.WaitingForQueuedItems:
					return "Waiting for queued items";
					case RobotWorkerStatus.RunningJob:
					return "Running Job";
				case RobotWorkerStatus.FinishRunningJob:
					return "Finish running job";
				case RobotWorkerStatus.Pausing:
					return "Pausing";
				case RobotWorkerStatus.Paused:
					return "Paused";
				default:
					return "Stopped";
			}
		}
	}
	
	public class RobotWorkerStatusChangeEventArgs : EventArgs
	{
		
		private RobotWorker.RobotWorkerStatus _RobotWorkerStatus;
		private Guid _CurrentJob;// 2017-04-13 changed "RobotJob" to "Guid"
		private string _Message;
		
		public RobotWorkerStatusChangeEventArgs(RobotWorker.RobotWorkerStatus RobotWorkerStatus,Guid CurrentJobID,string Message)// 2017-04-13 changed "RobotJob" to "Guid"
		{
			_RobotWorkerStatus = RobotWorkerStatus;
			_CurrentJob = CurrentJobID;
			_Message = Message;
		}
		public RobotWorker.RobotWorkerStatus RobotWorkerStatus
		{
			get { return _RobotWorkerStatus; }
		}
		public Guid CurrentJob// 2017-04-13 changed "RobotJob" to "Guid"
		{
			get { return _CurrentJob; }
		}
		public string Message
		{
			get { return _Message; }
		}
	}
}
