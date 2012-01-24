/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 12/01/12
 * Time: 12:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipLib;

namespace KillingCurve
{
	/// <summary>
	/// Description of KCMPNState.
	/// </summary>
	[State("Serial Dilution","Inoculates sample in 96 wells plate and dilutes")]
	public class KCMPNState:RunRobotState
	{
		new KCProtocol RunningInProtocol
		{
			get
			{return (KCProtocol) base.RunningInProtocol;}
		}
		int CultureLicInd;
		int MPNLicInd;
		double ReadAfter;
		
		public KCMPNState(KCProtocol RunningInIRProtocol,
		                 int CultureLicInd_, 
		                 int MPNLicInd_,
		                 double ReadAfter_)
			:base((Protocol)RunningInIRProtocol)
		{
			MPNLicInd     = MPNLicInd_;
			CultureLicInd = CultureLicInd_;
			ReadAfter     = ReadAfter_;
		}
				
		protected override OctoTip.OctoTipLib.RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(5);
			
			LicPos LPCulture = Utils.Ind2LicPos(RunningInProtocol.ProtocolParameters.CultureLicInd);
			LicPos LPMPN = Utils.Ind2LicPos(RunningInProtocol.ProtocolParameters.MPNLicInd);
			
			RJP.Add(new RobotJobParameter("FirstTimePoint", RobotJobParameter.ParameterType.Number, 0));
			RJP.Add(new RobotJobParameter("CultureCart", RobotJobParameter.ParameterType.Number, LPCulture.Cart));
			RJP.Add(new RobotJobParameter("CulturePos", RobotJobParameter.ParameterType.Number, LPCulture.Pos));
			RJP.Add(new RobotJobParameter("MPNCart", RobotJobParameter.ParameterType.Number, LPMPN.Cart));
			RJP.Add(new RobotJobParameter("MPNPos", RobotJobParameter.ParameterType.Number, LPMPN.Pos));
			RJP.Add(new RobotJobParameter("CultureWell", RobotJobParameter.ParameterType.Number, RunningInProtocol.ProtocolParameters.WellInd));
			        
			RobotJob RJ = new RobotJob(@"D:\RobotScripts\KillingCurve\KC_MPN.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			KCReadPlateState ReadPlateState = new KCReadPlateState(RunningInProtocol, MPNLicInd);
			RunningInProtocol.TasksList.Add(DateTime.Now.AddHours(ReadAfter),
			                                ReadPlateState);
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCIncubateState)};
		}
		#endregion
	}
}
