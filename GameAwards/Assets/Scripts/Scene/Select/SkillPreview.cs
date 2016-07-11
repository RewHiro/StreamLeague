using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SkillPreview : MonoBehaviour
{

    private CharacterSelect _selectPanel = null;

    private AIInput _aiInput = null;

    private Dictionary<CharacterSelect.SkillType, Action> _type = null;

    private CharacterSelect.SkillType _skillType = CharacterSelect.SkillType.SpeedController;

    private float PREVIEW_TIME = 0.0f;
    private Dictionary<CharacterSelect.SkillType, float> PREVIEW_END_TIME = null;

    private float _changeDirectionTime = 0.0f;

    private Transform _targetTransform = null;

    private Parry _skill = null;
    void Awake()
    {
        
        _aiInput = GetComponent<AIInput>();
        _type = new Dictionary<CharacterSelect.SkillType, Action>();
        _type.Add(CharacterSelect.SkillType.SpeedController, SpeedController);
        _type.Add(CharacterSelect.SkillType.SuperSpeed, SuperSpeed);
        _type.Add(CharacterSelect.SkillType.Invisible, Invisible);
        _type.Add(CharacterSelect.SkillType.Parry, Parry);
        _type.Add(CharacterSelect.SkillType.Tornado, Tornado);
        _type.Add(CharacterSelect.SkillType.Gravity, Gravity);

        PREVIEW_END_TIME = new Dictionary<CharacterSelect.SkillType, float>();
        PREVIEW_END_TIME.Add(CharacterSelect.SkillType.SpeedController,8.0f);
        PREVIEW_END_TIME.Add(CharacterSelect.SkillType.SuperSpeed, 8.0f);
        PREVIEW_END_TIME.Add(CharacterSelect.SkillType.Invisible, 6.5f);
        PREVIEW_END_TIME.Add(CharacterSelect.SkillType.Parry, 3.0f);
        PREVIEW_END_TIME.Add(CharacterSelect.SkillType.Tornado, 9.5f);
        PREVIEW_END_TIME.Add(CharacterSelect.SkillType.Gravity, 13.0f);

        foreach (var test in FindObjectsOfType<CharacterSelect>())
        {
            if (test.playerInput.getPlayerType == GamePadManager.Type.ONE)
            {
                _selectPanel = test;
                _skillType = test.isSkillType;
            }
            else
            {
                _selectPanel = test;
                _skillType = test.isSkillType;
            }
        }

        PREVIEW_TIME = PREVIEW_END_TIME[_skillType];
        if(_skillType == CharacterSelect.SkillType.SpeedController)
        {
            _aiInput.skill = true; //スキル発動に変える
            _aiInput.leftStickDirection = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f));
        }
        else if (_skillType == CharacterSelect.SkillType.SuperSpeed)
        {
            _aiInput.skill = true; //スキル発動に変える
            _aiInput.leftStickDirection = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f));
        }
    }

    void Start()
    {
        if (_skillType == CharacterSelect.SkillType.Parry)
        {
            _skill = GetComponent<Parry>();
            _skill.attack.isInRange = true;
            _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    void Update()
    {
        if (_skillType == CharacterSelect.SkillType.Random) { return; }
        if (!_selectPanel.isPreviewSkill) { return; }
        PREVIEW_TIME -= Time.deltaTime;
        _type[_skillType]();

        if (PREVIEW_TIME < 0.0f)
        {
            Destroy(gameObject.transform.parent.gameObject);
            _selectPanel.isPreviewSkill = false;
        }

    }

    void LateUpdate()
    {
        if (_selectPanel.playerInput.IsPush(GamePadManager.ButtonType.B))
        {
            Destroy(gameObject.transform.parent.gameObject);
            _selectPanel.isPreviewSkill = false;
            if(_skillType == CharacterSelect.SkillType.Tornado)
            {
                Destroy(GameObject.FindGameObjectWithTag("Effect"));
            }
        }
    }

    void SpeedController()
    {
        _changeDirectionTime += Time.deltaTime;
        if (_changeDirectionTime > 1.0f)
        {
            _aiInput.leftStickDirection = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f));
            _changeDirectionTime = 0.0f;
        }
    }

    void SuperSpeed()
    {
        _changeDirectionTime += Time.deltaTime;
        if(_changeDirectionTime > 1.0f)
        {
            _aiInput.leftStickDirection = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f));
            _changeDirectionTime = 0.0f;
        }
    }

    void Invisible()
    {
        _aiInput.skill = true;
    }

    void Parry()
    {
        
        var direction = _targetTransform.position - transform.position;
        direction.Normalize();
        if (direction.x > 0.0f)
        {
            _aiInput.skill = true;
        }
        else if (direction.x < 0.0f)
        {
            _aiInput.skill = true;
        }
        else if (direction.z > 0.0f)
        {
            _aiInput.skill = true;
        }
        else if (direction.z < 0.0f)
        {
            _aiInput.skill = true;
        }



    }

    void Tornado()
    {
        _aiInput.skill = true;
    }

    void Gravity()
    {
        _aiInput.skill = true;
    }
}
