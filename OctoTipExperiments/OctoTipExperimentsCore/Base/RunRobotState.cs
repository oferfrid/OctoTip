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
using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipExperiments.Core.Base
{
	/// <summary>
	/// Description of RunRobotState.
	/// </summary>
	public abstract class RunRobotState:State
	{
		RobotJob RunRobotJob=null;
		
		public RunRobotState():base()
		{
		}
		
		public RunRobotState(Protocol RunningInProtocol):base(RunningInProtocol)
		{
			
		}
		
		public abstract RobotJob GetRobotJob();
		
		public override void DoWork()
		{
			OctoTip.OctoTipLib.RobotJob.Status JobStatus;
			RunRobotJob = GetRobotJob();
			if (RunRobotJob == null)
			{
				throw new NullReferenceException("GetRobotJob returned a null RobotJob");
			}
			int StateSamplelingRate = Convert.ToInt32(ConfigurationManager.AppSettings["StateSamplelingRate"]);
			if (StateSamplelingRate == 0)
			{
				throw new NullReferenceException("AppSettings key for StateSamplelingRate is null");
			}
			
			// Inserting the Job in the queue
			RunRobotJob.TestJobParameters();
			RobotJobsQueueServiceClient RJQClient = new RobotJobsQueueServiceClient();
			
			try
			{
				Guid JobID = RJQClient.AddRobotJob(RunRobotJob);
				
				
				// keep monitoring the queue till the job status is changed to
				JobStatus = RJQClient.GetJobStatus(JobID);
				while ((JobStatus == RobotJob.Status.Queued   ||
				        JobStatus == RobotJob.Status.Enqueued ||
				        JobStatus == RobotJob.Status.Running) &&
				       !this.RunningInProtocol.ShouldStop        )
				{
					//TODO: Handling paused state
					if (this.RunningInProtocol.ShouldPause)
					{
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Paused,String.Empty));
						while(this.RunningInProtocol.ShouldPause && !this.RunningInProtocol.ShouldStop)
						{
							System.Threading.Thread.Sleep(StateSamplelingRate);
						}
						if (this.RunningInProtocol.ShouldStop)
						{
							this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Stopped,String.Empty));
						}
						else
						{
							this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Started,String.Empty));
						}
					}
				
					
					// Handling RuntimeError
					// TODO: paused while runtime error
					// TODO: create error status in protocol
					if (JobStatus == RobotJob.Status.RuntimeError)
					{
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(
							Protocol.ProtocolStatus.Error ,"Runtime Error"));
						this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
							this ,State.Status.RuntimeError, "Runtime Error"));
						while (JobStatus == RobotJob.Status.RuntimeError
						       && !this.RunningInProtocol.ShouldStop)
						{
							System.Threading.Thread.Sleep(StateSamplelingRate);
							JobStatus = RJQClient.GetJobStatus(JobID);
						}
						
						// Exiting runtime error could be for many reasons. 
						// Only if resumed running and stop request wasn't given, should we change the status back to running
						if (!this.RunningInProtocol.ShouldStop ||
						   JobStatus == RobotJob.Status.Running)
						{
							this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
								this ,State.Status.Active, "Running"));
						}
							
					}
					
					

					System.Threading.Thread.Sleep(StateSamplelingRate);
					JobStatus = RJQClient.GetJobStatus(JobID);
					
				}
				//TODO: Handling stoped state
				if (this.RunningInProtocol.ShouldStop)
				{
					this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Stopped,String.Empty));
				}
				else
				{
					// Handling termination statuses
					switch (JobStatus)
					{
						case RobotJob.Status.Failed:
							this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
								this , State.Status.Failed, "Failed"));
							break;
						case RobotJob.Status.TerminatedByUser:
							this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
								this , State.Status.Failed, "Terminated by user"));
							break;
						case RobotJob.Status.Finished:
							this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
								this , State.Status.Inactive, "Terminated successfully"));
							break;
					}
				}

			}
			catch (System.ServiceModel.EndpointNotFoundException e)
			{
				//TODO:HAve a nice output 
				throw e;
			}
			
		}
	}
}
