using UnityEngine;
 
[AddComponentMenu("Skahal/Materials/SHScrollTextureScript")]
public class ScrollTextureScript : MonoBehaviour 
{
	#region Campos
	private Vector2 m_originalOffset;
	#endregion
	
	#region Propriedades
	public Vector2 ScrollSpeed = new Vector2(0.01f, 0.01f);
	public Vector2 MinScroll = new Vector2(-10, -10);
	public Vector2 MaxScroll = new Vector2(10, 10);
	#endregion
	
	#region Metodos
	void Start()
	{
		m_originalOffset = GetComponent<Renderer>().material.mainTextureOffset;
	}
	
	void FixedUpdate () 
	{
		float offsetX = GetComponent<Renderer>().material.mainTextureOffset.x;
		float offsetY = GetComponent<Renderer>().material.mainTextureOffset.y;
		
		if(offsetX < MinScroll.x || offsetX > MaxScroll.x)
		{
			ScrollSpeed.x *= -1;
		}
		
		if(offsetY < MinScroll.y || offsetY > MaxScroll.y)
		{
			ScrollSpeed.y *= -1;
		}
		
		m_originalOffset += new Vector2(ScrollSpeed.x, ScrollSpeed.y);
		GetComponent<Renderer>().material.mainTextureOffset = m_originalOffset;
	}
	#endregion
}
