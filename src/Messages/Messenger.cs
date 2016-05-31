#region Usings
using UnityEngine;
using System.Collections.Generic;
using Skahal.Logging;
using System.Linq;
#endregion

public class Messenger : MonoBehaviour
{
	#region Fields
	private static Messenger s_instance;
	private Dictionary<string, List<GameObject>> m_registeredGameObjects = new Dictionary<string, List<GameObject>> ();
	#endregion
	
	#region Constructors
	static Messenger ()
	{
		var go = new GameObject ("SHMessenger");
		s_instance = go.AddComponent<Messenger> ();
	}	
	#endregion
	
	#region Life cycle
	private void Awake ()
	{
		DontDestroyOnLoad (this);
	}
	
	private void OnLevelWasLoaded ()
	{
		foreach (var msg in m_registeredGameObjects)
		{
			var removedCount = msg.Value.RemoveAll (
			(go) => {
				return go == null; 
			});
			
			if (removedCount > 0)
			{
				SHLog.Debug ("[Messenger] {0} GameObjects was removed from message '{1}'. {2} GameObjects remaining.", removedCount, msg.Key, msg.Value.Count);
			}
		}
	}
	#endregion
	
	#region Static public methods
	public static void Register (GameObject go, params string[] messages)
	{
		lock (typeof(Messenger))
		{
			if (CanBeUsed (go, null))
			{
				foreach (string msg in messages)
				{
					if (!s_instance.m_registeredGameObjects.ContainsKey (msg))
					{
						s_instance.m_registeredGameObjects.Add (msg, new List<GameObject> ());
					}
		
					List<GameObject> gos = s_instance.m_registeredGameObjects [msg];
			
					if (!gos.Contains (go))
					{
						s_instance.m_registeredGameObjects [msg].Add (go);
					}
				}
			}
		}
	}
	
	public static void Unregister (GameObject go, params string[] messages)
	{
		lock (typeof(Messenger))
		{
			if (s_instance != null)
			{
				foreach (string msg in messages)
				{
					if (s_instance.m_registeredGameObjects.ContainsKey (msg))
					{
						List<GameObject> gos = s_instance.m_registeredGameObjects [msg];
				
						if (gos.Contains (go))
						{
							gos.Remove (go);
						}
					}
				}
			}
		}
	}
	
	public static void Send (string message, object value)
	{
		lock (typeof(Messenger))
		{
			if (CanBeUsed (null, message))
			{
				if (s_instance.m_registeredGameObjects.ContainsKey (message))
				{
					List<GameObject> gos = s_instance.m_registeredGameObjects [message];
					int length = gos.Count; 
			
					//SHLog.Debug("[Messenger] Enviando a mensagem '" + message + "' para '" + length + "' GameObjects registrados.");
			
					for (int i = 0; i < length; i++)
					{
						try
						{
							//SHLog.Debug("[Messenger] - " + gos[i].name);
							gos [i].SendMessage (message, value, SendMessageOptions.DontRequireReceiver);
						}
						catch (MissingReferenceException ex)
						{
							SHLog.Error("[Messenger] Error while sending message '{0}': {1}.", message, ex.Message);
						}
					}
				}
				else
				{
					SHLog.Warning ("[Messenger] The message '{0}' has no receivers.", message);
				}
			}
		}
	}
	
	public static void Send (string message)
	{
		Send (message, null);
	}
	#endregion

	#region Private Methods
	private static bool CanBeUsed (GameObject go, string message)
	{
		var can = s_instance != null;
		
		if (!can)
		{
			if (go == null)
			{
				SHLog.Warning ("[Messenger] Attempt to send the message '{0}' without the Messenger script has been add to a GameObject.", message);
			}
			else
			{
				SHLog.Warning ("[Messenger] The GameObject '{0}' has attempt to register message without the Messenger script has been add to a GameObject.", go.name);		
			}
		}
		
		return can;
	}
	#endregion
}