#region Usings
using UnityEngine;
using System.Collections;
using System.Linq;
using System;
#endregion

namespace Skahal.Reflection
{
	/// <summary>
	/// Helper for reflection stuffs
	/// </summary>
	public static class SHReflection
	{
		#region Fields
		private static Type[] s_currentAssemblyTypes;
		#endregion 
		
		#region Constructor
		/// <summary>
		/// Initializes the <see cref="Skahal.Reflection.SHReflection"/> class.
		/// </summary>
		static SHReflection ()
		{
			s_currentAssemblyTypes = typeof(SHReflection).Assembly.GetTypes ();
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Finds types in current assembly that implements the specified interface.
		/// </summary>
		/// <returns>
		/// The types.
		/// </returns>
		/// <param name='interfaceType'>
		/// Interface type.
		/// </param>
		public static Type[] FindInterfaceImplementations (Type interfaceType)
		{
			return s_currentAssemblyTypes.Where (t => t.GetInterface (interfaceType.Name) != null).ToArray ();
		}
		#endregion
	}
}