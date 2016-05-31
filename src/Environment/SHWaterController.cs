using UnityEngine;
using System.Collections;

[AddComponentMenu("Skahal/Environment/SHWaterController")]
[ExecuteInEditMode] // Make water live-update even when not in play mode
public class SHWaterController : MonoBehaviour
{
	#region Enums
	public enum WaterMode
	{
		Simple = 0,
		Reflective = 1,
		Refractive = 2,
	};
	#endregion
	
	#region Fields
	private Hashtable m_ReflectionCameras = new Hashtable (); // Camera -> Camera table
	private Hashtable m_RefractionCameras = new Hashtable (); // Camera -> Camera table
	
	private RenderTexture m_ReflectionTexture = null;
	private RenderTexture m_RefractionTexture = null;
	private WaterMode m_HardwareWaterSupport = WaterMode.Refractive;
	private int m_OldReflectionTextureSize = 0;
	private int m_OldRefractionTextureSize = 0;
	private static bool s_InsideWater = false;
	private Material m_material;
	//private WaterMode m_waterMode;
	private Vector4 m_waveSpeed;
	private float m_waveScale;
	private Vector4 m_waveScale4;
	private float m_xWaveSpeedScaled;
	private float m_yWaveSpeedScaled;
	private float m_zWaveSpeedScaled;
	private float m_wWaveSpeedScaled;
	private Camera m_camera;
	private Camera m_reflectionCamera;
	private Camera m_refractionCamera;
	#endregion
	
	#region Properties
	public WaterMode m_WaterMode = WaterMode.Refractive;
	public bool m_DisablePixelLights = true;
	public int m_TextureSize = 256;
	public float m_ClipPlaneOffset = 0.07f;
	public LayerMask m_ReflectLayers = -1;
	public LayerMask m_RefractLayers = -1;
	#endregion

	#region Methods
	
	#region Life cycle
	private void Awake ()
	{
		m_material = GetComponent<Renderer>().sharedMaterial;
		
		// Actual water rendering mode depends on both the current setting AND
		// the hardware support. There's no point in rendering refraction textures
		// if they won't be visible in the end.
		m_HardwareWaterSupport = FindHardwareWaterSupport ();
		//m_waterMode = GetWaterMode ();
		
		m_waveSpeed = m_material.GetVector ("WaveSpeed");
		m_waveScale = m_material.GetFloat ("_WaveScale");
		m_waveScale4 = new Vector4 (m_waveScale, m_waveScale, m_waveScale * 0.4f, m_waveScale * 0.45f);
		
		m_material.SetVector ("_WaveScale4", m_waveScale4);
		m_xWaveSpeedScaled = m_waveSpeed.x * m_waveScale4.x;
		m_yWaveSpeedScaled = m_waveSpeed.y * m_waveScale4.y;
		m_zWaveSpeedScaled = m_waveSpeed.z * m_waveScale4.z;
		m_wWaveSpeedScaled = m_waveSpeed.w * m_waveScale4.w;
		
		m_camera = Camera.current;
//		CreateWaterObjects (m_camera, out m_reflectionCamera, out m_refractionCamera);
//		UpdateCameraModes (m_camera, m_reflectionCamera);
//		UpdateCameraModes (m_camera, m_refractionCamera);
		
	}
	#endregion
	
	// This is called when it's known that the object will be rendered by some
	// camera. We render reflections / refractions and do other updates here.
	// Because the script executes in edit mode, reflections for the scene view
	// camera will just work!
	public void OnWillRenderObject ()
	{
		//	return;
//		if (!enabled || !renderer || !m_material || !renderer.enabled)
//		{
//			return;
//		}
			
//		Camera cam = Camera.current;
//		if (!cam)
//		{
//			return;
//		}
//	
		// Safeguard from recursive water reflections.		
		if (s_InsideWater)
		{
			return;
		}
		
		s_InsideWater = true;
		
		if (m_camera == null)
		{
			m_camera = Camera.current;
			CreateWaterObjects (m_camera, out m_reflectionCamera, out m_refractionCamera);
			UpdateCameraModes (m_camera, m_reflectionCamera);
			UpdateCameraModes (m_camera, m_refractionCamera);
			
			Shader.DisableKeyword ("WATER_SIMPLE");
			Shader.DisableKeyword ("WATER_REFLECTIVE");
			Shader.EnableKeyword ("WATER_REFRACTIVE");
		}
//		Camera reflectionCamera, refractionCamera;
		
		
		// find out the reflection plane: position and normal in world space
		Vector3 pos = transform.position;
		Vector3 normal = transform.up;
		
		// Optionally disable pixel lights for reflection/refraction
		int oldPixelLightCount = QualitySettings.pixelLightCount;
		QualitySettings.pixelLightCount = 0;
		
		//	UpdateCameraModes (m_camera, m_reflectionCamera);
		//	UpdateCameraModes (m_camera, m_refractionCamera);
		
		// Render reflection if needed
		
		// Reflect camera around reflection plane
		float d = -Vector3.Dot (normal, pos) - m_ClipPlaneOffset;
		Vector4 reflectionPlane = new Vector4 (normal.x, normal.y, normal.z, d);
	
		Matrix4x4 reflection = Matrix4x4.zero;
		CalculateReflectionMatrix (ref reflection, reflectionPlane);
		Vector3 oldpos = m_camera.transform.position;
		Vector3 newpos = reflection.MultiplyPoint (oldpos);
		m_reflectionCamera.worldToCameraMatrix = m_camera.worldToCameraMatrix * reflection;
	
		// Setup oblique projection matrix so that near plane is our reflection
		// plane. This way we clip everything below/above it for free.
		Vector4 clipPlane = CameraSpacePlane (m_reflectionCamera, pos, normal, 1.0f);
		Matrix4x4 projection = m_camera.projectionMatrix;
		CalculateObliqueMatrix (ref projection, clipPlane);
		m_reflectionCamera.projectionMatrix = projection;
		
		m_reflectionCamera.cullingMask = ~(1 << 4) & m_ReflectLayers.value; // never render water layer
		m_reflectionCamera.targetTexture = m_ReflectionTexture;
		GL.SetRevertBackfacing (true);
		m_reflectionCamera.transform.position = newpos;
		Vector3 euler = m_camera.transform.eulerAngles;
		m_reflectionCamera.transform.eulerAngles = new Vector3 (-euler.x, euler.y, euler.z);
		m_reflectionCamera.Render ();
		m_reflectionCamera.transform.position = oldpos;
		GL.SetRevertBackfacing (false);
		m_material.SetTexture ("_ReflectionTex", m_ReflectionTexture);
		
		
		// Render refraction
		m_refractionCamera.worldToCameraMatrix = m_camera.worldToCameraMatrix;
	
		// Setup oblique projection matrix so that near plane is our reflection
		// plane. This way we clip everything below/above it for free.
		clipPlane = CameraSpacePlane (m_refractionCamera, pos, normal, -1.0f);
		projection = m_camera.projectionMatrix;
		CalculateObliqueMatrix (ref projection, clipPlane);
		m_refractionCamera.projectionMatrix = projection;
		
		m_refractionCamera.cullingMask = ~(1 << 4) & m_RefractLayers.value; // never render water layer
		m_refractionCamera.targetTexture = m_RefractionTexture;
		m_refractionCamera.transform.position = m_camera.transform.position;
		m_refractionCamera.transform.rotation = m_camera.transform.rotation;
		m_refractionCamera.Render ();
		m_material.SetTexture ("_RefractionTex", m_RefractionTexture);
		
		// Restore pixel light count
		QualitySettings.pixelLightCount = oldPixelLightCount;
		
		s_InsideWater = false;
	}
	
	
	// Cleanup all the objects we possibly have created
	void OnDisable ()
	{
		if (m_ReflectionTexture)
		{
			DestroyImmediate (m_ReflectionTexture);
			m_ReflectionTexture = null;
		}
		if (m_RefractionTexture)
		{
			DestroyImmediate (m_RefractionTexture);
			m_RefractionTexture = null;
		}
		foreach (DictionaryEntry kvp in m_ReflectionCameras)
			DestroyImmediate (((Camera)kvp.Value).gameObject);
		m_ReflectionCameras.Clear ();
		foreach (DictionaryEntry kvp in m_RefractionCameras)
			DestroyImmediate (((Camera)kvp.Value).gameObject);
		m_RefractionCameras.Clear ();
	}
	
	
	// This just sets up some matrices in the material; for really
	// old cards to make water texture scroll.
	void Update ()
	{
		//return;
//		if (!renderer)
//		{
//			return;
//		}
//		
//		
//		if (!m_material)
//		{
//			return;
//		}
//			

		
		// Time since level load, and do intermediate calculations with doubles
		double t = Time.timeSinceLevelLoad / 20.0;
//		Vector4 offsetClamped = new Vector4 (
//			(float)System.Math.IEEERemainder (m_waveSpeed.x * m_waveScale4.x * t, 1.0),
//			(float)System.Math.IEEERemainder (m_waveSpeed.y * m_waveScale4.y * t, 1.0),
//			(float)System.Math.IEEERemainder (m_waveSpeed.z * m_waveScale4.z * t, 1.0),
//			(float)System.Math.IEEERemainder (m_waveSpeed.w * m_waveScale4.w * t, 1.0)
//		);
		
		var offsetClamped = new Color (
			(float)System.Math.IEEERemainder (m_xWaveSpeedScaled * t, 1.0),
			(float)System.Math.IEEERemainder (m_yWaveSpeedScaled * t, 1.0),
			(float)System.Math.IEEERemainder (m_zWaveSpeedScaled * t, 1.0),
			(float)System.Math.IEEERemainder (m_wWaveSpeedScaled * t, 1.0));
		
		m_material.SetColor ("_WaveOffset", offsetClamped);
		//m_material.SetVector ("_WaveScale4", m_waveScale4);
			
//		Vector3 waterSize = renderer.bounds.size;		
//		Vector3 scale = new Vector3 (waterSize.x * m_waveScale4.x, waterSize.z * m_waveScale4.y, 1);
//		Matrix4x4 scrollMatrix = Matrix4x4.TRS (new Vector3 (offsetClamped.x, offsetClamped.y, 0), Quaternion.identity, scale);
//		m_material.SetMatrix ("_WaveMatrix", scrollMatrix);
				
//		scale = new Vector3 (waterSize.x * m_waveScale4.z, waterSize.z * m_waveScale4.w, 1);
//		scrollMatrix = Matrix4x4.TRS (new Vector3 (offsetClamped.z, offsetClamped.w, 0), Quaternion.identity, scale);
//		m_material.SetMatrix ("_WaveMatrix2", scrollMatrix);
	}
	
	private void UpdateCameraModes (Camera src, Camera dest)
	{
		if (dest == null)
		{
			return;
		}
		// set water camera to clear the same way as current camera
		dest.clearFlags = src.clearFlags;
		dest.backgroundColor = src.backgroundColor;		
		if (src.clearFlags == CameraClearFlags.Skybox)
		{
			Skybox sky = src.GetComponent (typeof(Skybox)) as Skybox;
			Skybox mysky = dest.GetComponent (typeof(Skybox)) as Skybox;
			if (!sky || !sky.material)
			{
				mysky.enabled = false;
			}
			else
			{
				mysky.enabled = true;
				mysky.material = sky.material;
			}
		}
		// update other values to match current camera.
		// even if we are supplying custom camera&projection matrices,
		// some of values are used elsewhere (e.g. skybox uses far plane)
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
	}
	
	// On-demand create any objects we need for water
	private void CreateWaterObjects (Camera currentCamera, out Camera reflectionCamera, out Camera refractionCamera)
	{
		WaterMode mode = GetWaterMode ();
		
		reflectionCamera = null;
		refractionCamera = null;
		
		if (mode >= WaterMode.Reflective)
		{
			// Reflection render texture
			if (!m_ReflectionTexture || m_OldReflectionTextureSize != m_TextureSize)
			{
				if (m_ReflectionTexture)
				{
					DestroyImmediate (m_ReflectionTexture);
				}
				m_ReflectionTexture = new RenderTexture (m_TextureSize, m_TextureSize, 16);
				m_ReflectionTexture.name = "__WaterReflection" + GetInstanceID ();
				m_ReflectionTexture.isPowerOfTwo = true;
				m_ReflectionTexture.hideFlags = HideFlags.DontSave;
				m_OldReflectionTextureSize = m_TextureSize;
			}
			
			// Camera for reflection
			reflectionCamera = m_ReflectionCameras [currentCamera] as Camera;
			if (!reflectionCamera) // catch both not-in-dictionary and in-dictionary-but-deleted-GO
			{
				GameObject go = new GameObject ("Water Refl Camera id" + GetInstanceID () + " for " + currentCamera.GetInstanceID (), typeof(Camera), typeof(Skybox));
				reflectionCamera = go.GetComponent<Camera>();
				reflectionCamera.enabled = false;
				reflectionCamera.transform.position = transform.position;
				reflectionCamera.transform.rotation = transform.rotation;
				reflectionCamera.gameObject.AddComponent <FlareLayer>();
				go.hideFlags = HideFlags.HideAndDontSave;
				m_ReflectionCameras [currentCamera] = reflectionCamera;
			}
		}
		
		if (mode >= WaterMode.Refractive)
		{
			// Refraction render texture
			if (!m_RefractionTexture || m_OldRefractionTextureSize != m_TextureSize)
			{
				if (m_RefractionTexture)
				{
					DestroyImmediate (m_RefractionTexture);
				}
				m_RefractionTexture = new RenderTexture (m_TextureSize, m_TextureSize, 16);
				m_RefractionTexture.name = "__WaterRefraction" + GetInstanceID ();
				m_RefractionTexture.isPowerOfTwo = true;
				m_RefractionTexture.hideFlags = HideFlags.DontSave;
				m_OldRefractionTextureSize = m_TextureSize;
			}
			
			// Camera for refraction
			refractionCamera = m_RefractionCameras [currentCamera] as Camera;
			if (!refractionCamera) // catch both not-in-dictionary and in-dictionary-but-deleted-GO
			{
				GameObject go = new GameObject ("Water Refr Camera id" + GetInstanceID () + " for " + currentCamera.GetInstanceID (), typeof(Camera), typeof(Skybox));
				refractionCamera = go.GetComponent<Camera>();
				refractionCamera.enabled = false;
				refractionCamera.transform.position = transform.position;
				refractionCamera.transform.rotation = transform.rotation;
				refractionCamera.gameObject.AddComponent <FlareLayer>();
				go.hideFlags = HideFlags.HideAndDontSave;
				m_RefractionCameras [currentCamera] = refractionCamera;
			}
		}
	}
	
	private WaterMode GetWaterMode ()
	{
		if (m_HardwareWaterSupport < m_WaterMode)
		{
			return m_HardwareWaterSupport;
		}
		else
		{
			return m_WaterMode;
		}
	}
	
	private WaterMode FindHardwareWaterSupport ()
	{
		if (!SystemInfo.supportsRenderTextures || !GetComponent<Renderer>())
		{
			return WaterMode.Simple;
		}
		
		if (!m_material)
		{
			return WaterMode.Simple;
		}
			
		string mode = m_material.GetTag ("WATERMODE", false);
		
		if (mode == "Refractive")
		{
			return WaterMode.Refractive;
		}
		if (mode == "Reflective")
		{
			return WaterMode.Reflective;
		}
			
		return WaterMode.Simple;
	}
	
	// Extended sign: returns -1, 0 or 1 based on sign of a
	private static float sgn (float a)
	{
		if (a > 0.0f)
		{
			return 1.0f;
		}
		if (a < 0.0f)
		{
			return -1.0f;
		}
		return 0.0f;
	}
	
	// Given position/normal of the plane, calculates plane in camera space.
	private Vector4 CameraSpacePlane (Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 offsetPos = pos + normal * m_ClipPlaneOffset;
		Matrix4x4 m = cam.worldToCameraMatrix;
		Vector3 cpos = m.MultiplyPoint (offsetPos);
		Vector3 cnormal = m.MultiplyVector (normal).normalized * sideSign;
		return new Vector4 (cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot (cpos, cnormal));
	}
	
	// Adjusts the given projection matrix so that near plane is the given clipPlane
	// clipPlane is given in camera space. See article in Game Programming Gems 5 and
	// http://aras-p.info/texts/obliqueortho.html
	private static void CalculateObliqueMatrix (ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 q = projection.inverse * new Vector4 (
			sgn (clipPlane.x),
			sgn (clipPlane.y),
			1.0f,
			1.0f
		);
		Vector4 c = clipPlane * (2.0F / (Vector4.Dot (clipPlane, q)));
		// third row = clip plane - fourth row
		projection [2] = c.x - projection [3];
		projection [6] = c.y - projection [7];
		projection [10] = c.z - projection [11];
		projection [14] = c.w - projection [15];
	}

	// Calculates reflection matrix around the given plane
	private static void CalculateReflectionMatrix (ref Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = (1F - 2F * plane [0] * plane [0]);
		reflectionMat.m01 = (- 2F * plane [0] * plane [1]);
		reflectionMat.m02 = (- 2F * plane [0] * plane [2]);
		reflectionMat.m03 = (- 2F * plane [3] * plane [0]);

		reflectionMat.m10 = (- 2F * plane [1] * plane [0]);
		reflectionMat.m11 = (1F - 2F * plane [1] * plane [1]);
		reflectionMat.m12 = (- 2F * plane [1] * plane [2]);
		reflectionMat.m13 = (- 2F * plane [3] * plane [1]);
	
		reflectionMat.m20 = (- 2F * plane [2] * plane [0]);
		reflectionMat.m21 = (- 2F * plane [2] * plane [1]);
		reflectionMat.m22 = (1F - 2F * plane [2] * plane [2]);
		reflectionMat.m23 = (- 2F * plane [3] * plane [2]);

		reflectionMat.m30 = 0F;
		reflectionMat.m31 = 0F;
		reflectionMat.m32 = 0F;
		reflectionMat.m33 = 1F;
	}
	#endregion
}