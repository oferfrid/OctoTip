/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 22/09/2011
 * Time: 09:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;


namespace OctoTip.OctoTipLib
{
	/// <summary>
	/// Description of RobotJobsQueue.
	/// </summary>
	public class RobotJobsQueue:List<RobotJob>
	{
		
		public event EventHandler<RobotJobsQueueChangedEventArgs> RobotJobsQueueChanged;
		
		private  void OnRobotJobsQueueChanged(RobotJobsQueueChangedEventArgs e)
		{
			// Make a temporary copy of the event to avoid possibility of
			// a race condition if the last subscriber unsubscribes
			// immediately after the null check and before the event is raised.
			EventHandler<RobotJobsQueueChangedEventArgs> handler = RobotJobsQueueChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		
				
		public RobotJobsQueue():base()
		{
			
		}
		
		public RobotJob GetNextRobotJob()
		{
			RobotJob RJ = null;
			if (this.Count>0)
			{
				this.Sort();
				RJ = this[0];
				this.RemoveAt(0);
				RJ.JobStatus = RobotJob.Status.Enqueued;
				OnRobotJobsQueueChanged(new RobotJobsQueueChangedEventArgs("remove 1"));
			}
			return RJ;
			
		}
		
		
		public Guid InsertRobotJob(RobotJob RJ)
		{
			Guid UniqueID =  RJ.GenerateUniqueID();
			RJ.UniqueID = UniqueID;
			int index = this.BinarySearch(RJ);
			if (index < 0)
			{
				this.Insert(~index, RJ);
			}
			else
			{
				this.Insert(index, RJ);
			}
			RJ.JobStatus = RobotJob.Status.Queued;
			OnRobotJobsQueueChanged(new RobotJobsQueueChangedEventArgs("Insert 1"));
			return UniqueID;
		}
		
	}
	public class RobotJobsQueueChangedEventArgs : EventArgs
	{
		private string _Massege	;
		public RobotJobsQueueChangedEventArgs(string Massege)
		{
			this._Massege = Massege;
		}
		
		public string Massege
		{
			get {return _Massege;}
		}
	}
	
}
