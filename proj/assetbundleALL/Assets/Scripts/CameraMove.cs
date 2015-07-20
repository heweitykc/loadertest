using UnityEngine;
using System.Collections;

//移动摄像机
public class CameraMove : MonoBehaviour {
    public Camera camera;

	void Start () {
	
	}
		
	void Update () {
        Touch[] touches = Input.touches;
        foreach (Touch touch in touches) {
            if (touch.phase == TouchPhase.Moved) {
                Vector2 v = touch.deltaPosition * Time.deltaTime * 4f;
                camera.transform.Translate(0,0,-v.y,Space.World);
            }
        }
	}
}
