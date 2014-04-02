/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/01/2012
 * Time: 14:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace OctoTip.OctoTipPlus.ProtocolParametersFields
{
	/// <summary>
	/// Description of IntField.
	/// </summary>
	public class IntField:FieldInterface
	{
			
		public Control getFieldControl(object Value)
		{
			TextBox intFieldControl = new TextBox();
			intFieldControl.Text = Value.ToString();
			return intFieldControl;
		}
		
		public Control getFieldControl(string Value)
		{
		int intValue;
			try
			{
				intValue = Convert.ToInt32(Value);
			}
			catch(Exception e)
			{
				throw new Exception(String.Format("ProtocolParameters Default Value {0} not in the correct format",Value),e);
			}
			
			return getFieldControl(intValue);
		}
		
		public object getValue(Control FieldControl)
		{
			int Value = System.Convert.ToInt32((FieldControl as TextBox).Text);
			return Value;
		}
		
		
		public bool IsValueNull(Control FieldControl)
		{
			return (FieldControl as TextBox).Text.Trim()==string.Empty;
		}
	}
}
