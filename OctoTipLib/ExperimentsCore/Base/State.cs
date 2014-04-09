/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 27/09/2011
 * Time: 15:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace OctoTip.Lib.ExperimentsCore.Base
{
	/// <summary>
	/// Description of State.
	/// </summary>
	public abstract class State:IState
	{

		public const string LOG_NAME = "OctoTipExperimentManager";
		private LogString myLogger = LogString.GetLogString(LOG_NAME);
		
		protected int StateSamplelingRate  = 0;
		public State( )
		{
			StateSamplelingRate = Convert.ToInt32(ConfigurationManager.AppSettings["StateSamplelingRate"]);
						if (StateSamplelingRate == 0)
						{
							throw new NullReferenceException("AppSettings key for StateSamplelingRate is null");
						}
		}		
				
		#region Status handeling and events
				
		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		private volatile bool _ShouldStop = false;
		private volatile bool _ShouldPause = false;
		
		protected bool ShouldPause 
		{
			get	{return _ShouldPause;}
		}
		protected bool ShouldStop 
		{
			get	{return _ShouldStop;}
		}
		
		private Statuses _CurrentStatus = Statuses.Stopped;
		
		public Statuses CurrentStatus
		{
			get
			{
				return _CurrentStatus;
			}
		}
	
		protected void SetCurrentStatus(Statuses CurrentStatus,string Message )
		{
			//Log the status change And Raise the event.
			Log(string.Format("{0}>{1} {2}",_CurrentStatus , CurrentStatus , Message));
			_CurrentStatus = CurrentStatus;
			OnStatusChanged(new StateStatusChangeEventArgs(this,CurrentStatus,Message));
		}
		
		public event EventHandler<StateDisplayedDataChangeEventArgs> StateDisplayedDataChange;
		public event EventHandler<StateStatusChangeEventArgs> StateStatusChange;
		
		protected virtual void DisplayData(string Message)
		{
			StateDisplayedDataChangeEventArgs e = new StateDisplayedDataChangeEventArgs(this,Message);
			EventHandler<StateDisplayedDataChangeEventArgs> handler = StateDisplayedDataChange;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		
		private void OnStatusChanged(StateStatusChangeEventArgs e)
		{
			EventHandler<StateStatusChangeEventArgs> handler = StateStatusChange;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		
		
		public void RequestStop(string Message)
		{
			SetCurrentStatus(Statuses.Stoping,"Stop Request");
			_ShouldStop = true;
			_ShouldPause  = false;
		}
		public void RequestPause()
		{
			SetCurrentStatus(Statuses.Pausing,"Pause Request");
			_ShouldPause  = true;
		}
		
		public void RequestRestart()
		{
				if(CurrentStatus== Statuses.Paused)
				{
					SetCurrentStatus(Statuses.Starting,"Resuming");
					_ShouldPause  = false;
					
				}
				else
				{
					throw new Exception("Can't Resturt if not pused");
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
			FatalError,
			RuntimeError,
			EndedSuccessfully
		}
		
		
		#endregion
		
		protected void Log (string Message)
		{
			myLogger.Add(this.GetType().Name + ":" + Message );
		}
		
		#region static
		public static  List<Type> NextStates()
		{
			return new List<Type>(0);
		}
		
		#endregion
		
		
		public void Start()
		{
			SetCurrentStatus(Statuses.Started,"Starting State");
			DoWork();
			if (CurrentStatus == Statuses.Started)
			{
			SetCurrentStatus(Statuses.EndedSuccessfully,"Ending State");
			}
		}
		protected abstract void DoWork();
	
	}
	
	#region EventArgs
	
	public class StateStatusChangeEventArgs : EventArgs
	{
		
		private string _Message;
		private State _CurrentState;
		private State.Statuses _StateStatus;
		
		public StateStatusChangeEventArgs(State CurrentState,State.Statuses StateStatus,string Message)
		{
			this._CurrentState  = CurrentState;
			this._Message = Message;
			this._StateStatus = StateStatus;
		}
		public string Message
		{
			get { return _Message; }
		}
		
		public State CurrentState
		{
			get { return _CurrentState; }
		}
		
		public State.Statuses StateStatus
		{
			get { return _StateStatus; }
		}
	}

	
		public class StateDisplayedDataChangeEventArgs:EventArgs
	{
		
		private string _Message;
		private State _State;

		public StateDisplayedDataChangeEventArgs(State CurrentState,string Message)
		{
			this._State = CurrentState;
			this._Message = Message;
		}
		public string Message
		{
			get { return _Message; }
		}
		public State State
		{
			get { return _State; }
		}
	}
		#endregion
		
}
