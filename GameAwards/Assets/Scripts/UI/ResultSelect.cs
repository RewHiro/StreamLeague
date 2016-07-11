using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSelect : MonoBehaviour
{
    [SerializeField]
    private InputBase _base = null;

    [SerializeField]
    GameObject _oneMoreUI = null;

    [SerializeField]
    Button _button = null;

    AudioManager _audioManager = null;

    List<RectTransform> _buttons = new List<RectTransform>();

    /// <summary>
    /// trueだとタイトル
    /// </summary>
    private bool _selectTitle = false;

    void Start()
    {
        foreach (var input in FindObjectsOfType<InputBase>())
        {
            if (input.getPlayerType == GamePadManager.Type.ONE)
            {
                _base = input;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            _buttons.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }
        
        _audioManager = AudioManager.instance;
        _oneMoreUI.SetActive(false);
    }

    void InValidButton()
    {
        foreach (var b in FindObjectsOfType<Button>())
        {
            var nav = new Navigation();
            nav.mode = Navigation.Mode.None;
            b.navigation = nav;
        }
    }

    public void CharaSelect()
    {
        if (SceneManagerUtility.instance.getFader.isFade) return;
        SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Select);
        _audioManager.PlaySe(SoundName.SeName.decide);
        _audioManager.StopBgm();
        InValidButton();
    }

    public void Title()
    {
        if (SceneManagerUtility.instance.getFader.isFade) return;
        SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Title);
        _audioManager.PlaySe(SoundName.SeName.decide);
        _audioManager.StopBgm();
        InValidButton();

    }

    public void Retry()
    {
        if (SceneManagerUtility.instance.getFader.isFade) return;
        SceneManagerUtility.instance.StartTransition(1.0f, SceneManager.GetActiveScene().name);
        _audioManager.PlaySe(SoundName.SeName.decide);
        _audioManager.StopBgm();
        InValidButton();

    }

    public void PlayMoveSound()
    {
        _audioManager.PlaySe(SoundName.SeName.cursor);
    }

    public void Select()
    {
        _button.Select();
        _audioManager.StopSe();
    }

    public void Selected(int num)
    {
        _buttons[num].localScale = Vector3.one * 1.2f;
    }

    public void DeSelected(int num)
    {
        _buttons[num].localScale = Vector3.one;
    }

    //void Update()
    //{
    //    if (_base.isLeftStickFrontDown)
    //    {
    //        _selectTitle = false;
    //    }
    //    else if (_base.isLeftStickBackDown)
    //    {
    //        _selectTitle = true;
    //    }

    //    if (_base.IsPush(GamePadManager.ButtonType.A))
    //    {
    //        if (_selectTitle)
    //        {
    //            if (SceneManagerUtility.instance.getFader.isFade) return;
    //            SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Title);
    //            AudioManager.instance.StopBgm();
    //        }
    //        else
    //        {
    //            if (SceneManagerUtility.instance.getFader.isFade) return;
    //            SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.Select);
    //            AudioManager.instance.StopBgm();
    //        }
    //    }
    //}
}
