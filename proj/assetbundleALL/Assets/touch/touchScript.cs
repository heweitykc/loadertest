using UnityEngine;
using System.Collections;

/*
    鼠标在任意角度控制3d对象的旋转
*/
public class touchScript : MonoBehaviour {
    public float speed = 1f;

    bool inMouse = false;
	
	void Start () {
	
	}
	
	
	void Update () {
	
	}

    Vector3 startPos;
    void OnMouseDown() {
        Debug.Log("OnMouseDown");
        startPos = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        if (!inMouse) return;
        Debug.Log("OnMouseDrag");
        Vector3 deltav = Input.mousePosition - startPos;        
        deltav = new Vector3(deltav.y,-deltav.x,0);
        deltav = Camera.main.transform.TransformVector(deltav);
        transform.Rotate(deltav * speed, Space.World);
        startPos = Input.mousePosition;
        Debug.Log(deltav);
    }

    void OnMouseEnter()
    {
        inMouse = true;
    }

    void OnMouseExit()
    {
        inMouse = false;
    }
}
