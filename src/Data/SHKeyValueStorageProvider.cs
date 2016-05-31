#region Usings
using UnityEngine;
using System.Collections;
using Skahal.Common;
using System;
#endregion

namespace Skahal.Data
{
	/// <summary>
	/// SH key value storage provider.
	/// </summary>
	public static class SHKeyValueStorageProvider 
	{	
		#region Events
		public static event EventHandler Initialized;
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets the current provider.
		/// </summary>
		public static ISHKeyValueStorageProvider Current { get; private set; }
		#endregion
		
		#region Methods
		/// <summary>
		/// Initialize the specified provider.
		/// </summary>
		/// <param name='provider'>
		/// Provider.
		/// </param>
		public static void Initialize (ISHKeyValueStorageProvider provider)
		{
			Current = provider;
			Initialized.Raise(typeof(SHKeyValueStorageProvider));
		}
		#endregion
	}
}