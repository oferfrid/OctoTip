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
					
					//TODO: Handling stoped state
					if (this.RunningInProtocol.ShouldStop)
					{
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Stopped,String.Empty));
					}
					
					// Handling RuntimeError
					// TODO: paused while runtime error
					// TODO: create error status in protocol
					if (JobStatus == RobotJob.Status.RuntimeError)
					{
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Started,"Runtime Error"));
						while (JobStatus == RobotJob.Status.RuntimeError
						       && !this.RunningInProtocol.ShouldStop)
						{
							System.Threading.Thread.Sleep(StateSamplelingRate);
							JobStatus = RJQClient.GetJobStatus(JobID);
						}
					}
					
					

					System.Threading.Thread.Sleep(StateSamplelingRate);
					JobStatus = RJQClient.GetJobStatus(JobID);
					
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
