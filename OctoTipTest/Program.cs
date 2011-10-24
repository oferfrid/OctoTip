/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 21/09/2011
 * Time: 10:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using OctoTip.OctoTipExperiments;
using OctoTip.OctoTipLib;
using OctoTip.OctoTipExperiments.Core.Base;

namespace OctoTip.OctoTipTest
{
	
	class Program
	{
//		static void  HandleChangeStatusEvent(object sender, RobotWrapperEventArgs e)
//		{
//			Console.WriteLine("received this message: {0}", e.ScriptTerminationStatus.ToString());
//		}
//
		public static void Main(string[] args)
		{
			
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(3);
			
			LicPos LP = Utils.Ind2LicPos(1);
			
			RJP.Add(new RobotJobParameter("Lic6Cart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic6Pos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("AMPWellInd",RobotJobParameter.ParameterType.Number,24));
			        
			RobotJob RJ = new RobotJob(@"D:\OctoTip\SampleData\Evo1\EvoStarter.esc",RJP);
			
			OctoTip.OctoTipLib.RobotJobsQueueServiceClient SC = new RobotJobsQueueServiceClient();
			SC.AddRobotJob(RJ);
			
						
			Console.ReadKey();
		}
		
		
		
		
	}
}


