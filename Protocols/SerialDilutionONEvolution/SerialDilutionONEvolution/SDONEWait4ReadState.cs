/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 23/04/2014
 * Time: 12:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;

namespace SerialDilutionONEvolution
{
	/// <summary>
	/// Description of SDONEWait4ReadState.
	/// </summary>
	[State("Wait 4 Read","Wait for read")]
	public class SDONEWait4ReadState:SDONEWaitState
	{
		public SDONEWait4ReadState(double Hours2Wait):base(Hours2Wait)
		{
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(SDONEReadWellState)};
		}
		#endregion
	}
}
