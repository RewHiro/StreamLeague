using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// キャラクター選択用のスクリプト
/// </summary>
public class CharacterSelect : MonoBehaviour
{
    //ランダム演出の際にスキルタイプを切り替える回数
    [SerializeField, Range(6, 20)]
    private int RANDOM_COUNT_END = 8;

    //ランダム演出の際にスキル選択して、次の選択までの待機時間
    [SerializeField, Range(0.05f, 0.30f)]
    private float WAIT_TIME = 0.15f;

    [SerializeField]
    private GameObject _previewPanel = null;

    [SerializeField]
    private GameObject _modelDrawPanel = null;

    [SerializeField]
    private GameObject _skillAnim = null;

    [SerializeField]
    private Animator _animator = null;

    private PlayerInput _playerInput = null;
    public PlayerInput playerInput
    {
        get { return _playerInput; }
    }

    /// <summary>
    ///スキルタイプの種類
    /// </summary>
    public enum SkillType
    {
        SpeedController,
        SuperSpeed,
        Tornado,
        Gravity,
        Invisible,
        Parry,
        Random
    }

    //スキルタイプによって呼ぶ関数を変える
    private Dictionary<SkillType, Action> _selectList = null;
    //スキルタイプによってパネルの場所を変える
    private Dictionary<SkillType, Vector3> _panelPositionList = null;

    //現在選ばれているスキルタイプ
    private SkillType _skillType = SkillType.Random;
    /// <summary>
    /// 選ばれているスキルタイプ
    /// </summary>
    public SkillType isSkillType
    {
        get { return _skillType; }
    }

    private bool _characterDecide = false;
    /// <summary>
    /// キャラが決定したかどうか
    /// </summary>
    public bool isCharacterDecide
    {
        get { return _characterDecide; }
        set { _characterDecide = value; }
    }

    private bool _productionFlug = false;
    /// <summary>
    /// ランダム演出してるときにtrueが返ってくる
    /// </summary>
    public bool isProductionRandom
    {
        get { return _productionFlug; }
    }

    private Fader _fader = null;

    private bool _previewFlug = false;
    //スキルプレビュー中かどうか
    public bool isPreviewSkill
    {
        get { return _previewFlug; }
        set { _previewFlug = value; }
    }

    //private List<GameObject> _previewPrefab = null;
    //private Dictionary<SkillType, GameObject> _previewSelect = null;

    //パネル座標
    private Vector3 SPEED_CONTROLLER_POS = new Vector3(-98, 175, 0);
    private Vector3 SUPRE_SPEED_POS = new Vector3(98, 175, 0);
    private Vector3 INVISIBLE_POS = new Vector3(192, 0, 0);
    private Vector3 PARRY_POS = new Vector3(98, -175, 0);
    private Vector3 TORNADO_POS = new Vector3(-98, -175, 0);
    private Vector3 GRAVITY_POS = new Vector3(-192, 0, 0);
    private Vector3 RANDOM_POS = Vector3.zero;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _selectList = new Dictionary<SkillType, Action>();
        _selectList.Add(SkillType.Random, Random);
        _selectList.Add(SkillType.SpeedController, SpeedController);
        _selectList.Add(SkillType.SuperSpeed, SuperSpeed);
        _selectList.Add(SkillType.Tornado, Tornado);
        _selectList.Add(SkillType.Gravity, Gravity);
        _selectList.Add(SkillType.Invisible, Invisible);
        _selectList.Add(SkillType.Parry, Parry);
 
        _panelPositionList = new Dictionary<SkillType, Vector3>();
        _panelPositionList.Add(SkillType.Random, RANDOM_POS);
        _panelPositionList.Add(SkillType.SpeedController, SPEED_CONTROLLER_POS);
        _panelPositionList.Add(SkillType.SuperSpeed, SUPRE_SPEED_POS);
        _panelPositionList.Add(SkillType.Invisible, INVISIBLE_POS);
        _panelPositionList.Add(SkillType.Parry, PARRY_POS);
        _panelPositionList.Add(SkillType.Tornado, TORNADO_POS);
        _panelPositionList.Add(SkillType.Gravity, GRAVITY_POS);

        //最初のキャラ選択配置を左右にした。
        if(_playerInput.getPlayerType == GamePadManager.Type.ONE)
        {
            _skillType = SkillType.Gravity;
        }
        else if(_playerInput.getPlayerType == GamePadManager.Type.TWO)
        {
            _skillType = SkillType.Invisible;
        }

        transform.localPosition = _panelPositionList[_skillType];

        //_previewPrefab = new List<GameObject>();
        //_previewPrefab.AddRange(Resources.LoadAll<GameObject>("Effect/PreviewPrefab"));

        //_previewSelect = new Dictionary<SkillType, GameObject>();
        //_previewSelect.Add(SkillType.Gravity, _previewPrefab[0]);
        //_previewSelect.Add(SkillType.Invisible, _previewPrefab[1]);
        //_previewSelect.Add(SkillType.Parry, _previewPrefab[2]);
        //_previewSelect.Add(SkillType.SpeedController, _previewPrefab[3]);
        //_previewSelect.Add(SkillType.SuperSpeed, _previewPrefab[4]);
        //_previewSelect.Add(SkillType.Tornado, _previewPrefab[5]);

        //if (TitleSelect.isSelectOnePlayer && _playerInput.getPlayerType != GamePadManager.Type.ONE)
        //{
        //    AiType();
        //}
    }

    void Start()
    {
        _fader = FindObjectOfType<Fader>();
    }

    /// <summary>
    /// AIの時はこちら側でスキルを決める処理
    /// </summary>
    public void AiType()
    {
        _skillType = RandomSkillType();
        _selectList[_skillType]();
        transform.localPosition = _panelPositionList[_skillType];
        _characterDecide = true;
    }


    void Update()
    {
        if (_fader.isFade) { return; }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Wait")) { return; }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Television")) { return; };
        if (SelectManager.isWaiting) { return; }
        if (SelectManager.isTitleBack) { return; }
        if (_characterDecide) { return; }
        if (_previewFlug) { return; }
        transform.localPosition = _panelPositionList[_skillType];
        if (_productionFlug) { return; }
        _selectList[_skillType]();
        //Preview();
    }

    public void LateUpdate()
    {
        if (SelectManager.isWaiting) { return; }
        if (SelectManager.isTitleBack) { return; }
        if (_productionFlug) { return; }
        if (_playerInput.IsPush(GamePadManager.ButtonType.B))
        {
            _characterDecide = false;
        }
    }

    //スキルのプレビュー
    void Preview()
    {
        if (_previewFlug) { return; }
        if(_skillType == SkillType.Random) { return; }
        //セレクトに変える
        if (_playerInput.IsPush(GamePadManager.ButtonType.BACK))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            //var previewObject = Instantiate(_previewSelect[_skillType]) as GameObject;
            //previewObject.transform.parent = _previewPanel.transform;
            //previewObject.transform.localPosition = Vector3.zero;
            _previewFlug = true;
        }

        if (_previewFlug)
        {
            _previewPanel.SetActive(true);
            _modelDrawPanel.SetActive(false);
            //_skillAnim.SetActive(true);
        }
        else
        {
            _previewPanel.SetActive(false);
            _modelDrawPanel.SetActive(true);
            //_skillAnim.SetActive(false);
        }
    }

    void Random()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.A) && !SelectManager.isTitleBack)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            StartCoroutine(RandomProduction());
        }
        else if(_playerInput.isLeftStickUpperLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.SpeedController;
        }
        else if (_playerInput.isLeftStickLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Gravity;
        }
        else if (_playerInput.isLeftStickRightDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Invisible;
        }
        else if (_playerInput.isLeftStickBackDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Tornado;
        }
    }

    void SpeedController()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.A) && !SelectManager.isTitleBack)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _characterDecide = true;
        }
        else if (_playerInput.isLeftStickRightDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.SuperSpeed;
        }
        else if (_playerInput.isLeftStickLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Gravity;
        }
        else if (_playerInput.isLeftStickBackDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Random;
        }
    }

    void SuperSpeed()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.A) && !SelectManager.isTitleBack)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _characterDecide = true;
        }
        else if (_playerInput.isLeftStickRightDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Invisible;
        }
        else if (_playerInput.isLeftStickLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.SpeedController;
        }
        else if (_playerInput.isLeftStickBackDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Random;
        }
    }

    void Tornado()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.A) && !SelectManager.isTitleBack)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _characterDecide = true;
        }
        else if (_playerInput.isLeftStickFrontDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Random;
        }
        else if (_playerInput.isLeftStickLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Gravity;
        }
        else if (_playerInput.isLeftStickRightDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Parry;
        }
    }

    void Gravity()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.A) && !SelectManager.isTitleBack)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _characterDecide = true;
        }
        else if (_playerInput.isLeftStickFrontDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.SpeedController;
        }
        else if (_playerInput.isLeftStickBackDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Tornado;
        }
        else if (_playerInput.isLeftStickRightDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Random;
        }
    }

    void Invisible()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.A) && !SelectManager.isTitleBack)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _characterDecide = true;
        }
        else if (_playerInput.isLeftStickFrontDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.SuperSpeed;
        }
        else if (_playerInput.isLeftStickBackDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Parry;
        }
        else if (_playerInput.isLeftStickLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Random;
        }
    }

    void Parry()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.A) && !SelectManager.isTitleBack)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _characterDecide = true;
        }
        else if (_playerInput.isLeftStickFrontDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Random;
        }
        else if (_playerInput.isLeftStickRightDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Invisible;
        }
        else if (_playerInput.isLeftStickLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = SkillType.Tornado;
        }
    }

    /// <summary>
    /// スキルをランダムで返す関数
    /// </summary>
    /// <returns></returns>
    private SkillType RandomSkillType()
    {
        var typeIndex = UnityEngine.Random.Range(0, 6);
        return typeIndex == 0 ? SkillType.SpeedController :
            typeIndex == 1 ? SkillType.SuperSpeed :
            typeIndex == 2 ? SkillType.Invisible :
            typeIndex == 3 ? SkillType.Parry :
            typeIndex == 4 ? SkillType.Tornado : SkillType.Gravity;
    }

    /// <summary>
    /// ランダムを選択した際は、演出を追加
    /// 何回かスキルタイプを選択した後最終決定する
    /// </summary>
    /// <returns></returns>
    private IEnumerator RandomProduction()
    {
        _productionFlug = true;
        var count = 0;
        while(count != RANDOM_COUNT_END)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _skillType = RandomSkillType();
            count++;
            yield return new WaitForSeconds(WAIT_TIME);
        }
        yield return null;
        AudioManager.instance.PlaySe(SoundName.SeName.decide);
        _characterDecide = true;
        _productionFlug = false;
        yield return null;
    }
}
