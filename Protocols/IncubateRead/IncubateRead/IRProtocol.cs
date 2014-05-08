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

namespace IncubateRead
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	[Protocol("Incubate Read 384" ,"Ofer Fridman","OD measurements while growing in Liconic in 6 plate")]
	public class IRProtocol:Protocol
	{
		private List<double>[] OD = new List<double>[2];
		DateTime StartTime;
		private FileInfo OutputFile;
		
		
		public new IRProtocolParameters ProtocolParameters
		{
			get{return (IRProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		public IRProtocol(IRProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			for (int i=0;i<OD.Length;i++)
			{
				OD[i] = new List<double>(100);
			}
			
			ProtocolParameters = Parameters;
		}
		
		protected override void DoWork()
		{
			this.StartTime = DateTime.Now;
			
			
			int round = ProtocolParameters.StartRound;
	
			OutputFile = new FileInfo(ProtocolParameters.OutputFile);
			
			
        	if (OutputFile.Exists) 
        	{
	        	OutputFile.Delete();
	            using (StreamWriter sw = OutputFile.CreateText()) 
	            {
	            	sw.WriteLine("Time\tOD reads");
	            	sw.Flush();
	            }	
        	}
			
			while (DateTime.Now.Subtract(StartTime).TotalHours < this.ProtocolParameters.TotalTime && !ShouldStop)
			{
				 
				UpdateProtocolMessege();
				IRReadState _IRReadState = new IRReadState(round++,ProtocolParameters.LicInd,ProtocolParameters.OutputFile,ProtocolParameters.TypeOfPlate, ProtocolParameters.TypeOfMeasurment );
				this.ChangeState(_IRReadState);
				double[] Result = 	 _IRReadState.GetReadResult();
				OD[0].Add(Result[0]);
				OD[1].Add(Result[1]);
				UpdateProtocolMessege();
				this.ChangeState(new IRIncubateState(ProtocolParameters.ReadFrequency));	

//				message = string.Format( "End of read, Remainig time: {0} Hours",(this.ProtocolParameters.TotalTime-DateTime.Now.Subtract(StartTime).TotalHours).ToString("0.00"));
//				OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs(message));
//				
			}			
		}
		
		
		private void UpdateProtocolMessege()
		{
			string message = string.Format( "Remainig time: {0} Hours \n",(this.ProtocolParameters.TotalTime-DateTime.Now.Subtract(StartTime).TotalHours).ToString("0.00"));
			
			for (int i = 0;i<OD[1].Count;i++)
			{
				message += string.Format("{0}|\t{1}\t{2}\n",i.ToString(),OD[0][i].ToString("0.0000"),OD[1][i].ToString("0.0000"));
			}
			
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