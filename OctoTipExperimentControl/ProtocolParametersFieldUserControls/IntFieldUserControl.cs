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
		public IntFieldUserControl(string Title,int Value):this()
		{
			this.ValueTextBox.Text = Value.ToString();
			this.ParamNameLabel.Text = Title;
		}
		
		public int GetValue()
		{
			return Convert.ToInt32(this.ValueTextBox.Text);
		}
		
		public override object GetObjectValue()
		{
			return (object)Convert.ToInt32(this.ValueTextBox.Text);
		}
		
		public override void SetError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value Not In the write fromat (int)\n" + Error);
		}
		
	}
}
