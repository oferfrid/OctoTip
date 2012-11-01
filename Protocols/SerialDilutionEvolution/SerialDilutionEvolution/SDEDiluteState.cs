/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2012
 * Time: 14:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace SerialDilutionEvolution
{
	/// <summary>
	/// Description of SDEDiluteState.
	/// </summary>
	[State("Dilut","Dilute main evolution batch culture")]
	 public class SDEDiluteState:State
	{
		public SDEDiluteState()
		{
		}
	 	
		protected override void DoWork()
		{
			throw new NotImplementedException();
		}
	}
}
