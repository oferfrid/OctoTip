/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 27/09/2011
 * Time: 15:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Timers;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace OctoTip.OctoTipExperiments
{
	/// <summary>
	/// Description of WaitState.
	/// </summary>
	[State("Incubate","What for sum time in incubatir")]
	public class IncubateState:State
	{
		
		Timer tmr ;
		public DateTime WaitStarted;
		public TimeSpan PauseTime;
		public DateTime PauseStarted;
		
		public int Cycle;
		
		public double minutes2Wait;
		
		public IncubateState():base()
		{
			
		}
		
		public IncubateState(Protocol RunningInProtocol , double minutes2Wait ):base(RunningInProtocol)
		{
						
			tmr = new Timer();       // Doesn't require any args
			tmr.Interval = 100;
			
			tmr.Elapsed += tmr_Elapsed; // Uses an event instead of a delegate
			
			this.minutes2Wait = minutes2Wait;
			
			ZeroCounters();
		}
		
		private void ZeroCounters()
		{
			WaitStarted=DateTime.MaxValue;
			PauseTime = TimeSpan.Zero;
			PauseStarted =DateTime.MaxValue;
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(WaitState)};
		}
		#endregion
		public override void DoWork()
		{
			WaitStarted	= DateTime.Now;
			tmr.Start();
			
			
			while (!RunningInProtocol.ShouldStop && !(GetRemainingTime().Ticks<0.0))
			{
				
				if (RunningInProtocol.ShouldPause && PauseStarted==DateTime.MaxValue)
				{//status change to Pause
					PauseStarted = 		DateTime.Now;
					this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Paused,"Countdwone Paused"));
				}
				
				if (!RunningInProtocol.ShouldPause && PauseStarted!=DateTime.MaxValue)
				{//resturting
					PauseTime = PauseTime.Add(DateTime.Now - PauseStarted);
					PauseStarted=DateTime.MaxValue;
					this.RunningInProtocol.OnStatusChanged(new ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus.Started,"Countdwone resumed"));
				}
				
				System.Threading.Thread.Sleep(1000);
				
			}
			
			tmr.Stop();
			ZeroCounters();
			
		}
		
		private TimeSpan GetRemainingTime()
		{
			TimeSpan Time2Wait = (WaitStarted.AddMinutes(minutes2Wait) - DateTime.Now);
			Time2Wait = Time2Wait.Add(PauseTime);
			
			if(PauseStarted!=DateTime.MaxValue)
			{
				Time2Wait = Time2Wait.Add(DateTime.Now - PauseStarted);
			}
			return Time2Wait;
		}
		
		void tmr_Elapsed (object sender, EventArgs e)
		{
			//TODO:Update view
			this.RunningInProtocol.OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs("Terminting in " + GetRemainingTime().TotalSeconds + " seconds Now: " + DateTime.Now ));
		}
		
	}
}
