/// <author>
/// 新井大一
/// </author>

using UnityEngine;

public class StreakLineMover : MonoBehaviour
{
    [SerializeField]
    Vector2 MAX_SWING_WIDTH = Vector2.zero;

    [SerializeField]
    float SWING_SPEED = 1.0f;

    StreakLineManager _streakLineManager = null;
    TrailRenderer _trailRenderer = null;
    float _theta = 0.0f;

    void Start()
    {
        _streakLineManager = transform.parent.GetComponent<StreakLineManager>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    void LateUpdate()
    {

        if (_streakLineManager.isUpdate)
        {

            var swingXWidth = Random.Range(0.0f, MAX_SWING_WIDTH.x);
            var swingYWidth = Random.Range(0.0f, MAX_SWING_WIDTH.y);

            var time = 0.0f;
            time = _streakLineManager.time;
            if (time >= _streakLineManager.lifeTime)
            {
                time = _streakLineManager.lifeTime;
            }

            var x = Mathf.Sin(_theta) * swingXWidth;
            var y = Mathf.Cos(_theta) * swingYWidth;
            var z = _streakLineManager.distance * (time / _streakLineManager.lifeTime);

            _theta += Time.deltaTime * SWING_SPEED;

            transform.localPosition = new Vector3(x, y, z);
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, _streakLineManager.distance);
        }
    }
}