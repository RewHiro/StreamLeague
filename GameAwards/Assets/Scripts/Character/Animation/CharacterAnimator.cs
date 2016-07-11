using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    InputBase _inputBase = null;
    Animator _animator = null;

    void Start()
    {
        _inputBase = GetComponent<InputBase>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetFloat("Speed", Vector3.Dot(_inputBase.getLeftStickDirection, _inputBase.getLeftStickDirection));
    }

    public void ChangeIdle()
    {
        _animator.SetFloat("Speed", 0.0f);
    }
}