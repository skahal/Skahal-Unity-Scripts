#region Usings
using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Skahal.Common;
using Skahal.Logging;
using System.Collections.Generic;
#endregion

namespace Skahal.Data
{
	/// <summary>
	/// Global Server based key value storage provider.
	/// </summary>
	public class SHGlobalServerKeyValueStorageProvider : ISHKeyValueStorageProvider
	{
		#region Fields
		private string m_serverAddress;
		private string m_playerId;
		private Dictionary<string, string> m_cache;
		#endregion
		
		#region Constructors
		public SHGlobalServerKeyValueStorageProvider (string serverAddress, string playerId)
		{
			m_serverAddress = serverAddress;
			m_playerId = playerId;
			m_cache = new Dictionary<string, string>();
		}
		#endregion
		
		#region ISHKeyValueStorageProvider implementation
		public event EventHandler<SHSettingValueFailedEventArgs> SettingValueFailed;
		
		public void Set (string key, string value)
		{
			SetCache(key, value);
			
			var keyValue = new SHKeyValue ();
			keyValue.Key = key;
			keyValue.Value = value;
			
			SH.StartCoroutine (Request (keyValue, "SetValue"));
		}
		
		private void SetCache (string key, string value)
		{
			if (!m_cache.ContainsKey (key))
			{
				m_cache.Add (key, value);
			}
			
			m_cache [key] = value;	
		}
		
		public void Get (string key, Action<string> valueReceived)
		{
			if (m_cache.ContainsKey (key))
			{
				valueReceived (m_cache [key]);	
				return;
			}
			
			var keyValue = new SHKeyValue ();
			keyValue.Key = key;
			SH.StartCoroutine (Request (keyValue, "GetValue", (value) => {
				SetCache (key, value);
				valueReceived (value);
			}));
			
		}
		
		#endregion
		
		#region Helpers
		private IEnumerator Request (SHKeyValue keyValue, string methodName, Action<string> valueReceived = null)
		{
			var url = string.Format (CultureInfo.InvariantCulture, 
				"{0}/services/storage/{1}.ashx?playerId={2}&key={3}&value={4}", 
				m_serverAddress, methodName, m_playerId, keyValue.Key, keyValue.Value);
			
			SHLog.Debug ("SHGlobalServerKeyValueStorageProvider: requesting url '{0}'.", url);
			
			var www = new WWW (url);
			yield return www;
				
			if (String.IsNullOrEmpty (www.error))
			{
				var response = www.text;
				
				if (!String.IsNullOrEmpty (response))
				{
					SHLog.Debug ("SHGlobalServerKeyValueStorageProvider: response received '{0}'.", response);
					
					if (valueReceived != null)
					{
						valueReceived (response.Replace("\"", ""));
					}
				}
			}
			else
			{
				SettingValueFailed.Raise (this, new SHSettingValueFailedEventArgs (keyValue));
			}
		}
		#endregion
	}
}