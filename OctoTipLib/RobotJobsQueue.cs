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
			}
			return RJ;
			
		}
		
		
		public Guid InsertRobotJob(RobotJob RJ)
		{
			Guid UniqueID =  RJ.GenerateUniqueID();
			int index = this.BinarySearch(RJ);
			if (index < 0)
			{
				this.Insert(~index, RJ);
			}
			else
			{
				this.Insert(index, RJ);
			}
			
			return UniqueID;
		}
		
	}
	
	
}
