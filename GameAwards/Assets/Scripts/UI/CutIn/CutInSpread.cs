using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 背景を広げる処理
/// </summary>
public class CutInSpread : MonoBehaviour {

    // 広げる背景
    [SerializeField]
    Image _image;

    // ズーム
    enum Zoom {
        UP,     // 上げる
        DOWN    // 下げる
    }
    Zoom _zoom = Zoom.DOWN;

	// Use this for initialization
	void Start () {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        switch (_zoom)
        {
            case Zoom.UP:
                float offset1 = 1.1f;
                var value1 = (new Vector3(offset1, offset1, offset1) - _image.transform.localScale) / 100.0f;
                _image.transform.localScale += value1;

                float alpha1 = (1.0f - _image.color.a) / 10.0f;
                _image.color += new Color(0.0f, 0.0f, 0.0f, alpha1);

                break;
            case Zoom.DOWN:
                float offset2 = 0.9f;
                var value2 = (new Vector3(offset2, offset2, offset2) - _image.transform.localScale) / 3.0f;

                _image.transform.localScale += value2;

                float alpha2 = (1.0f - _image.color.a) / 3.0f;
                _image.color += new Color(0.0f, 0.0f, 0.0f, alpha2);

                if (_image.transform.localScale.x < offset2 + 0.1f)
                {
                    _zoom = Zoom.UP;
                }
                break;
        }
	}
}
