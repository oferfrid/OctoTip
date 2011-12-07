/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/10/2011
 * Time: 10:55
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
	/// Description of IntFieldUserControl.
	/// </summary>
	public partial class IntFieldUserControl :BaseFieldUserControl
	{
		public IntFieldUserControl():base()
		{
			InitializeComponent();
		}
		public IntFieldUserControl(string Title,int Value,string DefaultValue):this()
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
			return GetValue()==0;
		}
		
		public int GetValue()
		{
			return Convert.ToInt32(this.ValueTextBox.Text);
		}
		
		public override object GetObjectValue()
		{
			return (object)Convert.ToInt32(this.ValueTextBox.Text);
		}
		
		public override void SetFormatError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value Not In the write fromat (int)\n" + Error);
		}
		public override void SetNullError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value can't be null\n" + Error);
		}
	}
}
