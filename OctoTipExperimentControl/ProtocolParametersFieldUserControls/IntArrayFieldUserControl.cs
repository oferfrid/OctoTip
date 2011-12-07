/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/10/2011
 * Time: 14:04
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
	/// Description of IntArrayFieldUserControl.
	/// </summary>
	public partial class IntArrayFieldUserControl : BaseFieldUserControl
	{
		public IntArrayFieldUserControl():base()
		{
			InitializeComponent();
		}
		public IntArrayFieldUserControl(string Title,int[] Value,string DefaultValue):this()
		{
			if (Value!=null)
			{
			this.ValueTextBox.Text = string.Join(",",Value);
			}
						else
			{
			this.ValueTextBox.Text = 	DefaultValue;
			}
			this.ParamNameLabel.Text = Title;
		}
		
		public override bool IsNull()
		{
			return GetValue().Length==0;
		}
		
		public int[] GetValue()
		{
				string[] StringValues = this.ValueTextBox.Text.Split(',');
			int[]  Values = new int[StringValues.Length];
			for (int i=0;i<Values.Length;i++)
			{
				Values[i] = Convert.ToInt32(StringValues[i]);
			}
			
			
			return Values;
		}
		
		public override object GetObjectValue()
		{
			
			
			
			return (object)GetValue();
		}
		
		public override void SetFormatError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value Not In the write fromat (int,int,int...)\n" + Error);
		}
		public override void SetNullError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value can't be null\n" + Error);
		}
	}
}
