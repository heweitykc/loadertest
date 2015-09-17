using UnityEngine;
using System.Collections;

public class RotateAeound : MonoBehaviour {
    public Transform target;
    public float angle = 2f;
    public GameObject prefab0;

    public Camera global;
    public Camera local;

    void Start () {
        if (global == null) return;
        global.gameObject.SetActive(true);
        local.gameObject.SetActive(false);
    }
	
	
	void Update () {
        transform.RotateAround(target.position,  target.up, angle);
	}

    void OnGUI()
    {
        if (transform.GetComponent<RotateAeound>().global == null) return;
        if (GUI.Button(new Rect(200f, 100f, 200, 55), "增加"))
        {
            var newb = Instantiate(prefab0);
            newb.transform.position = transform.position +
                new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
            newb.GetComponent<RotateAeound>().global = null;
            newb.GetComponent<RotateAeound>().local = null;
        }
        if (GUI.Button(new Rect(200f, 200f, 200, 55), "camera"))
        {
            global.gameObject.SetActive(!global.gameObject.activeSelf);
            local.gameObject.SetActive(!global.gameObject.activeSelf);
        }
    }
}
