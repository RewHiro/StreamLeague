using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GaugeLine : MonoBehaviour {

    // 始点・終点となるエネルギー画像
    [SerializeField]
    Image _energyImageStart = null;
    [SerializeField]
    Image _energyImageEnd = null;

    // ラインの画像
    [SerializeField]
    Image _lineImage = null;

    // Use this for initialization
    void Start () {
        //// ラインの位置を決める
        //// 座標や大きさを取るまでの階層が長いので変数に
        //var startPos = _energyImageStart.rectTransform.localPosition;
        //var startSize = _energyImageStart.rectTransform.localScale;
        //var endPos = _energyImageEnd.transform.localPosition;
        //var endSize = _energyImageEnd.rectTransform.localScale;

        //// 半分の距離を求める
        //var lengthHalf = (endPos - startPos) / 2;
        //// 角度を求めるd
        //var angle = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x);
        //// 平均の大きさを求める
        //var size = (startSize + endSize) / 2.0f;

        //// 上記で求めた値を入れていく
        //_lineImage.rectTransform.localPosition = startPos + lengthHalf;
        //_lineImage.rectTransform.eulerAngles = Vector3.forward * angle * Mathf.Rad2Deg;
        //_lineImage.rectTransform.localScale = size;
    }
	
	// Update is called once per frame
	void Update () {
        // ImageStart と ImageEnd が同じ色なら
        if(_energyImageStart.color == _energyImageEnd.color &&
            _energyImageStart.color != Color.white &&
            _energyImageEnd.color != Color.white)
        {
            // 同じ色にする
            _lineImage.color = _energyImageStart.color;

            // 画像を出す
            _lineImage.enabled = true;
        }
        else
        {
            // それ以外なら白(初期値)にする
            _lineImage.color = Color.white;

            // 画像を消す
            _lineImage.enabled = false;
        }
    }
}
