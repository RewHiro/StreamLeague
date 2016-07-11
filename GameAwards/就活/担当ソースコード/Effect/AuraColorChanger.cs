/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// オーラの色を変更する機能
/// </summary>
public class AuraColorChanger : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] _AuraPrefabs = null;

    void Start()
    {

        var particelSystem = GetComponent<ParticleSystem>();

        var skillType = GetComponentInParent<CharacterParameter>().getParameter.skillType;
        var playerType = GetComponentInParent<InputBase>().getPlayerType;

        switch (skillType)
        {
            case CharacterSelect.SkillType.SpeedController:
            case CharacterSelect.SkillType.SuperSpeed:
                if (playerType == GamePadManager.Type.ONE)
                {
                    particelSystem = _AuraPrefabs[0];
                }
                else
                {
                    particelSystem = _AuraPrefabs[3];
                }

                break;

            case CharacterSelect.SkillType.Gravity:
            case CharacterSelect.SkillType.Tornado:
                if (playerType == GamePadManager.Type.ONE)
                {
                }
                else
                {
                }
                break;

            case CharacterSelect.SkillType.Invisible:
            case CharacterSelect.SkillType.Parry:
                if (playerType == GamePadManager.Type.ONE)
                {
                }
                else
                {
                }
                break;
        }
    }
}