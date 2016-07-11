using UnityEngine;
using System.Collections;

public class AttackEffecter : MonoBehaviour
{
    [SerializeField]
    GameObject _attackEffectPrefab = null;

    GameObject _attackEffect = null;

    PlayerState _playerState = null;


    void Start()
    {
        _playerState = GetComponent<PlayerState>();
    }

    void Update()
    {
        if (_playerState.state == PlayerState.State.ATTACK)
        {
            if (_attackEffect != null) return;
            _attackEffect = Instantiate(_attackEffectPrefab);
            _attackEffect.transform.SetParent(transform);
            _attackEffect.transform.position = transform.position;
            _attackEffect.transform.rotation = transform.rotation;
        }
        else
        {
            if (_attackEffect == null) return;
            var particleSystem = _attackEffect.GetComponent<ParticleSystem>();
            particleSystem.loop = false;
            if (!particleSystem.IsAlive())
            {
                Destroy(_attackEffect);
            }
        }
    }
}