/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 20:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.Lib.ExperimentsCore.Attributes
{
	/// <summary>
	/// Description of ProtocolAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class StateAttribute: Attribute
	{
		
		public StateAttribute(string DisplayName,string Description)
		{
			m_Description = Description;
			m_DisplayName = DisplayName;
		}

		private string m_Description;
		public string Description
		{
			get { return m_Description; }
		}
		
		private string m_DisplayName;
		public string DisplayName
		{
			get { return m_DisplayName; }
		}
		
		//TODO:Add Vertion And creator...
	}
}
