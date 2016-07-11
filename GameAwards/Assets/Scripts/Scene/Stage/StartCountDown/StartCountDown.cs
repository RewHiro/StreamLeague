using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartCountDown : MonoBehaviour {

    // プレイヤーたちの情報
    PlayerState[] _players = null;

    // 表示するテキスト
    [SerializeField]
    Image[] _images = null;

    // カウントダウンが始まるまでの猶予時間(秒)
    [SerializeField]
    float _startCountDelay = 1.0f;
    public float startCountDelay { get { return _startCountDelay; } }

    // 猶予時間が過ぎてから何秒で始めるかのカウントダウン(秒)
    [SerializeField]
    float _countDown = 3.0f;
    public float countDown { get { return _countDown; } }

    // カウントダウンが終わってスタートを表示し続ける時間(秒)
    [SerializeField]
    float _startDrawTime = 1.0f;

    // プレイヤーを操作できるようにしたりできないようにしたりするのに使う
    // true : 操作できる
    // false : 操作できない
    // (これを切り替えるだけだと動かないです、PlayerComponentEnabled() と一緒に使います)
    bool _playerEnable = false;

	// Use this for initialization
	void Start () {
        // プレイヤーたちの情報を集める
        _players = FindObjectsOfType<PlayerState>();

        // プレイヤーの操作をできないようにする
        PlayerComponentEnabled(_playerEnable);

        // まだテキストの背景はいらないので消しておく
        //_textBackImage.enabled = false;

        FindObjectOfType<EnergyCreater>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 猶予時間があるなら
        if (_startCountDelay > 0.0f)
        {
            // 猶予時間を減らす
            _startCountDelay -= Time.deltaTime;

            if(_startCountDelay <= 0)
            {
                AudioManager.instance.PlaySe(SoundName.SeName.countdown);
            }
        }
        // カウントダウンをする
        else if (_countDown > 0.0f)
        {
            // カウントダウンを減らす
            _countDown -= Time.deltaTime;
            foreach(var image in _images)
            {
                image.enabled = false;
            }
            if (_countDown > 2.0f)
            {
                _images[0].enabled = true;
            }
            else if (_countDown > 1.0f)
            {
                _images[1].enabled = true;
            }
            else if (_countDown > 0.0f)
            {
                _images[2].enabled = true;
            }

        }
        // スタートを描画する
        else if (_startDrawTime > 0.0f)
        {
            PlayerComponentEnabled(true);

            // 描画時間を減らす
            _startDrawTime -= Time.deltaTime;

            foreach (var image in _images)
            {
                image.enabled = false;
            }
            _images[_images.Length - 1].enabled = true;
        }
        // スタートの描画時間を過ぎたら
        else
        {            
            // カウントダウンが終わって必要なくなったので消す
            Destroy(gameObject);
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

            // プレイヤーの回避
            if (player.GetComponent<Avoidance>() != null)
            {
                player.GetComponent<Avoidance>().enabled = enable;
            }

            // プレイヤーのスキル
            if (player.GetComponent<SkillBase>() != null)
            {
                player.GetComponent<SkillBase>().enabled = enable;
            }

            if (player.GetComponent<CharacterAnimator>() != null)
            {
                player.GetComponent<CharacterAnimator>().enabled = enable;
            }
        }
    }
}
