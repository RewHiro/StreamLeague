using UnityEngine;
using System.Collections;
using System;

public class SuperSpeed : SkillBase {

    private PlayerState _state;
    private Vector3 _direction = Vector3.zero;

    [SerializeField, Range(40f, 80f)] 
    private float _speed = 40f;
    public float skillSpeed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private float _count = 0;

    private Vector3 _moveLength = Vector3.zero;
    public float moveSpeed
    {
        get; set;
    }
    public float moveLength
    {
        get; set;
    }

    [SerializeField]
    private int MOVE_COUNT = 3;
    public int moveCount
    {
        get { return MOVE_COUNT; }
        set { MOVE_COUNT = value; }
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
        _state = GetComponent<PlayerState>();
    }

    void Update()
    {
        //if(_input.isRightStickFlick && !_skillActive)
        //{
        //    SkillStart();
        //}

        if (_input.isSkill && !_skillActive && playerState.state == PlayerState.State.NORMAL && playerState.state != PlayerState.State.ATTACK)
        {
            SkillStart();
        }

    }

    public override void SkillStart()
    {
        base.SkillStart();
        StartCoroutine(MaxSpeed());
    }

    private IEnumerator MaxSpeed()
    {
        AudioManager.instance.PlaySe(SoundName.SeName.highperspeed);
        //_state.state = PlayerState.State.SPEED_SKILL; //stateを変更
        var effect = Instantiate(_skilleffect[4], transform.localPosition, transform.localRotation) as GameObject; //エフェクトを生成
        effect.transform.parent = transform; //エフェクトを自分の子供にする
        //スキル発動中の操作
        //while(_count != MOVE_COUNT)
        //{
        //    //_activeTime += Time.deltaTime; //時間をはかる
        //    if(_input.isLeftStickFlick)
        //    {
        //        _count++;
        //        _direction = _input.getLeftStickDirection; //入力の向きを所得
        //    }
        //    transform.Translate(_direction * _speed * Time.deltaTime, Space.World); //移動
        //    if (_input.getLeftStickDirection.x != 0 || _input.getLeftStickDirection.z != 0) //モデルの向きを合わせる
        //        transform.eulerAngles = new Vector3(
        //            0.0f,
        //            -((Mathf.Atan2(_input.getLeftStickDirection.z, _input.getLeftStickDirection.x) - Mathf.PI / 2) * 180 / Mathf.PI),
        //            0.0f);

        //    yield return null;
        //}
        //while(_activeTime < 0.5f)
        //{
        //    _activeTime += Time.deltaTime;
        //    transform.Translate(_direction * _speed * Time.deltaTime, Space.World); //移動
        //    if (_input.getLeftStickDirection.x != 0 || _input.getLeftStickDirection.z != 0) //モデルの向きを合わせる
        //        transform.eulerAngles = new Vector3(
        //            0.0f,
        //            -((Mathf.Atan2(_input.getLeftStickDirection.z, _input.getLeftStickDirection.x) - Mathf.PI / 2) * 180 / Mathf.PI),
        //            0.0f);

        //    yield return null;
        //}
        //_count = 0;
        //_state.state = PlayerState.State.NORMAL; //プレイヤーの状態を戻す

        float yAngle = (-transform.eulerAngles.y + 90.0f) * (float)Math.PI / 180.0f;

        var rigidbody = GetComponent<Rigidbody>();

        while (_moveLength.magnitude < moveLength)
        {
            var value = new Vector3(Mathf.Cos(yAngle), 0.0f, Mathf.Sin(yAngle)) * moveSpeed * Time.deltaTime;
            rigidbody.AddForce(value, ForceMode.Impulse);
            _moveLength += value;
            //rigidbody.velocity = value;
            yield return null;
        }
        rigidbody.velocity = Vector3.zero;
       _moveLength = Vector3.zero;
      

        StartCoroutine(SKillCoolTime()); //スキル終了後
        //Destroy(effect);

        yield return new WaitForSeconds(0.5f);

        effect.transform.parent = null;
        effect.GetComponent<ParticleSystem>().loop = false;
        //Destroy(effect.gameObject);
        yield return null;
    }
}
