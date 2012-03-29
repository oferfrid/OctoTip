/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/03/2012
 * Time: 21:03
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
	/// Description of TWWait1State.
	/// </summary>
	[State("Wait 2 State","Just Test this")]
	public class TWWait2State:WaitState
	{
		public TWWait2State():base(new TimeSpan(0,0,30))
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
			return new List<Type>{typeof(TWWait1State)};
		}
		#endregion
	}
}
