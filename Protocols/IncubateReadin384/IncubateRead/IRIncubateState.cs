/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 18/12/11
 * Time: 11:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace IncubateReadin384
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	[State("Incubate state","Incubating in Liconic")]
	public class IRIncubateState:WaitState
	{
				
		public IRIncubateState(double ReadFrequency):base(DateTime.Now.AddMinutes(ReadFrequency))
		{
		}
		
		protected override void OnWaitStart()
		{
			//throw new NotImplementedException();
		}
		
		protected override void OnWaitEnd()
		{
			//throw new NotImplementedException();
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(IRReadState)};
		}
		#endregion
	}
}
