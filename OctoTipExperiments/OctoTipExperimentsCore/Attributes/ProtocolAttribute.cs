/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 20:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.OctoTipExperiments.Attributes
{
	/// <summary>
	/// Description of ProtocolAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ProtocolAttribute: Attribute
	{
		public ProtocolAttribute(string description)
        {
            m_description = description;
        }

        private string m_description;

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }
	}
}
