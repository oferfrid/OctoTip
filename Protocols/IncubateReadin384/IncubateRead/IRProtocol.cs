/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 18/12/11
 * Time: 10:52 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace IncubateReadin384
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	[Protocol("Incubate Read in 384" ,"Ofer Fridman","OD measurements while growing in Liconic in 384 plate")]
	public class IRProtocol:Protocol
	{
		
		DateTime StartTime;
		
		public new IRProtocolParameters ProtocolParameters
		{
			get{return (IRProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		public IRProtocol(IRProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{

		}
		
		protected override void DoWork()
		{
			this.StartTime = DateTime.Now;
			
			
			while (DateTime.Now.Subtract(StartTime).TotalHours < this.ProtocolParameters.TotalTime && !ShouldStop)
			{
				
				UpdateProtocolMessege();
				IRReadState _IRReadState = new IRReadState(ProtocolParameters.LicInd );
				this.ChangeState(_IRReadState);
				FileInfo ResultFileInfo = 	 _IRReadState.GetReadResultFileInfo();
				string OutpotFile = string.Format("{0}_{1:yyyyMMddHHmm}_{2:0}.xml", this.ProtocolParameters.Name,this.StartTime ,	ProtocolParameters.StartRound++);
				ResultFileInfo.CopyTo(ProtocolParameters.OutputPath+OutpotFile);
				//Log("output file:" + OutpotFile);
				this.ChangeState(new IRIncubateState(ProtocolParameters.ReadFrequency));
			}
		}
		
		
		private void UpdateProtocolMessege()
		{
			string message = string.Format( "Remainig time: {0} Hours \n",(this.ProtocolParameters.TotalTime-DateTime.Now.Subtract(StartTime).TotalHours).ToString("0.00"));
			
			this.DisplayData(message);
			
		}
		
		#region static
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(IRReadState)
					,typeof(IRIncubateState)};
		}
		#endregion
	}
}