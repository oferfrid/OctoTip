/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 04/10/2011
 * Time: 14:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace OctoTip.Lib.ExperimentsCore.Base
{
	/// <summary>
	/// Description of ProtocolParameters.
	/// </summary>
	public abstract class ProtocolParameters:IProtocolParameters
	{
		[ProtocolParameterAtribute("Shared Resources File Path",@"C:\Users\OWNER\Documents\Programing\OctoTipProtocols\SharedResources\",true)]
		public string SharedResourcesFilePath;
		[ProtocolParameterAtribute("Name Of The Protocol","",true)]
		public string Name;
		
		public bool IsInitialized = false;
		
		public override string ToString()
		{
			string FieldNames=string.Empty;// = "Name=" + Name +"\n";
			
			FieldInfo[] ProtocolFields = ProtocolProvider.GetProtocolParametersFields(this);
			foreach (FieldInfo FI in ProtocolFields)
			{
				FieldNames += string.Format("{0}={1}\n", FI.Name,FI.GetValue(this).ToString());
			}
			return FieldNames;
			
		}
		
		public abstract bool IsValid();
		public abstract string GetErrorMessage();
	

	}
}
