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

namespace GrowKillCurvePlaiting
{
	/// <summary>
	/// Description of GKCPGrow1State.
	/// </summary>
	[State("Grow1","Wait for sample to get to ON befor dilution")]
	public class GKCPGrow1State:GKCPGrowState
	{
		public GKCPGrow1State(double HoursOfGrow):base(HoursOfGrow)
		{

		}
			#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(GKCPDilut1State)};
		}
		#endregion
	}
}
