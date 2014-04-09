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
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Reflection;
using  OctoTip.Lib.ExperimentsCore;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Attributes;
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
				if (ProtocolParametersField.Name != "IsInitialized")
				{
					string title = (ProtocolParametersField.GetCustomAttributes(typeof(ProtocolParameterAtribute),true)[0] as ProtocolParameterAtribute).Title ;
					string DefaultValue = (ProtocolParametersField.GetCustomAttributes(typeof(ProtocolParameterAtribute),true)[0] as ProtocolParameterAtribute).DefaultValue ;
					
					ProtocolParametersFieldUserControl PPFUC;
					if (!FormProtocolParameters.IsInitialized)
					{
						PPFUC = new ProtocolParametersFieldUserControl(ProtocolParametersField.FieldType,DefaultValue,title);
					}
					else
					{
						PPFUC = new ProtocolParametersFieldUserControl(ProtocolParametersField.FieldType,ProtocolParametersField.GetValue(FormProtocolParameters),title);
					}
					
					AddProtocolParametersFieldUserControl(PPFUC);
				}
			}
		}
		private void AddProtocolParametersFieldUserControl(ProtocolParametersFieldUserControl ProtocolParametersFieldUserControl)
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
				
				ProtocolParametersFieldUserControl FieldUserControl = (ProtocolParametersFieldUserControl)ProtocolParametersFieldUserControls[i];
				FieldUserControl.ClearError();
				try
				{
				if ((ProtocolParametersFields[i].GetCustomAttributes(typeof(ProtocolParameterAtribute),true)[0] as ProtocolParameterAtribute).Mandatory
				    && FieldUserControl.IsValueNull())
				{//field is mandatory & empty
						FieldUserControl.SetNullError();
						ErrorFlag = true;
				}
				else
				{
					object Value = FieldUserControl.GetValue();
					ProtocolParametersFields[i].SetValue(FormProtocolParameters,Value);
				}
				}
				catch (System.FormatException )
				{
					FieldUserControl.SetFormatError();
					ErrorFlag = true;
				}
			}
			
			if(!ErrorFlag &&!FormProtocolParameters.IsValid())
			{
				Errorlabel.Text = FormProtocolParameters.GetErrorMessage();
				ErrorFlag = true;
			}
			
			if(!ErrorFlag)
			{
				FormProtocolParameters.IsInitialized = true;
				ParentProtocolUserControl.SetNewUserControlProtocolParameters(FormProtocolParameters);
				ParentProtocolUserControl.UpdateUserControlProtocolName();
				this.Close();
			}
		}
		
//				try
//				{
//					((IFieldUserControl)ProtocolParametersFieldUserControls[i]).ClearError();
//					if ((ProtocolParametersFields[i].GetCustomAttributes(typeof(ProtocolParameterAtribute),true)[0] as ProtocolParameterAtribute).Mandatory
//					    && ((IFieldUserControl)ProtocolParametersFieldUserControls[i]).IsNull())
//					{
//						//field is mandatory & empty
//						((IFieldUserControl)ProtocolParametersFieldUserControls[i]).SetNullError(string.Empty);
//						ErrorFlag = true;
//					}
//					else
//					{
//						Value = ((IFieldUserControl)ProtocolParametersFieldUserControls[i]).GetObjectValue();
//						ProtocolParametersFields[i].SetValue(FormProtocolParameters,Value);
//					}
//				}
//				catch (System.FormatException )
//				{
//					((IFieldUserControl)ProtocolParametersFieldUserControls[i]).SetFormatError(string.Empty);
//					ErrorFlag = true;
//					//throw ex;
//				}
//
//			}
//			if(!ErrorFlag)
//			{
//				ParentProtocolUserControl.SetNewUserControlProtocolParameters(FormProtocolParameters);
//				ParentProtocolUserControl.UpdateUserControlProtocolName();
//				this.Close();
//			}
//
		
		
		void CancelbuttonClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
