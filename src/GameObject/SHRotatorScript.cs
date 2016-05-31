using UnityEngine;
 
[AddComponentMenu("Skahal/Game Objects/SHRotatorScript")]
public class SHRotatorScript : MonoBehaviour 
{
	#region Propriedades
	public Vector3 Rotation;
	#endregion
	
	#region Metodos
	void Update () 
	{
		transform.Rotate(Rotation);
	}
	#endregion
}
