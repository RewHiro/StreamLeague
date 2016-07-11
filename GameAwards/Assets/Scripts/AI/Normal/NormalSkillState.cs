/// <author>
/// 新井大一
/// </author>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルを使用する機能
/// </summary>
public class NormalSkillState : State<NormalAI>
{
    public NormalSkillState(NormalAI normal_ai) :
        base(normal_ai)
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
        _stateManager.ChangeState(NormalAI.State.COLLECT);
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
        while (_skillBase.isSkillsIn)
        {
            while (_time < 1.0f)
            {
                _time += Time.deltaTime;
                yield return null;
            }
            _time = 0.0f;
            _stateManager.aiInput.rightStickDirection = _stateManager.aiInput.leftStickDirection;
        }

        _stateManager.aiInput.rightStickFlick = false;
    }

    IEnumerator Tornado()
    {
        yield return null;
    }

    IEnumerator Gravity()
    {
        while (_time < 3.0f)
        {
            _time += Time.deltaTime;
            yield return null;
        }

        _stateManager.aiInput.pushR3 = false;
        _stateManager.aiInput.releaseR3 = true;

        yield return null;

        _stateManager.aiInput.rightStickBackFlick = true;

        yield return null;

        _stateManager.aiInput.rightStickBackFlick = false;
    }

    IEnumerator Invisible()
    {
        yield return null;
    }

    IEnumerator Parry()
    {
        var direction = _targetTransform.position - _stateManager.transform.position;
        direction.Normalize();

        if (direction.x > 0.0f)
        {
            _stateManager.aiInput.rightStickRightFlick = true;
        }
        else if (direction.x < 0.0f)
        {
            _stateManager.aiInput.rightStickLeftFlick = true;
        }
        else if (direction.z > 0.0f)
        {
            _stateManager.aiInput.rightStickFrontFlick = true;
        }
        else if (direction.z < 0.0f)
        {
            _stateManager.aiInput.rightStickBackFlick = true;
        }


        yield return null;

        _stateManager.aiInput.rightStickRightFlick = false;
        _stateManager.aiInput.rightStickLeftFlick = false;
        _stateManager.aiInput.rightStickFrontFlick = false;
        _stateManager.aiInput.rightStickBackFlick = false;
    }
}