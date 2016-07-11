/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// 次のエネルギーの点に向かせる機能
/// </summary>
public class StreakLineLookAt : MonoBehaviour
{

    public Transform targetTransform { get; set; }
    public Color color { get; set; }

    ParticleSystem _particleSystem = null;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (targetTransform == null) return;
        var distance = Vector3.Distance(targetTransform.position, transform.position);
        _particleSystem.startLifetime = distance / 6.0f;
        _particleSystem.startColor = color;
        transform.LookAt(targetTransform);
    }
}
