/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 24/09/2011
 * Time: 11:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipManager
{
	
	[ServiceContract]
	public interface IRobotJobsQueueService
	{
		[OperationContract]
		string SayHello(string name);
	}


	/// <summary>
	/// Description of RobotJobsQueueService.
	/// </summary>
	public class RobotJobsQueueService:IRobotJobsQueueService
	{
		 LogString logger;
		public RobotJobsQueueService()
		{
			     logger = LogString.GetLogString(OctoTipManager.MainForm.LOG_NAME);
			      logger.Add("RobotJobsQueueService started");
            
		}
		
		public string SayHello(string name)
		{
			
			
			string response = string.Format("Hello, {0}", name);
                logger.Add(response);
			return string.Format(response);
		}
		
	}
}
