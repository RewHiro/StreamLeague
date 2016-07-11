using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountNumberUI : MonoBehaviour {

    // 画像
    [SerializeField]
    Image _image = null;

    // 数字を出すまでのディレイ
    [SerializeField]
    float _drawDelay = 0.0f;

    // 数字を出す時間
    [SerializeField]
    float _drawMaxTime = 1.0f;
    float _drawTimer = 0.0f;

    // 動く数字をゆっくりにする時間
    [SerializeField]
    float _slowStartTime = 0.4f;
    // スローにする時間
    [SerializeField]
    float _slowTime = 0.4f;

    // 画像の最大サイズ(X, Y両方大きさ同じ)
    [SerializeField]
    float _imageSize = 1.0f;

    // 画像の初期サイズ
    [SerializeField]
    float _initSize = 0.0f;

	// Use this for initialization
	void Start () {
        _drawTimer = _drawMaxTime;
        _image.rectTransform.localScale = Vector3.one * _initSize;
	}

    // Update is called once per frame
    void Update()
    {
        if (_drawDelay > 0.0f)
        {
            _drawDelay -= Time.deltaTime;
        }
        else if (_drawTimer > _drawMaxTime - _slowStartTime)
        {          
            Move(0.5f);
        }
        else if (_drawTimer > _drawMaxTime - _slowStartTime - _slowTime)
        {
            Move(3.0f);
        }
        else
        {
            Move(0.5f);
        }

    }

    void Move(float speedPercentage)
    {
        float time = (_drawTimer / _drawMaxTime);
        float r = time * Mathf.PI;
        const float xLength = 200.0f;
        const float yLength = 50.0f;
        _image.rectTransform.localPosition = new Vector3(Mathf.Cos(r) * xLength, -Mathf.Sin(r) * yLength, 0.0f);

        float size = Mathf.Sin(time * Mathf.PI);
        if(size < 0) { size = 0; }
        _image.rectTransform.localScale = Vector3.one * size;

        _drawTimer -= Time.deltaTime / speedPercentage;
    }
}
