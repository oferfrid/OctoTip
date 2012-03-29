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

namespace OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls
{
	/// <summary>
	/// Description of FieldInterface.
	/// </summary>
	public interface FieldInterface
	{
		Control getFieldControl(object Value);
		Control getFieldControl(string Value);
		object  getValue(Control FieldControl);	
		bool IsValueNull(Control FieldControl);
	}
}