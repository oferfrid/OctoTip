/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 21/09/2011
 * Time: 10:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.OctoTipTest
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			
			OctoTip.OctoTipLib.RobotJob RP = new OctoTip.OctoTipLib.RobotJob(@"C:\Users\Public\Documents\Learn\BioLab\programing\OctoTip\SampleData\" + "NewScript1.esc");
			RP.TestJob();
			OctoTipLib.RobotJobsQueue RJQ = new OctoTipLib.RobotJobsQueue();
			RJQ.InsertRobotJob(RP);
			
			OctoTip.OctoTipLib.RobotJob RP1 = new OctoTip.OctoTipLib.RobotJob(@"C:\Users\Public\Documents\Learn\BioLab\programing\OctoTip\SampleData\" + "NewScript2.esc");
			RJQ.InsertRobotJob(RP1);
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}