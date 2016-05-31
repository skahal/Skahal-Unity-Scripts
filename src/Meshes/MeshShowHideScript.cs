using UnityEngine;
 
[AddComponentMenu("Skahal/Meshes/SHMeshShowHideScript")]
public class MeshShowHideScript : MonoBehaviour 
{
	#region Metodos
	private void Hide()
	{
		Component[] allRenderer = GetComponentsInChildren(typeof(Renderer));
	
		foreach (Renderer r in allRenderer)
		{
			r.enabled = false;
		}
	}
	
	private void Show()
	{
		Component[] allRenderer = GetComponentsInChildren(typeof(Renderer));
	
		foreach (Renderer r in allRenderer)
		{
			r.enabled = true;
		}
	}
	#endregion
}
