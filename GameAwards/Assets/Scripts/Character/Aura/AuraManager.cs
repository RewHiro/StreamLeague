using UnityEngine;
using System.Collections;

public class AuraManager : MonoBehaviour
{

    [SerializeField]
    GameObject _auraManager = null;

    //EnergyConnect _ownEnergyConnect = null;
    //EnergyConnect _rivalEnergyConnect = null;

    Attack _attack = null;
    EnergyConnect _connect = null;
    PlayerState _playerState = null;
    ParticleSystem _particle = null;

    void Start()
    {
        //_ownEnergyConnect = GetComponent<EnergyConnect>();

        var type = GetComponent<InputBase>().getPlayerType;

        //foreach (var energyConnect in FindObjectsOfType<EnergyConnect>())
        //{
        //    if (energyConnect.GetComponent<InputBase>().getPlayerType == type) continue;
        //    _rivalEnergyConnect = energyConnect;
        //}

        _playerState = GetComponent<PlayerState>();
        _attack = GetComponent<Attack>();
        _connect = GetComponent<EnergyConnect>();

        _particle = _auraManager.GetComponentInChildren<ParticleSystem>();

        _auraManager.SetActive(true);
    }

    void Update()
    {
        //Debug.Log(_particle);
        if (_playerState.state == PlayerState.State.AVOIDANCE)
        {
            _particle.startSize = 0;
        }
        else
        {
            if (_connect.connectNum > 6)
            {
                _particle.startSize = 5;
            }
            else if (_connect.connectNum > 4)
            {
                _particle.startSize = 3.5f;
            }
            else if (_connect.connectNum > 2)
            {
                _particle.startSize = 2;
            }
            else if (_connect.connectNum > 0)
            {
                _particle.startSize = 1;
            }
            else
            {
                _particle.startSize = 0;
            }
        }
        //if (_attack.isCanAttack)
        //{
        //    if (_auraManager.activeSelf) return;
        //    _auraManager.SetActive(true);
        //}
        //else
        //{
        //    if (!_auraManager.activeSelf) return;
        //    _auraManager.SetActive(false);
        //}

        //if (_ownEnergyConnect.connectNum > _rivalEnergyConnect.connectNum)
        //{
        //    _auraManager.SetActive(true);
        //}
        //else
        //{
        //    _auraManager.SetActive(false);
        //}
    }
}
