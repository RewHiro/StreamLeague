using UnityEngine;
using System.Collections;
using System;

public class SpeedController : SkillBase {

    [SerializeField]
    private float _maxSpeed = 3f;
    public float moveSpeedUp
    {
        get { return _maxSpeed;}
        set { _maxSpeed = value; }
    }

    private Move _move = null;

    private PlayerState _state = null;

    private float NORMAL_SPEED = 15.0f;

    [SerializeField, Tooltip("５の倍数にしてください")]
    private float MAX_UP_SPEED = 30.0f;
    public float maxUpSpeed
    {
        get { return MAX_UP_SPEED; }
        set { MAX_UP_SPEED = value; }
    }

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
        PrefabsLoad();
        _move = GetComponent<Move>();
        _state = GetComponent<PlayerState>();
        NORMAL_SPEED = _move.speed;
    }

    void Update()
    {
        if (_input.isSkill && !_skillActive && playerState.state == PlayerState.State.NORMAL && playerState.state != PlayerState.State.ATTACK)
        {
            StartCoroutine(SpeedControl(_maxSpeed));
            base.SkillStart();
        }
    }

    void FixedUpdate()
    {
        if (!isSkillsIn) return;
        transform.Translate(_input.getLeftStickDirection * _move.speed * Time.deltaTime, Space.World);
    }

    public override void SkillStart()
    {
        base.SkillStart();
        StartCoroutine(SpeedControl(_maxSpeed));
    }

    public override void AnotherSkillStart()
    {
        base.SkillStart();
        StartCoroutine(SpeedControl(_maxSpeed));
    }

    private IEnumerator SpeedControl(float speed)
    {
        AudioManager.instance.PlaySe(SoundName.SeName.speedcontroller);
        _state.state = PlayerState.State.SPEED_SKILL;
        _move.speed += speed;
        var effect = Instantiate(_skilleffect[3], transform.localPosition, transform.localRotation) as GameObject;
        var count = 0.0f;
        var number = 1;
        effect.transform.parent = transform;
        while(_activeTime < SKILL_ACTIVE_TIME)
        {
            count += Time.deltaTime;
            _activeTime += Time.deltaTime;
            if ((int)count == number)
            {
                number++;
                _move.speed += speed;
                if(_move.speed > MAX_UP_SPEED)
                {
                    _move.speed = MAX_UP_SPEED;
                }
            }
            if (_input.getLeftStickDirection.x != 0 || _input.getLeftStickDirection.z != 0)
                transform.eulerAngles = new Vector3(
                    0.0f,
                    -((Mathf.Atan2(_input.getLeftStickDirection.z, _input.getLeftStickDirection.x) - Mathf.PI / 2) * 180 / Mathf.PI),
                    0.0f);

            yield return null;
        }
        if (_state.state == PlayerState.State.SPEED_SKILL)
        {
            _state.state = PlayerState.State.NORMAL;
        }
        _move.speed = NORMAL_SPEED;
        StartCoroutine(SKillCoolTime());
        effect.GetComponent<ParticleSystem>().loop = false;
        //Destroy(effect);
        yield return null;
    }
}
