/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 26/01/2012
 * Time: 12:12
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
	/// Description of BooleanFieldUserControl.
	/// </summary>
	public partial class BooleanFieldUserControl : BaseFieldUserControl
	{
		public BooleanFieldUserControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public BooleanFieldUserControl(string Title,bool Value,string DefaultValue):this()
		{
			this.ValueCheckBox.Checked = Value;
			this.ValueCheckBox.Text = Title;
		}
		
		public override void SetNullError(string Error)
		{
			errorProvider.SetError(this.ValueCheckBox ,String.Empty);
		}
		
		public override void SetFormatError(string Error)
		{
			errorProvider.SetError(this.ValueCheckBox ,String.Empty);
		}
		
		public override bool IsNull()
		{
			return false;
		}
		
		public override object GetObjectValue()
		{
			return this.ValueCheckBox.Checked ;
		}
	}
}
