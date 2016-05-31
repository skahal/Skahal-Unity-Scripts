#region Usings
using UnityEngine;
using System.Collections;
using System;
#endregion

namespace Skahal.Data
{
	/// <summary>
	/// Defines a interface for a key-value storage provider.
	/// </summary>
	public interface ISHKeyValueStorageProvider
	{
		#region Events
		event EventHandler<SHSettingValueFailedEventArgs> SettingValueFailed;
		#endregion
		
		#region Methods
		/// <summary>
		/// Sets a value.
		/// </summary>
		/// <param name='key'>
		/// Key.
		/// </param>
		/// <param name='value'>
		/// Value.
		/// </param>
		void Set(string key, string value);

		/// <summary>
		/// Get a value.
		/// </summary>
		/// <param name='key'>
		/// Key.
		/// </param>
		/// <param name='valueReceived'>
		/// Value received.
		/// </param>
		void Get(string key, Action<string> valueReceived);
		#endregion
	}
}