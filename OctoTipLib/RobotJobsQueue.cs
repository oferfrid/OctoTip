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
	public class RobotJobsQueue:BindingListView<RobotJob>
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
		
		public RobotJob GetNextRobotJob()
		{
			RobotJob RJ = null;
			if (this.Count>0)
			{
				//TODO:change the delection by priority...
				RJ = this[0];
				//this.RemoveAt(0);
				RJ.JobStatus = RobotJob.Status.Enqueued;
				//OnRobotJobsQueueChanged(new RobotJobsQueueChangedEventArgs("remove 1"));
				//this.ResetBindings();
			}
			return RJ;
			
		}
		
		
		public Guid InsertRobotJob(RobotJob RJ)
		{
			Guid UniqueID =  RJ.GenerateUniqueID();
			RJ.UniqueID = UniqueID;
			this.Insert(Count, RJ);
			RJ.JobStatus = RobotJob.Status.Queued;
			//OnRobotJobsQueueChanged(new RobotJobsQueueChangedEventArgs("Insert 1"));
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
