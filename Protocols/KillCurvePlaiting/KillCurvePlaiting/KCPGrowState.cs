/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 11/05/2012
 * Time: 13:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace KillCurvePlaiting
{
	/// <summary>
	/// Description of KCPGrowState.
	/// </summary>
	[State("Wait 2 ON","Wait for sample to get ro ON")]
	public class KCPGrowState:WaitState
	{

		public KCPGrowState(double HoursOfGrow):base(DateTime.Now.AddHours(HoursOfGrow))
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
			return new List<Type>{typeof(KCPEndGrowState)};
		}
		#endregion
	}
}

