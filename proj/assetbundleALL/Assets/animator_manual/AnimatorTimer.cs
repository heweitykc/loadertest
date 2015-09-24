using UnityEngine;
using System.Collections;

public class AnimatorTimer : MonoBehaviour {

    int currentName;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        GetComponent<Animator>().speed = 0f;
        currentName = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash;
    }
	
	// Update is called once per frame
	void Update () {
        //GetComponent<Animator>().Update(value * Time.unscaledDeltaTime);
	}

    float value = 0;
    void OnGUI()
    {
        value = GUI.HorizontalSlider(new Rect(100, 100, 200, 10), value, 0, 1);
        GetComponent<Animator>().CrossFade(currentName, 0, 0, value);
    }
}
