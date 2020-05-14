using UnityEngine;
using System.Collections;

[AddComponentMenu("Skahal/GUI/SHGUIText")]
public class SHGUIText : MonoBehaviour
{
	#region Campos
	private static SHGUIText s_instance;
	#endregion
	
	#region Metodos
	
	#region Ciclo de vida
	void Awake()
	{
		s_instance = this;
	}
	#endregion

	
	#region ChangeFontSize
	public static void ChangeFontSize(UnityEngine.UI.Text guiText, float firstDelay, float sizeDelay, int toFontSize)
	{
		s_instance.StartCoroutine(DelayChangeFontSize(guiText, firstDelay, sizeDelay, toFontSize));
	}

	static IEnumerator DelayChangeFontSize(UnityEngine.UI.Text guiText, float firstDelay, float sizeDelay, int toFontSize)
	{
		yield return new WaitForSeconds(firstDelay);
	
		while (guiText.fontSize != toFontSize)
		{
			guiText.fontSize += toFontSize > guiText.fontSize ? 1 : -1;
			yield return new WaitForSeconds(sizeDelay);
		}
	}
	#endregion
	
	#endregion
}

