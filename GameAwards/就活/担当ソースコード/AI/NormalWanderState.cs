/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

public class NormalWanderState : State<NormalAI>
{
    const float WANDER_TIME = 0.2f;
    const int WANDET_STOP_COUNT = 5;

    public NormalWanderState(NormalAI normal_ai) :
        base(normal_ai)
    {
        _attack = _stateManager.GetComponent<Attack>();
    }

    public override void Enter()
    {
        _coroutine = _stateManager.StartCoroutine(Wander(_stateManager.getRandomDirection));
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        _stateManager.StopCoroutine(_coroutine);
        _count = 0;
    }

    Attack _attack = null;
    Coroutine _coroutine = null;

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

        if (_attack.isCanAttack && _attack.isInRange) _stateManager.ChangeState(NormalAI.State.ATTACK);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.GetHashCode() != HashTagName.Wall) return;
        _stateManager.aiInput.leftStickDirection *= -1;
    }
}
