/// <author>
/// 新井大一
/// </author>

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステートを管理する元になるクラス
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TEnum"></typeparam>
public abstract class StateManagerBase<T, TEnum> : MonoBehaviour
    where T : class where TEnum : System.IConvertible
{

    /// <summary>
    /// ステートを変更する
    /// </summary>
    /// <param name="state">ステート</param>
    public virtual void ChangeState(TEnum state)
    {
        _stateMachine.ChangeState(_stateList[state.ToInt32(null)]);
    }

    /// <summary>
    /// その状態か
    /// </summary>
    /// <param name="state">ステート</param>
    /// <returns></returns>
    public virtual bool IsCurrentState(TEnum state)
    {
        return _stateMachine.currentState == _stateList[state.ToInt32(null)];
    }

    //-----------------------------------------------------------------
    protected List<State<T>> _stateList = new List<State<T>>();

    protected StateMachine<T> _stateMachine = new StateMachine<T>();

    protected virtual void Update()
    {
        _stateMachine.Update();
    }

    public void OnCollisionEnter(Collision collision)
    {
        _stateMachine.currentState.OnCollisionEnter(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        _stateMachine.currentState.OnCollisionStay(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        _stateMachine.currentState.OnCollisionExit(collision);
    }

    public void OnTriggerEnter(Collider other)
    {
        _stateMachine.currentState.OnTriggerEnter(other);
    }

    public void OnTriggerStay(Collider other)
    {
        _stateMachine.currentState.OnTriggerStay(other);
    }

    public virtual void OnTriggerExit(Collider other)
    {
        _stateMachine.currentState.OnTriggerExit(other);
    }
}