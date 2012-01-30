/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/10/2011
 * Time: 09:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.OctoTipExperiments.Core.Attributes
{
	/// <summary>
	/// Description of ProtocolParameterAtribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ProtocolParameterAtribute: Attribute
	{
		public ProtocolParameterAtribute(string Title,string DefaultValue,bool Mandatory)
		{
			m_Title  = Title;
			m_DefaultValue  = DefaultValue;
			m_Mandatory = Mandatory;
		}
		public ProtocolParameterAtribute(string Title,string DefaultValue):this( Title, DefaultValue, true)
		{
			
		}
		public ProtocolParameterAtribute(string Title)
		{
			m_Title  = Title;
			m_DefaultValue  = string.Empty;
		}

		
		private bool m_Mandatory;

		public bool Mandatory
		{
			get { return m_Mandatory; }
			set { m_Mandatory = value; }
		}
		
		private string m_Title;

		public string Title
		{
			get { return m_Title; }
			set { m_Title = value; }
		}
		
		private string m_DefaultValue;

		public string DefaultValue
		{
			get { return m_DefaultValue; }
			set { m_DefaultValue = value; }
		}
	}
}


	