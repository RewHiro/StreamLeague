/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

/// <summary>
/// 防御する機能
/// </summary>
public class StrongDefenceState : State<StrongAI>
{

    const float WANDER_TIME = 1.0f;
    const int WANDET_STOP_COUNT = 5;

    public StrongDefenceState(StrongAI strong_ai):base(strong_ai)
    {
        _playerState = _stateManager.GetComponent<PlayerState>();
    }

    public override void Enter()
    {
        _stateManager.aiInput.defence = true;
    }

    public override void Execute()
    {
        _stateManager.ChangeState(StrongAI.State.COLLECT);
    }

    public override void Exit()
    {
        _stateManager.aiInput.defence = false;
        _count = 0;
    }

    PlayerState _playerState = null;
    int _count = 0;

    /// <summary>
    /// 徘徊
    /// </summary>
    IEnumerator Wander(Vector3 direction)
    {
        _count++;
        _stateManager.aiInput.leftStickDirection = direction * 0.8f;

        var time = 0.0f;

        while (time <= WANDER_TIME)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (_count == WANDET_STOP_COUNT)
        {
            _stateManager.ChangeState(_stateManager.getRandomState);
        }
        else
        {
            yield return Wander(_stateManager.getRandomDirection);
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.GetHashCode() != HashTagName.Wall) return;
        _stateManager.aiInput.leftStickDirection *= -1;
    }
}
