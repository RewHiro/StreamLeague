/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// 弱いAI
/// </summary>
public class WeakAI : StateManagerBase<WeakAI, WeakAI.State>
{
    float _time = 0.0f;
    EnergyConnect _energyConnect = null;

    public enum State
    {
        COLLECT,        //　集める
        ATTACK,         //　攻撃
        DEFENCE,        //　防御
        WAIT,           //  待つ
        NUM,            //　enumの数
    }

    /// <summary>
    /// AI操作スクリプト
    /// </summary>
    public AIInput aiInput { get; private set; }

    public bool isActive { get; set; }

    /// <summary>
    /// ランダムでStateを取得(CHASE以外)
    /// </summary>
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

    void Awake()
    {
        aiInput = GetComponent<AIInput>();

        _stateList.Add(new WeakCollectState(this));
        _stateList.Add(new WeakAttackState(this));
        _stateList.Add(new WeakDefenceState(this));
        _stateList.Add(new WeakWaitState(this));

        _energyConnect = GetComponent<EnergyConnect>();

        ChangeState(State.COLLECT);
    }

    protected override void Update()
    {
        base.Update();

        if (isActive)
        {
            _time += Time.deltaTime;

            if (_time >= 5.0f)
            {
                _time = 0.0f;
                var state = getRandomState;
                ChangeState(state);
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
