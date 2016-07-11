/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// トランジションユニバーサルシステム
/// </summary>
public class Fader : MonoBehaviour
{

    //　マテリアルの情報
    const string RANGE_NAME = "_Range";

    //　フェード時間
    [SerializeField]
    float time = 1.0f;

    /// <summary>
    /// 全体の透明度
    /// </summary>
    public float range
    {
        get
        {
            return _material.GetFloat(RANGE_NAME);
        }

        set
        {
            _material.SetFloat(RANGE_NAME, value);
        }
    }

    /// <summary>
    /// フェード状態か
    /// </summary>
    public bool isFade
    {
        get
        {
            return _fadeState.isFade;
        }
    }

    /// <summary>
    /// フェードイン状態か
    /// </summary>
    public bool isFadeIn
    {
        get
        {
            return _fadeState.isFadeIn;
        }
    }

    /// <summary>
    /// フェードアウト状態か
    /// </summary>
    public bool isFadeOut
    {
        get
        {
            return _fadeState.isFadeOut;
        }
    }

    /// <summary>
    /// フェードインを始める
    /// </summary>
    /// <param name="fade_time">フェードする時間</param>
    /// <returns>自分を返す</returns>
    public Fader StartFadeIn(float fade_time = 1.0f)
    {
        if (_fadeState.isFadeIn) return this;

        _fadeState.state = FadeState.State.IN;
        StopAllCoroutines();
        StartCoroutine(FadeIn(fade_time));

        return this;
    }

    /// <summary>
    /// フェードアウトを始める
    /// </summary>
    /// <param name="fade_time">フェードする時間</param>
    /// <returns>自分を返す</returns>
    public Fader StartFadeOut(float fade_time = 1.0f)
    {
        if (_fadeState.isFadeOut) return this;

        _fadeState.state = FadeState.State.OUT;
        StopAllCoroutines();
        StartCoroutine(FadeOut(fade_time));

        return this;
    }

    //--------------------------------------------------------------

    //　フェード状態
    FadeState _fadeState = new FadeState();
    //　Material情報
    Material _material = null;


    void Awake()
    {
        var graphic = GetComponent<Graphic>();
        var render = GetComponent<Renderer>();

        if (graphic != null)
        {
            graphic.material = Instantiate(graphic.material);
            _material = graphic.material;
        }

        if (render != null)
        {
            _material = render.material;
        }
    }

    void Start()
    {
        StartFadeOut(time);
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <param name="fade_time">フェードする時間</param>
    /// <returns>IEnumeratorを返す</returns>
    IEnumerator FadeIn(float fade_time = 1.0f)
    {
        float time = 0.0f;
        var min = _material.GetFloat(RANGE_NAME);
        var max = 2.0f;

        while (_material.GetFloat(RANGE_NAME) < max)
        {
            Fade(ref time, fade_time, min, max);
            yield return null;
        }

        _material.SetFloat(RANGE_NAME, max);
        _fadeState.state = FadeState.State.WAIT;
    }

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <param name="fade_time">フェードする時間</param>
    /// <returns>IEnumeratorを返す</returns>
    IEnumerator FadeOut(float fade_time = 1.0f)
    {
        float time = 0.0f;
        float min = 0.0f;
        float max = _material.GetFloat(RANGE_NAME);

        while (_material.GetFloat(RANGE_NAME) > min)
        {
            Fade(ref time, fade_time, max, min);
            yield return null;
        }

        _material.SetFloat(RANGE_NAME, min);
        _fadeState.state = FadeState.State.WAIT;
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <param name="time">現在の時間</param>
    /// <param name="fade_time">フェードする時間</param>
    /// <param name="begin_value">始まりの値</param>
    /// <param name="end_value">終わりの値</param>
    void Fade(ref float time, float fade_time, float begin_value, float end_value)
    {
        time += Time.deltaTime;
        var range = Mathf.Lerp(begin_value, end_value, time / fade_time);
        _material.SetFloat(RANGE_NAME, range);
    }
}
