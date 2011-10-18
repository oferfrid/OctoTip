/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace Evo1
{
	/// <summary>
	/// Description of Evo1.
	/// </summary>
	[Protocol("Cycling Evolution")]
	public class EvoProtocol:Protocol
	{
		
		public EvoProtocolParameters Parameters;
		
		public EvoProtocol():base()
		{
			
		}
		
		public EvoProtocol(EvoProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			this.Parameters = Parameters;
		}
		
		
		
		protected override void OnProtocolStart()
		{
			throw new NotImplementedException();
		}
		
		
		
		
		
		#region static
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(EvoAddbLacState)
					,typeof(EvoDilut2AmpState)
					,typeof(EvoDilutState)
					,typeof(EvoGrow1ReadState)
					,typeof(EvoGrow1State)
					,typeof(EvoGrow2ReadState)
					,typeof(EvoGrow2State)
					,typeof(EvoKillReadState)
					,typeof(EvoKillState)
					,typeof(EvoStarterState)};
		}
		#endregion
	}
}
