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
		
		
		public RobotWorker()
		{
		}
		
		
		
		public void StartReadingQueue()
		{
			_Status = RobotWorkerStatus.Started;
			while (!_ShouldStop)
			{
				
				if (!_ShouldPause)
				{
					
					RobotJob RJ =  MainForm.RJQ.GetNextRobotJob();
					
					
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
			//OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Stoping,"Trying to Stopped"));
			_ShouldStop = true;
		}
		public void RequestPause()
		{
			//OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Pausing,"Trying to Pause"));
			_ShouldPause  = true;
		}
		public void RequestResume()
		{
			//OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Starting,"Resuming"));
			_ShouldPause  = false;
			
		}
		
		
		
		public RobotWorkerStatus Status
		{
			get {return _Status;}
		}
		
		public enum RobotWorkerStatus
		{Stopped,Started,Paused}
	}
}
