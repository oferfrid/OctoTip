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

using OctoTip.OctoTipExperiments.Core.Interfaces;

namespace OctoTip.OctoTipExperiments.Core.Base
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
		
		
		public State CurentState;
		
		
		public ProtocolStatus _Status = ProtocolStatus.Stopped;
		
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		private volatile bool _ShouldStop = false;
		private volatile bool _ShouldPause = false;
		
		
		
		
		public  ProtocolParameters ProtocolParameters;
		
		public  Protocol(ProtocolParameters ProtocolParameters)
		{
			this.ProtocolParameters = ProtocolParameters;
		}
		public  Protocol()
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
			this.OnProtocolStateStatusChange(new ProtocolStateStatusChangeEventArgs(NewState,CurentState,State.Status.StateChanged,string.Empty));
			CurentState = NewState;
			
			
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
		}
		
		
		public void   RequestStop()
		{
			OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Stoping,"Trying to Stopped"));
			_ShouldStop = true;
		}
		public void RequestPause()
		{
			OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Pausing,"Trying to Pause"));
			_ShouldPause  = true;
		}
		public void RequestResume()
		{
			OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Starting,"Resuming"));
			_ShouldPause  = false;
			
		}
		
		public enum ProtocolStatus
		{Stopped,Stoping,Started,Starting,Paused,Pausing}
		
		
		
		#endregion
		public void DoWork()
		{
			OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Started,"Started"));
			
			OnProtocolStart();
			
			OnStatusChanged(new ProtocolStatusChangeEventArgs(ProtocolStatus.Stopped,"Stoped"));
			
			
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
		private State _CurentState;
		private State _PreviuseState;
		private State.Status _StateStatus;
		
		public ProtocolStateStatusChangeEventArgs(State CurentState,State PreviuseState,State.Status StateStatus,string Messege)
		{
			this._CurentState  = CurentState;
			this._PreviuseState  = PreviuseState;
			this._Messege = Messege;
			this._StateStatus = StateStatus;
		}
		public string Messege
		{
			get { return _Messege; }
		}
		
		public State PreviuseState
		{
			get { return _PreviuseState; }
		}
		public State CurentState
		{
			get { return _CurentState; }
		}
		
		public State.Status StateStatus
		{
			get { return _StateStatus; }
		}
	}

}

