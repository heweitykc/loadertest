using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class Ball : MonoBehaviour {
    public int num = 100;
    public Mesh mesh;

    Vector3[] newVertices;
    int[] indexes;
    Vector2[] newUV;
    
    void Awake() {
        Debug.Log("aake");
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material.SetFloat("_Num",num);
        Generate(num);        
    }

    void Generate(int num)
    {
        newVertices = new Vector3[num+1];
        newUV = new Vector2[num+1];

        mesh.Clear();
        newVertices[0] = Vector3.zero;
        newVertices[1] = new Vector3(0, 0.5f, 0);
        newUV[0] = new Vector2(0.5f, 0.5f);
        newUV[1] = new Vector2(0.5f,1f);
        indexes = new int[3 * (num+1)];
        float angle = 360f / num;        

        //创建顶点
        for (int i = 2; i <= num; i++) {
            newVertices[i] = Quaternion.Euler(0, 0, angle * (i-1)) * newVertices[1];
            Vector3 v = newVertices[i].normalized * 0.5f;
            newUV[i] = new Vector2(v.x + 0.5f, v.y + 0.5f);

            //Debug.Log(Vector3.Dot(v.normalized, newVertices[1].normalized));
        }

        //创建索引
        for (int i = 0; i <= num; i++)
        {
            if (i == num)
            {
                indexes[i * 3 + 0] = 1;
                indexes[i * 3 + 1] = i;
                indexes[i * 3 + 2] = 0;
            }  else {
                indexes[i * 3 + 0] = i + 1;
                indexes[i * 3 + 1] = i;
                indexes[i * 3 + 2] = 0;
            }
        }

        mesh.vertices = newVertices;
        mesh.triangles = indexes;
        mesh.uv = newUV;
    }
		
	void Update () {

        Debug.Log(Time.deltaTime);

    }
}
