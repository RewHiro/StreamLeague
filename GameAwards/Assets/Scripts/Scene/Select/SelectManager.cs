using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// セレクトシーンの管理クラス
/// </summary>
public class SelectManager : MonoBehaviour
{

    [SerializeField, Tooltip("0:キャラクターセレクト、1:ステージセレクト")]
    private GameObject[] _selectScene = null;

    [SerializeField, Tooltip("0:１Pのセレクトパネル、1:２Pのセレクトパネル")]
    private CharacterSelect[] _characterSelectPanel = null;

    private static CharacterSelect.SkillType[] _skillType;
    /// <summary>
    /// 選択したスキルを保持しておく
    /// ゲームシーンでここからロードされる
    /// </summary>
    public static CharacterSelect.SkillType[] characterSkillType
    {
        get { return _skillType; }
    }

    [SerializeField, Tooltip("ステージ選択パネル")]
    private StageSelect _stagePanel = null;

    [SerializeField, Tooltip("キャラ選択完了したときの表示するパネル")]
    private GameObject _gameStartPanel = null;

    [SerializeField, Tooltip("タイトルに戻るときに表示するパネル")]
    private GameObject _titleBackPanel = null;

    [SerializeField, Tooltip("タイトルに戻るかどうかのときの選択パネル")]
    private GameObject _titleBackSelectPanel = null;

    [SerializeField]
    private SelectPanelScaler _iconManager = null;

    private static bool _titleBack = false;
    /// <summary>
    /// タイトルパネルが表示されてるかどうか
    /// </summary>
    public static bool isTitleBack
    {
        get { return _titleBack; }
    }

    //タイトルバックパネルの選択がどちらにあるか調べるbool
    private bool _titleBackYes = true;
    public bool isTitleBackYes
    {
        get { return _titleBackYes; }
    }

    private PlayerInput _playerInput = null;

    private bool _stageSelect = false;
    /// <summary>
    /// 現在ステージセレクトが表示されてるかどうか
    /// </summary>
    public bool isStageSelect
    {
        get { return _stageSelect; }
        set { _stageSelect = value; }
    }

    private static bool _waiting = false;
    /// <summary>
    /// シーンが変わるまでの待機中かどうか
    /// </summary>
    public static bool isWaiting
    {
        get { return _waiting; }
    }

    private Fader _fader = null;

    private Dictionary<StageSelect.StageType, SceneName.Name> _sceneName = null;

    private bool _start = true;
    private bool _end = false;
    private bool _stageIconMove = false;

    [SerializeField]
    private Animator[] _animator = null;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _skillType = new CharacterSelect.SkillType[2];
        _fader = FindObjectOfType<Fader>();
        _sceneName = new Dictionary<StageSelect.StageType, SceneName.Name>();
        _sceneName.Add(global::StageSelect.StageType.Stage1, SceneName.Name.Stage02);
        _sceneName.Add(global::StageSelect.StageType.Stage2, SceneName.Name.Stage03);
        _sceneName.Add(global::StageSelect.StageType.Stage3, SceneName.Name.Stage01);
        AudioManager.instance.PlayBgm(SoundName.BgmName.select);
        if(TitleSelect.isSelectOnePlayer)
        {
            _characterSelectPanel[1].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.THREE;
        }
    }

    void Update()
    {
        //フェイド状態なら操作出来ないように
        if (_fader.isFade) { return; }
        if (_animator[0].enabled) _animator[0].SetBool("Start", _start);
        _start = false;
        if (_waiting) { return; }
        CharacterSelect(); //キャラクターセレクト関連
        StageSelect(); //ステージセレクト関連
        TitleBackSelect(); //タイトルに戻る関連
    }

    /// <summary>
    /// Aを押した際にキャラクター側まで反応してしまうため
    /// 最後にUpdateを呼ばせるため
    /// </summary>
    public void LateUpdate()
    {
        if (!_titleBack) return;
        if (_playerInput.IsPush(GamePadManager.ButtonType.A))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            if (_titleBackYes)
            {
                _titleBack = false;
                AudioManager.instance.StopBgm();
                SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Title);
            }
            else
            {
                _titleBackPanel.SetActive(false);
                _titleBack = false;
            }
        }

    }

    /// <summary>
    /// ステージ選択中に呼ばれる関数
    /// </summary>
    void StageSelect()
    {
        if (!_stageSelect) { return; }
        if (_titleBack) { return; }
        if (_end) { return; }
        if (!_stageIconMove)
        {
            if (_animator[2].GetCurrentAnimatorStateInfo(0).IsName("StageIcon"))
            {
                _animator[2].SetBool("Move", _stageIconMove);
                _stageIconMove = true;
            }
        }
        else
        {
            _animator[2].SetBool("Move", _stageIconMove);
        }

        if (_playerInput.IsPush(GamePadManager.ButtonType.B))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cansel);
            if (!_stagePanel.isDecideStage)
            {
                _characterSelectPanel[0].isCharacterDecide = false;
                _characterSelectPanel[1].isCharacterDecide = false;
                if (TitleSelect.isSelectOnePlayer)
                {
                    _characterSelectPanel[0].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.ONE;
                    _characterSelectPanel[1].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.THREE;
                }

                _iconManager.isScaleDownFlug[0] = false;
                _iconManager.isScaleDownFlug[1] = false;
                _stageSelect = false;
                SceneSelect();
            }
            else
            {
                _stagePanel.isDecideStage = false;
            }
        }

        if (_stagePanel.isDecideStage)
        {
            _gameStartPanel.SetActive(true);
        }
        else
        {
            _gameStartPanel.SetActive(false);
        }

        if (_stagePanel.isDecideStage && _playerInput.IsPush(GamePadManager.ButtonType.START))
        {
            _end = true;
            _animator[1].SetBool("Change", _end);
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            StartCoroutine(GameStart(_sceneName[_stagePanel.isStageType]));
        }


    }

    //待機してからシーン移行
    private IEnumerator GameStart(SceneName.Name name)
    {
        _waiting = true;
        yield return new WaitForSeconds(0.5f);
        _waiting = false;
        AudioManager.instance.StopBgm();
        SceneManagerUtility.instance.StartTransition(1.0f, name);
    }

    /// <summary>
    /// キャラクターセレクト中の関数
    /// </summary>
    void CharacterSelect()
    {
        if (_stageSelect) { return; }
        if (_titleBack) { return; }
        if (_characterSelectPanel[0].isCharacterDecide &&
            _characterSelectPanel[1].isCharacterDecide
           )
        {
            _skillType[0] = _characterSelectPanel[0].isSkillType;
            _skillType[1] = _characterSelectPanel[1].isSkillType;
            if (TitleSelect.isSelectOnePlayer)
            {
                _characterSelectPanel[0].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.ONE;
                _characterSelectPanel[1].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.THREE;
            }
            StartCoroutine(SceneChange());
        }

        Cansel(_characterSelectPanel[0]);
        if (TitleSelect.isSelectOnePlayer && _characterSelectPanel[0].isCharacterDecide
            && _playerInput.IsPush(GamePadManager.ButtonType.B))
        {
            _characterSelectPanel[0].isCharacterDecide = false;
            _characterSelectPanel[0].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.ONE;
            _characterSelectPanel[1].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.THREE;
        }
        else
        {
            Cansel(_characterSelectPanel[1]);
        }

        if (_characterSelectPanel[0].isCharacterDecide && TitleSelect.isSelectOnePlayer
            && _characterSelectPanel[0].gameObject.GetComponent<InputBase>().getPlayerType == GamePadManager.Type.ONE
            )
        {
            _characterSelectPanel[1].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.ONE;
            _characterSelectPanel[0].gameObject.GetComponent<InputBase>().getPlayerType = GamePadManager.Type.THREE;
        }


    }

    void Cansel(CharacterSelect select)
    {
        if (select.isPreviewSkill) { return; }
        if (select.playerInput.IsPush(GamePadManager.ButtonType.B) &&
           !select.isCharacterDecide)
        {
            if (select.isProductionRandom) { return; }
            AudioManager.instance.PlaySe(SoundName.SeName.cansel);
            _titleBack = true;
            _titleBackPanel.SetActive(true);
        }
    }

    /// <summary>
    /// タイトルバックするか表示されてるときの関数
    /// </summary>
    void TitleBackSelect()
    {
        if (!_titleBack) { return; }
        if (_playerInput.isLeftStickLeftDown && !_titleBackYes)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _titleBackSelectPanel.transform.localPosition = new Vector3(-200, -100, 0);
            _titleBackYes = true;
        }
        else if (_playerInput.isLeftStickRightDown && _titleBackYes)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _titleBackSelectPanel.transform.localPosition = new Vector3(200, -100, 0);
            _titleBackYes = false;
        }
    }

    /// <summary>
    ///　選択されてるほうのPrefabを表示するための関数
    /// </summary>
    void SceneSelect()
    {
        _waiting = false;
        if (_stageSelect)
        {
            _animator[0].enabled = false; //切らないと警告はく
            _selectScene[0].SetActive(false);
            _selectScene[1].SetActive(true);
        }
        else
        {
            _animator[0].enabled = true;
            _selectScene[0].SetActive(true);
            _selectScene[1].SetActive(false);
        }
    }

    /// <summary>
    /// シーン遷移関数
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneChange()
    {
        _waiting = true;
        yield return new WaitForSeconds(1.0f);
        _stageSelect = true;
        SceneSelect();
    }
}
