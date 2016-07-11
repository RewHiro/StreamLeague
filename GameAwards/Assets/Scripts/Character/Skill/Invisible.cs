using UnityEngine;
using System.Collections;

/// <summary>
/// スキル「インビジブル」
/// 回避用で一定時間消える
/// 
/// モデルに入れたらあたり判定を外す処理を忘れないように入れること
/// </summary>
public class Invisible : SkillBase
{
    
    [SerializeField, Tooltip("消える時間")]
    float TRANSMISSION = 0.008f;
    public float Transmission
    {
        get { return TRANSMISSION; }
        set { TRANSMISSION = value; }
    }
    [SerializeField, Tooltip("加速スピード")]
    float ACCEL_COUNT = 0.01f;
    public float AccelCount
    {
        get { return ACCEL_COUNT; }
        set { ACCEL_COUNT = value; }
    }
    [SerializeField, Tooltip("移動幅")]
    float MOVE_VALUE = 0.02f;
    public float MoveValue
    {
        get { return MOVE_VALUE; }
        set { MOVE_VALUE = value; }
    }

    enum Type
    {
        Start = 1,
        End = -1
    }

    const float LOOP_COUNT = 1.0f;
    const float CLEAR_COLOR = 0.0f;

    private SkinnedMeshRenderer _renderer = null;

    private float _movement = 0.0f; //横幅の移動量
    private float _alpha = 1.0f;
    private int _sinCount = 0; //Sinのカウント
    private float _accel = 0.0f; //加速度
    private float _widthMovement = 0.0f;
    private GameObject _playerNum = null;
    private PlayerState _state = null;

    [SerializeField]
    PlayerState _playerState = null;    // プレイヤーの状態
    public PlayerState playerState
    {
        get
        {
            if (_playerState == null)
            {
                _playerState = GetComponent<PlayerState>();
            }
            return _playerState;
        }
    }

    override public void Start()
    {
        base.Start();
        _activeTime = SKILL_ACTIVE_TIME;
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _state = GetComponent<PlayerState>();
        var playerNumberText = GetComponentInChildren<PlayerNumberText>();
        if (playerNumberText != null)
        {
            _playerNum = GetComponentInChildren<PlayerNumberText>().gameObject;
        }
        PrefabsLoad();
    }

    void Update()
    {
        //if (_input.isCommandInvisible && !_skillActive)
        //{
        //    SkillStart();
        //}

        if(_input.isSkill && !_skillActive && playerState.state == PlayerState.State.NORMAL && playerState.state != PlayerState.State.ATTACK)
        {
            SkillStart();
        }

    }

    public override void SkillStart()
    {
        base.SkillStart();
        StartCoroutine(InvisibleSkill());
    }

    /// <summary>
    /// スキルが始まったら透けていく,かつ
    /// 消えていくときに少しずつ左右に振れていく
    /// </summary>
    /// <returns></returns>
    private IEnumerator InvisibleSkill()
    {
        AudioManager.instance.PlaySe(SoundName.SeName.invisible);
        _state.state = PlayerState.State.AVOIDANCE;
        //var effect = Instantiate(_skilleffect[0], transform.localPosition, transform.localRotation) as GameObject;
        GetComponent<TrailRenderer>().enabled = false;
        transform.FindChild("AuraManager").gameObject.SetActive(false);
        if (_playerNum != null)
        {
            _playerNum.SetActive(false);
        }

        while (_accel < LOOP_COUNT)
        {
            TransformLoop(Type.Start);
            FlushLoop(TRANSMISSION);
            yield return null;
        }
        AlphaClear(CLEAR_COLOR);
        yield return new WaitForSeconds(SKILL_ACTIVE_TIME);
        while (_accel > 0.0f)
        {
            TransformLoop(Type.End);
            FlushLoop(-TRANSMISSION);
            //yield return null;
        }
        AlphaClear(1.0f);
        if (_playerNum != null)
        {
            _playerNum.SetActive(true);
        }
        GetComponent<TrailRenderer>().enabled = true;
        _state.state = PlayerState.State.NORMAL;
        transform.FindChild("AuraManager").gameObject.SetActive(true);
        //Destroy(effect);
        StartCoroutine(SKillCoolTime());
        yield return null;
    }

    /// <summary>
    /// 左右に揺れる
    /// </summary>
    /// <param name="type">プラスかマイナスか</param>
    private void TransformLoop(Type type)
    {
        _sinCount++;
        _accel += ACCEL_COUNT * (int)type;
        var localPosition = transform.localPosition;
        localPosition.x += (_movement * Mathf.Sin(_sinCount)) * (int)type;
        transform.localPosition = localPosition;
        _movement += (MOVE_VALUE * _accel) * (int)type;
    }

    /// <summary>
    /// α値をきりの良い数値にする
    /// </summary>
    /// <param name="clear"></param>
    private void AlphaClear(float clear)
    {
        _alpha = clear;
        _renderer.material.color = new Color(1, 1, 1, _alpha);
    }

    /// <summary>
    /// α値を少しずつ上限する
    /// </summary>
    /// <param name="transmission"></param>
    private void FlushLoop(float transmission)
    {
        _renderer.material.color = new Color(1, 1, 1, _alpha);
        _alpha -= transmission;
    }
}
