using UnityEngine;
using System.Collections;

public class ReplacementShader : MonoBehaviour {
    public Camera _camera;
    public Shader _shader;
    public string replaceTag;

	void Awake () {
        _camera.SetReplacementShader(_shader, replaceTag);
    }
	
	
	void Update () {
	
	}
}
