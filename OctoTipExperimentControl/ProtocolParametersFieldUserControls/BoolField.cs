/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/01/2012
 * Time: 19:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls
{
	/// <summary>
	/// Description of BoolField.
	/// </summary>
	public class BoolField:FieldInterface
	{
		public Control getFieldControl(object Value)
		{
			CheckBox BoolFieldControl = new CheckBox();
			BoolFieldControl.Checked = (bool)Value;
			return BoolFieldControl;
		}
		
		public System.Windows.Forms.Control getFieldControl(string Value)
		{
			bool BoolValue;
			try
			{
				BoolValue = Convert.ToBoolean(Value);
			}
			catch(Exception e)
			{
				throw new Exception(String.Format("ProtocolParameters Default Value {0} not in the correct format",Value),e);
			}
			return getFieldControl(BoolValue);
		}
		
		public object getValue(Control FieldControl)
		{
			return ((CheckBox)FieldControl).Checked;
		}
		
		
		public bool IsValueNull(Control FieldControl)
		{
			return false;
		}
	}
}
