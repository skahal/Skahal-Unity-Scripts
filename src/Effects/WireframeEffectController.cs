#region Usings
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Skahal.Logging;

#endregion

/// <summary>
/// Wireframe effect controller.
/// <remarks>
/// Works only with gameobjects that has a MeshRenderer attached. 
/// Does not works with SkinnedMeshRender: Matrix stack full depth reached
/// </remarks>
/// </summary>
[AddComponentMenu("Skahal/Effects/WireframeEffectController")]
public class WireframeEffectController : MonoBehaviour
{
	
	#region Fields
	private Vector3[] m_lines;
	private List<Vector3> m_linesArray;
	private Material m_lineMaterial;
	private MeshRenderer m_meshRenderer;
	private Material m_originalMaterial;
	#endregion
	
	#region Constructors
	public WireframeEffectController ()
	{
		ShowAction = () => {
			GetComponent<Renderer>().enabled = true;	
		};
		
		HideAction = () => {
			GetComponent<Renderer>().enabled = false;	
		};
	}
	#endregion
	
	#region Editor properties
	public Color LineColor = Color.green;
	public Color BackgroundColor = Color.black;
	public bool ZWrite = true;
	public bool AWrite = true;
	public bool Blend = true; 
	public Camera TargetCamera;
	#endregion 
	
	#region Properties
	public System.Action ShowAction;
	public System.Action HideAction;
	#endregion
	
	#region Life cycle
	private void Start ()
	{ 
		if (GetComponent<Renderer>().enabled) {
			HideAction();
			m_meshRenderer = GetComponent<MeshRenderer> (); 
			m_originalMaterial = m_meshRenderer.sharedMaterial;
		
			if (!m_meshRenderer) {
				enabled = false;
				return;
			}
		
			m_meshRenderer.sharedMaterial = new Material ("Shader \"Lines/Background\" { Properties { _Color (\"Main Color\", Color) = (1,1,1,1) } SubShader { Pass {" + (ZWrite ? " ZWrite on " : " ZWrite off ") + (Blend ? " Blend SrcAlpha OneMinusSrcAlpha" : " ") + (AWrite ? " Colormask RGBA " : " ") + "Lighting Off Offset 1, 1 Color[_Color] }}}"); 
			m_lineMaterial = new Material ("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha BindChannels { Bind \"Color\",color } ZWrite On Cull Front Fog { Mode Off } } } }"); 

			m_lineMaterial.hideFlags = HideFlags.HideAndDontSave; 
			m_lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave; 

			m_linesArray = new List<Vector3> ();
			var filter = GetComponent<MeshFilter> (); 
			var mesh = filter.sharedMesh; 
		
			if (!mesh) {
				mesh = filter.mesh;
			}
		
		
			var vertices = mesh.vertices; 
			var triangles = mesh.triangles; 

			for (var i = 0; i < triangles.Length / 3; i++) { 
				m_linesArray.Add (vertices [triangles [i * 3]]); 
				m_linesArray.Add (vertices [triangles [i * 3 + 1]]); 
				m_linesArray.Add (vertices [triangles [i * 3 + 2]]); 
			} 

			m_lines = m_linesArray.ToArray (); 
		} else {
			enabled = false;
		}
	}
	
	
	private void OnDestroy ()
	{
		if (enabled) {
			GetComponent<Renderer>().sharedMaterial = m_originalMaterial;
			ShowAction();
		}
	}
	
	private void OnRenderObject ()
	{    
		if (Camera.current == TargetCamera) {
			m_meshRenderer.sharedMaterial.color = BackgroundColor; 
			m_lineMaterial.SetPass (0); 
	
			//GL.PushMatrix (); 
			GL.MultMatrix (transform.localToWorldMatrix); 
			GL.Begin (GL.LINES); 
			GL.Color (Color.green); 
	
			for (var i = 0; i < m_lines.Length / 3; i++) { 
				GL.Vertex (m_lines [i * 3]); 
				GL.Vertex (m_lines [i * 3 + 1]); 
	
				GL.Vertex (m_lines [i * 3 + 1]); 
				GL.Vertex (m_lines [i * 3 + 2]); 
	
				GL.Vertex (m_lines [i * 3 + 2]); 
				GL.Vertex (m_lines [i * 3]); 
			} 
	
			GL.End (); 
			//GL.PopMatrix (); 
		}
	}
	#endregion
}
