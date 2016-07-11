using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class CharacterParameter : MonoBehaviour {

    [Serializable]
    public struct JsonData
    {
        public float hp;
        public float speed;
        public CharacterSelect.SkillType skillType;
        public Color color;

        //速度調整
        public float speedControllerCoolTime;
        public float speedControllerActiveTime;
        public float speedControllerMoveSpeedUp;
        public float speedcontrollerMaxUpSpeed;

        //超スピード
        public float superSpeedCoolTime;
        //public float superSpeedspeed;
        //public int superSpeedMoveCount;
        public float superSpeedMoveSpeed;
        public float superSpeedMoveLength;

        //トルネード
        public float tornadoCoolTime;
        public float tornadoActiveTime;
        public int tornadoPower;

        //グラビティ
        public float gravityCoolTime;
        public float gravityActiveTime;
        public float gravityExtent;

        //インビジブル
        public float invisibleCoolTime;
        public float invisibleActiveTime;
        public float invisibleTransmission;
        public float invisibleAccelCount;
        public float invisibleMoveValue;

        //パリィ
        public float parryCoolTime;
        public float parryPower;
        public float parryRange;
    }

    [SerializeField]
    JsonData _jsonData = new JsonData();

    public JsonData getParameter
    {
        get
        {
            return _jsonData;
        }
    }

    private CharacterSelect.SkillType _skillType = CharacterSelect.SkillType.Random;

    void Start()
    {
        if (GetComponent<InputBase>().getPlayerType == GamePadManager.Type.ONE)
        {
            _skillType = SelectManager.characterSkillType[0];
        }
        else if(GetComponent<InputBase>().getPlayerType == GamePadManager.Type.TWO)
        {
            _skillType = SelectManager.characterSkillType[1];
        }
        ReadJson();
    }

    void  ReadJson()
    {
        //var path = Application.dataPath + "/Json/Parameter.json";
        //if (!File.Exists(path)) return;
        //StreamReader sr = new StreamReader(path);
        //var data = JsonUtility.FromJson<JsonData>(sr.ReadToEnd());
        //sr.Close();

        var textAsset = Resources.Load<TextAsset>("Json/Parameter");
        var data = JsonUtility.FromJson<JsonData>(textAsset.text);

        //データを代入する
        var hp = GetComponent<HpManager>();
        hp.setMaxHP = data.hp;
        var speed = GetComponent<Move>();
        speed.speed = data.speed;
        if(_skillType == CharacterSelect.SkillType.SpeedController)
        {
            var skill = GetComponent<SpeedController>();
            skill.EndCoolTime = data.speedControllerCoolTime;
            skill.SkillTime = data.speedControllerActiveTime;
            skill.moveSpeedUp = data.speedControllerMoveSpeedUp;
            skill.maxUpSpeed = data.speedcontrollerMaxUpSpeed;
        }
        else if(_skillType == CharacterSelect.SkillType.SuperSpeed)
        {
            var skill = GetComponent<SuperSpeed>();
            skill.moveLength = data.superSpeedMoveLength;
            skill.moveSpeed = data.superSpeedMoveSpeed;
            skill.EndCoolTime = data.superSpeedCoolTime;
        }
        else if(_skillType == CharacterSelect.SkillType.Tornado)
        {
            var skill = GetComponent<Tornado>();
            skill.EndCoolTime = data.tornadoCoolTime;
            skill.SkillTime = data.tornadoActiveTime;
            skill.power = data.tornadoPower;
        }
        else if(_skillType == CharacterSelect.SkillType.Gravity)
        {
            var skill = GetComponent<Gravity>();
            skill.EndCoolTime = data.gravityCoolTime;
            skill.SkillTime = data.gravityActiveTime;
            skill.extent = data.gravityExtent;
        }
        else if(_skillType == CharacterSelect.SkillType.Invisible)
        {
            var skill = GetComponent<Invisible>();
            skill.MoveValue = data.invisibleMoveValue;
            skill.AccelCount = data.invisibleAccelCount;
            skill.Transmission = data.invisibleTransmission;
            skill.EndCoolTime = data.invisibleCoolTime;
            skill.SkillTime = data.invisibleActiveTime;
        }
        else if(_skillType == CharacterSelect.SkillType.Parry)
        {
            var skill = GetComponent<Parry>();
            skill.EndCoolTime = data.parryCoolTime;
            skill.power = data.parryPower;
            skill.parryRange = data.parryRange;
        }
    }

}
