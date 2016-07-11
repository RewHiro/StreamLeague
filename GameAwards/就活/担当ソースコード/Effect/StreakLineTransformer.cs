/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// 流脈の位置情報更新する機能
/// </summary>
public class StreakLineTransformer : MonoBehaviour
{

    public Transform startPosition
    {
        get
        {
            return _startPosition;
        }

        set
        {
            _startPosition = value;
        }
    }

    public Transform endPosition
    {
        get
        {
            return _endPosition;
        }

        set
        {
            _endPosition = value;
        }
    }

    //------------------------------------------------------

    ParticleSystem _particleSystem = null;
    Transform _startPosition = null;
    Transform _endPosition = null;
    float _speed = 0.0f;

    // Use this for initialization
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _speed = _particleSystem.startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_startPosition == null) return;
        if (_endPosition == null) return;
        var distance = Vector3.Distance(_startPosition.position, _endPosition.position);
        transform.position = _startPosition.position;
        transform.LookAt(_endPosition.position);

        _particleSystem.startLifetime = distance / _speed;
    }
}
