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
	public class RobotJobsQueue:SortedList<double,RobotJob>
	{
		
		public RobotJobsQueue():base()
		{
			
		}
		/// <summary>
		/// Adds an element to the PriorityQueue.
		/// </summary>
		/// <param name="RJ">RobotJob</param>
		/// <param name="Priority">Priority , 0 first 1 end</param>
		public void Enqueue(RobotJob RJ,double Priority)
		{
			if (Priority<0 || Priority >= 1)
			{
				throw new Exception ("Priority is [0,1)");
			}

			Priority = GetPriority(Priority);
			base.Add(Priority,RJ);
			
		}
		
		
		public void Enqueue(RobotJob RJ)
		{
			Enqueue(RJ,0.5);
		}
		
		private double GetPriority(double Priority)
		{
			if (this.ContainsKey(Priority))
			{
				// add the new after the last similer key
				double[] Keys = new double[base.Count];
				base.Keys.CopyTo(Keys,0);
				for (int i=0;i<Keys.Length;i++)
				{
					if (Keys[i]==Priority)
					{
						if (i+1<Keys.Length)
						{
							Priority=(Keys[i]+Keys[i+1])/2.0;
						}
						else
						{
							Priority=(Keys[i]+1.0)/2.0;
						}
					}
				}
				
				
			}
			return Priority;
		}
		/// <summary>
		/// Removes the element from the head of the PriorityQueue.
		/// </summary>
		/// <returns>RobotJob</returns>
		public RobotJob Dequeue()
		{
			RobotJob Rj = base[base.Keys[0]];
			return Rj;
		}
	}
	
	
}
