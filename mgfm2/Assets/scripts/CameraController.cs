using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public WebCamTexture cam;
	
	void Start () {
		cam = new WebCamTexture();


	}
}
