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
using System.Reflection;
using System.Xml.Serialization;
using OctoTip.OctoTipExperiments.Protocols;
using OctoTip.OctoTipLib;
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipManager;

namespace OctoTip.OctoTipTest
{
	
	class Program
	{
		public static void Main(string[] args)
		{
			
			//XmlSerializer writer =	new XmlSerializer(typeof(WaitState));
			
			
			
			
			Console.WriteLine("start init RobotJobsQueueServiceClient!");


			RobotJobsQueueServiceClient RC = new RobotJobsQueueServiceClient();


			Console.WriteLine("Robot Status: {0}",RC.GetRobotStatus());


			List<RobotJobParameter> RP= new List<RobotJobParameter>();
			RP.Add(new RobotJobParameter("v1",RobotJobParameterType.Number,4.4));
			RP.Add(new RobotJobParameter("v1",RobotJobParameterType.String,"dfasd"));
			       
			OctoTip.OctoTipLib.RobotJob RJ = new OctoTip.OctoTipLib.RobotJob(@"C:\Users\Public\Documents\Learn\BioLab\programing\OctoTip\SampleData\" + "Temp.esc",RP);
			//RJ.CreateScript();
			
			
			Random r = new Random();
			RJ.Priority = (double)r.Next()/int.MaxValue;
			RC.TestConnection("tt");
			RC.AddRobotJob(RJ);


			
			
//			List<Type> ProtocolList = availableTypes.FindAll(delegate(Type t)
//			                                                 {
//			                                                 	List<Type> interfaceTypes = new List<Type>(t.GetInterfaces());
//			                                                 	object[] arr = t.GetCustomAttributes(typeof(ProtocolAttribute), true);
//			                                                 	return !(arr == null || arr.Length == 0) && interfaceTypes.Contains(typeof(IProtocol));
//			                                                 });
//
			
//			foreach (Type ProtocolData in ProtocolProvider.ProtocolsData)
			//            {
//			foreach (Type ProtocolData in ProtocolHostProvider.ProtocolsData)
			//            {
//				Console.WriteLine(ProtocolData.Name);
			//            }
			
//			RobotWrapper RW = new RobotWrapper();
//			RW.RunScript("Temp");
//				Console.WriteLine(ProtocolData.Name);
			//            }

			
			
//			Console.Write("Press any key to continue . . . ");
//			Console.ReadKey(true);
		}
	}
}

