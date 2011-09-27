/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 25/09/2011
 * Time: 13:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace OctoTip.OctoTipLib.Experiment
{
	/// <summary>
	/// Description of Protocol.
	/// </summary>
	public class Protocol
	{
		public State CurentState;
		public List<State> ProtocolStates;
		
		public Protocol(List<State> ProtocolStates)
		{
			this.ProtocolStates = ProtocolStates;
	
		}
		
		public void ChangeState(State NextState)
		{
			CurentState = NextState;
		}
		
		public void StartProtocol()
		{
			
		}
		
		
	}
}
