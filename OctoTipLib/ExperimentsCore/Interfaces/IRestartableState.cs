/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 23/04/2012
 * Time: 21:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.Lib.ExperimentsCore.Interfaces
{
	/// <summary>
	/// IRestartableState alawing a resturt of state after failer  
	/// </summary>
	public interface IRestartableState
	{
		 void Restart();//remembre to reset CurrentStatus by using SetCurrentStatus
	}
}
