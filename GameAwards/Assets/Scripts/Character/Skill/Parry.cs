using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// スキルはじく、受け流す
/// </summary>
public class Parry : SkillBase
{
    [SerializeField]
    private float POWER = 20000.0f; //これくらい強くないと反応しなかったｗ
    public float power
    {
        get { return POWER; }
        set { POWER = value; }
    }

    private List<GameObject> _playerList = null;
    private Attack _playerAttack = null;
    public Attack attack
    {
        get { return _playerAttack; }
    }
    private Move _playerMove = null;

    private Vector3 _normal = Vector3.zero;

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

    // 相手のプレイヤーの情報
    PlayerState rivalPlayer = null;

    // パリィの発動範囲
    public float parryRange
    {
        get; set;
    }

    private void Awake()
    {
        _playerAttack = GetComponent<Attack>();
    }

    override public void Start()
    {
        base.Start();
        _playerMove = GetComponent<Move>();
        _playerList = new List<GameObject>();
        gameObject.tag = "Untagged"; //一度tagを外す
        _playerList.AddRange(GameObject.FindGameObjectsWithTag("Player")); //プレイヤーtagのオブジェクトをトルネードの対象にする
        gameObject.tag = "Player"; //自分のtagをプレイヤーに戻す
        _state = GetComponent<PlayerState>();
        PrefabsLoad();

        var players = FindObjectsOfType<PlayerState>();
        foreach (var player in players)
        {
            if(_input.getPlayerType != player.GetComponent<InputBase>().getPlayerType)
            {
                rivalPlayer = player;
            }
        }
    }

    void Update()
    {
        if (!_playerAttack.isInRange && playerState.state != PlayerState.State.NORMAL && playerState.state == PlayerState.State.ATTACK) { return; }
        if (_skillActive) { return; }

        var enemylocalPos = _playerList[0].transform.localPosition;
        var localPosition = transform.localPosition;
        _normal = Vector3.Normalize(enemylocalPos - localPosition);
        //はじく処理
        //if(_normal.x < 0.0f && _input.isRightStickLeftFlick)
        //{
        //    base.SkillStart();
        //    StartCoroutine(SkillParry());
        //}
        //else if(_normal.x > 0.0f && _input.isRightStickRightFlick)
        //{
        //    base.SkillStart();
        //    StartCoroutine(SkillParry());
        //}
        //else if(_normal.z < 0.0f && _input.isRightStickBackFlick)
        //{
        //    base.SkillStart();
        //    StartCoroutine(SkillParry());
        //}
        //else if(_normal.z > 0.0f && _input.isRightStickFrontFlick)
        //{
        //    base.SkillStart();
        //    StartCoroutine(SkillParry());
        //}

        if (_input.isSkill && nowCoolTime >= COOL_TIME)
        {
            // 相手プレイヤーとの距離を測る
            //var length = transform.position - rivalPlayer.transform.position;
            //if (length.magnitude > parryRange)
            //{
            //    nowCoolTime = 0.0f;
            //    StartCoroutine(SKillCoolTime()); 
            //    return;
            //}
            base.SkillStart();
            StartCoroutine(SkillParry());
        }

        //if (_normal.x < 0.0f && _input.isSkill)
        //{
        //    base.SkillStart();
        //    StartCoroutine(SkillParry());
        //}
        //else if (_normal.x > 0.0f && _input.isSkill)
        //{
        //    base.SkillStart();
        //    StartCoroutine(SkillParry());
        //}
        //else if (_normal.z < 0.0f && _input.isSkill)
        //{
        //    base.SkillStart();
        //    StartCoroutine(SkillParry());
        //}
        //else if (_normal.z > 0.0f && _input.isSkill)
        //{
        //    base.SkillStart();
        //    StartCoroutine(SkillParry());
        //}

    }

    //はじく
    private IEnumerator SkillParry()
    {
        AudioManager.instance.PlaySe(SoundName.SeName.parry);
        GetComponent<PlayerState>().state = PlayerState.State.AVOIDANCE;
        var effect = Instantiate(_skilleffect[2], transform.localPosition, transform.localRotation) as GameObject;

        if (rivalPlayer.state != PlayerState.State.AVOIDANCE)
        {
            _playerList[0].GetComponent<Rigidbody>().AddForce(_normal * POWER);
            if (rivalPlayer.state == PlayerState.State.ATTACK)
            {
                rivalPlayer.state = PlayerState.State.NORMAL;
            }
            else if (rivalPlayer.state == PlayerState.State.DEFENSE)
            {
                var energys = rivalPlayer.GetComponent<EnergyConnect>().connectList;
                foreach(var energy in energys)
                {
                    Destroy(energy);
                }
                rivalPlayer.state = PlayerState.State.NORMAL;
            }
            rivalPlayer.GetComponent<Attack>().ResetPlayerState(false, true);

        }

        //yield return new WaitForSeconds(SKILL_ACTIVE_TIME);
        _playerList[0].GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<PlayerState>().state = PlayerState.State.NORMAL;
        StartCoroutine(SKillCoolTime());
        yield return null;
    }
}
