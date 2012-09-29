/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 20/09/2012
 * Time: 15:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace CalibrateL1210Growth
{
	/// <summary>
	/// Description of CLGIncubateState.
	/// </summary>
	[State("Incubate","Incubate the sample")]
	public class CLGIncubateState:WaitState
	{
		public CLGIncubateState(double MinutesOfIncubation):base(DateTime.Now.AddMinutes(MinutesOfIncubation))
		{
			
		}
		
		protected override void OnWaitStart()
		{
		}
		
		protected override void OnWaitEnd()
		{
		
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(CLGSampleState),typeof(CLGReadState)};
		}
		#endregion
		
	}
}
