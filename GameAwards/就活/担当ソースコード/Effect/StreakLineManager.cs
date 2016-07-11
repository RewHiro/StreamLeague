/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

/// <summary>
/// 流脈を管理する機能
/// </summary>
public class StreakLineManager : MonoBehaviour
{
    [SerializeField]
    GameObject _streakLinePrefab = null;

    /// <summary>
    /// 流脈が移動する時間
    /// </summary>
    [SerializeField]
    float LIFE_TIME = 0.0f;

    public int num { get; set; }

    /// <summary>
    /// 次のエネルギーまでの距離
    /// </summary>
    public float distance
    {
        get; private set;
    }

    public float time
    {
        get; private set;
    }

    public float lifeTime
    {
        get
        {
            return LIFE_TIME;
        }
    }

    /// <summary>
    /// 流脈を動かすか
    /// </summary>
    public bool isUpdate
    {
        get
        {
            return _isUpdate;
        }
    }

    public EnergyConnect energyConnect
    {
        get
        {
            return _energyConnect;
        }

        set
        {
            _energyConnect = value;
        }
    }

    EnergyConnect _energyConnect = null;
    TrailRenderer _trailRenderer = null;

    bool _isUpdate = false;
    int _energyCount = 0;
    int _count = 0;

    void Start()
    {
        var streakLine = Instantiate(_streakLinePrefab);
        streakLine.transform.SetParent(transform);
        streakLine.transform.position = transform.position;

        _trailRenderer = streakLine.GetComponent<TrailRenderer>();

        StartCoroutine(MoveStart());
    }

    /// <summary>
    /// 流脈の待機時間
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveStart()
    {
        _trailRenderer.enabled = false;

        while (_energyConnect.connectNum < 2)
        {
            yield return null;
        }

        var halfMaxNum = FindObjectOfType<EnergyCreater>().energyMaxNum;
        var halfNum = _energyConnect.connectNum / 2;
        var time = (_energyConnect.connectNum - 1) * (lifeTime + _trailRenderer.time) / halfNum * num;
        yield return new WaitForSeconds(time);
        _energyCount = _energyConnect.connectNum;
        _trailRenderer.enabled = true;
        yield return Move();
    }

    /// <summary>
    /// 流脈を移動
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        yield return null;

        if (_count >= _energyConnect.connectNum - 1) _count = 0;
        if (_energyConnect.connectNum < 2) yield return MoveStart();
        if(_energyConnect.connectList[_count] == null)yield return MoveStart();

        var startEnergy = _energyConnect.connectList[_count];
        var endEnergy = _energyConnect.connectList[_count + 1];

        if (startEnergy == null || endEnergy == null)
        {
            _count = 0;
            distance = 0.0f;
            yield return MoveStart();
        }

        var startTransform = _energyConnect.connectList[_count].transform.parent;
        var endTransform = _energyConnect.connectList[_count + 1].transform.parent;


        if (_count == 0)
        {
            _trailRenderer.Clear();
        }

        distance = 0.0f;
        //_trailRenderer.transform.position = startTransform.position;
        transform.SetParent(startTransform, false);
        //transform.position = startTransform.position;

        yield return null;

        if (_count == 0)
        {
        }


        time = 0.0f;

        var state = _energyConnect.GetComponent<PlayerState>();

        while (time < LIFE_TIME)
        {
            if (state.state == PlayerState.State.DEFENSE)
            {
                yield return OnDefenceMove();
            }

            if (startTransform == null || endTransform == null)
            {
                _count = 0;
                distance = 0.0f;
                _isUpdate = false;
                yield return MoveStart();
            }

            //if (_energyConnect.connectNum != _energyCount)
            //{
            //    _energyCount = _energyConnect.connectNum;
            //    _count = 0;
            //    distance = 0.0f;
            //    _isUpdate = false;
            //    yield return MoveStart();
            //}

            _isUpdate = true;
            distance = Vector3.Distance(startTransform.position, endTransform.position);
            transform.LookAt(endTransform.position);
            time += Time.deltaTime;
            yield return null;
        }

        _isUpdate = false;

        var stopTime = 0.0f;

        while (stopTime < _trailRenderer.time)
        {
            stopTime += Time.deltaTime;
            yield return null;
        }

        _count++;

        yield return Move();

    }

    /// <summary>
    /// 防御時に移動する処理
    /// </summary>
    /// <returns></returns>
    IEnumerator OnDefenceMove()
    {
        var startTransform = _energyConnect.connectList[_count].transform.parent;
        var endTransform = _energyConnect.transform.parent;

        while (time < LIFE_TIME)
        {
            if (endTransform == null) break;

            distance = Vector3.Distance(startTransform.position, endTransform.position);
            transform.LookAt(endTransform.position);
            time += Time.deltaTime;
            yield return null;
        }

        _isUpdate = false;

        var state = _energyConnect.GetComponent<PlayerState>();
        while (state.state == PlayerState.State.DEFENSE)
        {
            yield return null;
        }

        _count = 0;
        distance = 0.0f;
        _isUpdate = false;

        yield return MoveStart();
    }

}