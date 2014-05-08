/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/01/2012
 * Time: 19:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace OctoTip.OctoTipPlus.ProtocolParametersFields
{
	/// <summary>
	/// Description of DoubleField.
	/// </summary>
	public class DoubleField:FieldInterface
	{
		
		public Control getFieldControl(object Value)
		{
			TextBox doubleFieldControl = new TextBox();
			doubleFieldControl.Text = Value.ToString();
			return doubleFieldControl;
		}
		
		public Control getFieldControl(string Value)
		{
		double doubleValue;
			try
			{
				doubleValue = Convert.ToDouble(Value);
			}
			catch(Exception e)
			{
				throw new Exception(String.Format("ProtocolParameters Default Value {0} not in the correct format",Value),e);
			}
			
			return getFieldControl(doubleValue);
		}
		
		public object getValue(Control FieldControl)
		{
			double Value = System.Convert.ToDouble((FieldControl as TextBox).Text);
			return Value;
		}
		
		
		
		public bool IsValueNull(Control FieldControl)
		{
			return (FieldControl as TextBox).Text.Trim()==string.Empty;
		}
	}
}
