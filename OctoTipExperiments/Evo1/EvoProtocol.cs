/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace Evo1
{
	/// <summary>
	/// Description of Evo1.
	/// </summary>
	[Protocol("Cyclic Evolution" ,"Ofer Fridman","Cyclic Evolution In 6 Wells Plate")]
	public class EvoProtocol:Protocol
	{
		
		private double _CurentOD = 0;
		public List<double> ODs = new List<double>(20);
		
		public double CurentOD
		{
			get{return _CurentOD;}
			set
			{
				_CurentOD = value;
				ODs.Add(_CurentOD);
			}
			
		}
		
		
		public EvoProtocolParameters EvoProtocolParameters;
		
		public EvoProtocol():base()
		{
			
		}
		
		public EvoProtocol(EvoProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			this.EvoProtocolParameters = Parameters;
		}
		
		
		
		private void DesplayProtocolData(int Cycle, List<double> ODs,TimeSpan RemainingTime)
		{
			string TextData = string.Empty;
			TextData+= string.Format("Cycle:{0}/{1}",Cycle,EvoProtocolParameters.FreezAmpEppendorfInds.Length) + Environment.NewLine;
			if(ODs !=null)
			{
				TextData+= "OD:" + Environment.NewLine;
				foreach(double OD in ODs)
				{
					TextData+= OD.ToString("0.000") + Environment.NewLine;
				}
			}
			if (RemainingTime!=TimeSpan.Zero)
			{
				TextData+=string.Format("Remaining Time:{0}(minuts)",RemainingTime.TotalMinutes.ToString("0.00")) + Environment.NewLine;
			}
			
			this.OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs(TextData));
		}
		
		protected override void OnProtocolStart()
		{
			
			int PlateInd = EvoProtocolParameters.FirstPlateInd;
			int WellInd = 1;
			
			int NumberOfCycles = EvoProtocolParameters.FreezAmpEppendorfInds.Length;
			//this.ChangeState(new EvoStarterState(this,PlateInd));
			
			
			for(int c = 0 ;c<NumberOfCycles;c++)
			{
				Log(string.Format("Cycle {0} Started on plate:{1} Well:{2}",c,PlateInd,WellInd));
				DesplayProtocolData(c,null,TimeSpan.Zero);
				DateTime StartKillTime = DateTime.Now;
				
				//Read Plate at the begining of the kill
				
				ODs.Clear();

				while((StartKillTime.AddMinutes(EvoProtocolParameters.KillingTime)-DateTime.Now)>TimeSpan.Zero)
				{
					TimeSpan RemainingTime = StartKillTime.AddMinutes(EvoProtocolParameters.KillingTime)-DateTime.Now;
					this.ChangeState(new EvoKillReadState(this,PlateInd,WellInd));
					Log("EvoKillReadState OD:" + CurentOD.ToString("0.000"));
					DesplayProtocolData(c,ODs,TimeSpan.Zero);
					
					RemainingTime = StartKillTime.AddMinutes(EvoProtocolParameters.KillingTime)-DateTime.Now;
					DesplayProtocolData(c,ODs,RemainingTime);
					
					if (RemainingTime.TotalMinutes > EvoProtocolParameters.TimeBetweenReads)
					{
						this.ChangeState(new EvoKillState(this,DateTime.Now.AddMinutes(EvoProtocolParameters.TimeBetweenReads)));
						
					}
					else
					{
						this.ChangeState(new EvoKillState(this,DateTime.Now.Add(RemainingTime)));
						DesplayProtocolData(c,ODs,TimeSpan.Zero);
						
					}
				}
				
				//Add b-Lac
				this.ChangeState(new EvoAddbLacState(this,PlateInd,WellInd));
				DesplayProtocolData(c,ODs,TimeSpan.Zero);
				
				
				ODs.Clear();
				//Read for Reference OD
				this.ChangeState(new EvoGrow1ReadState(this,PlateInd,WellInd));
				Log("EvoGrow1ReadState OD:" + CurentOD.ToString("0.000"));
				DesplayProtocolData(c,ODs,TimeSpan.Zero);
				
				double Tara = CurentOD ;
				
				//Wait for OD
				while((CurentOD - Tara )< EvoProtocolParameters.NetODFirstDilution)
				{
					this.ChangeState(new EvoGrow1State(this));
					this.ChangeState(new EvoGrow1ReadState(this,PlateInd,WellInd));
					Log("EvoGrow1ReadState OD:" + CurentOD.ToString("0.000"));
					DesplayProtocolData(c,ODs,TimeSpan.Zero);
					
				}
				
				//dilute to new well
				this.ChangeState(new EvoDilutState(this,PlateInd,WellInd,++WellInd));
				DesplayProtocolData(c,ODs,TimeSpan.Zero);
				
				ODs.Clear();
				//Read for Reference OD
				this.ChangeState(new EvoGrow2ReadState(this,PlateInd,WellInd));
				Log("EvoGrow2ReadState OD:" + CurentOD.ToString("0.000"));
				DesplayProtocolData(c,ODs,TimeSpan.Zero);
				
				Tara = CurentOD ;
				while((CurentOD - Tara )< EvoProtocolParameters.NetODAMPDilution)
				{
					this.ChangeState(new EvoGrow2State(this));
					
					this.ChangeState(new EvoGrow2ReadState(this,PlateInd,WellInd));
					Log("EvoGrow2ReadState OD:" + CurentOD.ToString("0.000"));
					DesplayProtocolData(c,ODs,TimeSpan.Zero);
					
				}
				if( WellInd==6)
				{
					Log("EvoDilut2AmpState replace plate");
					this.ChangeState(new EvoDilut2AmpState(this,PlateInd,++PlateInd,WellInd,1,EvoProtocolParameters.FreezAmpEppendorfInds[c],EvoProtocolParameters.AMPEppendorfInd));
				}
				else
				{
					this.ChangeState(new EvoDilut2AmpState(this,PlateInd,WellInd,++WellInd,EvoProtocolParameters.FreezAmpEppendorfInds[c],EvoProtocolParameters.AMPEppendorfInd));
					
				}
				DesplayProtocolData(c,ODs,TimeSpan.Zero);
			}
		}

		#region static
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(EvoAddbLacState)
					,typeof(EvoDilut2AmpState)
					,typeof(EvoDilutState)
					,typeof(EvoGrow1ReadState)
					,typeof(EvoGrow1State)
					,typeof(EvoGrow2ReadState)
					,typeof(EvoGrow2State)
					,typeof(EvoKillReadState)
					,typeof(EvoKillState)
					,typeof(EvoStarterState)};
		}
	}
	#endregion
}

