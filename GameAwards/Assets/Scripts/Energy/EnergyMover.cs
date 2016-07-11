using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnergyMover : MonoBehaviour
{

    enum State
    {
        Normal,
        Concentration,
        Rotation,
        //Random,

        max
    }

    [SerializeField]
    private float END_MOVE_TIME = 3.0f;
    [SerializeField]
    private float COOL_TIME = 2.0f;
    [SerializeField]
    private static State _state = State.Normal; //全部のエネルギーが同じstateになるようにstaticにしてる
    [SerializeField]
    private float _changeTime = 10.0f; //resetTimeがchangeTimeをこえたらstateを変える

    private float _resetTime = 0.0f; //次のステートまでのタイムカウント

    private Vector3 _direction = Vector3.zero; //移動する向き

    private Rigidbody _rigidbody = null;

    private Vector3 _prevPos = Vector3.zero; //Randomのときの１フレーム前の自分の座標を保存する変数

    private bool _moveFlug = false; //ゲームがスタートしたかどうか

    [SerializeField]
    private StartCountDown _countDown = null; //カットインがまだあるかどうか

    private Dictionary<State, Action> _selectType = null; //stateによって呼ぶ関数を変える

    private Vector3 _concentrationPos = Vector3.zero; //集中型の時のエネルギーの目標ポジション
    private Vector3 _rotatePos = Vector3.zero;
    private Vector3 _normalPos = Vector3.zero;

    [SerializeField]
    private float _rotateTime = 6.0f;

    public float speed
    {
        get; set;
    }

    void Start()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _countDown = FindObjectOfType<StartCountDown>();
        //_state = RandomState();
        _selectType = new Dictionary<State, Action>();
        _selectType.Add(State.Normal, Normal);
        _selectType.Add(State.Concentration, Concentration);
        _selectType.Add(State.Rotation, Rotation);
        //_selectType.Add(State.Random, RandomM);
        speed = 2.0f;
        _concentrationPos = GetComponent<EnergyConcentrationPos>().concentrationPos;
        _rotatePos = GetComponent<EnergyConcentrationPos>().rotateStartPos;
        _normalPos = GetComponent<EnergyConcentrationPos>().normalPos;
    }

    private void Update()
    {
        if(_rigidbody == null)
        {
            _rigidbody = GetComponentInChildren<Rigidbody>();
        }

        if (_countDown != null) { return; }
        else
        {
            _countDown = FindObjectOfType<StartCountDown>();
        }
        _selectType[_state]();
    }

    //回転型になる
    void Rotation()
    {
        if (_moveFlug) { return; }
        if (_countDown == null)
        {
            _moveFlug = true;
            StartCoroutine(RotationMove());
        }
        else if (_countDown.countDown < 0.0f)
        {
            _moveFlug = true;
            StartCoroutine(RotationMove());
        }
    }

    //集中型になる
    void Concentration()
    {
        _resetTime += Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _concentrationPos, Time.deltaTime * speed);
        if(_resetTime > _changeTime)
        {
            _resetTime = 0.0f;
            _state = RandomState();
        }
    }

    //通常型になる
    void Normal()
    {
        _resetTime += Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _normalPos, Time.deltaTime * speed);
        if (_resetTime > _changeTime)
        {
            _resetTime = 0.0f;
            _state = RandomState();
        }
    }

    //ランダム型
    void RandomM()
    {
        if (_moveFlug) { return; }
        if (_countDown == null)
        {
            _moveFlug = true;
            StartCoroutine(RandomMove());
        }
        else if (_countDown.countDown < 0.0f)
        {
            _moveFlug = true;
            StartCoroutine(RandomMove());
        }
    }

    /// <summary>
    /// ランダム型はコルーチンで回す
    /// </summary>
    /// <returns></returns>
    private IEnumerator RandomMove()
    {
        _direction = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 0, UnityEngine.Random.Range(-0.1f, 0.1f));
        while (_resetTime < END_MOVE_TIME)
        {
            _resetTime += Time.deltaTime;
            transform.Translate(_direction, Space.World);
            _prevPos = transform.localPosition;
            _rigidbody.velocity = Vector3.zero;
            yield return null;
        }
        _resetTime = 0.0f;
        yield return new WaitForSeconds(COOL_TIME);
        _state = RandomState();
        _moveFlug = false;
        //yield return StartCoroutine(RandomMove());

    }

    /// <summary>
    /// 回転型のコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotationMove()
    {
        GetComponentInParent<EnergyRotater>().rotateFlug = true;
        while (_resetTime < _changeTime)
        {
            _resetTime += Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _rotatePos, Time.deltaTime * speed);
            yield return null;
        }
        _resetTime = 0.0f;
        //yield return new WaitForSeconds(_rotateTime);
        GetComponentInParent<EnergyRotater>().rotateFlug = false;
        _state = RandomState();
        _moveFlug = false;
    }


    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.GetHashCode() != HashTagName.Wall) { return; }
        _direction *= -1;
        transform.localPosition = _prevPos;
    }

    /// <summary>
    /// ランダムでStateを切り替える関数
    /// </summary>
    /// <returns></returns>
    private State RandomState()
    {
        var index = UnityEngine.Random.Range(0, (int)State.max);
        return index == 0 ? State.Normal :
            index == 1 ? State.Concentration : State.Rotation;
    }
}
