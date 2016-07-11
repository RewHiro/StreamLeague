/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using System.Collections;

/// <summary>
/// 光の点滅
/// </summary>
public class LightDirector : MonoBehaviour
{
    /// <summary>
    /// 明るさの最大値
    /// </summary>
    [SerializeField]
    float MAX_INTENSITY = 1.0f;

    /// <summary>
    /// 明るさの最小値
    /// </summary>
    [SerializeField]
    float MIN_INTENSITY = 0.0f;

    /// <summary>
    /// 点滅する時間
    /// </summary>
    [SerializeField]
    float MOVE_TIME = 1.0f;

    /// <summary>
    /// 一周するまでまつ時間
    /// </summary>
    [SerializeField]
    float WAIT_TIME = 3.0f;

    /// <summary>
    /// ディレイ時間
    /// </summary>
    [SerializeField]
    float START_WAIT_TIME = 0.0f;

    Light _light = null;

    void Start()
    {
        _light = GetComponent<Light>();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(START_WAIT_TIME);
        yield return BlinkLight();
    }

    /// <summary>
    /// 点滅
    /// </summary>
    /// <returns></returns>
    IEnumerator BlinkLight()
    {
        float time = 0.0f;

        while (time < MOVE_TIME)
        {
            time += Time.deltaTime;
            _light.intensity = Mathf.Lerp(MIN_INTENSITY, MAX_INTENSITY, time / MOVE_TIME);
            yield return null;
        }

        yield return new WaitForSeconds(WAIT_TIME);

        time = 0.0f;

        while (time < MOVE_TIME)
        {
            time += Time.deltaTime;
            _light.intensity = Mathf.Lerp(MAX_INTENSITY, MIN_INTENSITY, time / MOVE_TIME);
            yield return null;
        }

        yield return BlinkLight();
    }
}