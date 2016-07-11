using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnergyMoveManager : MonoBehaviour {

    // エネルギーの座標情報
    [SerializeField]
    List<EnergyConcentrationPos> _posData;

    // エネルギーの動きの状態
    enum State
    {
        Normal,         // 初期位置
        Concentration,  // 収束
        Rotation,       // 回転

        max             // ランダム Range の時に使う
    }
    [SerializeField]
    State _state = State.Normal;

    [SerializeField]
    private float END_MOVE_TIME = 3.0f;
    [SerializeField]
    private float COOL_TIME = 2.0f;
    [SerializeField]
    private float _changeTime = 10.0f; //resetTimeがchangeTimeをこえたらstateを変える

    private float _resetTime = 0.0f; //次のステートまでのタイムカウント

    private Vector3 _direction = Vector3.zero; //移動する向き

    private Vector3 _prevPos = Vector3.zero; //Randomのときの１フレーム前の自分の座標を保存する変数

    private bool _moveFlug = false; //ゲームがスタートしたかどうか

    [SerializeField]
    private StartCountDown _countDown = null; //カットインがまだあるかどうか

    private Dictionary<State, Action> _selectType = null; //stateによって呼ぶ関数を変える

    public bool isChange { get; set; }

    public float speed
    {
        get; set;
    }

    // Use this for initialization
    void Start () {
        _countDown = FindObjectOfType<StartCountDown>();

        //_state = RandomState();

        _selectType = new Dictionary<State, Action>();
        _selectType.Add(State.Normal, Normal);
        _selectType.Add(State.Concentration, Concentration);
        _selectType.Add(State.Rotation, Rotation);

        speed = 2.0f;
    }
	
	// Update is called once per frame
	void Update () {
        isChange = isChange ? false : isChange;
        if (_countDown != null) {
            _state = State.Normal;
            return;
        }
        else
        {
            _countDown = FindObjectOfType<StartCountDown>();
        }
        _selectType[_state]();

        //Debug.Log(_state);
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

        foreach (var energy in _posData)
        {
            energy.transform.localPosition = Vector3.MoveTowards(energy.transform.localPosition, energy.concentrationPos, Time.deltaTime * speed);
        }

        if (_resetTime > _changeTime)
        {
            _resetTime = 0.0f;
            _state = RandomState();
        }
    }

    //通常型になる
    void Normal()
    {
        _resetTime += Time.deltaTime;

        foreach (var energy in _posData)
        {
            energy.transform.localPosition = Vector3.MoveTowards(energy.transform.localPosition, energy.normalPos, Time.deltaTime * speed);
        }

        if (_resetTime > _changeTime)
        {
            _resetTime = 0.0f;
            _state = RandomState();
        }
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
            foreach (var energy in _posData)
            {
                energy.transform.localPosition = Vector3.MoveTowards(energy.transform.localPosition, energy.rotateStartPos, Time.deltaTime * speed);
            }
            yield return null;
        }
        _resetTime = 0.0f;
        //yield return new WaitForSeconds(_rotateTime);
        GetComponentInParent<EnergyRotater>().rotateFlug = false;
        _state = RandomState();
        _moveFlug = false;
    }

    /// <summary>
    /// ランダムでStateを切り替える関数
    /// </summary>
    /// <returns></returns>
    private State RandomState()
    {
        var index = UnityEngine.Random.Range(0, (int)State.max);

        isChange = true;

        return index == 0 ? State.Normal :
            index == 1 ? State.Concentration : State.Rotation;
    }
}
