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


		
		
		public RobotWorkerStatus _Status = RobotWorkerStatus.Stopped;
		
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		private volatile bool _ShouldStop = false;
		private volatile bool _ShouldPause = false;
		private RobotWrapper Robot;
		
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
					
					RobotJob RJ =  MainForm.RJQ.GetNextRobotJob();
					if(RJ!=null)
					{
						RJ.CreateScript();
						OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJub,RJ,"Running...."));
						 ScriptStatuses STS = Robot.RunScript(RJ.ScriptFilePath);
						switch (STS)
						{
							case ScriptStatuses.Success:
								OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,"Jub terminated Successfuly "));
								break;
								case ScriptStatuses.TerminatedByUser:
								OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,"Jub Terminated By the User "));
								break;
								case ScriptStatuses.Failed:
								OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,"Jub Failed"));
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
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJub,null,"Jub Resumed...."));
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
			_ShouldStop = true;
		}
		public void RequestPause()
		{
			_ShouldPause  = true;
		}
		public void RequestResume()
		{
			_ShouldPause  = false;
			
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
				case ScriptStatuses.RuntimeError:
					OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.RunningJub,null,"Jub Runtime Error"));
					break;
			}
			
		}
		
		
		
		public RobotWorkerStatus Status
		{
			get {return _Status;}
		}
		
		public enum RobotWorkerStatus
		{Stopped,WaitingForQueuedItems,RunningJub,FinishRunningJub,Paused}
	}
	
	public class RobotWorkerStatusChangeEventArgs : EventArgs
	{
		
		private RobotWorker.RobotWorkerStatus _RobotWorkerStatus;
		private RobotJob _CurentJub;
		private string _Message;
		
		public RobotWorkerStatusChangeEventArgs(RobotWorker.RobotWorkerStatus RobotWorkerStatus,RobotJob CurentJub,string Message)
		{
			_RobotWorkerStatus = RobotWorkerStatus;
			_CurentJub=CurentJub;
			_Message = Message;
		}
		public RobotWorker.RobotWorkerStatus RobotWorkerStatus
		{
			get { return _RobotWorkerStatus; }
		}
		public RobotJob CurentJub
		{
			get { return _CurentJub; }
		}
		public string Messege
		{
			get { return _Message; }
		}
	}
}
