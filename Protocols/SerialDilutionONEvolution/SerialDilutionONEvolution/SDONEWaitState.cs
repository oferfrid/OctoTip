/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 23/04/2014
 * Time: 12:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Base;

namespace SerialDilutionONEvolution
{
	/// <summary>
	/// Description of SDONEWaitState.
	/// </summary>
	public class SDONEWaitState:WaitState
	{
		public SDONEWaitState(double Hours2Wait):base(DateTime.Now.AddHours(Hours2Wait))
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
	}
}
