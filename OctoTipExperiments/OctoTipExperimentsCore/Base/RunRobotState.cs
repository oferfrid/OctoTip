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
		
		RobotJobsQueueServiceClient RJQClient ;
		
		public int StateSamplelingRate;
		
		public RunRobotState():base()
		{
		}
		
		public RunRobotState(Protocol RunningInProtocol):base(RunningInProtocol)
		{
			StateSamplelingRate = Convert.ToInt32(ConfigurationManager.AppSettings["StateSamplelingRate"]);
			if (StateSamplelingRate == 0)
			{
				throw new NullReferenceException("AppSettings key for StateSamplelingRate is null");
			}
			
		}
		
		protected abstract RobotJob BeforeRobotRun();
		protected abstract void AfterRobotRun();
		
		public override void DoWork()
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
			
			
			
			Guid JobID = RJQClient.AddRobotJob(RunRobotJob);
			RunRobotJob.UniqueID = JobID;
			
			
			do
			{
				
				// keep monitoring the queue till the job status is changed to
				JobStatus = GetJobStatus(RunRobotJob);
				
				//pause state
				if (this.RunningInProtocol.ShouldPause)
				{
					this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Paused,"RobotJob Pausing not implimented yet"));
					while(this.RunningInProtocol.ShouldPause)
					{
						System.Threading.Thread.Sleep(StateSamplelingRate);
					}
					if (!this.RunningInProtocol.ShouldStop)
					{
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Started,"Protocol resumed"));
					}
				}
				
				if (JobStatus == RobotJob.Status.RuntimeError)
				{
					this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.RuntimeError ,"Runtime Error"));
					this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(this ,State.Status.RuntimeError, "Runtime Error"));
					
					while (JobStatus == RobotJob.Status.RuntimeError && !this.RunningInProtocol.ShouldStop)
					{
						System.Threading.Thread.Sleep(StateSamplelingRate);
						JobStatus = GetJobStatus(RunRobotJob);
					}
					if(!this.RunningInProtocol.ShouldStop)
					{
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Started ,"Runtime Error resumed"));
						this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(this ,State.Status.Active, "Runtime Error resumed"));
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
				!this.RunningInProtocol.ShouldStop);
			
			//Handling termination statuses
			switch (JobStatus)
			{
				case (RobotJob.Status.Failed):
					this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
						this , State.Status.Failed, "Failed"));
					RunningInProtocol.RequestStop();
					break;
				case (RobotJob.Status.TerminatedByUser):
					this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
						this , State.Status.Failed, "Terminated by user"));
					RunningInProtocol.RequestStop();
					break;
				case (RobotJob.Status.Finished):
					this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
						this , State.Status.Inactive, "Terminated successfully"));
					AfterRobotRun();
					break;
			}
			
			
		}
		
		
//
//
//
//						{
//
		////					//TODO: Handling paused state
		////					if (this.RunningInProtocol.ShouldPause)
		////					{
		////						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Paused,String.Empty));
		////						while(this.RunningInProtocol.ShouldPause && !this.RunningInProtocol.ShouldStop)
		////						{
		////							System.Threading.Thread.Sleep(StateSamplelingRate);
		////						}
		////						if (this.RunningInProtocol.ShouldStop)
		////						{
		////							this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Stopped,String.Empty));
		////						}
		////						else
		////						{
		////							this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Started,String.Empty));
		////						}
		////					}
//
//
//							// Handling RuntimeError
//							// TODO: paused while runtime error
//							// TODO: create error status in protocol
//							if (JobStatus == RobotJob.Status.RuntimeError)
//							{
//								this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(
//									Protocol.ProtocolStatus.RuntimeError ,"Runtime Error"));
//								this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
//									this ,State.Status.RuntimeError, "Runtime Error"));
//								while (JobStatus == RobotJob.Status.RuntimeError
//								       && !this.RunningInProtocol.ShouldStop)
//								{
//									System.Threading.Thread.Sleep(StateSamplelingRate);
//									JobStatus = RJQClient.GetJobStatus(RunRobotJob.UniqueID);
//								}
//
//								// Exiting runtime error could be for many reasons.
//								// Only if resumed running and stop request wasn't given, should we change the status back to running
//								if (!this.RunningInProtocol.ShouldStop ||
//								    JobStatus == RobotJob.Status.Running)
//								{
//									this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
//										this ,State.Status.Active, "Running"));
//								}
//
//							}
//
//
//
//							System.Threading.Thread.Sleep(StateSamplelingRate);
//							JobStatus = RJQClient.GetJobStatus(RunRobotJob.UniqueID);
//
//						}
//						//TODO: Handling stoped state
//						if (this.RunningInProtocol.ShouldStop)
//						{
//							this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Stopped,String.Empty));
//						}
//						else
//						{
//							// Handling termination statuses
//							switch (JobStatus)
//							{
//								case RobotJob.Status.Failed:
//									this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
//										this , State.Status.Failed, "Failed"));
//									RunningInProtocol.RequestStop();
//									break;
//								case RobotJob.Status.TerminatedByUser:
//									this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
//										this , State.Status.Failed, "Terminated by user"));
//									RunningInProtocol.RequestStop();
//									break;
//								case RobotJob.Status.Finished:
//									this.RunningInProtocol.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(
//										this , State.Status.Inactive, "Terminated successfully"));
//									AfterRobotRun();
//									break;
//							}
//						}

		
		
		
		
		
		
		private RobotJob.Status GetJobStatus(RobotJob RunRobotJob)
		{
			RobotJob.Status JobStatus = RJQClient.GetJobStatus(RunRobotJob.UniqueID);
			string messege = string.Format("{0}({1})>{2}",RunRobotJob.ScriptName,RunRobotJob.UniqueID,JobStatus.ToString());
			this.RunningInProtocol.OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs(messege));
			
			return JobStatus;
		}
	}
}
