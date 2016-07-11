using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountStartUI : MonoBehaviour {

    // 画像
    [SerializeField]
    Image _image = null;

    // 数字を出すまでのディレイ
    [SerializeField]
    float _drawDelay = 4.0f;

    // 画像を表示する時間
    [SerializeField]
    float _drawMaxTime = 1.0f;
    float _drawTime = 0.0f;

    // 最初に画像早く大きくする時間
    [SerializeField]
    float _startFastTime = 0.7f;

    // 画像をフェードさせる大きさ
    [SerializeField]
    float _fadeSize = 1.5f;

    // 画像の初期サイズ
    [SerializeField]
    float _initSize = 0.0f;

    // 画像の最大サイズ
    [SerializeField]
    float _maxSize = 1.0f;

	// Use this for initialization
	void Start () {
        _drawTime = _drawMaxTime;
        _image.rectTransform.localScale = Vector3.one * _initSize;
    }
	
	// Update is called once per frame
	void Update () {
	    if(_drawDelay > 0.0f)
        {
            _drawDelay -= Time.deltaTime;
        }
        else if (_drawTime > _drawMaxTime - _startFastTime)
        {
            Draw(0.1f);
        }
        else
        {
            Draw(10.0f);
        }

        if (_image.rectTransform.localScale.x > _fadeSize)
        {
            _image.color -= Color.black * 0.1f;
        }
    }

    void Draw(float speedPercentage)
    {
        float time = (_drawTime / _drawMaxTime);
        float size = _drawMaxTime - time;

        _image.rectTransform.localScale = Vector3.one * size;

        _drawTime -= Time.deltaTime / speedPercentage;
    }
}
