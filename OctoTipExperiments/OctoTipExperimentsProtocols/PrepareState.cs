/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 27/09/2011
 * Time: 15:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using OctoTip.OctoTipExperiments.Core.Base ;

namespace OctoTip.OctoTipExperiments.Protocols
{
	/// <summary>
	/// Description of PrepareState.
	/// </summary>
	public class PrepareState:State
	{
		public PrepareState(Protocol RunningInProtocol ):base(RunningInProtocol)
		{
			
		}
		
		public override void DoWork()
		{
			throw new NotImplementedException();
		}
	}
}
