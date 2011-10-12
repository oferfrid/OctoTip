﻿/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 21:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Interfaces;

namespace OctoTip.OctoTipExperiments
{
	/// <summary>
	/// Description of MPNProtocol.
	/// </summary>
	[Protocol("Preform an MPN Evaluarion Of coulture")]
	public class MPNProtocol:Protocol
	{
		
		#region static
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(WaitState1),typeof(RoboRunState1),typeof(WaitState2)};
		}
		#endregion
		
		MPNProtocolParameters Parameters;
		
		public MPNProtocol():base()
		{
			
		}
		
		public MPNProtocol(MPNProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			this.Parameters = Parameters;
		}
		
		public override void OnStatusChanged(ProtocolStatusChangeEventArgs e)
		{
			// Do any specific processing here.

			// Call the base class event invocation method.
			base.OnStatusChanged(e);
		}
		
		protected override void OnProtocolStart()
		{
			//throw new NotImplementedException();
			
			this.ChangeState(new WaitState1(this,DateTime.Now.AddMinutes(Parameters.WaitB4start)));
				CurrentState.DoWork();
				
			for( int i=0 ;i<MPNProtocolParameters.NumberOfSycles;i++)
			{
				this.ChangeState( new RoboRunState1(this));
				CurrentState.DoWork();
				
				this.ChangeState(new WaitState2(this,new TimeSpan(0,0,30)));
				CurrentState.DoWork();
				
			
				
			}
			
			
			
		}
		
		
		public MPNProtocolParameters MPNProtocolParameters
		{
			get{return (MPNProtocolParameters)this.ProtocolParameters;}
		}
	}
}
