/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 08/10/2011
 * Time: 07:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Threading;

namespace OctoTip.OctoTipExperiments.Core.Base
{
	/// <summary>
	/// Description of WaitState.
	/// </summary>
	public abstract class WaitState:State
	{
		
		public double? WaitMinutes = null;
		public DateTime? WaitUntil = null;
		private TimeSpan TotalPause = TimeSpan.Zero;
		private DateTime? PauseStarted = null;
			
		public WaitState():base()
		{}
		
		
		public WaitState(Protocol RunningInProtocol ,double WaitMinutes):base(RunningInProtocol)
		{
			this.WaitMinutes = WaitMinutes;
		}
		
		public WaitState(Protocol RunningInProtocol ,DateTime WaitUntil):base(RunningInProtocol)
		{
				this.WaitUntil = WaitUntil;
		}
		
		public sealed override void DoWork()
		{
			
			OnWaitStart();
			int StateSamplelingRate = Convert.ToInt32(ConfigurationManager.AppSettings["StateSamplelingRate"]);
						if (StateSamplelingRate == 0)
						{
							throw new NullReferenceException("AppSettings key for StateSamplelingRate is null");
						}
						
			if	(WaitMinutes!=null)
			{
				while (!RunningInProtocol.ShouldStop &&
				       RemainingTime().TotalMinutes<0)
				{
					if (this.RunningInProtocol.ShouldPause)
					{
						PauseStarted = DateTime.Now;
						
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Paused,String.Empty));
						while(this.RunningInProtocol.ShouldPause && !this.RunningInProtocol.ShouldStop)
						{
							Thread.Sleep(StateSamplelingRate);			
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
					
					Thread.Sleep(StateSamplelingRate);	
				}
				
			}
			else if(WaitUntil!=null)
			{
				
				while (!RunningInProtocol.ShouldStop &&
				       RemainingTime().TotalMinutes<0)
				{
					if (this.RunningInProtocol.ShouldPause)
					{
						
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Paused,String.Empty));
						while(this.RunningInProtocol.ShouldPause && !this.RunningInProtocol.ShouldStop)
						{
							Thread.Sleep(StateSamplelingRate);			
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
					
					Thread.Sleep(StateSamplelingRate);
					
				}
				
			
			}
			else
			{
				throw new ArgumentNullException("WaitUntil,WaitMinutes");
			}
		
			if (!this.RunningInProtocol.ShouldStop)
			{
				OnWaitEnd();
			}
			
		}
		
		private TimeSpan RemainingTime()
		{
			TimeSpan RemainingTime = TimeSpan.Zero;
			if	(WaitMinutes!=null)
			{
				
			}
			else if(WaitUntil!=null)
			{
				RemainingTime = (TimeSpan)(WaitUntil - DateTime.Now) ;
			}
		return RemainingTime;
		}
		
		protected abstract void OnWaitEnd();
		protected abstract void OnWaitStart();
	
		
	}
}
