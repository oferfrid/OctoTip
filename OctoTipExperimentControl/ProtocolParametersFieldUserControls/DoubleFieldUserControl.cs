/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 23/10/2011
 * Time: 16:50
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
	/// Description of DoubleFieldUserControl.
	/// </summary>
	public partial class DoubleFieldUserControl : BaseFieldUserControl
	{
		public DoubleFieldUserControl():base()
		{
			InitializeComponent();
		}
		public DoubleFieldUserControl(string Title,double Value,string DefaultValue):this()
		{
			if (Value!=0)
			{
			this.ValueTextBox.Text = Value.ToString();
			}
			else
			{
			this.ValueTextBox.Text = 	DefaultValue;
			}
			this.ParamNameLabel.Text = Title;
		}
		
		public override bool IsNull()
		{
			return GetValue()==0.0;
		}
		
		public double GetValue()
		{
			return Convert.ToDouble(this.ValueTextBox.Text);
		}
		
		public override object GetObjectValue()
		{
			return (object)Convert.ToDouble(this.ValueTextBox.Text);
		}
		
		public override void SetFormatError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value Not In the write fromat (double)\n" + Error);
		}
		public override void SetNullError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value can't be null\n" + Error);
		}
	}
}
