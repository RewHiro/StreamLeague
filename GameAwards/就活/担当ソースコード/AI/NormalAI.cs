/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

/// <summary>
/// スタンダードのAI
/// </summary>
public class NormalAI : StateManagerBase<NormalAI,NormalAI.State>
{

    public enum State
    {
        COLLECT,    //集める
        GREED,      //盗む
        SKILL,      //スキル
        ATTACK,     //攻撃
        DEFENCE,    //防御
        WAIT,       //待機
        NUM,        //ステートの数
    }

    public AIInput aiInput { get; private set; }

    public State getRandomState
    {
        get
        {
            var state = Random.Range(0, (int)State.NUM);
            return (State)state;
        }
    }

    /// <summary>
    /// ランダムで向きを取得
    /// </summary>
    public Vector3 getRandomDirection
    {
        get
        {
            return new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
        }
    }

    EnergyConnect _rivalEnergyConnect = null;
    PlayerState _rivalPlayerState = null;
    SkillBase _skillBase = null;
    float _time = 0.0f;

    void Awake()
    {
        aiInput = GetComponent<AIInput>();

        _stateList.Add(new NormalCollectState(this));
        _stateList.Add(new NomarlGreedState(this));
        _stateList.Add(new NormalSkillState(this));
        _stateList.Add(new NormalAttackState(this));
        _stateList.Add(new NormalDefenceState(this));
        _stateList.Add(new NormalWaitState(this));

        _skillBase = GetComponent<SkillBase>();

        var players = GameObject.FindGameObjectsWithTag(TagName.Player);
        var rival = players.ObjectCloseToTheSecond(transform.position).gameObject;

        _rivalEnergyConnect = rival.GetComponent<EnergyConnect>();
        _rivalPlayerState = rival.GetComponent<PlayerState>();

        ChangeState(State.COLLECT);
    }

    protected override void Update()
    {
        base.Update();

        if (_rivalPlayerState.state == PlayerState.State.ATTACK)
        {
            if (_rivalEnergyConnect.connectNum >= 6)
            {
                ChangeState(State.DEFENCE);
            }
        }
        else if (_rivalPlayerState.state == PlayerState.State.NORMAL)
        {
            if (_rivalEnergyConnect.connectNum >= 4)
            {
                if (!IsCurrentState(State.GREED))
                {
                    ChangeState(State.GREED);
                }
            }
        }

        if (!_skillBase.skillActive)
        {
            _time += Time.deltaTime;
            if (_time >= 1.0f)
            {
                _time = 0.0f;
                var random = Random.Range(0, 100);
                if (random <= 40)
                {
                    ChangeState(State.SKILL);
                }
            }
        }
        else
        {
            _time = 0.0f;
        }
    }

    void LateUpdate()
    {
    }
}
