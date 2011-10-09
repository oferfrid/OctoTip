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
using System.Threading;
using System.Xml.Serialization;
using OctoTip.OctoTipExperiments.Protocols;
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
			
			//XmlSerializer writer =	new XmlSerializer(typeof(WaitState));
			
			
			
			
			Console.WriteLine("start init RobotJobsQueueServiceClient!");


			


			


			List<RobotJobParameter> RP= new List<RobotJobParameter>();
			RP.Add(new RobotJobParameter("v1",RobotJobParameter.ParameterType.Number,Convert.ToInt32(args[0])));
			RP.Add(new RobotJobParameter("v2",RobotJobParameter.ParameterType.String,"dfasd"));
			
			OctoTip.OctoTipLib.RobotJob RJ = new OctoTip.OctoTipLib.RobotJob(@"D:\OctoTip\SampleData\" + "Temp.esc",RP);
			//RJ.CreateScript();
			
			
			Random r = new Random();
			RJ.Priority = (double)r.Next()/int.MaxValue;
			
			
			
			RobotJobsQueueServiceClient RC = new RobotJobsQueueServiceClient();
			//Console.WriteLine("Robot Status: {0}",RC.GetRobotStatus());
			RC.TestConnection("tt");
			Guid G = RC.AddRobotJob(RJ);
			Console.WriteLine(G);
			 ConsoleKeyInfo cki;

			do
			{
				cki = Console.ReadKey();
				
				Console.WriteLine("GetStatus? (Esc for exit)");
				Console.WriteLine(RC.GetJobStatus(G));
				
			}
			while (cki.Key != ConsoleKey.Escape);
			
		}

		
		
//
//
//
//			Console.WriteLine("exit?");
//			Console.ReadKey();
		
		
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
		
		
//
//			RobotWrapper RW = new RobotWrapper();
//			//pub.RaiseCustomEvent += HandleCustomEvent;
//			RW.StatusChangeEvent += HandleChangeStatusEvent; //HandleChangeStatusEvent;
//
//			try
//			{
//				Thread T = new Thread(RW.RunScript);
//				T.Start("temp");
//				System.Threading.Thread.Sleep(20000);
//				Console.WriteLine("requesting pause");
//				RW.RequestPause();
//
//				System.Threading.Thread.Sleep(20000);
//				Console.WriteLine("requesting resume");
//				RW.RequestResume();
//
		////				RW.CheckScriptStatus();
//
		////				if (STS != ScriptTerminationStatusType.Success)
		////				{
		////					Console.WriteLine("script failed");
		////				}
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine(e.ToString());
//				Console.Write("Press any key to continue . . . ");
//				Console.ReadKey(true);
//			}
		////			RobotWrapper RW = new RobotWrapper();
		////			RW.RunScript("Temp");
		////				Console.WriteLine(ProtocolData.Name);
//			//            }
//
//
//			Console.Write("Press any key to continue . . . ");
//			Console.ReadKey(true);
//
		////			Console.Write("Press any key to continue . . . ");
		////			Console.ReadKey(true);
	}
	
}


