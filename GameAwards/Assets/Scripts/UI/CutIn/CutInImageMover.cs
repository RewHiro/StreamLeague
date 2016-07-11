using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CutInImageMover : MonoBehaviour {

    // 動かす画像(RectTransform)
    [SerializeField]
    RectTransform _imageRectTransform;

    // 動かしたい向き
    [SerializeField]
    Vector3 _vector = Vector3.zero;

    // 動かす速さ
    [SerializeField]
    float _speed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _imageRectTransform.localPosition += _vector.normalized * _speed;
	}
}
