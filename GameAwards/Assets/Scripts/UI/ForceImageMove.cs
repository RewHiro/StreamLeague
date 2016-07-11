using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForceImageMove : MonoBehaviour {

    // 龍脈の画像
    [SerializeField]
    Image _moveImage;

    // エネルギーの画像たち
    [SerializeField]
    Image[] _energys;

    // どのエネルギー向かわせるかのイテレータ
    int _moveIter = 0;

    // プレイヤーの色
    Color _color = Color.white;

    // 何Pか
    [SerializeField]
    int playerNum = 0;

	// Use this for initialization
	void Start () {
        var players = FindObjectsOfType<InputBase>();
        foreach(var player in players)
        {
            if(player.getPlayerType == (GamePadManager.Type)playerNum)
            {
                _color = player.GetComponent<CharacterParameter>().getParameter.color;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        // エネルギーの２番目が白なら流れれないので龍脈を見えなくする
        if (_energys[0].color == _color && _energys[1].color == _color)
        {
            _moveImage.enabled = true;
        }
        else
        {
            _moveImage.enabled = false;
        }

        // イテレーターがエネルギーの最後のインデックスと一致したらリセットする
        if (_moveIter == _energys.Length - 1)
        {
            Reset();
        }

        // 次のエネルギーとプレイヤーの色が同じだったら
        if (_energys[_moveIter + 1].color == _color)
        {
            // エネルギーの距離をとって動く大きさを計算する
            var energylength = _energys[_moveIter + 1].rectTransform.localPosition - _energys[_moveIter].rectTransform.localPosition;
            var moveValue = energylength / 10.0f;

            // 次のエネルギーに向きを回転させる
            var angle = Mathf.Atan2(energylength.y, energylength.x) * 180 / Mathf.PI;
            _moveImage.rectTransform.eulerAngles = new Vector3(0, 0, angle);

            // 龍脈画像を動かす
            _moveImage.rectTransform.localPosition += moveValue;

            // 次のエネルギーに近づいたらイテレーターを増やして次のエネルギーの位置に龍脈画像を動かす
            var imagelength = _energys[_moveIter + 1].rectTransform.localPosition - _moveImage.rectTransform.localPosition;
            if (imagelength.magnitude < 20.0f)
            {
                _moveImage.rectTransform.localPosition = _energys[_moveIter + 1].rectTransform.localPosition;
                ++_moveIter;
            }
        }
        else
        {
            Reset();
        }
	}

    // 初期値に戻す
    void Reset()
    {
        _moveIter = 0;
        _moveImage.rectTransform.localPosition = _energys[0].rectTransform.localPosition;
    }
}
