using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialBackImage : MonoBehaviour {

    [SerializeField]
    Image _backImage;

    [SerializeField]
    float _fadeSpeed = 0.3f;

    float _sinCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _sinCount += _fadeSpeed;
        _backImage.color = new Color(1,1,1,Mathf.Abs(Mathf.Sin(_sinCount) * 0.5f) + 0.5f);
	}
}
