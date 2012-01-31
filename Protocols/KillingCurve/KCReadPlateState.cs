/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 12/01/12
 * Time: 12:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib;

namespace KillingCurve
{
	/// <summary>
	/// Description of KCReadPlateState.
	/// </summary>
	[State("Read MPN Plate","Reads MPN plates after incubation")]
	public class KCReadPlateState:ReadState
	{
		new KCProtocol RunningInProtocol
		{
			get
			{return (KCProtocol) base.RunningInProtocol;}
		}
		int MPNLicInd;
			
		public KCReadPlateState(KCProtocol RunningInProtocol,
		                       int         MPNLicInd_):base((Protocol)RunningInProtocol)
		{
			MPNLicInd = MPNLicInd_;
		}
			
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(MPNLicInd);
			
			RJP.Add(new RobotJobParameter("MPNCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("MPNPos",RobotJobParameter.ParameterType.Number,LP.Pos));
			        
			RobotJob RJ = new RobotJob(@"D:\RobotScripts\KillingCurve\KC_Read.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			FileInfo MyFileInfo = GetMeasurementsResultsFile();
			string NewFileName = "MPN" + MPNLicInd.ToString() + @"_" +
 				                 String.Format("{0:yyyyMMddHHmm}", DateTime.Now) + @".xml";
			try 
			{
				MyFileInfo.MoveTo(MyFileInfo.Directory.FullName + @"\" + NewFileName);
			} catch (Exception ex) {
				throw(ex);
			}

		}
		
		public override string ToString()
		{
			string txt = this.GetType().ToString() +
				@" MPN Liconic index: " + MPNLicInd.ToString();
			
			return txt;
		}
		

		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCIncubateState)};
		}
		#endregion
				
	}
}
