/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 18/12/11
 * Time: 10:52 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace IncubateRead
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	[Protocol("Incubate Read" ,"Irit Reisman","OD measurements while growing in Liconic in 24 plate")]
	public class IRProtocol:Protocol
	{
		public new IRProtocolParameters ProtocolParameters
		{
			get{return (IRProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		public IRProtocol(IRProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			ProtocolParameters = Parameters;
		}
		
		protected override void OnProtocolStart()
		{
			DateTime StartTime = DateTime.Now;
			string message;
			
			while (DateTime.Now.Subtract(StartTime).TotalHours < this.ProtocolParameters.TotalTime && !ShouldStop)
			{
				message = "Hours left: " + 
					(this.ProtocolParameters.TotalTime-DateTime.Now.Subtract(StartTime).TotalHours).ToString("0.00");
				OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs(message));
				this.ChangeState(new IRReadState(this));
				message = "Hours left: " + 
					(this.ProtocolParameters.TotalTime-DateTime.Now.Subtract(StartTime).TotalHours).ToString("0.00");
				OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs(message));
				this.ChangeState(new IRIncubateState(this));	
			}
			if (DateTime.Now.Subtract(StartTime).TotalHours > 0)
			{
				this.ChangeState(new IRReadState(this));
			}
			
		}
		
		
		
		#region static	
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(IRReadState)
					,typeof(IRIncubateState)};
		}
		#endregion
	}
}