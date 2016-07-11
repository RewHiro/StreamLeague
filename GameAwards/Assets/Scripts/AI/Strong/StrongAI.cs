/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

/// <summary>
/// 強いAI
/// </summary>
public class StrongAI : StateManagerBase<StrongAI,StrongAI.State>
{

    public enum State
    {
        COLLECT,    //集める
        GREED,      //盗む
        SKILL,      //スキル
        ATTACK,     //攻撃
        DEFENCE,    //防御
        NUM         //ステートの数
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

    /// <summary>
    /// 攻撃すべきか
    /// </summary>
    bool shouldChangeAttack
    {
        get
        {
            return _energyConnect.connectNum > _rivalEnergyConnect.connectNum &&
                _rivalEnergyConnect.connectNum >= ENERGY_MAX_NUM / 2; 
        }
    }

    /// <summary>
    /// 盗むべきか
    /// </summary>
    bool shouldChangeGreed
    {
        get
        {
            return _rivalEnergyConnect.connectNum >= ENERGY_MAX_NUM / 2;
        }
    }

    /// <summary>
    /// 防御すべきか
    /// </summary>
    bool shouldChangeDefence
    {
        get
        {
            return _rivalPlayerState.state == PlayerState.State.ATTACK && 
                _rivalEnergyConnect.connectNum >= ENERGY_MAX_NUM / 2;
        }
    }

    /// <summary>
    /// スキルを使用するべきか
    /// </summary>
    bool shouldChangeSkill
    {
        get
        {
            return !_skillBase.skillActive;
        }
    }

    EnergyConnect _energyConnect = null;
    SkillBase _skillBase = null;
    EnergyConnect _rivalEnergyConnect = null;
    PlayerState _rivalPlayerState = null;
    CharacterParameter _characterParameter = null;
    int ENERGY_MAX_NUM = 0;

    void Awake()
    {
        aiInput = GetComponent<AIInput>();

        _stateList.Add(new StrongCollectState(this));
        _stateList.Add(new StrongGreedState(this));
        _stateList.Add(new StrongSkillState(this));
        _stateList.Add(new StrongAttackState(this));
        _stateList.Add(new StrongDefenceState(this));

        ChangeState(State.COLLECT);

        _energyConnect = GetComponent<EnergyConnect>();
        _skillBase = GetComponent<SkillBase>();

        var players = GameObject.FindGameObjectsWithTag(TagName.Player);
        var rival = players.ObjectCloseToTheSecond(transform.position).gameObject;

        _rivalEnergyConnect = rival.GetComponent<EnergyConnect>();
        _rivalPlayerState = rival.GetComponent<PlayerState>();

        ENERGY_MAX_NUM = FindObjectOfType<EnergyCreater>().energyMaxNum;

        _characterParameter = GetComponent<CharacterParameter>();
    }

    protected override void Update()
    {
        base.Update();

        if (shouldChangeGreed)
        {
            if (!IsCurrentState(State.GREED))
            {
                ChangeState(State.GREED);
            }
        }

        if (shouldChangeAttack) ChangeState(State.ATTACK);

        if (_characterParameter.getParameter.skillType == CharacterSelect.SkillType.Invisible)
        {
            if (shouldChangeDefence)
            {
                if (shouldChangeSkill) ChangeState(State.SKILL);
            }
        }
        else
        {
            if (shouldChangeDefence) ChangeState(State.DEFENCE);
            if (shouldChangeSkill) ChangeState(State.SKILL);
        }
    }

    void LateUpdate()
    {
    }
}
