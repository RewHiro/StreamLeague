/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// 集める機能
/// </summary>
public class NormalCollectState : State<NormalAI>
{
    int attackNum = 0;

    public NormalCollectState(NormalAI normal_ai) :
        base(normal_ai)
    {
        var players = GameObject.FindGameObjectsWithTag(TagName.Player);

        _type = _stateManager.GetComponent<AIInput>().getPlayerType;
        _attack = _stateManager.GetComponent<Attack>();
        _playerState = _stateManager.GetComponent<PlayerState>();
    }

    public override void Enter()
    {
        var type = (WhoHaveEnergy.WhoHave)_type;
        var energies = GameObject.FindGameObjectsWithTag(TagName.Energy).Where(energy => energy.GetComponent<WhoHaveEnergy>()._whoHave != type);

        attackNum = UnityEngine.Random.Range(0, 8);

        if (energies.ToArray().Length == 0)
        {
            _stateManager.ChangeState(NormalAI.State.ATTACK);
            return;
        }

        var target = energies.NearestObject(_stateManager.transform.position);


        if (target == _targetTransform)
        {
            var randomEnergys = GameObject.FindGameObjectsWithTag(TagName.Energy);
            _targetTransform = randomEnergys.ToArray()[Random.Range(0, energies.Count() - 1)].transform;
        }
        else
        {
            _targetTransform = target;
        }



        if (_targetTransform == null) _stateManager.ChangeState(NormalAI.State.COLLECT);

        _whoHaveEnergy = _targetTransform.GetComponent<WhoHaveEnergy>();

        if ((int)_whoHaveEnergy._whoHave == (int)_type) _stateManager.ChangeState(NormalAI.State.COLLECT);

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

                if (Physics.Raycast(position, direction.normalized, out raycastHit, 50.0f, mask, QueryTriggerInteraction.Collide))
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
        if (_targetTransform == null) _stateManager.ChangeState(NormalAI.State.COLLECT);

        if (_isHitWall)
        {
            _time += Time.deltaTime;

            if (_time >= 1.0f)
            {
                _stateManager.ChangeState(NormalAI.State.ATTACK);
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

    void HitEnergy(Collider other)
    {

        if (other.tag.GetHashCode() != HashTagName.Energy) return;
        if (other.gameObject != _targetTransform.gameObject) return;
        _stateManager.ChangeState(NormalAI.State.COLLECT);
    }

    void HitPoint(Collider other)
    {
        if (_isHighPoint) return;
        if (other.tag.GetHashCode() != HashTagName.Energy) return;

        var type = (WhoHaveEnergy.WhoHave)_type;
        var energies = GameObject.FindGameObjectsWithTag(TagName.Energy).Where(energy => energy.GetComponent<WhoHaveEnergy>()._whoHave != type);
        if (attackNum == energies.ToArray().Length)
        {
            _stateManager.ChangeState(NormalAI.State.ATTACK);
        }
        else
        {
            var rand = UnityEngine.Random.Range(0, 10);
            if (rand <= 3)
            {
                _stateManager.ChangeState(NormalAI.State.WAIT);
            }
            else
            {
                _stateManager.ChangeState(NormalAI.State.COLLECT);
            }
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

    void HitStay(Collider other)
    {
        if (other.tag.GetHashCode() != HashTagName.Energy) return;
        if ((int)_whoHaveEnergy._whoHave == (int)_type) return;
        var point = GameObject.FindGameObjectWithTag(TagName.Finish);
        _saveTargetTransform = _targetTransform;
        _targetTransform = point.transform;
    }

    void HitFinishPoint(Collider other)
    {
        if (_isHighPoint) return;
        if (_saveTargetTransform == null) return;
        if (other.tag.GetHashCode() != HashTagName.Finish) return;
        _targetTransform = _saveTargetTransform;
        _saveTargetTransform = null;
    }

    GamePadManager.Type _type = GamePadManager.Type.ALL;
    Transform _targetTransform = null;
    Attack _attack = null;
    WhoHaveEnergy _whoHaveEnergy = null;
    Transform _saveTargetTransform = null;
    PlayerState _playerState = null;
    bool _isHitWall = false;
    
    float _time = 0.0f;

    bool _isHighPoint = false;
}
