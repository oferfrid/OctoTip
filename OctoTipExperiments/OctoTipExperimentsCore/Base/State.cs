/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 27/09/2011
 * Time: 15:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using OctoTip.OctoTipExperiments.Core.Interfaces;

namespace OctoTip.OctoTipExperiments.Core.Base
{
	/// <summary>
	/// Description of State.
	/// </summary>
	public abstract class State:IState
	{
		
		protected Protocol RunningInProtocol;
		public  State(Protocol RunningInProtocol )
		{
			this.RunningInProtocol = RunningInProtocol;
		}
		public State( )
		{
		
		}
		
		public static  List<Type> NextStates()
		{
			return new List<Type>(0);
		}
		
		public abstract void DoWork();
	

public enum Status {StateChanged,StateStatusChanged }
		
	}
}
