/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 27/09/2011
 * Time: 14:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using EVOAPILib;

using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipManager
{
	/// <summary>
	/// Description of RobotWrapper.
	/// </summary>
	public class RobotWrapper
	{
		private EVOAPILib.System Evo;
		public  const string LOG_NAME = "OctoTipManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
			
		public RobotWrapper()
		{
			Evo = new EVOAPILib.SystemClass();
//			Evo.StatusChanged +=  Evo_StatusChanged;
		}
		
		private void Logon()
		{			
			SC_Status RobotStatus;
			string UserName = ConfigurationManager.AppSettings["UserName"];
			string Password = ConfigurationManager.AppSettings["Password"];

	
			try
			{
				Evo.Logon(UserName,Password,0,0);
				RobotStatus = Evo.GetStatus();
				Console.WriteLine(RobotStatus.ToString());
				// initialazing
				if (RobotStatus == SC_Status.STATUS_NOTINITIALIZED)
				{
					Evo.Initialize();
					Console.WriteLine(RobotStatus.ToString());
				}
			}
			catch(Exception e)
			{
				myLogger.Add("Logon Error");
				myLogger.Add(e.ToString());
				throw e;
			}			
		}
		
		private void Logoff()
		{
			try
			{
				Evo.Logoff();
			}
			catch(Exception e)
			{
				myLogger.Add("Logoff Error");
				myLogger.Add(e.ToString());
				throw e;
			}
		}
		
		public SC_ScriptStatus GetScriptStatus(int ScriptID)
		{
			SC_ScriptStatus SS = Evo.GetScriptStatus(ScriptID);
			return SS;
		}
		
		public void RunScript(string ScriptName)
		{		
			SC_ScriptStatus ScriptStatus     = SC_ScriptStatus.SS_UNKNOWN;
			SC_ScriptStatus PrevScriptStatus = SC_ScriptStatus.SS_UNKNOWN;
			SC_Status		RobotStatus      = SC_Status.STATUS_UNKNOWN;
			SC_Status		PrevRobotStatus  = SC_Status.STATUS_UNKNOWN;
			try
			{
				Logon();
				Console.WriteLine("in run script");
				int ScriptID = Evo.PrepareScript(ScriptName);
				RobotStatus = Evo.GetStatus();
				Console.WriteLine(RobotStatus.ToString());
				Evo.StartScript(ScriptID, 0, 0);
				DateTime StartTime = DateTime.Now;
				TimeSpan TS = DateTime.Now - StartTime;

				while(TS.TotalSeconds<60)
				{
					ScriptStatus = Evo.GetScriptStatus(ScriptID);
					RobotStatus  = Evo.GetStatus();
										
					if ((ScriptStatus != PrevScriptStatus) || (RobotStatus != PrevRobotStatus))
					{
						Console.WriteLine(ScriptStatus.ToString());
						Console.WriteLine(RobotStatus.ToString());
						PrevScriptStatus = ScriptStatus;
						PrevRobotStatus  = RobotStatus;
					}
					TS = (DateTime.Now - StartTime);					
				}
//				Console.WriteLine("Press any key to continue . . . ");
//				Console.ReadKey(true);
			}
			catch(Exception e)
			{
				myLogger.Add("Run Script Error");
				myLogger.Add(e.ToString());
			}
			finally
			{
				Logoff();
			}
		}
		
//		public void Evo_StatusChanged(object sender, EventArgs e)
//		{
//		 
//		}
	
	}
}
