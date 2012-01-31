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
		
		public ProtocolStatus _Status = ProtocolStatus.Stopped;
		
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		private volatile bool _ShouldStop = false;
		private volatile bool _ShouldPause = false;
		
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
			
		}
		 ~Protocol()
		{
		
		}
		
		
		public void SetNewProtocolParameters(ProtocolParameters ProtocolParameters)
		{
			this.ProtocolParameters = ProtocolParameters;
		}
		#region Public Propertis
		public ProtocolStatus Status
		{
			get {return _Status;}
		}

		
		public bool ShouldStop
		{
			get { return _ShouldStop;}
		}
		
		public bool ShouldPause
		{
			get { return _ShouldPause;}
		}
		
		#endregion
		#region status Handeling
		
		public void ChangeState(State NewState)
		{
			if(!this._ShouldStop)
			{
			
			this.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(NewState, State.Status.Active,"Started"));
			CurrentState = NewState;
			Log(CurrentState.GetType().ToString() + " Started");
			CurrentState.DoWork();
			Log(CurrentState.GetType().ToString() + " Ended");
			}
			
			
		}

		
		// The event. Note that by using the generic EventHandler<T> event type
		// we do not need to declare a separate delegate type.
		public event EventHandler<ProtocolStatusChangeEventArgs> StatusChanged;
		//The event-invoking method that derived classes can override.
		public virtual void OnStatusChanged(ProtocolStatusChangeEventArgs e)
		{
			this._Status = e.NewStatus;
			// Make a temporary copy of the event to avoid possibility of
			// a race condition if the last subscriber unsubscribes
			// immediately after the null check and before the event is raised.
			EventHandler<ProtocolStatusChangeEventArgs> handler = StatusChanged;
			if (handler != null)
			{
				handler(this, e);
			}
			this.Log(string.Format("Protocol status changed to: {0}",this._Status));
		}
		
		public event EventHandler<ProtocolDisplayedDataChangeEventArgs> DisplayedDataChange;
		//The event-invoking method that derived classes can override.
		public virtual void OnDisplayedDataChange(ProtocolDisplayedDataChangeEventArgs e)
		{
			// Make a temporary copy of the event to avoid possibility of
			// a race condition if the last subscriber unsubscribes
			// immediately after the null check and before the event is raised.
			EventHandler<ProtocolDisplayedDataChangeEventArgs> handler = DisplayedDataChange;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		
		public event EventHandler<ProtocolStateDisplayedDataChangeEventArgs> StateDisplayedDataChange;
		//The event-invoking method that derived classes can override.
		public virtual void OnStateDisplayedDataChange(ProtocolStateDisplayedDataChangeEventArgs e)
		{
			// Make a temporary copy of the event to avoid possibility of
			// a race condition if the last subscriber unsubscribes
			// immediately after the null check and before the event is raised.
			EventHandler<ProtocolStateDisplayedDataChangeEventArgs> handler = StateDisplayedDataChange;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		
		public event EventHandler<ProtocolStateStatusChangeEventArgs> StateStatusChange;
		//The event-invoking method that derived classes can override.
		public virtual void OnProtocolStateStatusChange(ProtocolStateStatusChangeEventArgs e)
		{
			// Make a temporary copy of the event to avoid possibility of
			// a race condition if the last subscriber unsubscribes
			// immediately after the null check and before the event is raised.
			EventHandler<ProtocolStateStatusChangeEventArgs> handler = StateStatusChange;
			if (handler != null)
			{
				handler(this, e);
			}
			this.Log(string.Format("State {0} status changed to: {1}",e.CurrentState.GetType().Name,e.StateStatus));
		}
		
		
		
		public void   RequestStop()
		{
			OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Stoping,"Trying to Stopped"));
			_ShouldStop = true;
			_ShouldPause  = false;
			Log("Request Stop");			
		}
		public void RequestPause()
		{
			OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Pausing,"Trying to Pause"));
			_ShouldPause  = true;
			Log("Request Pause");
		}
		public void RequestStart()
		{
			switch(this.Status)
			{
				case(ProtocolStatus.Paused):
					OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Starting,"Resuming"));
					_ShouldPause  = false;
					Log("Resumed");
					break;
				case(ProtocolStatus.Stopped):
					OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Starting,"Startting"));
					RunningThread.Start();
					break;
			}
			
			
			
		}
		
		public enum ProtocolStatus
		{
			Stopped,
			Stoping,
			Started,
			Starting,
			Paused,
			Pausing,
			Error,
			RuntimeError,
			EndedSuccessfully
		}
		
		
		
		#endregion
		public void DoWork()
		{
			OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Started,"Started"));
			Log("Started");
			try {
				OnProtocolStart();
				OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Stopped,"Ended Successfully!"));	
			} 
			catch (Exception e) 
			{
				myLogger.Add(e.ToString());
				OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Error,"Error:" + e.Message ));
				Log("Error:" + e.Message);
				RunningThread.Abort();
				
			}
				
				
				
			
		}
		
		public void Log(string Messege)
		{
			myProtocolLogger.Add(Messege);
		}
		
		
		protected abstract   void OnProtocolStart();
		

		
	}
	
	
	// Special EventArgs class to hold info about The Status Change.
	
	//TODO:Add real Protocol Status Change Events args!
	public class ProtocolStatusChangeEventArgs : EventArgs
	{
		private Protocol.ProtocolStatus _NewStatus;
		private string _Messege;

		public ProtocolStatusChangeEventArgs(Protocol.ProtocolStatus NewStatus,string Messege)
		{
			this._NewStatus = NewStatus;
			this._Messege = Messege;
		}
		public Protocol.ProtocolStatus NewStatus
		{
			get { return _NewStatus; }
		}
		public string Messege
		{
			get { return _Messege; }
		}
	}
	
	public class ProtocolStateDisplayedDataChangeEventArgs:EventArgs
	{
		
		private string _Messege;
		private State _State;

		public ProtocolStateDisplayedDataChangeEventArgs(State CurentState,string Messege)
		{
			this._State = CurentState;
			this._Messege = Messege;
		}
		public string Messege
		{
			get { return _Messege; }
		}
		public State State
		{
			get { return _State; }
		}
	}
		
		
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
	public class ProtocolStateStatusChangeEventArgs : EventArgs
	{
		
		private string _Messege;
		private State _CurrentState;
		private State.Status _StateStatus;
		
		public ProtocolStateStatusChangeEventArgs(State CurrentState,State.Status StateStatus,string Message)
		{
			this._CurrentState  = CurrentState;
			this._Messege = Messege;
			this._StateStatus = StateStatus;
		}
		public string Messege
		{
			get { return _Messege; }
		}
		
		public State CurrentState
		{
			get { return _CurrentState; }
		}
		
		public State.Status StateStatus
		{
			get { return _StateStatus; }
		}
	}

}

