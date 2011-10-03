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
		
		public ProtocolParameterAtribute(string Title)
		{
			m_Title  = Title;
		}

		private string m_Title;

		public string Title
		{
			get { return m_Title; }
			set { m_Title = value; }
		}
	}
}


	