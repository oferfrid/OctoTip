/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 20:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using OctoTip.OctoTipExperiments.Core.Interfaces;
using OctoTip.Lib;

namespace OctoTip.Lib.ExperimentsCore.Base
{
	/// <summary>
	/// Description of Protocol.
	/// </summary>
	public abstract class Protocol:IProtocol
	{
		
		#region Static
		public static  List<Type> ProtocolStates()
		{
			return new List<Type>(0);
		}
		#endregion
		
		
		public State CurrentState;
		
		private Thread RunningThread;
		
		
		protected int StateSamplelingRate;
		
		//Log
		public const string LOG_NAME = "OctoTipExperimentManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		private LogString myProtocolLogger;
		
		
		public  ProtocolParameters ProtocolParameters;
		
		public  Protocol(ProtocolParameters ProtocolParameters)
		{
			this.ProtocolParameters = ProtocolParameters;
			RunningThread = new Thread(DoWork);
			myProtocolLogger = LogString.GetLogString(ProtocolParameters.Name);
			
		}
		public  Protocol()
		{
			StateSamplelingRate = Convert.ToInt32(ConfigurationManager.AppSettings["StateSamplelingRate"]);
			if (StateSamplelingRate == 0)
			{
				throw new NullReferenceException("AppSettings key for StateSamplelingRate is null");
			}
		}
		~Protocol()
		{
			CurrentState = null;
			//TODO:maybe terminate runing RunningThread.
		}
		
		
		public void SetNewProtocolParameters(ProtocolParameters ProtocolParameters)
		{
			this.ProtocolParameters = ProtocolParameters;
		}
		#region Public Propertis
		public Statuses Status
		{
			get {return _CurrentStatus;}
		}

		
		#endregion
		#region status Handeling
		
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		protected volatile bool ShouldStop = false;
		protected volatile bool ShouldPause = false;
		
		
		private Statuses _CurrentStatus = Statuses.Stopped;
		
		
		public void ChangeState(State NewState)
		{
			if (CurrentState !=null)
			{
				//remove event handeling from the recent state.
				CurrentState.StateDisplayedDataChange -=  OnProtocolStateDisplayedDataChange;
				CurrentState.StateStatusChange -=	OnProtocolStateStatusChange;
			}
			
			if(!this.ShouldStop)
			{
				//do not move to the next state if paused.
				if (ShouldPause && !ShouldStop)
				{
					SetCurrentStatus( Statuses.Paused,"Paused between states");
					while (ShouldPause && !ShouldStop)
					{
						Thread.Sleep(StateSamplelingRate);
					}
					
				}
				if(!this.ShouldStop)
				{
					SetCurrentStatus( Statuses.Started,"Started");				
					CurrentState = NewState;
					CurrentState.StateDisplayedDataChange += OnProtocolStateDisplayedDataChange;
					CurrentState.StateStatusChange +=	OnProtocolStateStatusChange;
					Log(CurrentState.GetType().ToString() + " Started");
					CurrentState.Start();
					Log(CurrentState.GetType().ToString() + " Ended");
				}
				
			}
		}

		protected void SetCurrentStatus(Statuses CurrentStatus,string Messege )
		{
			//Log the status change And Raise the event.
			Log(string.Format("{0}>{1} {2}",_CurrentStatus , CurrentStatus , Messege));
			_CurrentStatus = CurrentStatus;
			OnStatusChanged(new  ProtocolStatusChangeEventArgs(CurrentStatus,Messege));
		}
		

		public event EventHandler<ProtocolStatusChangeEventArgs> StatusChanged;
		public event EventHandler<ProtocolDisplayedDataChangeEventArgs> DisplayedDataChange;
		public event EventHandler<StateDisplayedDataChangeEventArgs> StateDisplayedDataChange;
		public event EventHandler<StateStatusChangeEventArgs> StateStatusChange;
		
	
		private void OnStatusChanged(ProtocolStatusChangeEventArgs e)
		{
			EventHandler<ProtocolStatusChangeEventArgs> handler = StatusChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		
		protected  void DisplayData(string Messege)
		{
			ProtocolDisplayedDataChangeEventArgs e = new ProtocolDisplayedDataChangeEventArgs(Messege);
			EventHandler<ProtocolDisplayedDataChangeEventArgs> handler = DisplayedDataChange;
			if (handler != null)
			{
				handler(this, e);
			}
		}
				
		private void OnProtocolStateDisplayedDataChange(object sender,StateDisplayedDataChangeEventArgs e)
		{
			EventHandler<StateDisplayedDataChangeEventArgs> handler = StateDisplayedDataChange;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		
		protected  virtual void OnProtocolStateStatusChange(object sender,StateStatusChangeEventArgs e)
		{
			EventHandler<StateStatusChangeEventArgs> handler = StateStatusChange;
			if (handler != null)
			{
				handler(this, e);
			}
			
			switch(e.StateStatus)
			{
					case State.Statuses.Paused:
					this.SetCurrentStatus(Statuses.Paused,"Paused from state");
					break;
					case State.Statuses.Started:
					this.SetCurrentStatus(Statuses.Started,"started from state");
					break;
					case State.Statuses.Stopped:
					this.SetCurrentStatus(Statuses.Stopped,"Stopped From State");
					break;
					case State.Statuses.FatalError:
					this.SetCurrentStatus(Statuses.FatalError,"FatalError From State");
					break;
					
					case State.Statuses.RuntimeError:
					this.SetCurrentStatus(Statuses.RuntimeError,"RuntimeError From State");
					break;
					
					case State.Statuses.EndedSuccessfully:
					this.SetCurrentStatus(Statuses.EndedSuccessfully,"Ended Successfully From State");
					break;
		
			//Stoping ??,
			//Starting, ??
			// Pausing, ??
			

			}
			
			
			this.Log(string.Format("State {0} status changed to: {1}",e.CurrentState.GetType().Name,e.StateStatus));
		}
		
		
		
		public void   RequestStop()
		{
			SetCurrentStatus(Statuses.Stoping,"Stop Requested , Trying to Stopped");
			ShouldStop = true;
			ShouldPause  = false;
			CurrentState.RequestStop("Stop Reqested From Protocol");
		}
		public void RequestPause()
		{
			SetCurrentStatus(Statuses.Pausing,"Pause Requested, Trying to Pause");
			ShouldPause  = true;
			CurrentState.RequestPause();
		}
		public void RequestStart()
		{
			switch(this.Status)
			{
				case(Statuses.Paused):
					SetCurrentStatus(Statuses.Starting,"Resuming");
					ShouldPause  = false;
					if (CurrentState.CurrentStatus==State.Statuses.Paused)
					{
						CurrentState.RequestRestart();
					}
					break;
				case(Statuses.Stopped):
					SetCurrentStatus(Statuses.Starting,"Startting");
					RunningThread.Start();
					break;
			}
			
			
			
		}
		
		public enum Statuses
		{
			Stopped,
			Stoping,
			Started,
			Starting,
			Paused,
			Pausing,
			Error,
			FatalError,
			RuntimeError,
			EndedSuccessfully
		}
		
		
		
		#endregion
		public void Start()
		{
			OnStatusChanged(new ProtocolStatusChangeEventArgs(Statuses.Started,"Started"));
			Log("Started");
			try {
				this.Start();
				OnStatusChanged(new ProtocolStatusChangeEventArgs(Statuses.Stopped,"Ended Successfully!"));
			}
			catch (Exception e)
			{
				myLogger.Add(e.ToString());
				OnStatusChanged(new ProtocolStatusChangeEventArgs(Statuses.Error,"Error:" + e.Message ));
				Log("Error:" + e.Message);
				RunningThread.Abort();
				
			}
			
			
			
			
		}
		
		public void Log(string Messege)
		{
			myProtocolLogger.Add("Protocol(" + this.ProtocolParameters.Name + "):" + Messege);
		}
		
		
		protected abstract   void DoWork();
		

		
	}
	
	
	// Special EventArgs class to hold info about The Status Change.
	
	//TODO:Add real Protocol Status Change Events args!
	public class ProtocolStatusChangeEventArgs : EventArgs
	{
		private Protocol.Statuses _NewStatus;
		private string _Messege;

		public ProtocolStatusChangeEventArgs(Protocol.Statuses NewStatus,string Messege)
		{
			this._NewStatus = NewStatus;
			this._Messege = Messege;
		}
		public Protocol.Statuses NewStatus
		{
			get { return _NewStatus; }
		}
		public string Messege
		{
			get { return _Messege; }
		}
	}
	

	#region EventArgs
	
	public class ProtocolDisplayedDataChangeEventArgs : EventArgs
	{
		
		private string _Messege;

		public ProtocolDisplayedDataChangeEventArgs(string Messege)
		{
			
			this._Messege = Messege;
		}
		public string Messege
		{
			get { return _Messege; }
		}
	}
	
	#endregion
}

