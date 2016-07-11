using UnityEngine;
using System.Collections;

public class NormalWaitState : State<NormalAI>
{
    float _waitTime = 1.0f;

    public NormalWaitState(NormalAI normal_ai):
        base(normal_ai)
    {

    }

    public override void Enter()
    {
        _stateManager.aiInput.leftStickDirection = Vector3.zero;
        _waitTime = UnityEngine.Random.Range(0.0f, 1.0f);
    }

    public override void Execute()
    {
        _time += Time.deltaTime;
        if (_time < _waitTime) return;
        _stateManager.ChangeState(_stateManager.getRandomState);
    }

    public override void Exit()
    {
        _time = 0.0f;
    }

    float _time = 0.0f;
}
