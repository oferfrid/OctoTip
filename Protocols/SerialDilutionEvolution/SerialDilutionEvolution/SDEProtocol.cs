/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2012
 * Time: 14:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace SerialDilutionEvolution
{
	/// <summary>
	/// Description of SDEProtocol.
	/// </summary>
	[Protocol("Serial Dilution Evolution","Ofer Fridman","Serial Dilution Evolution experiment")]
	public class SDEProtocol:Protocol
	{
		public SDEProtocol()
		{
		}
		
		protected override void DoWork()
		{
			throw new NotImplementedException();
		}
	}
}
