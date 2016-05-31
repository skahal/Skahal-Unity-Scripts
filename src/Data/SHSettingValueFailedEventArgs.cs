#region Usings
using UnityEngine;
using System.Collections;
using System;
using Skahal.Common;
#endregion

namespace Skahal.Data
{
	/// <summary>
	/// Setting value failed event arguments.
	/// </summary>
	public class SHSettingValueFailedEventArgs : EventArgs
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Data.SHSettingValueFailedEventArgs"/> class.
		/// </summary>
		/// <param name='keyValue'>
		/// Key value.
		/// </param>
		public SHSettingValueFailedEventArgs (SHKeyValue keyValue)
		{
			KeyValue = keyValue;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets the key value.
		/// </summary>
		/// <value>
		/// The key value.
		/// </value>
		public SHKeyValue KeyValue { get; private set; }
		#endregion
	}
}