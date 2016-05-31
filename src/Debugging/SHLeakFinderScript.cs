using UnityEngine;
using System.Text;
using Skahal.Logging;
 

[AddComponentMenu("Skahal/Debug/SHLeakFinderScript")]
public class SHLeakFinderScript : MonoBehaviour
{
	void Update()
	{
		if (Input.touchCount >= 3)
		{
			StringBuilder log = new StringBuilder();
			
			log.AppendLine("=================================");
			log.AppendLine("Skahal Studios Leak Finder Script");
			log.AppendLine("=========LOADED OBJECTS==========");
			log.AppendLine("All " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)).Length);
			log.AppendLine("Textures " + Resources.FindObjectsOfTypeAll(typeof(Texture)).Length);
			log.AppendLine("AudioClips " + Resources.FindObjectsOfTypeAll(typeof(AudioClip)).Length);
			log.AppendLine("Meshes " + Resources.FindObjectsOfTypeAll(typeof(Mesh)).Length);
			log.AppendLine("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
			log.AppendLine("GameObjects " + Resources.FindObjectsOfTypeAll(typeof(GameObject)).Length);
			log.AppendLine("Components " + Resources.FindObjectsOfTypeAll(typeof(Component)).Length);
			
			log.AppendLine("=========ACTIVE OBJECTS==========");
			log.AppendLine("=================================");
			log.AppendLine("All " + GameObject.FindObjectsOfType(typeof(UnityEngine.Object)).Length);
			log.AppendLine("Textures " + GameObject.FindObjectsOfType(typeof(Texture)).Length);
			log.AppendLine("AudioClips " + GameObject.FindObjectsOfType(typeof(AudioClip)).Length);
			log.AppendLine("Meshes " + GameObject.FindObjectsOfType(typeof(Mesh)).Length);
			log.AppendLine("Materials " + GameObject.FindObjectsOfType(typeof(Material)).Length);
			log.AppendLine("GameObjects " + GameObject.FindObjectsOfType(typeof(GameObject)).Length);
			log.AppendLine("Components " + GameObject.FindObjectsOfType(typeof(Component)).Length);
			SHLog.Debug(log.ToString());
		}
	}
}

