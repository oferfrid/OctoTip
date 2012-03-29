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
using OctoTip.OctoTipExperiments.Core.Interfaces;

namespace OctoTip.Lib.ExperimentsCore.Base
{
	/// <summary>
	/// Description of State.
	/// </summary>
	public abstract class State:IState
	{

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
	
		protected void SetCurrentStatus(Statuses CurrentStatus,string Messege )
		{
			//Log the status change And Raise the event.
			Log(string.Format("{0}>{1} {2}",_CurrentStatus , CurrentStatus , Messege));
			_CurrentStatus = CurrentStatus;
			OnStatusChanged(new StateStatusChangeEventArgs(this,CurrentStatus,Messege));
		}
		
		public event EventHandler<StateDisplayedDataChangeEventArgs> StateDisplayedDataChange;
		public event EventHandler<StateStatusChangeEventArgs> StateStatusChange;
		
		protected virtual void DisplayData(string Messege)
		{
			StateDisplayedDataChangeEventArgs e = new StateDisplayedDataChangeEventArgs(this,Messege);
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
		
		
		public void RequestStop(string Messege)
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
		
		protected void Log (string Messege)
		{
		//TODO:LOG!
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
			SetCurrentStatus(Statuses.EndedSuccessfully,"Ending State");
		}
		protected abstract void DoWork();
	
	}
	
	#region EventArgs
	
	public class StateStatusChangeEventArgs : EventArgs
	{
		
		private string _Messege;
		private State _CurrentState;
		private State.Statuses _StateStatus;
		
		public StateStatusChangeEventArgs(State CurrentState,State.Statuses StateStatus,string Message)
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
		
		public State.Statuses StateStatus
		{
			get { return _StateStatus; }
		}
	}

	
		public class StateDisplayedDataChangeEventArgs:EventArgs
	{
		
		private string _Messege;
		private State _State;

		public StateDisplayedDataChangeEventArgs(State CurrentState,string Messege)
		{
			this._State = CurrentState;
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
		#endregion
		
}
