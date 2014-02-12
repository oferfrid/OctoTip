/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/11/2012
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace MDK99
{
	/// <summary>
	/// Description of SDONEProtocolParameters.
	/// </summary>
	public class MDKProtocolParameters:ProtocolParameters
	{
		public MDKProtocolParameters()
		{
		}
		
		[ProtocolParameterAtribute("Liconic curent plate Index","1",true)]
		public int LicPlatePosition;
		[ProtocolParameterAtribute("Bacteria epindorf Index","1",true)]
		public int GermIndex;
		[ProtocolParameterAtribute("Beta-Lac epindorf Index","2",true)]
		public int BLacIndex;
		[ProtocolParameterAtribute(@"Antibiotic concentration in truogh (ug/ml)","40",true)]
		public double TroughConcentration;
		[ProtocolParameterAtribute(@"Minimum antibiotic concentration (ug/ml)","2",true)]
		public double MinConcentration;
		[ProtocolParameterAtribute(@"Maximum antibiotic concentration (ug/ml)","22",true)]
		public double MaxConcentration;
		[ProtocolParameterAtribute("Minimum time in antibiotic (hours)","0.5",true)]
		public double MinTime;
		[ProtocolParameterAtribute("Maximum time in antibiotic (hours)","5",true)]
		public double MaxTime;
		[ProtocolParameterAtribute("Final incubation time (hours)","12",true)]
		public double FinIncTime;
		[ProtocolParameterAtribute("Current inoculation cycle","0",true)]
		public int InoculateCycle;
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\MDK99\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("Shared Resources file path",@"D:\OctoTip\Protocols\MDK99\SharedResources\")]
		public string SharedResourcesFilePath;
		
		
	}
}
