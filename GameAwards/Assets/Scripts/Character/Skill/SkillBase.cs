using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillBase : MonoBehaviour {

    public bool skillActive
    {
        get
        {
            return _skillActive;
        }
    }

    /// <summary>
    /// スキルを使用できるようになるまでの時間
    /// </summary>
    [SerializeField, Tooltip("次にスキルを使えるようになるまでの時間")]
    protected float COOL_TIME = 5.0f;
    public float EndCoolTime
    {
        get { return COOL_TIME; }
        set { COOL_TIME = value; }
    }

    private float _coolTime = 0.0f;
    public float nowCoolTime
    {
        get { return _coolTime; }
        set { _coolTime = value; }
    }

    /// <summary>
    /// スキルを発動している時間
    /// </summary>
    protected float _activeTime = 0.0f;

    [SerializeField, Tooltip("スキルが発動している時間")]
    protected float SKILL_ACTIVE_TIME = 5.0f;
    /// <summary>
    /// スキル発動時間
    /// </summary>
    public float SkillTime
    {
        get { return SKILL_ACTIVE_TIME; }
        set { SKILL_ACTIVE_TIME = value; }
    }


    /// <summary>
    /// スキルを発動しているかどうか
    /// </summary>
    //[SerializeField]
    protected bool _skillActive = true;

    /// <summary>
    /// プレイヤーかどうか
    /// </summary>
    [SerializeField]
    bool _isPlayer = true;

    /// <summary>
    /// InputBase
    /// </summary>
    protected InputBase _input = null;

    /// <summary>
    /// effectのPrfabを入れてるList
    /// </summary>
    [SerializeField]
    protected List<GameObject> _skilleffect = null;

    private bool _skillsIn = true;
    /// <summary>
    /// スキルが発動してる間のみtrueを返す(CoolTime中はfalse)
    /// </summary>
    public bool isSkillsIn
    {
        get { return _skillsIn; }
    }

    virtual public void Start()
    {
        //プレイヤーだったらPlayerのインプット
        //AIだったらAIのインプット
        _input = GetComponent<InputBase>();
        StartCoroutine(SKillCoolTime());
    }

    protected void PrefabsLoad()
    {
        _skilleffect = new List<GameObject>();
        _skilleffect.AddRange(Resources.LoadAll<GameObject>("Character/SkillEffectPrefab"));
    }

    /// <summary>
    /// スキルを発動した瞬間に呼ぶ関数
    /// </summary>
    public virtual void SkillStart()
    {
        AudioManager.instance.PlaySe(SoundName.SeName.skillactivate);
        _skillActive = true;
        _skillsIn = true;
        _coolTime = 0.0f;
    }

    /// <summary>
    /// スキルのクールタイムまで終わったら呼ばれる関数
    /// </summary>
    protected virtual void SkillEnd()
    {
        _activeTime = 0.0f;
        _coolTime = COOL_TIME;
        _skillActive = false;
    }

    public virtual void SkillCansel()
    {
        
    }

    public virtual void AnotherSkillStart()
    {

    }

    /// <summary>
    /// スキルのクールタイムを実装
    /// </summary>
    /// <returns></returns>
    protected IEnumerator SKillCoolTime()
    {
        _skillsIn = false;
        while(_coolTime < COOL_TIME)
        {
            _coolTime += Time.deltaTime;
            yield return null;
        }
        SkillEnd();
    }
}
