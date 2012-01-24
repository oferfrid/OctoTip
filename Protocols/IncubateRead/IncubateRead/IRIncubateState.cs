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
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace IncubateRead
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	[State("Incubate state","Incubating in Liconic")]
	public class IRIncubateState:WaitState
	{
		new IRProtocol RunningInProtocol
		{
			get
			{return (IRProtocol) base.RunningInProtocol;}
		}
		
		public IRIncubateState(IRProtocol RunningInProtocol):base(RunningInProtocol, DateTime.Now.AddMinutes(RunningInProtocol.ProtocolParameters.ReadFrequency))
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
