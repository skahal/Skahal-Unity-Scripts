using UnityEngine;
 

[AddComponentMenu("Skahal/Game Objects/Floating Script")]
public class FloatingScript : MonoBehaviour
{
	public float waterLevel;
	public float floatHeight;
	public Vector3 buoyancyCentreOffset;
	public float bounceDamp;

	void FixedUpdate () 
	{	
		Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
		float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);
	
		if (forceFactor > 0f)
		{
			Vector3 uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
			GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint);
		}
	}
}

