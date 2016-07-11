/// <author>
/// 新井大一
/// </author>


using UnityEngine;

public class SlopeClimber : MonoBehaviour
{
    [SerializeField]
    float _hitY = 0.0f;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.GetHashCode() != HashTagName.Player) return;
        var position = collision.transform.position;
        var collider = collision.gameObject.GetComponent<CapsuleCollider>();
        position.y =_hitY + collider.height * 0.5f;
        collision.transform.position = position;
    }
}
