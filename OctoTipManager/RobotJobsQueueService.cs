﻿/*
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
		string TestConnection(string name);
		[OperationContract]
		void AddRobotJob(RobotJob RJ);
		[OperationContract]
		string GetRobotStatus();
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
		}
		
		public string TestConnection(string name)
		{
			string response = string.Format("Test Connection from {0}", name);
			logger.Add(response);
			return string.Format(response);
		}
		
		
		public void AddRobotJob(RobotJob RJ)
		{
			OctoTip.OctoTipManager.MainForm.RJQ.InsertRobotJob(RJ);
			string response = string.Format("Added new job, {0}", RJ.ScriptName );
			
			logger.Add(response);
		}
		
		
	
		public string GetRobotStatus()
		{
			string RobotStatus = "OK";
			string LogEntery = string.Format("GetRobotStatus, responed {0}", RobotStatus);
			
			logger.Add(LogEntery);
			//TODO: DoIT!
			return RobotStatus;
		}
		
	}
}