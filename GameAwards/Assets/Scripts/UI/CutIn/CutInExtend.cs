using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// カットインの画像を縦にのばす処理
/// </summary>
public class CutInExtend : MonoBehaviour {

    // のばす画像たち
    [SerializeField]
    Image[] _images;

    // ディレイ(デバック : Unity側で再生するときに一瞬処理落ちして処理が確認できないため)
    float _count = 0.7f;

	// Use this for initialization
	void Start () {
	    foreach(var image in _images)
        {
            image.transform.localScale = (Vector3.right + Vector3.forward);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(_count >= 0)
        {
            _count -= Time.deltaTime;
            return;
        }

        // 画像の大きさを変える値の大きさを
        float value = (1 - _images[0].transform.localScale.y) / 3.0f;

        foreach (var image in _images)
        {
            image.transform.localScale += Vector3.up * value;
        }
    }
}
