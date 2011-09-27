/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 25/09/2011
 * Time: 18:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace OctoTip.OctoTipLib.Experiment
{
	/// <summary>
	/// Description of StateEvoRuningState.
	/// </summary>
	public class StateEvoRuningState:State
	{
		public StateEvoRuningState(Protocol RuningInProtocol,List<State> OptionalNextStates)
			:base( RuningInProtocol,OptionalNextStates)
		{
			
		}
	}
}
