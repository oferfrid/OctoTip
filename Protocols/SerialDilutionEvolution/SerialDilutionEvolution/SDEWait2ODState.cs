/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/11/2012
 * Time: 17:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace SerialDilutionEvolution
{
	/// <summary>
	/// Description of SDEWait2ODState.
	/// </summary>
	[State("Wait 4 OD","Wait for Net OD")]
	public class SDEWait2ODState:WaitState
	{
		
		public SDEWait2ODState(double Hours2Wait):base(DateTime.Now.AddHours(Hours2Wait))
		{
		}
		
		protected override void OnWaitStart()
		{
			throw new NotImplementedException();
		}
		
		protected override void OnWaitEnd()
		{
			throw new NotImplementedException();
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(SDEReadODState)};
		}
		#endregion
	}
}
