/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/10/2011
 * Time: 11:05
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
	/// Description of StringFieldUserControl.
	/// </summary>
	public partial class StringFieldUserControl : BaseFieldUserControl
	{
		public StringFieldUserControl()
		{
			InitializeComponent();
		}
		
		public StringFieldUserControl(string Title,string Value,string DefaultValue):this()
		{
			if (Value!=string.Empty)
			{
			this.ValueTextBox.Text = Value;
			}
			else
			{
			this.ValueTextBox.Text = 	DefaultValue;
			}
			this.ParamNameLabel.Text = Title;
		}
		
		public string GetValue()
		{
			return this.ValueTextBox.Text;
		}
		
		public override object GetObjectValue()
		{
			return (object)this.ValueTextBox.Text;
		}
		
		public override void SetFormatError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value Not In the write fromat (string)");
		}
		
		
		public override bool IsNull()
		{
			return GetValue()==string.Empty;
		}
		public override void SetNullError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value can't be null\n" + Error);
		}
	}
}
