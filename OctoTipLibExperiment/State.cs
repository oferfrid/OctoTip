/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 25/09/2011
 * Time: 13:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;


namespace OctoTip.OctoTipLib.Experiment
{
	/// <summary>
	/// Description of State.
	/// </summary>
	public class State
	{
		Protocol RuningInProtocol;
		List<State> OptionalNextStates;
		
		public State(Protocol RuningInProtocol,List<State> OptionalNextStates)
		{
			this.RuningInProtocol = RuningInProtocol;
			this.OptionalNextStates = OptionalNextStates;
		}
		
	
		public State(Protocol RuningInProtocol)
		{
			this.RuningInProtocol = RuningInProtocol;
		}
		
	}
}
