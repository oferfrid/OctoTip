/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/03/2012
 * Time: 21:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace TestWait
{
	/// <summary>
	/// Description of TWProtocol.
	/// </summary>
	[Protocol("Test Wait","Ofer fridman","A simple protocol to test a program whith out the exo")]
	public class TWProtocol:Protocol
	{
		
		public TWProtocol(TWProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			
		}
			
		protected override void DoWork()
		{
			DateTime Start = DateTime.Now;
			while(((TimeSpan)(DateTime.Now - Start)).TotalMinutes <  ProtocolParameters.TotalTime && !this.ShouldStop)
			{
				this.ChangeState(new TWWait1State());
				this.ChangeState(new TWWait2State());
				
			}
		}
		
		
		public new TWProtocolParameters ProtocolParameters
		{
			get{return (TWProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		
		#region static	
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(TWWait1State)
					,typeof(TWWait2State)};
		}
		#endregion
	}
}
