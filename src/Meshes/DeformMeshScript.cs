#region Usings
using UnityEngine;
#endregion

[AddComponentMenu("Skahal/Meshes/SHDeformMeshScript")]
[RequireComponent(typeof(MeshCollider))]
public class DeformMeshScript : MonoBehaviour
{
    #region Campos
    private Mesh m_mesh;
    private Vector3[] m_permaVerts;
    private bool m_sleep = true;
    #endregion

    #region Propriedades
    public float MinForce = 1.0f;
    public float Multiplier = 0.1f;
    public float DeformRadius = 1.0f;
    public float MaxDeform = 0.0f;
    public float BounceBackSpeed = 0.0f;
    public float BounceBackSleepCap = 0.001f;
    public bool OnCollision = true;
    public bool OnCall = true;
    public bool UpdateCollider = false;
    public bool UpdateColliderOnBounce = false;
    #endregion

    #region M�todos
    private void Start()
    {
        MeshFilter filter = (MeshFilter) GetComponent("MeshFilter");
        m_mesh = filter.mesh;

        if (GetComponent("MeshCollider") == null)
        {
            UpdateCollider = false;
            UpdateColliderOnBounce = false;
        }

        m_permaVerts = m_mesh.vertices;
    }
    #endregion


    private void OnCollisionEnter (Collision collision)
    {
	    if (OnCollision && collision.relativeVelocity.magnitude >= MinForce) 
        {
		    m_sleep = false;
		    Vector3[] vertices = m_mesh.vertices;
		    Matrix4x4 tf = transform.worldToLocalMatrix;
		    for (int i=0;i<vertices.Length;i++) 
            {
			    foreach (ContactPoint contact in collision.contacts) 
                {
				    Vector3 point = tf.MultiplyPoint(contact.point);
				    Vector3 vec = tf.MultiplyVector(collision.relativeVelocity * UsedMass(collision));
    				
                    if ((point-vertices[i]).magnitude < DeformRadius) 
                    {
					    vertices[i] += vec*(DeformRadius-(point-vertices[i]).magnitude)/DeformRadius*Multiplier;
					    if (MaxDeform > 0 && (vertices[i]-m_permaVerts[i]).magnitude > MaxDeform)
                        {
						    vertices[i] = m_permaVerts[i] + (vertices[i]-m_permaVerts[i]).normalized*MaxDeform;
					    }
				    }
			    }
    			
		    }
		    m_mesh.vertices = vertices;
		    m_mesh.RecalculateNormals();
		    m_mesh.RecalculateBounds();

  		    if (UpdateCollider) 
            {
                ((MeshCollider)GetComponent("MeshCollider")).sharedMesh = m_mesh;
            }
	    }
    }

    public void Deform(Vector3 point, Vector3 direction) 
    {
	    if (OnCall && direction.magnitude >= MinForce) 
        {
		    m_sleep = false;
		    Vector3[] vertices = m_mesh.vertices;
		    Matrix4x4 tf = transform.worldToLocalMatrix;
		    point = tf.MultiplyPoint(point);
		    Vector3 vec = tf.MultiplyVector(direction);

		    for (int i=0;i<vertices.Length;i++) 
            {
			    if ((point-vertices[i]).magnitude <= DeformRadius) 
                {
				    vertices[i] += vec*(DeformRadius-(point-vertices[i]).magnitude)/DeformRadius*Multiplier;
    				
                    if (MaxDeform > 0 && (vertices[i]-m_permaVerts[i]).magnitude > MaxDeform) 
                    {
					    vertices[i] = m_permaVerts[i] + (vertices[i]-m_permaVerts[i]).normalized*MaxDeform;
				    }
			    }
		    }
		    m_mesh.vertices = vertices;
		    m_mesh.RecalculateNormals();
		    m_mesh.RecalculateBounds();

  		    if (UpdateCollider) 
            {
                ((MeshCollider)GetComponent("MeshCollider")).sharedMesh = m_mesh;
            }
	    }
    }

    private void Update () 
    {
	    if (!m_sleep && BounceBackSpeed > 0) 
        {
		    m_sleep = true;
		    Vector3[] vertices = m_mesh.vertices;
		    for (int i=0;i<vertices.Length;i++) {
			    vertices[i] += (m_permaVerts[i] - vertices[i])*(Time.deltaTime*BounceBackSpeed);
			    if ((m_permaVerts[i]-vertices[i]).magnitude >= BounceBackSleepCap) {m_sleep = false;}
		    }
		    m_mesh.vertices = vertices;
		    m_mesh.RecalculateNormals();
		    m_mesh.RecalculateBounds();
  		    
            if (UpdateCollider) 
            {
                ((MeshCollider)GetComponent("MeshCollider")).sharedMesh = m_mesh;
            }
	    }
    }

    private float UsedMass(Collision collision)
    {
	    if (collision.rigidbody) 
        {
		    if (GetComponent<Rigidbody>()) 
            {
			    if (collision.rigidbody.mass > GetComponent<Rigidbody>().mass) 
                {
				    return (collision.rigidbody.mass);
			    }
			    else 
                {
				    return (GetComponent<Rigidbody>().mass);
			    }
		    }
		    else 
            {
			    return (collision.rigidbody.mass);
		    }
	    }
	    else if (GetComponent<Rigidbody>()) 
        {
		    return (GetComponent<Rigidbody>().mass);
	    }
	    else 
        {
            return 1;
        }
    }
}