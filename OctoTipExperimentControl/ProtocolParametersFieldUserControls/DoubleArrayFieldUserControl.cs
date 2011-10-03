/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/10/2011
 * Time: 14:12
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
	/// Description of DoubleArrayFieldUserControl.
	/// </summary>
	public partial class DoubleArrayFieldUserControl : BaseFieldUserControl
	{
	public DoubleArrayFieldUserControl():base()
		{
			InitializeComponent();
		}
		public DoubleArrayFieldUserControl(string Title,double[] Value):this()
		{
			if (Value!=null)
			{
			this.ValueTextBox.Text = string.Join(",",Value);
			}
			this.ParamNameLabel.Text = Title;
		}
		
		public double[] GetValue()
		{
				string[] StringValues = this.ValueTextBox.Text.Split(',');
			double[]  Values = new double[StringValues.Length];
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
		
		public override void SetError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value Not In the write fromat (int,int,int...)\n" + Error);
		}
	}
}
