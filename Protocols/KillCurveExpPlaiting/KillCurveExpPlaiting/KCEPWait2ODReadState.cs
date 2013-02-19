/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 19/02/2013
 * Time: 14:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of SDEWait2ODReadState.
	/// </summary>
	[State("Wait4OD","Wait for OD Read")]
	public class KCEPWait2ODReadState:KCEPGrowState
	{
		
		public KCEPWait2ODReadState(double HoursOfGrow):base(HoursOfGrow)
		{

		}
			#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCEPReadODState)};
		}
		#endregion
		
	}
}
