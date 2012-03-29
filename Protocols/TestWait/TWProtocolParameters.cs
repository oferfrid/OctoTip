/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/03/2012
 * Time: 21:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace TestWait
{
	/// <summary>
	/// Description of TWProtocolParameters.
	/// </summary>
	public class TWProtocolParameters:ProtocolParameters
	{
		[ProtocolParameterAtribute("Total Growth time (min)","5",true)]
		public double TotalTime;
		[ProtocolParameterAtribute("Time to wait (min)","1",true)]
		public double Time2Wait;
	}
}
