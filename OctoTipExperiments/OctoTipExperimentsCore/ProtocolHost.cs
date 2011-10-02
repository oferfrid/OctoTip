/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 20:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.OctoTipExperiments.Core.Interfaces;


namespace OctoTip.OctoTipExperiments.Core
{
	/// <summary>
	/// Description of ProtocolHost.
	/// </summary>
	public class ProtocolHost
	{
		
		private IProtocol m_Protocol;
		
		 public ProtocolHost(IProtocol Protocol)
        {
            m_Protocol = Protocol;
        }
		 
		 
		 
	}
}
