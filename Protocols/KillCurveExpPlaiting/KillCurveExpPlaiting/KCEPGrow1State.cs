/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 19/02/2013
 * Time: 14:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of KCEPGrow1State.
	/// </summary>
	[State("Grow1","Wait for sample to get to ON befor dilution")]
	public class KCEPGrow1State:KCEPGrowState
	{
		public KCEPGrow1State(double HoursOfGrow):base(HoursOfGrow)
		{

		}
			#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCEPDilut1State)};
		}
		#endregion
	}
}
