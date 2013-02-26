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

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of KCEPGrowState.
	/// </summary>
	public abstract class KCEPGrowState:WaitState
	{

		public KCEPGrowState(double HoursOfGrow):base(DateTime.Now.AddHours(HoursOfGrow))
		{

		}
		
		protected override void OnWaitStart()
		{
			
		}
		
		protected override void OnWaitEnd()
		{
			
		}
		
	}
}

