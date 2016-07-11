using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CutInManager : MonoBehaviour {

    // このオブジェクトが動きだすまでのディレイ(秒)
    [SerializeField]
    float _startDelay = 1.0f;
    public float startDelay
    {
        get { return _startDelay; }
    }

    // このオブジェクトを消す時間(秒)
    [SerializeField]
    float _destroyTime = 3.0f;
    public float destroyTime
    {
        get { return _destroyTime; }
    }

    // 消すときにカウントダウンprefabを生成する
    [SerializeField]
    GameObject _countDownObj = null;

    // プレイヤーたちの情報
    PlayerState[] _players = null;

    // カットインの画像たち
    [SerializeField]
    Image[] _images = null;

    // フェイドアウトのbool
    bool _fadeFlag = false;

    // フェイドの時間
    float _fateTime = 1.0f;

    // Use this for initialization
    void Start()
    {
        // プレイヤーたちの情報を集める
        _players = FindObjectsOfType<PlayerState>();

        // プレイヤーの操作をできないようにする
        PlayerComponentEnabled(false);
    }
	
	// Update is called once per frame
	void Update () {
        // ディレイ待機の時間なら
        if(_startDelay >= 0)
        {
            // ディレイ時間を減らす
            _startDelay -= Time.deltaTime;

            if(_startDelay <= 0)
            {
                AudioManager.instance.PlaySe(SoundName.SeName.icon);
            }
        }
        // 消す時間が０秒以下なら
	    else if(_destroyTime <= 0 && !_fadeFlag)
        {
            // フェイドを開始する
            _fadeFlag = true;
        }
        else if (_fadeFlag)
        {
            // 画像を透明にしていく
            foreach(var image in _images)
            {
                image.color = new Color(1.0f, 1.0f, 1.0f,_fateTime);
            }
            _fateTime -= Time.deltaTime;

            if(_fateTime <= 0)
            {
                // このオブジェクトを消す
                Destroy(gameObject);

                // カウントダウンを生成する
                var countDownObj = Instantiate(_countDownObj);
                countDownObj.transform.parent = transform.parent; // 親をこのオブジェクトと同じにする
                countDownObj.transform.localPosition = Vector3.zero;
                countDownObj.transform.localScale = Vector3.one;
            }
        }
        // 消す時間が０秒より上なら
        else
        {
            // 消す時間を減らす
            _destroyTime -= Time.deltaTime;
        }
    }

    // プレイヤーの操作のスクリプトのオンオフを切り替える
    void PlayerComponentEnabled(bool enable)
    {
        foreach (var player in _players)
        {
            // プレイヤーの移動
            if (player.GetComponent<Move>() != null)
            {
                player.GetComponent<Move>().enabled = enable;
            }

            // プレイヤーの攻撃
            if (player.GetComponent<Attack>() != null)
            {
                player.GetComponent<Attack>().enabled = enable;
            }

            // プレイヤーのスキル
            if (player.GetComponent<SkillBase>() != null)
            {
                player.GetComponent<SkillBase>().enabled = enable;
            }
        }
    }
}
