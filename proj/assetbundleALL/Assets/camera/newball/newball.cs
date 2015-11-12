using UnityEngine;
using System.Collections;

/*
    参考 http://matrix3d.github.io/assets/native3d/curve3d/
    http://http.developer.nvidia.com/GPUGems3/gpugems3_ch25.html
*/


public class newball : MonoBehaviour
{
    public Mesh mesh;
    Vector3[] newVertices;
    int[] indexes;
    Vector2[] newUV;

    public Vector3 v1 = new Vector3(1, 0, 0);

    void Start ()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Generate();
    }

    void Generate()
    {
        newVertices = new Vector3[3];
        newUV = new Vector2[3];

        newVertices[0] = new Vector3(0, 0, 0);
        newVertices[1] = v1;
        newVertices[2] = new Vector3(1,1,0);

        newUV[0] = new Vector2(0,0);
        newUV[1] = new Vector2(1,0);
        newUV[2] = new Vector2(1,1);

        indexes = new int[]{0,1,2 };

        mesh.vertices = newVertices;
        mesh.triangles = indexes;
        mesh.uv = newUV;
    }

    void Update ()
    {
        v1 = new Vector3(1, 0, 0) + new Vector3(1,1,0) * (Mathf.Sin(Time.realtimeSinceStartup)+1) * 0.5f;
        newVertices[1] = v1;
        mesh.vertices = newVertices;
        //GetComponent<MeshRenderer>().material.SetFloat("_AnimPos", Mathf.Sin(Time.realtimeSinceStartup)+1);
    }
}
