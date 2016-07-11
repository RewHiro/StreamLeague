/// <author>
/// 新井大一
/// </author>


using UnityEngine;

/// <summary>
/// ステージ外に出たらリスポーンする機能
/// </summary>
public class Respawn : MonoBehaviour
{
    /// <summary>
    /// リスポーンする位置
    /// </summary>
    [SerializeField]
    Vector3 RESPAWN_POSITION = Vector3.zero;

    public void OnTriggerExit(Collider other)
    {
        if (other.tag.GetHashCode() != HashTagName.Player) return;
        other.transform.position = RESPAWN_POSITION;
    }
}
