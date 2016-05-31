#region Usings
using UnityEngine;
 
#endregion

[AddComponentMenu("Skahal/Messages/SHMessageReceiverScript")]
public class MessageReceiverScript : MonoBehaviour
{
	#region Properties
	/// <summary>
	/// As mensagens que o GameObject ira receber do Messenger.
	/// </summary>
	public string[] Messages;
	#endregion
	
	#region Methods
	void OnEnable()
	{
		Messenger.Register(gameObject, Messages);
	}

	void OnDisable()
	{
		Messenger.Unregister(gameObject, Messages);	
	}
	#endregion
}

