/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 08/04/2012
 * Time: 14:34
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
	/// Description of CAGrowToON.
	/// </summary>
	[State("Grow 2 ON","Grow to ON")]
	public class CAGrowToON:WaitState
	{

		public CAGrowToON(double TimeOfGrow):base(new DateTime().AddHours(TimeOfGrow))
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
			return new List<Type>{typeof(CADilutb2Amp),typeof(CADilutb2AmpInNewPlate)};
		}
		#endregion
	}
}
