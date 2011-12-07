/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/10/2011
 * Time: 13:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls
{
	/// <summary>
	/// Description of BaseFieldUserControl.
	/// </summary>
	public abstract partial  class BaseFieldUserControl : UserControl,IFieldUserControl
	{
		public BaseFieldUserControl()
		{
			InitializeComponent();
		}
		
		public abstract object GetObjectValue();
		
		
		public abstract void SetFormatError(string Error);
		public abstract void SetNullError(string Error);
		
		public abstract bool IsNull();
		
		public  void ClearError()
		{
			errorProvider.Clear();
		}
	}
}
