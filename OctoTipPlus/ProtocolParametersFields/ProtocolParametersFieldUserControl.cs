/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/01/2012
 * Time: 14:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OctoTip.OctoTipPlus.ProtocolParametersFields
{
	/// <summary>
	/// Description of ProtocolParametersFields.
	/// </summary>
	public partial class ProtocolParametersFieldUserControl : UserControl
	{
		
		FieldInterface Field;
		
		public ProtocolParametersFieldUserControl()
		{
			InitializeComponent();
		}
		
		
		private ProtocolParametersFieldUserControl(Type FieldType,string title):this()
		{
			Fieldlabel.Text = title;
			
			if (FieldType==typeof(int))
			{
				Field = new IntField();
			}
			else if(FieldType==typeof(double))
			{
				Field = new  DoubleField();
			}
			
			else if(FieldType==typeof(bool))
			{
				Field = new  BoolField();
			}
			
			else if(FieldType==typeof(int[]))
			{
				Field = new  IntArrayField();
			}
			else if(FieldType==typeof(string))
			{
				Field = new  StringField();
			}
			else if(FieldType==typeof(double[]))
			{
				Field = new DoubleArrayField();
			}
			else
			{
				throw new NotImplementedException("Parameter Type (" +  FieldType.ToString()  + ") Not Implemented yet");
			}
		}
		
		public ProtocolParametersFieldUserControl(Type FieldType,string Value,string title):this(FieldType,title)
		{
			ControlPanel.Controls.Add( Field.getFieldControl(Value));
		}
		
		public ProtocolParametersFieldUserControl(Type FieldType,object Value,string title):this(FieldType,title)
		{
			ControlPanel.Controls.Add( Field.getFieldControl(Value));
		}
		
		public bool IsValueNull()
		{
			return Field.IsValueNull(ControlPanel.Controls[0]);
		}
		
		public object GetValue()
		{
			return Field.getValue(ControlPanel.Controls[0]);
		}
		public void ClearError()
		{
			ControlErrorProvider.Clear();
		}

		public  void SetNullError()
		{
			ControlErrorProvider.SetError(this.Fieldlabel, "Value can't be null\n");
		}
		public  void SetFormatError()
		{
			ControlErrorProvider.SetError(this.Fieldlabel, "Value Not In the write fromat \n");
		}
		
		
		
	}
}
