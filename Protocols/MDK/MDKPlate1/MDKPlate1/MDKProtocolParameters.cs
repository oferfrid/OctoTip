﻿/*
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

namespace MDKPlate1
{
	/// <summary>
	/// Description of SDONEProtocolParameters.
	/// </summary>
	public class MDKProtocolParameters:ProtocolParameters
	{
		public MDKProtocolParameters()
		{
		}
		
		[ProtocolParameterAtribute("Current liconic plate index","34",true)]
		public int LicPlatePosition;
		[ProtocolParameterAtribute("Bacteria eppendorf index","1",true)]
		public int GermIndex;
		[ProtocolParameterAtribute("Beta-Lac eppendorf index","2",true)]
		public int BLacIndex;
		[ProtocolParameterAtribute(@"Antibiotic concentration in truogh (μg/ml)","200",true)]
		public double TroughConcentration;
		[ProtocolParameterAtribute(@"Minimum antibiotic concentration (μg/ml)","2",true)]
		public double MinConcentration;
		[ProtocolParameterAtribute("Minimum time in antibiotic (hours)","1",true)]
		public double MinTime;
		[ProtocolParameterAtribute("Maximum time in antibiotic (hours)","40",true)]
		public double MaxTime;
		[ProtocolParameterAtribute("Final incubation time (hours)","12",true)]
		public double FinIncTime;
		[ProtocolParameterAtribute("Current inoculation cycle","0",true)]
		public int InoculateCycle;
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\MDK\MDKPlate1\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("Shared Resources file path",@"D:\OctoTip\Protocols\MDK\MDKPlate1\SharedResources\")]
		public string SharedResourcesFilePath;
		[ProtocolParameterAtribute("Check MIC in top row?","true",true)]
		public bool MIC;
		[ProtocolParameterAtribute("Prepare plate?","true",true)]
		public bool Prep;
		
		
		public override bool IsValid()
		{
			if(LicPlatePosition<1||LicPlatePosition>34)
			{
				return false;
			}
			
			if(GermIndex<1||GermIndex>24)
			{
				return false;
			}
			
			if(BLacIndex<1||BLacIndex>24)
			{
				return false;
			}
			
			if(mu()<0.2||mu()>1)
			{
				return false;
			}
			
			if(MinTime>MaxTime)
			{
				return false;
			}
			
			if(InoculateCycle<0)
			{
				return false;
			}
			
			return true;
		}
		
		// Amount taken from previous column during dilution, normalized to 180 microliters 
		public double mu()
		{
			double alpha = Math.Pow((MinConcentration/TroughConcentration),0.1);
			return alpha*alpha/(1-alpha*alpha);
		}
		
		public override string GetErrorMessage()
		{
			string ErrorMsg = "";
			
			if(LicPlatePosition<1||LicPlatePosition>34)
			{
				ErrorMsg += string.Format("Plate position {0} not in range (1-34)/n",LicPlatePosition);
			}
			
			if(GermIndex<1||GermIndex>24)
			{
				ErrorMsg += string.Format("Eppendorf position {0} not in range (1-24)/n",GermIndex);
			}
			
			if(BLacIndex<1||BLacIndex>24)
			{
				ErrorMsg += string.Format("Eppendorf position {0} not in range (1-24)/n",BLacIndex);
			}
			
			if(mu()<0.2)
			{
				ErrorMsg += string.Format("Maximum-minimum concentrations too far apart");
			}
			
			if(mu()>1)
			{
				ErrorMsg += string.Format("Maximum-minimum concentrations too close together");
			}
			
			if(MinTime>MaxTime)
			{
				ErrorMsg += string.Format("Minimum incubation time {0} greater than maximum time {1}",MinTime,MaxTime);
			}
			
			if(InoculateCycle<0)
			{
				ErrorMsg += string.Format("Inoculation cycle {0} not in range (0-8)/n",InoculateCycle);
			}
			
			return ErrorMsg;
		}
		
		
	}
}