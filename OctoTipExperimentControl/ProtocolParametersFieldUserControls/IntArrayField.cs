/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/01/2012
 * Time: 19:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls
{
	/// <summary>
	/// Description of IntArrayField.
	/// </summary>
	public class IntArrayField:FieldInterface
	{
		
		
		public System.Windows.Forms.Control getFieldControl(object Value)
		{
			int[] IntArrayValue = (int[])Value;
			return getFieldControl(string.Join(",",IntArrayValue));
			
		}
		
		public System.Windows.Forms.Control getFieldControl(string Value)
		{
			TextBox intFieldControl = new TextBox();
			intFieldControl.Text = Value;
			return intFieldControl;
		}
		
		public object getValue(System.Windows.Forms.Control FieldControl)
		{
			
			string[] StringValues =((TextBox)FieldControl).Text.Split(',');
			int[] Values = new int[StringValues.Length];
			for (int i=0;i<Values.Length;i++)
			{
				Values[i] = Convert.ToInt32(StringValues[i]);
			}
			
			return Values;
			
		}
		
		public bool IsValueNull(Control FieldControl)
		{
			return (FieldControl as TextBox).Text.Trim()==string.Empty;
		}
	}
}
