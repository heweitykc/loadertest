using UnityEngine;
using System.Collections;

public class ugui : MonoBehaviour {
    public GameObject UI3D;
    public GameObject UI2D;

    void Start () {
	
	}

    bool ishow;	
	void OnGUI () {
        ishow = GUI.Toggle(new Rect(20, 20, 100, 30), ishow, ishow?"显示界面":"隐藏界面");
        UI3D.SetActive(ishow);
        UI2D.SetActive(ishow);
    }
}
