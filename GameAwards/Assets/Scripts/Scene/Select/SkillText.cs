using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SkillText : MonoBehaviour
{

    enum Type
    {
        Name,
        Description,
        Mix
    }

    [SerializeField]
    private CharacterSelect _select = null;

    [SerializeField]
    private Type _type = Type.Name;

    private Text _text = null;

    private Dictionary<Type, Action> _textType = null;

    [SerializeField]
    private GameObject[] _descriptionObj = null;

    //テキストを追加
    private char[] _randomText = "ランダムでキャラを選択する".ToCharArray();
    private char[] _speedControllerText = "加速して移動速度が\n速くなる".ToCharArray();
    private char[] _superSpeedText = "超加速して前方に\n瞬時に移動する".ToCharArray();
    private char[] _invisibleText = "一定時間姿を消し\n無敵になる".ToCharArray();
    private char[] _parryText = "衝撃波を発生させ\n相手を吹き飛ばす".ToCharArray();
    private char[] _gravityText = "範囲に入った相手の\n速度を遅くする".ToCharArray();
    private char[] _tornadoText = "相手をトルネードで\n吹き飛ばす".ToCharArray();

    private CharacterSelect.SkillType _prevSkillType = CharacterSelect.SkillType.Random;

    [SerializeField, Range(0.01f, 0.2f)]
    private float _textPopTime = 0.2f;
    private float _textMoveCount = 0;
    private int _count = 0;

    [SerializeField]
    private Vector3 RANDOM_POS;
    [SerializeField]
    private Vector3 TEXT_POS;

    void Start()
    {
        _text = GetComponent<Text>();
        _textType = new Dictionary<Type, Action>();
        _textType.Add(Type.Name, Name);
        _textType.Add(Type.Description, Description);
        _textType.Add(Type.Mix, Mix);
    }

    void Update()
    {
        _textType[_type]();
    }

    void Name()
    {
        if (_select.isSkillType == CharacterSelect.SkillType.Random)
        {
            _text.text = "ランダム";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.SpeedController)
        {
            _text.text = "ハイスピード";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Tornado)
        {
            _text.text = "トルネード";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Invisible)
        {
            _text.text = "インビジブル";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.SuperSpeed)
        {
            _text.text = "ブリンク";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Parry)
        {
            _text.text = "パリィ";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Gravity)
        {
            _text.text = "グラビティ";
        }
    }

    void TextCount(ref char[] text)
    {
        if(_prevSkillType != _select.isSkillType)
        {
            _count = 0;
            _text.text = "";
            _prevSkillType = _select.isSkillType;
        }
        if (_count > text.Length)
        {
            _count = text.Length;
        }
        _textMoveCount += Time.deltaTime;
        if(_textMoveCount > _textPopTime)
        {
            _textMoveCount = 0.0f;
            if (_count < text.Length) _text.text += text[_count];
            _count++;
        }
    }

    void Description()
    {
        if (_select.isSkillType == CharacterSelect.SkillType.Random)
        {
            _descriptionObj[0].SetActive(false);
            _descriptionObj[1].SetActive(false);
            transform.localPosition = RANDOM_POS;
            TextCount(ref _randomText);
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.SpeedController)
        {
            _descriptionObj[0].SetActive(true);
            _descriptionObj[1].SetActive(true);
            transform.localPosition = TEXT_POS;
            TextCount(ref _speedControllerText);
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Tornado)
        {
            _descriptionObj[0].SetActive(true);
            transform.localPosition = TEXT_POS;
            _descriptionObj[1].SetActive(true);
            TextCount(ref _tornadoText);
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Invisible)
        {
            _descriptionObj[0].SetActive(true);
            _descriptionObj[1].SetActive(true);
            transform.localPosition = TEXT_POS;
            TextCount(ref _invisibleText);
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.SuperSpeed)
        {
            _descriptionObj[0].SetActive(true);
            _descriptionObj[1].SetActive(true);
            transform.localPosition = TEXT_POS;
            TextCount(ref _superSpeedText);
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Parry)
        {
            _descriptionObj[0].SetActive(true);
            _descriptionObj[1].SetActive(true);
            transform.localPosition = TEXT_POS;
            TextCount(ref _parryText);
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Gravity)
        {
            _descriptionObj[0].SetActive(true);
            _descriptionObj[1].SetActive(true);
            transform.localPosition = TEXT_POS;
            TextCount(ref _gravityText);
        }
    }

    void Mix()
    {
        if (_select.isSkillType == CharacterSelect.SkillType.Random)
        {
            _text.text = "ランダム\n\nランダムで\nキャラを選択するぞ！";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.SpeedController)
        {
            _text.text = "速度調整\n\n移動速度を調整できるスキル\n操作\n右スティックを上下に弾くと発動";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Tornado)
        {
            _text.text = "トルネード\n\n相手を吸い寄せ引き離し出来るスキル\n操作\n右スティック回転させると発動\n右回転：吹き飛ばし\n左回転：吸い寄せ";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Invisible)
        {
            _text.text = "インビジブル\n\n姿を消すことが出来るスキル\n操作\n右スティックを左右に連続で倒す";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.SuperSpeed)
        {
            _text.text = "超スピード\n\n超スピードで移動できるスキル\n操作\n右スティックを弾くと発動\nスキル発動中は右スティックで操作\n弾いた方向へ移動する";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Parry)
        {
            _text.text = "はじき\n\n相手の攻撃を弾くスキル\n操作\n相手の攻撃が当たる瞬間に\n相手の方向に右スティックを弾く";
        }
        else if (_select.isSkillType == CharacterSelect.SkillType.Gravity)
        {
            _text.text = "グラビティ\n\n範囲に入った相手の速度を変えるスキル\n操作\n右スティックを押し込みで効果範囲拡大\n右スティック上下弾きで発動\n上：相手が速くなるゾーン発動\n下：相手が遅くなるゾーン発動";
        }
    }
}
