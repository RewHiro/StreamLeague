/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// プレイヤーがエネルギーに当たったら色を変更する機能
/// </summary>
public class HitChangeColor : MonoBehaviour
{
    [SerializeField]
    GameObject _drainEffectPrefab = null;

    /// <summary>
    /// エネルギーを誰も取られていない見た目にする
    /// </summary>
    public void Reset()
    {
        foreach (var energy in _enegies)
        {
            energy.SetActive(false);
        }

        _enegies[_enegies.Count - 1].SetActive(true);
    }

    /// <summary>
    /// エネルギーの色を変更
    /// </summary>
    /// <param name="player_type">プレイヤーの種類</param>
    /// <param name="skill_type">キャラのスキルタイプ</param>
    public void ChangeColor(GamePadManager.Type player_type, CharacterSelect.SkillType skill_type)
    {
        foreach (var energy in _enegies)
        {
            energy.SetActive(false);
        }

        switch (skill_type)
        {
            case CharacterSelect.SkillType.SpeedController:
            case CharacterSelect.SkillType.SuperSpeed:
                if (player_type == GamePadManager.Type.ONE)
                {
                    _enegies[0].SetActive(true);
                }
                else
                {
                    _enegies[3].SetActive(true);
                }
                break;

            case CharacterSelect.SkillType.Tornado:
            case CharacterSelect.SkillType.Gravity:
                if (player_type == GamePadManager.Type.ONE)
                {
                    _enegies[1].SetActive(true);
                }
                else
                {
                    _enegies[4].SetActive(true);
                }
                break;

            case CharacterSelect.SkillType.Invisible:
            case CharacterSelect.SkillType.Parry:
                if (player_type == GamePadManager.Type.ONE)
                {
                    _enegies[2].SetActive(true);
                }
                else
                {
                    _enegies[5].SetActive(true);
                }
                break;
        }

    }

    //---------------------------------------------------------------------------------------

    List<GameObject> _enegies = new List<GameObject>();
    WhoHaveEnergy _whoHaveEnergy = null;

    bool _isHit = false;

    void Start()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            _enegies.Add(transform.GetChild(i).gameObject);
        }

        _whoHaveEnergy = GetComponent<WhoHaveEnergy>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.GetHashCode() != HashTagName.Player) return;
        var playerState = collider.GetComponent<PlayerState>();
        if (playerState.state == PlayerState.State.ATTACK ||
            playerState.state == PlayerState.State.DEFENSE) return;

        var playerType = collider.GetComponent<InputBase>().getPlayerType;
        if ((int)_whoHaveEnergy._whoHave == (int)playerType) return;

        var skillType = collider.GetComponent<CharacterParameter>().getParameter.skillType;

        ChangeColor(playerType, skillType);


        var effect = Instantiate(_drainEffectPrefab);
        effect.transform.SetParent(collider.transform);
        effect.transform.position = collider.transform.position;

        AudioManager.instance.PlaySe(SoundName.SeName.energydrain);
    }
}