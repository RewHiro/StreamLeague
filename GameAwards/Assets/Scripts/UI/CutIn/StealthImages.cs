using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StealthImages : MonoBehaviour {

    [SerializeField]
    Image[] _images = null;

    [SerializeField, Range(0.0f, 1.0f)]
    float _alpha = 0.5f;

	// Use this for initialization
	void Start () {
	    foreach(var image in _images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, _alpha);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
