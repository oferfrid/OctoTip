/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 08/04/2012
 * Time: 14:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace CyclingInAmp
{
	/// <summary>
	/// Description of CAGrowToOD.
	/// </summary>
	[State("Grow 2 OD","Grow to OD")]
	public class CAGrowToOD:WaitState
	{

		public CAGrowToOD(double TimeOfGrow):base(new DateTime().AddHours(TimeOfGrow))
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
			return new List<Type>{typeof(CAGetOD)};
		}
		#endregion
		
		
	}
}
