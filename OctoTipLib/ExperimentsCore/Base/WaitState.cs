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
using System.Runtime.CompilerServices;
using System.Threading;

namespace OctoTip.Lib.ExperimentsCore.Base
{
	/// <summary>
	/// Description of WaitState.
	/// </summary>
	public abstract class WaitState:State
	{
		
		public TimeSpan WaitTime = TimeSpan.Zero;
		public DateTime? WaitUntil = null;
		
		public TimeSpan TotalPause = TimeSpan.Zero;
		public DateTime PauseStarted = DateTime.MaxValue;
		public DateTime WaitStarted = DateTime.MaxValue;
			
		public WaitState():base()
		{}
		
		
		public WaitState(TimeSpan WaitTime):base()
		{
			this.WaitTime = WaitTime;
		}
		
		public WaitState(DateTime WaitUntil):base()
		{
				this.WaitUntil = WaitUntil;
		}
		
		protected sealed override void DoWork()
		{
			
			OnWaitStart();
			
						
			if	(WaitTime!= TimeSpan.Zero)
			{
				WaitStarted = DateTime.Now;
				while (!this.ShouldStop && RemainingTime().TotalMinutes>0)
				{
					if (this.ShouldPause)
					{
						PauseStarted = DateTime.Now;
						
						this.SetCurrentStatus(Statuses.Paused,string.Empty);
						while(this.ShouldPause && !this.ShouldStop)
						{
							Thread.Sleep(StateSamplelingRate);
						}
						
						
						TotalPause += (DateTime.Now - PauseStarted );
						PauseStarted = DateTime.MaxValue;
						
						if (this.ShouldStop)
						{
							this.SetCurrentStatus(Statuses.Stopped,string.Empty);
						}
						else
						{
							this.SetCurrentStatus(Statuses.Started,string.Empty);
						}
					}
					
					this.DisplayData(RemainingTime().TotalMinutes.ToString("0.00") + "(minuts)");
					Thread.Sleep(StateSamplelingRate);	
				}
				
			}
			else if(WaitUntil!=null)
			{
				
				while (!this.ShouldStop &&  RemainingTime().TotalMinutes>0)
				{
					if (this.ShouldPause)
					{
						
						this.SetCurrentStatus(Statuses.Paused,String.Empty);
						while(this.ShouldPause && !this.ShouldStop)
						{
							DisplayData(RemainingTime().TotalMinutes.ToString("0.00") + "(minuts)");
							Thread.Sleep(StateSamplelingRate);			
						}
						
						if (this.ShouldStop)
						{
							this.SetCurrentStatus(Statuses.Stopped,String.Empty);
						}
						else
						{
							this.SetCurrentStatus(Statuses.Started,String.Empty);
						}
					}
					DisplayData(RemainingTime().TotalMinutes.ToString("0.00") + "(minuts)");
					Thread.Sleep(StateSamplelingRate);
					
				}
				
			
			}
			else
			{
				throw new ArgumentNullException("WaitUntil,WaitMinutes");
			}
		
			if (!this.ShouldStop)
			{
				OnWaitEnd();
			}
			
		}
		
		private TimeSpan RemainingTime()
		{
			TimeSpan RemainingTime = TimeSpan.Zero;
			if	(WaitTime!=TimeSpan.Zero)
			{
				RemainingTime = (WaitTime.Add(TotalPause)).Subtract(DateTime.Now - WaitStarted) ;
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
