/// <author>
/// 新井大一
/// </author>

using UnityEngine;


/// <summary>
/// ステートの元になるクラス
/// </summary>
/// <typeparam name="T"></typeparam>
abstract public class State<T>
{
    public State(T state_manager)
    {
        _stateManager = state_manager;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    public virtual void OnCollisionEnter(Collision collision)
    {
    }

    public virtual void OnCollisionStay(Collision collision)
    {
    }

    public virtual void OnCollisionExit(Collision collision)
    {
    }

    public virtual void OnTriggerEnter(Collider other)
    {
    }

    public virtual void OnTriggerStay(Collider other)
    {

    }

    public virtual void OnTriggerExit(Collider other)
    {

    }

    //---------------------------------------------------------
    protected T _stateManager;
}