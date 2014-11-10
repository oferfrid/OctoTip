/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 30/01/2012
 * Time: 12:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace OctoTip.OctoTipPlus.ProtocolParametersFields
{
	/// <summary>
	/// Description of DoubleArrayField.
	/// </summary>
	public class DoubleArrayField:FieldInterface
	{
		public System.Windows.Forms.Control getFieldControl(object Value)
		{
			double[] IntArrayValue = (double[])Value;
			return getFieldControl(string.Join(",",IntArrayValue));
			
		}
		
		public Control getFieldControl(string Value)
		{
			TextBox intFieldControl = new TextBox();
			intFieldControl.Text = Value;
			return intFieldControl;
		}
		
		public object getValue(System.Windows.Forms.Control FieldControl)
		{
			
			string[] StringValues =((TextBox)FieldControl).Text.Split(',');
			double[] Values = new double[StringValues.Length];
			for (int i=0;i<Values.Length;i++)
			{
				Values[i] = Convert.ToDouble(StringValues[i]);
			}
			
			return Values;
			
		}
		
		public bool IsValueNull(Control FieldControl)
		{
			return (FieldControl as TextBox).Text.Trim()==string.Empty;
		}
	}
}
