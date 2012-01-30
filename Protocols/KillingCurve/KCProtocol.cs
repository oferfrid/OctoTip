/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 09/01/12
 * Time: 4:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib;

namespace KillingCurve
{
	/// <summary>
	/// Description of KCProtocol.
	/// </summary>
	[Protocol("Killing Curve" ,"Irit Reisman","Performs MPN test at different time points")]
	public class KCProtocol:Protocol
	{
		public SortedList<DateTime,State>  TasksList = new SortedList<DateTime,State>(50);
				
		public new KCProtocolParameters ProtocolParameters
		{
			get{return (KCProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		

	
		
		public KCProtocol(KCProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			ProtocolParameters = Parameters;
		}
		
		protected override void OnProtocolStart()
		{		
			
			if (ProtocolParameters.PerformInoc)
			{
				this.ChangeState(new KCInoculateCultureState(this,
				                                             ProtocolParameters.CultureEppendorfInd,
				                                             ProtocolParameters.CultureLicInd,
				                                             ProtocolParameters.MPNLicInd,
				                                             ProtocolParameters.ReadAfter));
			}
			DateTime NextTaskTime = new DateTime();
			
			// init MPN points after inoculation (starting form 1 because first time point was with inoculation)
			// --------------------------------------------------------------------------------------------------
			for (int i=1; i<ProtocolParameters.SamplingTimesArray.Length; i++)
			{
				//debug
				Log("adding sampling points");
				// end debug
				TasksList.Add(DateTime.Now.AddHours(ProtocolParameters.SamplingTimesArray[i]),
				              new KCMPNState(this, 
				                             ProtocolParameters.CultureLicInd ,
				                             ProtocolParameters.MPNLicInd+i,
				                             ProtocolParameters.ReadAfter));
				//debug
				Log(@"Time: " + ProtocolParameters.SamplingTimesArray[i].ToString() +
				             @" ind: " + (ProtocolParameters.MPNLicInd+i).ToString());
				// end debug
			}
			
			// going over the task list
			// -------------------------
			while(TasksList.Count>0)
			{
				//debug
				Log(@"TasksList.Count " + TasksList.Count.ToString());
				Log(@"Keys: ");
				for (int i=0; i<TasksList.Count; i++)
				{
					Log(TasksList.Keys[i].ToString());
				}
				// end debug
				NextTaskTime = TasksList.Keys[0];
				
				this.ChangeState(new KCIncubateState(this,NextTaskTime));

				this.ChangeState(TasksList[TasksList.Keys[0]]);
				TasksList.Remove(NextTaskTime);
			}
			
		
		}
		
	    #region static	
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(KCInoculateCultureState)
					,typeof(KCMPNState)
					,typeof(KCIncubateState)
					,typeof(KCReadPlateState)
			};
		}
		#endregion
	}
	
	
	
}