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

using OctoTip.Lib;

namespace OctoTip.Manager
{
	
	[ServiceContract]
	public interface IRobotJobsQueueService
	{
		[OperationContract]
		string TestConnection(string name);
		[OperationContract]
		Guid AddRobotJob(RobotJob RJ);
		[OperationContract]
		string GetRobotStatus();
		[OperationContract]
		OctoTip.Lib.RobotJob.Status GetJobStatus(Guid UniqueID);
	}


	/// <summary>
	/// Description of RobotJobsQueueService.
	/// </summary>
	public class RobotJobsQueueService:IRobotJobsQueueService
	{
		
		LogString logger;
		public RobotJobsQueueService()
		{
			logger = LogString.GetLogString(Manager.MainForm.LOG_NAME);
		}
		
		public string TestConnection(string name)
		{
			string response = string.Format("Test Connection from {0}", name);
			//logger.Add(response);
			return string.Format(response);
		}
		
		public Guid AddRobotJob(RobotJob RJ)
		{
			Guid UniqueID = OctoTip.Manager.MainForm.FormRobotJobsQueue.InsertRobotJob(RJ);
			//MainForm.FormRobotJobsQueueHestoryDictionary.Add(RJ.UniqueID,RobotJob.Status.Queued);
			string Message = string.Format("Added new job, {0} as UniqueID: {1}", RJ.ScriptName,UniqueID );
			//logger.Add(Message);
			return UniqueID;
		}
		
		public RobotJob.Status GetJobStatus(Guid UniqueID)
		{
			RobotJob.Status JobStatus = RobotJob.Status.Failed;
			foreach(RobotJob RJ in OctoTip.Manager.MainForm.FormRobotJobsQueue)
			{
				if (RJ.UniqueID == UniqueID)
				{
					JobStatus = RJ.JobStatus;
				}
			}
			
			//string Message = string.Format("Statuses of Script UniqueID: {0} is: {1} ",UniqueID,SS );
			//logger.Add(Message);
			return JobStatus;
		}
		
	
		public string GetRobotStatus()
		{
			throw new NotImplementedException("Ha Ha Ha");
//			string RobotStatus = "OK";
//			string LogEntery = string.Format("GetRobotStatus, responed {0}", RobotStatus);
//			
//			logger.Add(LogEntery);
//TODO: DoIT!
//			return RobotStatus;
		}
		
	}
}
