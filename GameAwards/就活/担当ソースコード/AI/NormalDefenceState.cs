/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

/// <summary>
/// 防御する機能
/// </summary>
public class NormalDefenceState : State<NormalAI>
{
    const float WANDER_TIME = 3.0f;
    const int WANDET_STOP_COUNT = 5;

    public NormalDefenceState(NormalAI normal_ai) : base(normal_ai)
    {
        _playerState = _stateManager.GetComponent<PlayerState>();
    }

    public override void Enter()
    {
        _stateManager.aiInput.defence = true;
        //_stateManager.StartCoroutine(Wander(_stateManager.getRandomDirection));
    }

    public override void Execute()
    {
        //if (_playerState.state == PlayerState.State.DEFENSE) return;
        _stateManager.ChangeState(NormalAI.State.COLLECT);
    }

    public override void Exit()
    {
        _stateManager.aiInput.defence = false;
    }

    PlayerState _playerState = null;

    /// <summary>
    /// 徘徊
    /// </summary>
    IEnumerator Wander(Vector3 direction)
    {
        direction.y = 0.0f;
        _stateManager.aiInput.leftStickDirection = direction.normalized;

        var time = 0.0f;

        while (time <= WANDER_TIME)
        {

            time += Time.deltaTime;
            yield return null;
        }

        if (_playerState.state != PlayerState.State.DEFENSE)
        {
            _stateManager.ChangeState(NormalAI.State.COLLECT);
        }

        yield return Wander(_stateManager.getRandomDirection);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.GetHashCode() != HashTagName.Wall) return;
        _stateManager.aiInput.leftStickDirection *= -1;
    }
}
