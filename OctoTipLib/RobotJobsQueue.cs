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
using System.ComponentModel;
using System.Linq;

namespace OctoTip.Lib
{
	/// <summary>
	/// Description of RobotJobsQueue.
	/// </summary>
//	public class RobotJobsQueue:BindingList<RobotJob>
	public sealed class RobotJobsQueue:BindingList<RobotJob>
	{
		
		
		private static volatile RobotJobsQueue instance;
		private static object syncRoot = new Object();

		public event EventHandler<RobotJobsQueueChangedEventArgs> RobotJobsQueueChanged;
		
		public static RobotJobsQueue Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new RobotJobsQueue();
					}
				}

				return instance;
			}
		}

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
		
		
		private RobotJobsQueue():base()
		{
			
		}
		
		
		public RobotJob.Status GetJobStatus(Guid UniqueID)// 2017-04-13 changed "RobotJob" to "Guid"
		{
			lock (syncRoot)
			{
				RobotJob.Status JobStatus = RobotJob.Status.Failed;
				
				foreach(RobotJob RJ in this)
				{
					if (RJ.UniqueID.Equals(UniqueID))
					{
						JobStatus = RJ.JobStatus;
					}
				}
				
				//string Message = string.Format("Statuses of Script UniqueID: {0} is: {1} ",UniqueID,SS );
				//logger.Add(Message);
				return JobStatus;
			}    
		}
		
		public string GetScriptName(Guid UniqueID)// 2017-04-13 new
		{
			lock (syncRoot)
			{
				string ScriptName = string.Empty;
				
				foreach(RobotJob RJ in this)
				{
					if (RJ.UniqueID.Equals(UniqueID))
					{
						ScriptName = RJ.ScriptName;
					}
				}
				
				//string Message = string.Format("Statuses of Script UniqueID: {0} is: {1} ",UniqueID,SS );
				//logger.Add(Message);
				return ScriptName;
			}    
		}
		
		public string GetRobotJobDisplayParameters(Guid UniqueID)// 2017-04-13 new
		{
			lock (syncRoot)
			{
				string RobotJobDisplayParameters = string.Empty;
				
				foreach(RobotJob RJ in this)
				{
					if (RJ.UniqueID.Equals(UniqueID))
					{
						RobotJobDisplayParameters = RJ.RobotJobDisplayParameters;
					}
				}
				
				//string Message = string.Format("Statuses of Script UniqueID: {0} is: {1} ",UniqueID,SS );
				//logger.Add(Message);
				return RobotJobDisplayParameters;
			}    
		}
		
		public string GetScriptFilePath(Guid UniqueID)// 2017-04-13 new
		{
			lock (syncRoot)
			{
				string ScriptFilePath = string.Empty;
				
				foreach(RobotJob RJ in this)
				{
					if (RJ.UniqueID.Equals(UniqueID))
					{
						ScriptFilePath = RJ.ScriptFilePath;
					}
				}
				
				//string Message = string.Format("Statuses of Script UniqueID: {0} is: {1} ",UniqueID,SS );
				//logger.Add(Message);
				return ScriptFilePath;
			}    
		}
		
		public void WriteJobParameterFile(Guid UniqueID)// 2017-04-13 new
		{
			lock (syncRoot)
			{
				
				foreach(RobotJob RJ in this)
				{
					if (RJ.UniqueID.Equals(UniqueID))
					{
						RJ.WriteParameterFile();
					}
				}
				
				//string Message = string.Format("Statuses of Script UniqueID: {0} is: {1} ",UniqueID,SS );
				//logger.Add(Message);
			}    
		}
		
		
//		protected override bool SupportsSortingCore
//		{
//			get
//			{
//				return true;
//			}
//		}
//
//		protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
//		{
//			var modifier = direction == ListSortDirection.Ascending ? 1 : -1;
//			if (prop.PropertyType.GetInterface("IComparable") != null)
//			{
//				var items = Items.ToList();
//				items.Sort(new Comparison<RobotJob>((a, b) =>
//				                             {
//				                             	var aVal = prop.GetValue(a) as IComparable;
//				                             	var bVal = prop.GetValue(b) as IComparable;
//				                             	return aVal.CompareTo(bVal) * modifier;
//				                             }));
//				Items.Clear();
//				foreach (var i in items)
//					Items.Add(i);
//			}
//		}
//
		
		public Guid GetNextRobotJob()
		{
			lock (syncRoot)
			{
				RobotJob RJ = null;
			
				double Priority = 0;
			
				for (int i=0;i<this.Count;i++)
				{
					if (this[i].JobStatus == RobotJob.Status.Queued && this[i].Priority > Priority)
					{
						RJ = this[i];
						Priority = RJ.Priority;
					}
				}
				
				if (RJ != null)
				{
					RJ.JobStatus = RobotJob.Status.Enqueued;
					return new Guid(RJ.UniqueID.ToByteArray());// 2017-04-13 was "return RJ", moved into "if"
				}
				return RobotJob.NULL_ID;// 2017-04-18 added
			}
		}
		
		
		
		
		public Guid InsertRobotJob(RobotJob RJ)
		{
			
			lock (syncRoot)
			{
				
				Guid UniqueID = RJ.GenerateUniqueID();
				RJ.UniqueID = UniqueID;
			
				this.Add(RJ);
				RJ.JobStatus = RobotJob.Status.Queued;
				
				return UniqueID;
			}
		}
		
		
//		public RobotJob.Status GetJobStatus(Guid UniqueID)
//		{
//			//RobotJob.Status SS = OctoTip.Manager.MainForm.FormRobotJobsQueueHestoryDictionary[UniqueID];
//			RobotJob.Status JobStatus = RobotJob.Status.Failed;
//			foreach(RobotJob RJ in MainForm.MainFormRobotJobsQueue)
//			{
//				if (RJ.UniqueID == UniqueID)
//				{
//					JobStatus = RJ.JobStatus;
//				}
//			}
//
//			//string Message = string.Format("Statuses of Script UniqueID: {0} is: {1} ",UniqueID,SS );
//			//logger.Add(Message);
//			return JobStatus;
//		}
		
		public void setJobStatus(Guid UniqueID,RobotJob.Status NewStatus)
        {
            lock (syncRoot)
            {
                foreach (RobotJob RJ in this)
                {
                    if (RJ.UniqueID == UniqueID)
                    {
                        RJ.JobStatus=NewStatus;
                    }
                }
            }
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
