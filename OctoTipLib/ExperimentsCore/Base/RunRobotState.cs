/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 09/10/11
 * Time: 2:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using OctoTip.Lib;

namespace OctoTip.Lib.ExperimentsCore.Base
{
	/// <summary>
	/// Description of RunRobotState.
	/// </summary>
	public abstract class RunRobotState:State
	{
		protected RobotJob RunRobotJob=null;
		
		RobotJobsQueueServiceClient RJQClient ;
		
		
		public RunRobotState():base()
		{
		
		}
		
		
		
		protected abstract RobotJob BeforeRobotRun();
		protected abstract void AfterRobotRun();
		
		protected override void DoWork()
		{
			//Get the RobotJob from the dirived clases
			RunRobotJob = BeforeRobotRun();
			if (RunRobotJob == null)
			{
				throw new NullReferenceException("GetRobotJob returned a null RobotJob");
			}
			RobotJob.Status JobStatus = RobotJob.Status.Created;
			
			// Inserting the Job in the queue
			RunRobotJob.TestJobParameters();
			RJQClient = new RobotJobsQueueServiceClient();
			
			Guid JobID ;
			
			try 
			{
				 JobID = RJQClient.AddRobotJob(RunRobotJob);
				
			} 
			catch (System.ServiceModel.EndpointNotFoundException e)
			{
				throw new Exception("No Listener on " + RJQClient.Endpoint.ListenUri ,e);
			}
			RunRobotJob.UniqueID = JobID;
			this.Log(string.Format("Queued {0}, UniqueID: {1} (parameters={2})",RunRobotJob.ScriptName,RunRobotJob.UniqueID,RunRobotJob.RobotJobDisplayParameters));
			
			do
			{
				
				// keep monitoring the queue till the job status is changed to
				JobStatus = GetJobStatus(RunRobotJob);
				
				//pause state
				if (this.ShouldPause)
				{
					this.SetCurrentStatus(Statuses.Paused,"RobotJob Pausing not implimented yet");
					while(this.ShouldPause && !this.ShouldStop)
					{
						System.Threading.Thread.Sleep(this.StateSamplelingRate);
					}
					
					if (!this.ShouldPause && !this.ShouldStop)
					{
						this.SetCurrentStatus(State.Statuses.Started,"Protocol resumed");
					}
				}
				
				if (JobStatus == RobotJob.Status.RuntimeError)
				{
					this.SetCurrentStatus(State.Statuses.RuntimeError, "Runtime Error");
					
					while (JobStatus == RobotJob.Status.RuntimeError && !this.ShouldStop)
					{
						System.Threading.Thread.Sleep(StateSamplelingRate);
						JobStatus = GetJobStatus(RunRobotJob);
					}
					if(!this.ShouldStop)
					{
						this.SetCurrentStatus(State.Statuses.Started, "Runtime Error resumed");
					}
					else
					{
						break;
					}
					
				}
				System.Threading.Thread.Sleep(StateSamplelingRate);
				
			}
			while(
				JobStatus != RobotJob.Status.TerminatedByUser &&
				JobStatus != RobotJob.Status.Finished &&
				JobStatus != RobotJob.Status.Failed &&
				!this.ShouldStop);
			
			//Handling termination statuses
			switch (JobStatus)
			{
				case (RobotJob.Status.Failed):
					this.SetCurrentStatus(State.Statuses.FatalError, "Failed");
					this.RequestStop("Robot Job Failed");
					break;
				case (RobotJob.Status.TerminatedByUser):
					this.SetCurrentStatus(State.Statuses.FatalError, "Terminated by user");
					//this.RequestStop("Terminated by user");
					break;
				case (RobotJob.Status.Finished):
					this.SetCurrentStatus( State.Statuses.EndedSuccessfully, "Terminated successfully");
					AfterRobotRun();
					break;
			}
			
			
		}
		
		
		private RobotJob.Status GetJobStatus(RobotJob RunRobotJob)
		{
			RobotJob.Status JobStatus = RJQClient.GetJobStatus(RunRobotJob.UniqueID);
			string messege = string.Format("{0}({1})>{2}",RunRobotJob.ScriptName,RunRobotJob.UniqueID,JobStatus.ToString());
			this.DisplayData(messege);
			return JobStatus;
		}
	}
}
