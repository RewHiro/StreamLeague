/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// 壁に当たったらリジッドボディを調整する機能
/// </summary>
public class HitStopRigidbody : MonoBehaviour
{
    float RADIUS = 10.0f;

    float _startPositionY = 0.0f;
    Rigidbody _rigidBody = null;

    void Awake()
    {
        _startPositionY = transform.position.y + 3.0f;
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_startPositionY < transform.position.y) return;

        var colliders = Physics.OverlapSphere(transform.position, RADIUS);

        foreach (var collider in colliders)
        {
            if (collider.tag.GetHashCode() != HashTagName.Wall) continue;
            var velocity = _rigidBody.velocity;
            velocity.x = 0.0f;
            velocity.z = 0.0f;
            _rigidBody.velocity = velocity;
            break;
        }
    }
}
