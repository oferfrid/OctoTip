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
		
		public double? WaitMinuts = null;
		public DateTime? WaitUntil = null;
			
		public WaitState():base()
		{}
		
		
		public WaitState(Protocol RunningInProtocol ,double WaitMinuts):base(RunningInProtocol)
		{
			this.WaitMinuts = WaitMinuts;
		}
		
		public WaitState(Protocol RunningInProtocol ,DateTime WaitUntil):base(RunningInProtocol)
		{
				this.WaitUntil = WaitUntil;
		}
		
		public sealed override void DoWork()
		{
			
			OnWaitStart();
			int StateSumplelingRate = Convert.ToInt32(ConfigurationManager.AppSettings["StateSumplelingRate"]);
						if (StateSumplelingRate == 0)
						{
							throw new NullReferenceException("AppSettings key for StateSumplelingRate is null");
						}
						
			if	(WaitMinuts!=null)
			{
				
			}
			else if(WaitUntil!=null)
			{
				
				while (!RunningInProtocol.ShouldStop &&
				       RemainingTime().TotalMinutes<0)
				{
					if (this.RunningInProtocol.ShouldPause)
					{
						
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Paused,String.Empty));
						while(this.RunningInProtocol.ShouldPause)
						{
							Thread.Sleep(StateSumplelingRate);			
						}
						this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Started,String.Empty));
					}
					
					Thread.Sleep(StateSumplelingRate);
					
				}
				
			
			}
			else
			{
				throw new ArgumentNullException("WaitUntil,WaitMinuts");
			}
		
			if (!this.RunningInProtocol.ShouldStop)
			{
				OnWaitEnd();
			}
			
		}
		
		private TimeSpan RemainingTime()
		{
			TimeSpan RemainingTime = TimeSpan.Zero;
			if	(WaitMinuts!=null)
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
