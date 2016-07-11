/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

/// <summary>
/// 巡回する機能
/// </summary>
public class WeakWanderState : State<WeakAI>
{
    const float WANDER_TIME = 0.2f;
    const int WANDET_STOP_COUNT = 5;

    public WeakWanderState(WeakAI weak_ai) :
        base(weak_ai)
    {
        _attack = _stateManager.GetComponent<Attack>();
    }

    public override void Enter()
    {
        _stateManager.StartCoroutine(Wander(_stateManager.getRandomDirection));
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        _stateManager.StopAllCoroutines();
        _count = 0;
    }

    Attack _attack = null;

    int _count = 0;

    /// <summary>
    /// 徘徊
    /// </summary>
    IEnumerator Wander(Vector3 direction)
    {
        _count++;
        _stateManager.aiInput.leftStickDirection = direction;

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

        if (_attack.isCanAttack && _attack.isInRange) _stateManager.ChangeState(WeakAI.State.ATTACK);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.GetHashCode() != HashTagName.Wall) return;
        _stateManager.aiInput.leftStickDirection *= -1;
    }
}
