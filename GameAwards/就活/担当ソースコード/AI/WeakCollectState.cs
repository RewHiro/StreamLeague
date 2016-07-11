/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// エネルギーを集める機能
/// </summary>
public class WeakCollectState : State<WeakAI>
{

    public WeakCollectState(WeakAI weak_ai) :
        base(weak_ai)
    {
        _attack = _stateManager.GetComponent<Attack>();
        _type = _stateManager.aiInput.getPlayerType;
        _energyCreater = GameObject.FindObjectOfType<EnergyCreater>();
    }

    public override void Enter()
    {
        var energies = GameObject.FindGameObjectsWithTag(TagName.Energy).Where(energy => energy.GetComponent<WhoHaveEnergy>()._whoHave == WhoHaveEnergy.WhoHave.NONE);
        //var energies = GameObject.FindGameObjectsWithTag(TagName.Energy);

        if (!_energyCreater.enabled && energies.ToArray().Length == 0)
        {
            _stateManager.ChangeState(WeakAI.State.ATTACK);
            return;
        }

        if (energies.ToArray().Length == 0)
        {
            if (!_stateManager.isActive)
            {
                _stateManager.isActive = true;
                return;
            }
        }

        Transform target = null;

        if (energies.ToArray().Length != 0)
        {
            target = energies.ToArray()[Random.Range(0, energies.ToArray().Length)].transform;
        }
        

        if (target == _targetTransform || _stateManager.isActive)
        {
            var randomEnergys = GameObject.FindGameObjectsWithTag(TagName.Energy);
            if (randomEnergys.ToArray().Length != 0)
            {
                _targetTransform = randomEnergys.ToArray()[Random.Range(0, randomEnergys.ToArray().Length)].transform;
            }
        }
        else
        {
            _targetTransform = target;
        }

        if (_targetTransform == null) return;

        _whoHaveEnergy = _targetTransform.GetComponent<WhoHaveEnergy>();

        //if ((int)_whoHaveEnergy._whoHave == (int)_type) _stateManager.ChangeState(WeakAI.State.COLLECT);

        if (SceneManager.GetActiveScene().name == "Stage02")
        {
            if (_targetTransform.parent.name == "EnergySpawn04")
            {
                _isHighPoint = true;
                _saveTargetTransform = _targetTransform;

                var position = _stateManager.transform.position;
                var passingPoint = GameObject.FindGameObjectWithTag(TagName.PassingPoint);
                var direction = passingPoint.transform.position - position;

                RaycastHit raycastHit;
                var mask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "Energy", "Player");

                if (Physics.Raycast(position, direction.normalized, out raycastHit, Mathf.Infinity, mask, QueryTriggerInteraction.Collide))
                {
                    if (raycastHit.transform.gameObject.tag.GetHashCode() == HashTagName.Wall)
                    {
                        _targetTransform = GameObject.FindGameObjectWithTag(TagName.PassingPoint01).transform;
                    }
                    else
                    {
                        _targetTransform = passingPoint.transform;
                    }
                }
                else
                {
                    _targetTransform = passingPoint.transform;
                }
            }
        }
    }

    public override void Execute()
    {
        if (_targetTransform == null) _stateManager.ChangeState(WeakAI.State.COLLECT);

        if (_isHitWall)
        {

            _time += Time.deltaTime;

            if (_time >= 1.0f)
            {
                _stateManager.ChangeState(WeakAI.State.ATTACK);
            }
        }

        var vector = _targetTransform.position - _stateManager.transform.position;
        vector.y = 0;

        _stateManager.aiInput.leftStickDirection = vector.normalized * 1.0f;

        if (GameObject.FindGameObjectsWithTag(TagName.Energy).Length == 0)
        {
            _stateManager.aiInput.leftStickDirection = Vector3.zero;
        }
    }

    public override void Exit()
    {
        _isHighPoint = false;
        _time = 0.0f;
    }

    public override void OnCollisionEnter(Collision collision)
    {
    }

    public override void OnCollisionStay(Collision collision)
    {
        HitWall(collision);
    }

    public override void OnCollisionExit(Collision collision)
    {
        OutHitWall(collision);
    }

    public override void OnTriggerEnter(Collider other)
    {
        HitPoint(other);
        HitPassingPoint(other);
        HitPassingPoint01(other);
    }

    public override void OnTriggerStay(Collider other)
    {
        HitEnergy(other);
    }

    public override void OnTriggerExit(Collider other)
    {

    }

    void HitEnergy(Collider other)
    {
        if (other.tag.GetHashCode() != HashTagName.Energy) return;
        if (_targetTransform == null) return;
        if (other.gameObject != _targetTransform.gameObject) return;
        _stateManager.ChangeState(WeakAI.State.COLLECT);
    }

    void HitPoint(Collider other)
    {
        if (_isHighPoint) return;
        if (other.tag.GetHashCode() != HashTagName.Energy) return;
        var rand = UnityEngine.Random.Range(0, 10);
        if (rand <= 4)
        {
            _stateManager.ChangeState(WeakAI.State.WAIT);
        }
        else
        {
            _stateManager.ChangeState(WeakAI.State.COLLECT);
        }
    }

    void HitPassingPoint(Collider other)
    {
        if (!_isHighPoint) return;
        if (other.tag.GetHashCode() != HashTagName.PassingPoint) return;
        _targetTransform = _saveTargetTransform;
        _isHighPoint = false;
    }

    void HitPassingPoint01(Collider other)
    {
        if (!_isHighPoint) return;
        if (other.tag.GetHashCode() != HashTagName.PassingPoint01) return;
        _targetTransform = GameObject.FindGameObjectWithTag(TagName.PassingPoint).transform;
    }

    void HitWall(Collision other)
    {
        var hash = other.gameObject.tag.GetHashCode();

        if (hash != HashTagName.Wall) return;

        _isHitWall = true;
    }

    void OutHitWall(Collision other)
    {
        var hash = other.gameObject.tag.GetHashCode();

        if (hash != HashTagName.Wall) return;

        _isHitWall = false;
    }

    GamePadManager.Type _type = GamePadManager.Type.ALL;
    Transform _targetTransform = null;
    Attack _attack = null;
    WhoHaveEnergy _whoHaveEnergy = null;
    Transform _saveTargetTransform = null;
    EnergyCreater _energyCreater = null;
    float _time = 0.0f;

    bool _isHighPoint = false;
    bool _isHitWall = false;

}