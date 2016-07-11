/// <author>
/// 新井大一
/// </author>


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    GameObject _resulePanel = null;

    [SerializeField]
    GameObject _oneMoreUI = null;

    [SerializeField]
    ResultDirector _resultDirector = null;

    [SerializeField]
    Image _resultPlayerPanel = null;

    [SerializeField]
    Sprite[] _playerResultPanels = null;

    [SerializeField]
    GameObject _resultCutin = null;

    [SerializeField]
    bool isTutorial = false;

    [SerializeField]
    ResultLinesMover[] lines = null;

    /// <summary>
    /// スローする時間
    /// </summary>
    const float SLOW_TIME = 0.5f;

    /// <summary>
    /// 遷移する時間
    /// </summary>
    const float TRANSITION_TIME = 3.0f;

    /// <summary>
    /// タイトルに遷移する
    /// </summary>
    public void TrasitionTitle()
    {
        if (SceneManagerUtility.instance.getFader.isFade) return;
        SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Title);
        AudioManager.instance.StopBgm();
    }

    /// <summary>
    ///  キャラセレクトに遷移する
    /// </summary>
    public void TrasitionSelect()
    {
        if (SceneManagerUtility.instance.getFader.isFade) return;
        SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Select);
        AudioManager.instance.StopBgm();
    }

    //------------------------------------------------------

    IEnumerable<HpManager> _players = null;

    HpManager _onePlayer = null;
    HpManager _twoPlayer = null;

    void Awake()
    {
        _resulePanel.SetActive(false);
    }

    void Start()
    {
        _players = FindObjectsOfType<HpManager>();

        foreach (var player in _players)
        {
            var playerType = player.GetComponent<InputBase>().getPlayerType;
            if (playerType == GamePadManager.Type.ONE)
            {
                _onePlayer = player;
            }
            else if (playerType == GamePadManager.Type.TWO)
            {
                _twoPlayer = player;
            }
        }

        StartCoroutine(ResultUpdate());
    }

    /// <summary>
    /// リザルトの更新
    /// </summary>
    /// <returns></returns>
    IEnumerator ResultUpdate()
    {
        // どちらかのHPが0になるまで回す
        while (!_players.Any(player => player.getNowHp <= 0))
        {
            yield return null;
        }

        if (_onePlayer.getNowHp <= 0)
        {
            _resultPlayerPanel.sprite = _playerResultPanels[1];
        }
        else if (_twoPlayer.getNowHp <= 0)
        {
            _resultPlayerPanel.sprite = _playerResultPanels[0];
        }

        if (!isTutorial)
        {

            //　操作機能をOFFにする
            foreach (var input in FindObjectsOfType<InputBase>())
            {

                input.GetComponent<Attack>().enabled = false;
                input.GetComponent<SkillBase>().enabled = false;
                input.GetComponent<EnergyConnect>().enabled = false;

                var characterAnimator = input.GetComponent<CharacterAnimator>();
                characterAnimator.ChangeIdle();
                characterAnimator.enabled = false;

                input.GetComponent<Move>().enabled = false;
            }

            foreach (var attackRange in FindObjectsOfType<AttackRange>())
            {
                attackRange.enabled = false;
            }


            //GameCanvasを最前面にする
            _oneMoreUI.transform.parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            yield return new WaitForSeconds(SLOW_TIME);

            _resultDirector.enabled = true;

            yield return null;

            var audioManager = AudioManager.instance;
            audioManager.StopBgm();
            audioManager.PlayBgm(SoundName.BgmName.result);

            // アニメーションが終わったら
            while (!_resultDirector.isFinishAnimation)
            {
                yield return null;
            }

            _resultCutin.SetActive(true);
            audioManager.PlaySe(SoundName.SeName.winicon);

            // カットインが終わったら
            yield return new WaitForSeconds(3.0f);

            _oneMoreUI.SetActive(true);
            _oneMoreUI.GetComponentInChildren<ResultSelect>().Select();

        }
    }
}
