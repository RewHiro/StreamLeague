using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// スキル「グラビティ」
/// 重力を操作して敵を妨害するスキル
/// </summary>
public class Gravity : SkillBase {

    [SerializeField]
    private float POWER_LIMIT = 50.0f;

    [SerializeField, Range(0.1f, 1.0f)]
    private float _powerUpSpeed = 0.1f;

    //エフェクトの範囲
    private float _extent = 20.0f;
    public float extent
    {
        get { return _extent; }
        set { _extent = value; }
    }

    private List<GameObject> _playerList;
    /// <summary>
    /// グラビティを反応させたいplayerリスト
    /// effectにつけるスクリプトで使用予定
    /// </summary>
    public List<GameObject> playerList
    {
        get { return _playerList; }
    }

    public GameObject myGameObject
    {
        get { return gameObject; }
    }

    private bool _moveSpeedUp = false;
    public bool moveSpeedUp
    {
        get {return _moveSpeedUp; }
    }
    
    const float START_RELEASE_TIME = 0.5f;
    const float END_TIME = 0.0f;
    const float START_POWER = 0.0f;

    private float _releaseTime = 0.5f;
    //private float _power = 0.0f;

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

    public override void SkillStart()
    {
        base.SkillStart();
        StartCoroutine(GravityReserve());
    }

    override public void Start()
    {
        base.Start();
        PrefabsLoad();

        _playerList = new List<GameObject>();
        gameObject.tag = "Untagged"; //一度tagを外す
        _playerList.AddRange(GameObject.FindGameObjectsWithTag("Player")); //プレイヤーtagのオブジェクトをグラビティの対象にする
        gameObject.tag = "Player"; //自分のtagをプレイヤーに戻す
    }

    void Update()
    {

        if (_input.isSkill && !_skillActive && playerState.state == PlayerState.State.NORMAL && playerState.state != PlayerState.State.ATTACK)
        {
            SkillStart();
        }

        //押し込んでる間は力溜める、はじかないで戻したら溜めた力がなくなる
        //if (_input.IsPush(GamePadManager.ButtonType.R3) && !_skillActive)
        //{
        //    SkillStart();
        //}

        //if(_power > POWER_LIMIT)
        //{
        //    _power = POWER_LIMIT;
        //}
    }

    public override void SkillCansel()
    {
        base.SkillCansel();
    }

    //力をためる処理
    private IEnumerator GravityReserve()
    {
        //var effect = Instantiate(_skilleffect[1], transform.localPosition, transform.localRotation) as GameObject;
        //effect.GetComponent<GravityEffectTrigger>().getUsePlayer = gameObject;
        //var effectParticle = effect.GetComponent<ParticleSystem>();
        //var effectchild = effect.GetComponentsInChildren<ParticleSystem>();
        var effectTame = Instantiate(_skilleffect[6], transform.localPosition, transform.localRotation) as GameObject;
        effectTame.transform.parent = transform;
        //effectParticle.startSize = _power;
        //effectParticle.startColor = new Color(1, 1, 1, 0.2f);
        //foreach (var effectchilds in effectchild)
        //{
        //    effectchilds.startColor = new Color(1, 1, 1, 0.2f);
        //}
        var time = 0.0f;
        while (time < 1.0f)
        {
            time += Time.deltaTime;
            var localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2.0f, transform.localPosition.z);
            //effect.transform.localPosition = localPosition;
            //_power += _powerUpSpeed;
            //effectParticle.startSize = _power;
            //foreach (var effectchilds in effectchild)
            //{
            //    effectchilds.startSize = _power;
            //}

            //if (_input.isRightStickFrontFlick)
            //{
            //    StartCoroutine(GravityDown());
            //    Destroy(effect);
            //    Destroy(effectTame);
            //    yield break;
            //}
            //else if (_input.isRightStickBackFlick)
            //{
            //    StartCoroutine(GravityUp());
            //    Destroy(effect);
            //    Destroy(effectTame);
            //    yield break;
            //}
            //else if (_input.isReleaseR3)
            //{
            //    StartCoroutine(SkillStandby());
            //    SkillCansel();
            //    Destroy(effect);
            //    Destroy(effectTame);
            //    yield break;
            //}
            //yield return null;
        }
        StartCoroutine(GravityDown());
        effectTame.GetComponent<ParticleSystem>().loop = false;
        foreach(var particleChildren in effectTame.GetComponentsInChildren<ParticleSystem>())
        {
            particleChildren.Stop();
        }
        //Destroy(effect);
        //Destroy(effectTame);
        yield break;
    }

    //private IEnumerator SkillStandby()
    //{
    //    while(_releaseTime < END_TIME)
    //    {
    //        _releaseTime -= Time.deltaTime;
    //    if (_input.isRightStickFrontFlick)
    //        {
    //            StartCoroutine(GravityDown());
    //            break;
    //        }
    //        else if (_input.isRightStickBackFlick)
    //        {
    //            StartCoroutine(GravityUp());
    //            break;
    //        }
    //        yield return null;
    //    }

    //    _releaseTime = START_RELEASE_TIME;
    //    //_power = START_POWER;
    //    yield return null;
    //}

    /// <summary>
    /// グラビティを強くする
    /// </summary>
    /// <returns></returns>
    //private IEnumerator GravityUp()
    //{
    //    _moveSpeedUp = false;
    //    var localPosition = transform.localPosition;
    //    var effect = Instantiate(_skilleffect[1], localPosition, transform.localRotation) as GameObject;
    //    effect.GetComponent<GravityEffectTrigger>().getUsePlayer = gameObject;
    //    var effectParticle = effect.GetComponent<ParticleSystem>();
    //    //effectParticle.startSize = _power;
    //    //foreach(var effectchilds in effect.GetComponentsInChildren<ParticleSystem>())
    //    //{
    //    //    effectchilds.startSize = _power;
    //    //}
    //    //effect.GetComponent<CapsuleCollider>().radius = effectParticle.startSize * 0.2f;
    //    while (_activeTime < SKILL_ACTIVE_TIME)
    //    {
    //        _activeTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    Destroy(effect);
    //    foreach(var player in _playerList)
    //    {
    //        player.GetComponent<Move>().speed = 15;
    //    }
    //    //_power = START_POWER;
    //    _releaseTime = START_RELEASE_TIME;
    //    yield return null;
    //    StartCoroutine(SKillCoolTime());
    //    yield return null;
    //}

    /// <summary>
    /// グラビティを弱くする
    /// </summary>
    /// <returns></returns>
    private IEnumerator GravityDown()
    {
        AudioManager.instance.PlaySe(SoundName.SeName.graity);
        _moveSpeedUp = false;
        var localPosition = transform.localPosition;
        var effectPos = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.5f, transform.localPosition.z);
        var effect = Instantiate(_skilleffect[1], effectPos, transform.localRotation) as GameObject;
        effect.tag = "Effect";
        effect.GetComponent<GravityEffectTrigger>().getUsePlayer = gameObject;
        var effectParticle = effect.GetComponent<ParticleSystem>();
        effectParticle.startSize = _extent;
        foreach (var effectchilds in effect.GetComponentsInChildren<ParticleSystem>())
        {
            effectchilds.startSize = _extent;
        }
        effect.GetComponent<CapsuleCollider>().radius = _extent * 0.3f;
        while (_activeTime < SKILL_ACTIVE_TIME)
        {
            _activeTime += Time.deltaTime;
            yield return null;
        }
        effect.GetComponent<ParticleSystem>().loop = false;
        foreach (var effectchilds in effect.GetComponentsInChildren<ParticleSystem>())
        {
            effectchilds.Stop();
        }
        //Destroy(effect);
        effect.GetComponent<CapsuleCollider>().enabled = false;
        effect.GetComponent<GravityEffectTrigger>().enabled = false;
        yield return null;
        foreach (var player in _playerList)
        {
            player.GetComponent<Move>().speed = 15;
        }
        //_power = START_POWER;
        _releaseTime = START_RELEASE_TIME;
        yield return null;
        StartCoroutine(SKillCoolTime());
        yield return null;
    }
}
