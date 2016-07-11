using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

/// <summary>
/// タイトルの選択を管理するスクリプト
/// </summary>
public class TitleSelect : MonoBehaviour
{

    public enum Mode
    {
        GameMode,
        PlayerNum,
        Level
    }

    private Mode _mode = Mode.GameMode;
    public Mode mode
    {
        get { return _mode; }
    }

    public enum AILevel
    {
        Easy,
        Normal,
        Hard
    }

    /// <summary>
    /// 一人用の時、AIのレベルを渡す
    /// </summary>
    private static AILevel _aiLevel = AILevel.Easy;
    public static AILevel aiLevel
    {
        get { return _aiLevel; }
    }

    [SerializeField, Tooltip("ゲームタイプの選択パネル")]
    private GameObject _selectPanel = null;

    [SerializeField, Tooltip("0:1Pテキスト、1:2Pテキスト、2:チュートリアルテキスト")]
    private GameObject[] _playTypeText = null;

    [SerializeField, Tooltip("1Pか2Pかどちらが選ばれているかのパネル")]
    private GameObject _typeSelectPanel = null;

    [SerializeField]
    private GameObject _difficultyPanel = null;

    [SerializeField]
    private GameObject _aiLevelText = null;

    [SerializeField]
    private GameObject _tutorialObj = null;

    //-----------------------------------------------------

    private Animator _animator = null;
    private bool _titleLabelMove = false;
    private bool _enemyLevelMove = false;

    private PlayerInput _playerInput = null;

    private Vector3 BUTTLE_LABEL_POS = new Vector3(-564.0f, -99.0f, 0);
    private Vector3 TURTREAL_LABEL_POS = new Vector3(450.0f, -99.0f, 0);
    private Vector3 BUTTLE_MOVE_POS = new Vector3(-564.0f, 60.0f, 0);

    private Dictionary<Mode, Action> _selectType = null;

    [SerializeField]
    private TutorialImageDraw _tutorialImageDraw = null;

    /// <summary>
    /// 選択されているのが対戦ならtrue
    /// </summary>
    private bool _selectButtle = true;
    /// <summary>
    /// 対戦が選択されてたらtrue
    /// </summary>
    public bool isSelectButtle
    {
        get { return _selectButtle; }
    }

    [SerializeField]
    private Animator _logoAnimator = null;

    /// <summary>
    /// 選択されているのが１Pならtrue
    /// </summary>
    private static bool _selectOnePlay = true;
    public static bool isSelectOnePlayer
    {
        get { return _selectOnePlay; }
    }


    //シーンが変わるときに変わる
    //こいつが反応したら処理止める
    private bool _gameSelect = false;

    public void OnDestroy()
    {
    }

    void Start()
    {
        AudioManager.instance.PlayBgm(SoundName.BgmName.maintheme);
        _selectOnePlay = true;
        _playerInput = GetComponent<PlayerInput>();
        _selectPanel.transform.localPosition = BUTTLE_LABEL_POS;
        _selectType = new Dictionary<Mode, Action>();
        _selectType.Add(Mode.GameMode, GameSelect);
        _selectType.Add(Mode.PlayerNum, PlaySelect);
        _selectType.Add(Mode.Level, Difficulty);
        _animator = GetComponentInChildren<Animator>();
        _tutorialImageDraw = GetComponent<TutorialImageDraw>();
        Cursor.visible = false;
    }

    void Update()
    {
        if (_logoAnimator.GetCurrentAnimatorStateInfo(0).IsName("TitleLogoAnimation")) { return; }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerNumSelectMove")) { return; }
        if (_gameSelect) { return; }
        _selectType[_mode]();
    }

    /// <summary>
    /// AIのレベルの決定
    /// </summary>
    void Difficulty()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.B))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cansel);
            StartCoroutine(PlayNumBack());
        }

        //パネル座標移動
        if (_aiLevel == AILevel.Easy)
        {
            _difficultyPanel.transform.localPosition = new Vector3(-450, -120, 0);
            if (_playerInput.isLeftStickRightDown)
            {
                AudioManager.instance.PlaySe(SoundName.SeName.cursor);
                _aiLevel = AILevel.Normal;
            }
        }
        else if (_aiLevel == AILevel.Normal)
        {
            _difficultyPanel.transform.localPosition = new Vector3(0, -120, 0);
            if (_playerInput.isLeftStickLeftDown)
            {
                AudioManager.instance.PlaySe(SoundName.SeName.cursor);
                _aiLevel = AILevel.Easy;
            }
            else if (_playerInput.isLeftStickRightDown)
            {
                AudioManager.instance.PlaySe(SoundName.SeName.cursor);
                _aiLevel = AILevel.Hard;
            }
        }
        else if (_aiLevel == AILevel.Hard)
        {
            _difficultyPanel.transform.localPosition = new Vector3(450, -120, 0);
            if (_playerInput.isLeftStickLeftDown)
            {
                AudioManager.instance.PlaySe(SoundName.SeName.cursor);
                _aiLevel = AILevel.Normal;
            }
        }

        if (_playerInput.IsPush(GamePadManager.ButtonType.A))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            AudioManager.instance.StopBgm();
            _gameSelect = true;
            SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Select);
        }

    }

    private IEnumerator PlayNumBack()
    {
        _enemyLevelMove = false;
        _animator.SetBool("EnemyLevel", _enemyLevelMove);
        _selectOnePlay = true;
        _mode = Mode.PlayerNum;
        yield return null;
    }

    /// <summary>
    /// 1Pか2Pの選択
    /// </summary>
    void PlaySelect()
    {

        if (_playerInput.IsPush(GamePadManager.ButtonType.B))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cansel);


            StartCoroutine(LabelDownMove());
        }
        else if (_playerInput.isLeftStickFrontDown && !_selectOnePlay)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _selectOnePlay = true;
        }
        else if (_playerInput.isLeftStickBackDown && _selectOnePlay)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _selectOnePlay = false;
        }

        if (_playerInput.IsPush(GamePadManager.ButtonType.A))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            if (_selectOnePlay)
            {
                StartCoroutine(EnemyLevelSelect());
            }
            else
            {
                AudioManager.instance.StopBgm();
                _gameSelect = true;
                SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Select);
            }
        }

    }

    private IEnumerator EnemyLevelSelect()
    {
        _enemyLevelMove = true;
        _animator.SetBool("EnemyLevel", _enemyLevelMove);
        _mode = Mode.Level;
        _aiLevelText.SetActive(true);
        _playTypeText[1].SetActive(false);
        yield return null;
    }

    /// <summary>
    /// バトルのラベルがもとに戻る関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator LabelDownMove()
    {
        _titleLabelMove = false;
        _animator.SetBool("PlayNum", _titleLabelMove);
        _selectOnePlay = true;
        _mode = Mode.GameMode;
        yield return null;
    }

    /// <summary>
    /// チュートリアルか対戦かの選択
    /// </summary>
    void GameSelect()
    {
        if (_playerInput.isLeftStickLeftDown && !_selectButtle)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _selectButtle = true;
            _selectPanel.transform.localPosition = BUTTLE_LABEL_POS;
        }
        else if (_playerInput.isLeftStickRightDown && _selectButtle)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _selectButtle = false;
            _selectPanel.transform.localPosition = TURTREAL_LABEL_POS;
        }
        //バトルが選ばれている状態で選択したら
        if (_playerInput.IsPush(GamePadManager.ButtonType.A))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            if (_selectButtle)
            {
                StartCoroutine(LabelUpMove());
            }
            else
            {
                // チュートリアル画像を出す
                //AudioManager.instance.StopBgm();
                //_gameSelect = true;
                //SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.TutorialSelect);
                _tutorialImageDraw.StartImage();
                _tutorialImageDraw.enabled = true;
                _tutorialObj.SetActive(true);
                this.enabled = false;
            }
        }
    }

    /// <summary>
    /// バトル選択されたらラベルが動き出す関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator LabelUpMove()
    {
        _titleLabelMove = true;
        _animator.SetBool("PlayNum", _titleLabelMove);
        _mode = Mode.PlayerNum;
        yield return null;
    }
}
