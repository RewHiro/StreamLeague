/// <author>
/// 新井大一
/// </author>


using System.Collections;
using UnityEngine;

public class SceneManagerUtility : Singleton<SceneManagerUtility>
{
    [SerializeField]
    Fader _fader = null;

    public Fader getFader
    {
        get
        {
            return _fader;
        }
    }


    public void StartTransition(float time, SceneName.Name name)
    {
        if (_fader.isFade) return;
        _fader.StartFadeIn(time);
        StartCoroutine(Transition(time, name));
    }

    public void StartTransition(float time, string name)
    {
        if (_fader.isFade) return;
        _fader.StartFadeIn(time);
        StartCoroutine(Transition(time, name));
    }

    //------------------------------------------------------------------

    protected override void Awake()
    {
        base.Awake();
        Screen.fullScreen = true;
        QualitySettings.SetQualityLevel(5);
    }

    IEnumerator Transition(float time, SceneName.Name name)
    {
        while (_fader.isFadeIn)
        {
            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(name.ToString());

        yield return null;

        _fader.StartFadeOut(time);
    }

    IEnumerator Transition(float time, string name)
    {
        while (_fader.isFadeIn)
        {
            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(name.ToString());

        yield return null;

        _fader.StartFadeOut(time);
    }
}
