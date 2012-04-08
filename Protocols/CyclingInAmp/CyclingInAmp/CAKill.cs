/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 08/04/2012
 * Time: 11:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace CyclingInAmp
{
	/// <summary>
	/// Description of CAKillState.
	/// </summary>
	[State("Kill","Kill in AMP")]
	public class CAKill:WaitState
	{
		public CAKill(double TimeOfKill):base(new DateTime().AddHours(TimeOfKill))
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
			return new List<Type>{typeof(CAAddbLac)};
		}
		#endregion
	}
}
