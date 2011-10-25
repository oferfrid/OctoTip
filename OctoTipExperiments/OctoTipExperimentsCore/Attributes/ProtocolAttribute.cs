/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 20:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.OctoTipExperiments.Core.Attributes
{
	/// <summary>
	/// Description of ProtocolAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ProtocolAttribute: Attribute
	{
		public ProtocolAttribute(string ShortName ,string Author, string Description)
        {
          
		 m_ShortName =ShortName;
		 m_Author= Author;
		 m_Description = Description;  
			
        }

		
		string m_ShortName ;
		public string ShortName
        {
            get { return m_ShortName; }
            set { m_ShortName = value; }
        }
		
		string m_Author;
		public string Author
        {
            get { return m_Author; }
            set { m_Author = value; }
        }
		
		string m_Description;
		public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
	}
}
