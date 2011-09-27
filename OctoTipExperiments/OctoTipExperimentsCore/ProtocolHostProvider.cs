/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 20:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;


using OctoTip.OctoTipExperiments.Interfaces;
using OctoTip.OctoTipExperiments.Attributes;

namespace OctoTip.OctoTipExperiments.Core
{
	/// <summary>
	/// Description of ProtocolHostProvider.
	/// </summary>
	public static class ProtocolHostProvider
	{
		private static List<ProtocolHost> m_Protocols;
		
		public static List<ProtocolHost> Protocols
		{
			get
			{
				if (null == m_Protocols)
					Reload();

				return m_Protocols;
			}
		}

		public static List<Type> ProtocolsData
		{
			get
			{
				return GetAvalbleProtocolPlugIns(LoadPlugInAssemblies());
				
			}
		}
		

		public static void Reload()
		{
			if (null == m_Protocols)
				m_Protocols = new List<ProtocolHost>();
			else
				m_Protocols.Clear();

			List<Assembly> plugInAssemblies = LoadPlugInAssemblies();
			List<IProtocol> plugIns = GetPlugIns(plugInAssemblies);

			foreach (IProtocol Protocol in plugIns)
			{
				m_Protocols.Add(new ProtocolHost(Protocol));
			}
		}

		private static List<Assembly> LoadPlugInAssemblies()
		{
			DirectoryInfo dInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "PluginProtocol"));
			
			FileInfo[] files;
			
			if (dInfo.Exists)
			{
				files = dInfo.GetFiles("*.dll");
			}
			else
			{
				throw new Exception("Protocol Plugin Directory (\""  + dInfo.FullName + "\") is missing!");
			}
			

			List<Assembly> plugInAssemblyList = new List<Assembly>();

			if (null != files)
			{
				foreach (FileInfo file in files)
				{
					plugInAssemblyList.Add(Assembly.LoadFile(file.FullName));
				}
			}
			return plugInAssemblyList;
		}

		static List<IProtocol> GetPlugIns(List<Assembly> assemblies)
		{
			List<Type> availableTypes = new List<Type>();

			foreach (Assembly currentAssembly in assemblies)
			{
				availableTypes.AddRange(currentAssembly.GetTypes());
			}

			// get a list of objects that implement the IProtocol interface AND
			// have the CalculationPlugInAttribute
			List<Type> ProtocolList = availableTypes.FindAll(delegate(Type t)
			                                                 {
			                                                 	List<Type> interfaceTypes = new List<Type>(t.GetInterfaces());
			                                                 	object[] arr = t.GetCustomAttributes(typeof(ProtocolAttribute), true);
			                                                 	return !(arr == null || arr.Length == 0) && interfaceTypes.Contains(typeof(IProtocol));
			                                                 });

			// conver the list of Objects to an instantiated list of IProtocols
			return ProtocolList.ConvertAll<IProtocol>(delegate(Type t) { return Activator.CreateInstance(t) as IProtocol; });
		}
		
		static List<Type> GetAvalbleProtocolPlugIns(List<Assembly> assemblies)
		{
			List<Type> availableTypes = new List<Type>();

			foreach (Assembly currentAssembly in assemblies)
			{
				availableTypes.AddRange(currentAssembly.GetTypes());
			}

			// get a list of objects that implement the IProtocol interface AND
			// have the CalculationPlugInAttribute
			List<Type> ProtocolList = availableTypes.FindAll(delegate(Type t)
			                                                 {
			                                                 	List<Type> interfaceTypes = new List<Type>(t.GetInterfaces());
			                                                 	object[] arr = t.GetCustomAttributes(typeof(ProtocolAttribute), true);
			                                                 	return !(arr == null || arr.Length == 0) && interfaceTypes.Contains(typeof(IProtocol));
			                                                 });

			
			return ProtocolList;
		}
		
	}
}
