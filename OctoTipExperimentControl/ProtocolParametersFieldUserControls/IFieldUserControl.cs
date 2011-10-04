/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/10/2011
 * Time: 11:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls
{
	/// <summary>
	/// Description of IFieldUserControl.
	/// </summary>
	public interface IFieldUserControl
	{
		object GetObjectValue();
		void SetError(string Error);
		void ClearError();
	}
}
