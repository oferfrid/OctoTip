/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/01/2012
 * Time: 18:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace OctoTip.OctoTipPlus.ProtocolParametersFields
{
	/// <summary>
	/// Description of StringField.
	/// </summary>
	public class StringField:FieldInterface
	{
		public System.Windows.Forms.Control getFieldControl(object Value)
		{
		
			TextBox intFieldControl = new TextBox();
			intFieldControl.Text = (string)Value;
			return intFieldControl;
		}
		
		public System.Windows.Forms.Control getFieldControl(string Value)
		{
	
			TextBox intFieldControl = new TextBox();
			intFieldControl.Text = Value;
			return intFieldControl;
		}
		
		
		public object getValue(System.Windows.Forms.Control FieldControl)
		{
			string Value = (FieldControl as TextBox).Text;
			return Value;
		}
		
		public bool IsValueNull(Control FieldControl)
		{
			return (FieldControl as TextBox).Text.Trim()==string.Empty;
		}
	}
}
