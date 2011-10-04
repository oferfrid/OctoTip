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
		
		public StringFieldUserControl(string Title,string Value):this()
		{
			this.ValueTextBox.Text = Value;
			this.ParamNameLabel.Text = Title;
		}
		
		public string GetValue()
		{
			return this.ParamNameLabel.Text;
		}
		
		public override object GetObjectValue()
		{
			return (object)this.ValueTextBox.Text;
		}
		
		public override void SetError(string Error)
		{
			errorProvider.SetError(this.ValueTextBox, "Value Not In the write fromat (string)");
		}
		
	}
}
