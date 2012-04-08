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


using OctoTip.Lib.ExperimentsCore.Interfaces;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;


namespace OctoTip.Lib.ExperimentsCore
{
	/// <summary>
	/// Description of ProtocolProvider.
	/// </summary>
	public static class ProtocolProvider
	{
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

		
		public static Protocol GetProtocol(Type ProtocolType,ProtocolParameters ProtocolParameters)
		{
			return Activator.CreateInstance(ProtocolType,ProtocolParameters) as Protocol;
		}
		
		
		
		
		public static List<Type> GetProtocolStates(Type ProtocolType)
		{
			
			List<Type> ProtocolStates = new List<Type>(0) ;
			foreach (  MethodInfo mi in ProtocolType.GetMethods(BindingFlags.Static |BindingFlags.Public))
			{
				if(mi.Name == "ProtocolStates")
				{
					ProtocolStates = mi.Invoke(null,null) as List<Type>;
				}
				
			}
			return ProtocolStates;
		}
		
		
		public static List<Type> GetStateNextStates(Type StateType)
		{
			
			List<Type> NextStates = new List<Type>(0) ;
			foreach (  MethodInfo mi in StateType.GetMethods(BindingFlags.Static |BindingFlags.Public))
			{
				if(mi.Name == "NextStates")
				{
					NextStates = mi.Invoke(null,null) as List<Type>;
				}
				
			}
			return NextStates;
		}
		

		public static List<Assembly> GetUncompitbleProtocolPlugIns()
		{
			List<Assembly> assemblies = LoadPlugInAssemblies();
			List<Assembly> UncompitbleTypes = new List<Assembly>();
			foreach (Assembly currentAssembly in assemblies)
			{
				try
				{
					currentAssembly.GetTypes();
				}
				catch(System.Reflection.ReflectionTypeLoadException)
				{
					UncompitbleTypes.Add(currentAssembly);
				}
			}
			
			return UncompitbleTypes;
			
		}
		
		
		public static List<Type>  GetAvalbleProtocolPlugIns()
		{
			return GetAvalbleProtocolPlugIns(LoadPlugInAssemblies());
		}
		
		static List<Type> GetAvalbleProtocolPlugIns(List<Assembly> assemblies)
		{
			List<Type> availableTypes = new List<Type>();

			foreach (Assembly currentAssembly in assemblies)
			{
				try
				{
				availableTypes.AddRange(currentAssembly.GetTypes());
				}
				catch(System.Reflection.ReflectionTypeLoadException)
				{
					// just ignore un sported dlls
				//	throw new Exception("The suplied dll (" + currentAssembly.GetName()  + ") is not compatible with the current version, and was not loaded",e);
				}
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
		
		
		public static Type  GetStatePlugInByDesplayName(string StateDesplayName)
		{
			return GetStatePlugInByDesplayName(LoadPlugInAssemblies(),StateDesplayName);
		}
		
				
		static Type GetStatePlugInByDesplayName(List<Assembly> assemblies,string StateDesplayName)
		{
			List<Type> availableTypes = new List<Type>();

			foreach (Assembly currentAssembly in assemblies)
			{
				availableTypes.AddRange(currentAssembly.GetTypes());
			}

			// get a list of objects that implement the IProtocol interface AND
			// have the CalculationPlugInAttribute
			List<Type> StateList = availableTypes.FindAll(delegate(Type t)
			                                                 {
			                                                 	List<Type> interfaceTypes = new List<Type>(t.GetInterfaces());
			                                                 	StateAttribute[] arr = t.GetCustomAttributes(typeof(StateAttribute), true) as StateAttribute[];
			                                                 	bool IsState = false;
			                                                 	foreach (StateAttribute SA in arr)
			                                                 	{
			                                                 		IsState = SA.DisplayName ==StateDesplayName;
			                                                 	}
			                                                 	return  IsState &&!(arr == null || arr.Length == 0) && interfaceTypes.Contains(typeof(IState));
			                                                 });
			
			
				
			return StateList[0];
		}
		
		
		
		public static ProtocolParameters GetProtocolParameters(Type ProtocolData)
		{
			Type ProtocolParametersType=null;
			ConstructorInfo[] ProtocolConstructorInfos = ProtocolData.GetConstructors(BindingFlags.Public| BindingFlags.Instance);
			foreach (ConstructorInfo ProtocolConstructorInfo in ProtocolConstructorInfos)
			{
				ParameterInfo[] ProtocolConstructorParameterInfos = ProtocolConstructorInfo.GetParameters();
				foreach (ParameterInfo ProtocolConstructorParameterInfo in ProtocolConstructorParameterInfos)
				{
					
					if (ProtocolConstructorParameterInfo.ParameterType.GetInterface("OctoTip.OctoTipExperiments.Core.Interfaces.IProtocolParameters")!=null)
					{
						ProtocolParametersType = ProtocolConstructorParameterInfo.ParameterType;
						break;
					}
				}
			}
			//System.Diagnostics.Debug.WriteLine(Activator.CreateInstance(ProtocolParametersType));
			if (ProtocolParametersType == null)
			{
				throw new NullReferenceException("No apropriate ProtocolParametersType asochiated to this protocof found in the dll");
			}
				
			ProtocolParameters  NewProtocolParameters = (ProtocolParameters)(Activator.CreateInstance(ProtocolParametersType));
			
			
			return NewProtocolParameters;
		}
		
		public static FieldInfo[] GetProtocolParametersFields(ProtocolParameters ProtocolParameters )
			//public static void GetProtocolParametersTypes(IProtocolParameters ProtocolParameters )
		{
			FieldInfo[] ProtocolParametersFields= ProtocolParameters.GetType().GetFields();
			return ProtocolParametersFields;
		}
		
		public static Type[] GetAvalbleTypes()
		{
			return GetAvalbleTypes(LoadPlugInAssemblies());
		}




		static Type[] GetAvalbleTypes(List<Assembly> assemblies)
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
			                                                 	return (interfaceTypes.Contains(typeof(IState)) ||interfaceTypes.Contains(typeof(IProtocolParameters)) || interfaceTypes.Contains(typeof(IProtocol)));
			                                                 	//return (interfaceTypes.Contains(typeof(IProtocolParameters)) || interfaceTypes.Contains(typeof(IProtocol)));
			                                                 });

			
			return ProtocolList.ToArray();
		}
		
		
		public static string GetStateDesplayName(Type StateType)
		{
			return ((StateAttribute)StateType.GetCustomAttributes(typeof(StateAttribute),true)[0]).DisplayName;
		}
		public static string GetStateDesplayName(State State)
		{
			return GetStateDesplayName(State.GetType());
		}
		
		public static string GetStateDescription(Type StateType)
		{
			return ((StateAttribute)StateType.GetCustomAttributes(typeof(StateAttribute),true)[0]).Description;
		}
		public static string GetStateDescription(State State)
		{
			return GetStateDescription(State.GetType());
		}
		
	}
}
