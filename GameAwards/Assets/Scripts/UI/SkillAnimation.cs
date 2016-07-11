using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillAnimation : MonoBehaviour
{
    [SerializeField]
    GamePadManager.Type _type;

    private Animator _animator = null;

    private CharacterSelect.SkillType _skillType = CharacterSelect.SkillType.SpeedController;

    [SerializeField, Tooltip("デバッグでアニメーション確認用")]
    private CharacterSelect.SkillType DEBUG_TEST_SKILL_ANIM = CharacterSelect.SkillType.SpeedController;

    private List<RuntimeAnimatorController> _animators = null;

    [SerializeField]
    private CharacterSelect _select = null;

    [SerializeField]
    private bool _selectScene = false;

    void Start()
    {
        _animators = new List<RuntimeAnimatorController>();
        _animators.AddRange(Resources.LoadAll<RuntimeAnimatorController>("UI/Stick/SKillAnimator"));
        _animator = GetComponent<Animator>();

        foreach (var input in FindObjectsOfType<InputBase>())
        {
            if (_type != input.getPlayerType) continue;
            if(SelectManager.characterSkillType == null)
            {
                _skillType = DEBUG_TEST_SKILL_ANIM;
            }
            else if(_type == GamePadManager.Type.ONE)
            {
                _skillType = SelectManager.characterSkillType[0];
            }
            else if(_type == GamePadManager.Type.TWO)
            {
                _skillType = SelectManager.characterSkillType[1];
            }
        }

        SelectSkillAnimation();
    }

    void SelectSkillAnimation()
    {
        if (_skillType == CharacterSelect.SkillType.SpeedController || _skillType == CharacterSelect.SkillType.SuperSpeed)
        {
            _animator.runtimeAnimatorController = _animators[3];
        }
        else if (_skillType == CharacterSelect.SkillType.Gravity)
        {
            _animator.runtimeAnimatorController = _animators[0];
        }
        else if (_skillType == CharacterSelect.SkillType.Tornado)
        {
            _animator.runtimeAnimatorController = _animators[4];
        }
        else if (_skillType == CharacterSelect.SkillType.Invisible)
        {
            _animator.runtimeAnimatorController = _animators[1];
        }
        else if (_skillType == CharacterSelect.SkillType.Parry)
        {
            _animator.runtimeAnimatorController = _animators[2];
        }
        else
        {
            //Debug.Log("Skill Not Select");
        }
    }

    void Update()
    {
        if (!_selectScene) { return; }
        _skillType = _select.isSkillType;
        if(_skillType == CharacterSelect.SkillType.Random) { return; }
        SelectSkillAnimation();
    }

}
