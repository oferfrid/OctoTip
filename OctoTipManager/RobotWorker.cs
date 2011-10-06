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
			OnStatusChanged(new RobotWorkerStatusChangeEventArgs(RobotWorkerStatus.WaitingForQueuedItems,null,null,"Initilasing..."));
			while (!_ShouldStop)
			{
				
				if (!_ShouldPause)
				{
					
					RobotJob RJ =  MainForm.RJQ.GetNextRobotJob();
					if(RJ!=null)
					{
						RJ.CreateScript();
						Robot.RunScript(RJ.ScriptFilePath);
						
						
					}
					
				}
				else
				{
					_Status = RobotWorkerStatus.Paused;
				}
				Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["QueueSumplelingRate"]));
				
			}
			_Status = RobotWorkerStatus.Stopped;
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
			case ScriptStatuses.Failed:
				break;
			}
				
		}
		
		
		
		public RobotWorkerStatus Status
		{
			get {return _Status;}
		}
		
		public enum RobotWorkerStatus
		{Stopped,WaitingForQueuedItems,RunningJub,Paused}
	}
	
	public class RobotWorkerStatusChangeEventArgs : EventArgs
	{
		
		private RobotWorker.RobotWorkerStatus _RobotWorkerStatus;
		private RobotJob _CurentJub;
		private RobotJob _PreviuseJub;
		private string _Message;
		
		public RobotWorkerStatusChangeEventArgs(RobotWorker.RobotWorkerStatus RobotWorkerStatus,RobotJob CurentJub,RobotJob PreviuseJub,string Message)
		{
			_RobotWorkerStatus = RobotWorkerStatus;
			_CurentJub=CurentJub;
			_PreviuseJub =PreviuseJub;
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
		public RobotJob PreviuseJub
		{
			get { return PreviuseJub; }
		}
		public string Messege
		{
			get { return _Message; }
		}
	}
}
