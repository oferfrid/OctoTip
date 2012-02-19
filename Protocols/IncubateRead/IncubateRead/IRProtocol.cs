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
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace IncubateRead
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	[Protocol("Incubate Read" ,"Irit Reisman","OD measurements while growing in Liconic in 24 plate")]
	public class IRProtocol:Protocol
	{
		public List<double>[] OD = new List<double>[2];
		DateTime StartTime;
		public FileInfo OutputFile;
		
		
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
		
		protected override void OnProtocolStart()
		{
			this.StartTime = DateTime.Now;
			
			
			int round = 1;
			
			OutputFile = new FileInfo(string.Format("OD{0}-{1:yyyyMMddHHmm}.csv",this.ProtocolParameters.Name,this.StartTime));
			
			
        	if (OutputFile.Exists) 
        	{
        	OutputFile.Delete();
            using (StreamWriter sw = OutputFile.CreateText()) 
            {
            	sw.WriteLine("Time\tOD reads");
            }	
        }
			
			while (DateTime.Now.Subtract(StartTime).TotalHours < this.ProtocolParameters.TotalTime && !ShouldStop)
			{
				 
				UpdateProtocolMessege();
				
				this.ChangeState(new IRReadState(this,round++ ));
				UpdateProtocolMessege();
				
				
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
				message += string.Format("{0}|\t{1}\t{1}\n",i.ToString(),OD[1][i].ToString("0.000"),OD[2][i].ToString("0.000"));
			}
			
			
				OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs(message));
				
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