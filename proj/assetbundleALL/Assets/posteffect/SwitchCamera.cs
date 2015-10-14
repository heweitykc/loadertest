using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {

    public GameObject camera0;
    public GameObject camera1;

    void Start () {
        switchCamera();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "切换摄像机")) {
            switchCamera();
        }
    }

    void switchCamera()
    {
        camera0.SetActive(camera1.activeSelf);
        camera1.SetActive(!camera1.activeSelf);
    }
}
