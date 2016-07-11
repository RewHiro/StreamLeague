/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// スキルを使用する機能
/// </summary>
public class StrongSkillState : State<StrongAI>
{

    public StrongSkillState(StrongAI strong_ai) :
        base(strong_ai)
    {
        _skillType = (int)_stateManager.GetComponent<CharacterParameter>().getParameter.skillType;

        _actions.Add(SpeedController());
        _actions.Add(SuperSpeed());
        _actions.Add(Tornado());
        _actions.Add(Gravity());
        _actions.Add(Invisible());
        _actions.Add(Parry());


        _skillBase = _stateManager.GetComponent<SkillBase>();

        var playerType = _stateManager.GetComponent<InputBase>().getPlayerType;

        foreach (var player in GameObject.FindObjectsOfType<InputBase>())
        {
            if (player.getPlayerType == playerType) continue;
            _targetTransform = player.transform;
        }
    }

    public override void Enter()
    {

        _stateManager.aiInput.skill = true;
    }

    public override void Execute()
    {
        _stateManager.ChangeState(StrongAI.State.COLLECT);
    }

    public override void Exit()
    {
        _stateManager.aiInput.skill = false;
    }

    //----------------------------------------------------------------------


    List<IEnumerator> _actions = new List<IEnumerator>();
    SkillBase _skillBase = null;
    Transform _targetTransform = null;
    int _skillType = 0;

    float _time = 0.0f;

    IEnumerator SpeedController()
    {
        while (_skillBase.isSkillsIn)
        {
            _stateManager.aiInput.rightStickFrontFlick = false;
            while (_time < 1.0f)
            {
                _time += Time.deltaTime;
                yield return null;
            }
            _time = 0.0f;
            _stateManager.aiInput.rightStickFrontFlick = true;
        }

        _stateManager.aiInput.rightStickFrontFlick = false;
    }

    IEnumerator SuperSpeed()
    {
        yield return null;
    }

    IEnumerator Tornado()
    {
        yield return null;
    }

    IEnumerator Gravity()
    {
        yield return null;
    }

    IEnumerator Invisible()
    {
        yield return null;
    }

    IEnumerator Parry()
    {
        var direction = _targetTransform.position - _stateManager.transform.position;
        direction.Normalize();

        _stateManager.aiInput.leftStickDirection = direction;

        yield return null;
    }
}
