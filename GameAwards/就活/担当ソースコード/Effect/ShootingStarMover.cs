/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using System.Collections;

/// <summary>
/// 流星を動かす機能
/// </summary>
public class ShootingStarMover : MonoBehaviour
{
    /// <summary>
    /// ディレイ時間
    /// </summary>
    [SerializeField]
    float WAIT_TIME = 1.0f;

    /// <summary>
    /// 移動する時間
    /// </summary>
    [SerializeField]
    float MOVE_TIME = 5.0f;

    /// <summary>
    /// 移動する速さ
    /// </summary>
    [SerializeField]
    float SPEED = 1.0f;

    /// <summary>
    /// スポーンする範囲
    /// </summary>
    [SerializeField]
    float SPAWN_RANGE = 20.0f;

    Vector3 _startPosition = Vector3.zero;

    void Start()
    {
        _startPosition = transform.position;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(WAIT_TIME);

        yield return Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        float time = 0.0f;
        Vector3 rotateDirection = new Vector3(1, 1, 0).normalized;

        while (time <= MOVE_TIME)
        {
            time += Time.deltaTime;
            transform.Translate(Vector3.back * SPEED);
            transform.rotation = Quaternion.LookRotation(rotateDirection);
            yield return null;
        }

        var position = _startPosition;
        position.x = Random.Range(-SPAWN_RANGE, SPAWN_RANGE);
        transform.position = position;
        yield return Move();
    }
}
