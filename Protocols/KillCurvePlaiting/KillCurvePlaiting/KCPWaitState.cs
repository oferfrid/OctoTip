/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 06/05/2012
 * Time: 12:25
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
	/// Description of KCPWaitState.
	/// </summary>
	[State("Wait 4 sample","Wait for next sample")]
	public class KCPWaitState:WaitState
	{

		public KCPWaitState(double TimeOfWait):base(DateTime.Now.AddMinutes(TimeOfWait))
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
			return new List<Type>{typeof(KCPSampleState)};
		}
		#endregion
	}
}

