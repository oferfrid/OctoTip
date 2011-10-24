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
		
		public double CurentOD = 0;
		
		public EvoProtocolParameters EvoProtocolParameters;
		
		public EvoProtocol():base()
		{
			
		}
		
		public EvoProtocol(EvoProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			this.EvoProtocolParameters = Parameters;
		}
		
		
		
		protected override void OnProtocolStart()
		{
			int PlateInd = EvoProtocolParameters.FirstPlateInd;
			int WellInd = 1;
			
			int NumberOfCycles = EvoProtocolParameters.FreezAmpEppendorfInds.Length;
			
			this.ChangeState(new EvoStarterState(this,PlateInd));
			CurrentState.DoWork();
			
			for(int c = 0 ;c<NumberOfCycles;c++)
			{
				DateTime StartKillTime = DateTime.Now;
				
				//Read Plate at the begining of the kill
				this.ChangeState(new EvoKillReadState(this,PlateInd));
				CurrentState.DoWork();
				
				while((StartKillTime.AddMinutes(EvoProtocolParameters.KillingTime)-DateTime.Now)>TimeSpan.Zero)
				{
					TimeSpan RemainingTime = StartKillTime.AddMinutes(EvoProtocolParameters.KillingTime)-DateTime.Now;
					if (RemainingTime.TotalMinutes > EvoProtocolParameters.TimeBetweenReads)
					{
						this.ChangeState(new EvoKillState(this,DateTime.Now.AddMinutes(EvoProtocolParameters.TimeBetweenReads)));
						CurrentState.DoWork();
					}
					else
					{
						this.ChangeState(new EvoKillState(this,DateTime.Now.Add(RemainingTime)));
						CurrentState.DoWork();
					}
				}
				
				//read B4 adding b-Lac
				this.ChangeState(new EvoKillReadState(this,PlateInd));
				CurrentState.DoWork();
				//Add b-Lac
				this.ChangeState(new EvoAddbLacState(this,PlateInd,WellInd));
				CurrentState.DoWork();
				
				//Read for Reference OD
				this.ChangeState(new EvoGrow1ReadState(this,PlateInd,WellInd));
				CurrentState.DoWork();
				double Tara = CurentOD ;
				
				//Wait for OD
				while((CurentOD - Tara )< EvoProtocolParameters.NetODFirstDilution)
				{
					this.ChangeState(new EvoGrow1State(this));
					CurrentState.DoWork();
					this.ChangeState(new EvoGrow1ReadState(this,PlateInd,WellInd));
					CurrentState.DoWork();
				}
				
				//dilute to new well
				this.ChangeState(new EvoDilutState(this,PlateInd,WellInd,++WellInd));
				CurrentState.DoWork();
				
				//Read for Reference OD
				this.ChangeState(new EvoGrow2ReadState(this,PlateInd,WellInd));
				CurrentState.DoWork();
				Tara = CurentOD ;
				while((CurentOD - Tara )< EvoProtocolParameters.NetODFirstDilution)
				{
					this.ChangeState(new EvoGrow2State(this));
					CurrentState.DoWork();
					this.ChangeState(new EvoGrow2ReadState(this,PlateInd,WellInd));
					CurrentState.DoWork();
				}
				if( WellInd==6)
				{
					this.ChangeState(new EvoDilut2AmpState(this,PlateInd,++PlateInd,WellInd,1,EvoProtocolParameters.FreezAmpEppendorfInds[c],EvoProtocolParameters.AMPEppendorfInd));
					CurrentState.DoWork();
				}
				else
				{
					this.ChangeState(new EvoDilut2AmpState(this,PlateInd,WellInd,++WellInd,EvoProtocolParameters.FreezAmpEppendorfInds[c],EvoProtocolParameters.AMPEppendorfInd));
					CurrentState.DoWork();
				}			
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
		#endregion
	}
}
