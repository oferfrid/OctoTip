/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 01/05/2014
 * Time: 16:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace OctoTip.OctoTipPlus.ProtocolParametersFields
{
	/// <summary>
	/// Description of DateTimeField.
	/// </summary>
	public class DateTimeField:FieldInterface
	{
		
		public Control getFieldControl(object Value)
		{
			MaskedTextBox dateTimeFieldControl = new MaskedTextBox("00/00/0000 90:00");
			dateTimeFieldControl.ValidatingType = typeof(DateTime);
			if(((DateTime) Value).CompareTo(DateTime.MinValue)==0)
				dateTimeFieldControl.Text = string.Empty;
			else
				dateTimeFieldControl.Text = Value.ToString();
			
			return dateTimeFieldControl;
		}
		
		public Control getFieldControl(string Value)
		{
			DateTime dateTimeValue;
			
			try
			{
				dateTimeValue = Convert.ToDateTime(Value);
			}
			catch(FormatException)
			{
				dateTimeValue = DateTime.MinValue;
			}
			
			return getFieldControl(dateTimeValue);
		}
		
		public object getValue(Control FieldControl)
		{
			//return(Convert.ToDateTime((FieldControl as MaskedTextBox).Text));
			DateTime Value;
			
			try
			{
				Value = Convert.ToDateTime((FieldControl as MaskedTextBox).Text);
			}
			catch(FormatException)
			{
				Value = DateTime.MinValue;
			}
			
			return Value;
		}
		
		public bool IsValueNull(Control FieldControl)
		{
			return (FieldControl as MaskedTextBox).Text.Trim()==string.Empty;
		}
	}
}
