using UnityEngine;
using System.Collections;

public class SkillGaugeEffect : MonoBehaviour {

    public enum PlayerNumber
    {
        One,
        Two
    }


    [SerializeField]
    private PlayerNumber _playerNum = PlayerNumber.One;

    [SerializeField]
    private GameObject[] _effectObj = null;

    private CharacterSelect.SkillType _skillType = CharacterSelect.SkillType.Random;

    void Start()
    {
        if (_playerNum == PlayerNumber.One)
        {
            _skillType = SelectManager.characterSkillType[0];
            if (_skillType == CharacterSelect.SkillType.SpeedController
                || _skillType == CharacterSelect.SkillType.SuperSpeed)
            {
                _effectObj[0].SetActive(true);
            }
            else if (_skillType == CharacterSelect.SkillType.Gravity
                || _skillType == CharacterSelect.SkillType.Tornado)
            {
                _effectObj[1].SetActive(true);
            }
            else if (_skillType == CharacterSelect.SkillType.Invisible
                || _skillType == CharacterSelect.SkillType.Parry)
            {
                _effectObj[2].SetActive(true);
            }
        }
        else
        {
            _skillType = SelectManager.characterSkillType[1];
            if (_skillType == CharacterSelect.SkillType.SpeedController
                || _skillType == CharacterSelect.SkillType.SuperSpeed)
            {
                _effectObj[0].SetActive(true);
            }
            else if (_skillType == CharacterSelect.SkillType.Gravity
                || _skillType == CharacterSelect.SkillType.Tornado)
            {
                _effectObj[1].SetActive(true);
            }
            else if (_skillType == CharacterSelect.SkillType.Invisible
                || _skillType == CharacterSelect.SkillType.Parry)
            {
                _effectObj[2].SetActive(true);
            }
        }
    }
}
