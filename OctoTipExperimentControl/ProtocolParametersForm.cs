/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/10/2011
 * Time: 15:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Reflection;

using OctoTip.OctoTipExperiments.Core;
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls;

namespace OctoTip.OctoTipExperimentControl
{
	/// <summary>
	/// Description of ProtocolParametersForm.
	/// </summary>
	public partial class ProtocolParametersForm : Form
	{
		ProtocolParameters FormProtocolParameters;
		FieldInfo[] ProtocolParametersFields;
		
		ProtocolUserControl ParentProtocolUserControl;
		
		public ProtocolParametersForm()
		{
			InitializeComponent();
		}
		public ProtocolParametersForm(ProtocolUserControl ParentProtocolUserControl,Type ProtocolData):this(ParentProtocolUserControl,ProtocolProvider.GetProtocolParameters(ProtocolData))
		{
			
		}
		
		public ProtocolParametersForm(ProtocolUserControl ParentProtocolUserControl,ProtocolParameters ProtocolParameters):this()
		{
			this.ParentProtocolUserControl = ParentProtocolUserControl;
			this.FormProtocolParameters = ProtocolParameters;
			ProtocolParametersFields = ProtocolProvider.GetProtocolParametersFields(FormProtocolParameters);
		}
		
		
		private void ProtocolParametersFormLoad(object sender, EventArgs e)
		{
			foreach(FieldInfo ProtocolParametersField in ProtocolParametersFields)
			{
				string title = (ProtocolParametersField.GetCustomAttributes(typeof(ProtocolParameterAtribute),true)[0] as ProtocolParameterAtribute).Title ;
				string DefaultValue = (ProtocolParametersField.GetCustomAttributes(typeof(ProtocolParameterAtribute),true)[0] as ProtocolParameterAtribute).DefaultValue ;
				
				if (ProtocolParametersField.FieldType==typeof(int))
				{
					int Value = (int)(ProtocolParametersField.GetValue(FormProtocolParameters));
					
					ProtocolParametersFieldUserControls.IntFieldUserControl IUC =
						new ProtocolParametersFieldUserControls.IntFieldUserControl(title,Value,DefaultValue);
					AddProtocolParametersFieldUserControl(IUC);
				}
				
				else if(ProtocolParametersField.FieldType==typeof(double))
				{
					double Value = (double)(ProtocolParametersField.GetValue(FormProtocolParameters));
					
					ProtocolParametersFieldUserControls.DoubleFieldUserControl IUC =
						new ProtocolParametersFieldUserControls.DoubleFieldUserControl(title,Value,DefaultValue);
					AddProtocolParametersFieldUserControl(IUC);
				}
				
				else if(ProtocolParametersField.FieldType==typeof(bool))
				{
					bool Value = (bool)(ProtocolParametersField.GetValue(FormProtocolParameters));
					
					ProtocolParametersFieldUserControls.BooleanFieldUserControl IUC =
						new ProtocolParametersFieldUserControls.BooleanFieldUserControl(title,Value,DefaultValue);
					AddProtocolParametersFieldUserControl(IUC);
				}
				
				else if(ProtocolParametersField.FieldType==typeof(int[]))
				{
					int[] Value = (int[])(ProtocolParametersField.GetValue(FormProtocolParameters));
					
					IntArrayFieldUserControl IUC =
						new  IntArrayFieldUserControl(title,Value,DefaultValue);
					AddProtocolParametersFieldUserControl(IUC);
				}
				else if(ProtocolParametersField.FieldType==typeof(string))
				{
					string Value = (string)(ProtocolParametersField.GetValue(FormProtocolParameters));
					
					StringFieldUserControl IUC =new StringFieldUserControl(title,Value,DefaultValue);
					AddProtocolParametersFieldUserControl(IUC);
				}
				else if(ProtocolParametersField.FieldType==typeof(double[]))
				{
					double[] Value = (double[])(ProtocolParametersField.GetValue(FormProtocolParameters));
					
					DoubleArrayFieldUserControl IUC =
						new  DoubleArrayFieldUserControl(title,Value,DefaultValue);
					AddProtocolParametersFieldUserControl(IUC);
				}
				else
				{
					throw new NotImplementedException("Parameter Type (" +  ProtocolParametersField.FieldType.ToString()  + ") Not Implemented yet");
				}
				
				
			}
		}
		private void AddProtocolParametersFieldUserControl(UserControl ProtocolParametersFieldUserControl)
		{
			if (ParametersPanel.Controls.Count==0)
			{
				this.ParametersPanel.Controls.Add(ProtocolParametersFieldUserControl);
			}
			else
			{
				UserControl LastProtocolUserControl = (UserControl)ParametersPanel.Controls[ParametersPanel.Controls.Count-1];
				ProtocolParametersFieldUserControl.Location = new Point(LastProtocolUserControl.Left , LastProtocolUserControl.Bottom);
				ParametersPanel.Controls.Add(ProtocolParametersFieldUserControl);
			}
			
		}
		
		void UpdatebuttonClick(object sender, EventArgs e)
		{
			System.Windows.Forms.Control.ControlCollection  ProtocolParametersFieldUserControls = ParametersPanel.Controls;
			
			bool ErrorFlag = false;
			
			for (int i=0;i<ProtocolParametersFieldUserControls.Count;i++)
			{
				object Value;
				try
				{
					((IFieldUserControl)ProtocolParametersFieldUserControls[i]).ClearError();
					if ((ProtocolParametersFields[i].GetCustomAttributes(typeof(ProtocolParameterAtribute),true)[0] as ProtocolParameterAtribute).Mandatory
					    && ((IFieldUserControl)ProtocolParametersFieldUserControls[i]).IsNull())
					{
						//field is mandatory & empty
						((IFieldUserControl)ProtocolParametersFieldUserControls[i]).SetNullError(string.Empty);
						ErrorFlag = true;
					}
					else
					{
						Value = ((IFieldUserControl)ProtocolParametersFieldUserControls[i]).GetObjectValue();
						ProtocolParametersFields[i].SetValue(FormProtocolParameters,Value);
					}
				}
				catch (System.FormatException )
				{
					((IFieldUserControl)ProtocolParametersFieldUserControls[i]).SetFormatError(string.Empty);
					ErrorFlag = true;
					//throw ex;
				}
				
			}
			if(!ErrorFlag)
			{
				ParentProtocolUserControl.SetNewUserControlProtocolParameters(FormProtocolParameters);
				ParentProtocolUserControl.UpdateUserControlProtocolName();
				this.Close();
			}
			
		}
		
		void CancelbuttonClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
