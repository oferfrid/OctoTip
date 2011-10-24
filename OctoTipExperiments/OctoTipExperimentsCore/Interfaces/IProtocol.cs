﻿/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 20:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using OctoTip.OctoTipExperiments.Core.Base;

namespace OctoTip.OctoTipExperiments.Core.Interfaces
{
	/// <summary>
	/// Description of IProtocol.
	/// </summary>
	public interface IProtocol
	{
		void DoWork();
				
		void RequestStop();
		void RequestPause();
		void RequestStart();
		void ChangeState(State NewState);
		
		//TODO: Save State!
	}
	
	
	
}
