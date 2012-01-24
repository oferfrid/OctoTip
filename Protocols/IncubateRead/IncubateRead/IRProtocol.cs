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
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

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
			
			while (StartTime.Subtract(DateTime.Now).TotalHours < this.ProtocolParameters.TotalTime)
			{
				this.ChangeState(new IRReadState(this));
				this.ChangeState(new IRIncubateState(this));
			}
			if (StartTime.Subtract(DateTime.Now).TotalHours > 0)
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