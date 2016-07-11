/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

/// <summary>
/// 雷の演出
/// </summary>
public class LightningDirector : MonoBehaviour
{
    Bloom _bloom = null;

    /// <summary>
    /// 明るさの最大値
    /// </summary>
    [SerializeField]
    float MAX_INTENSITY = 1.5f;

    /// <summary>
    /// 明るさの最小値
    /// </summary>
    [SerializeField]
    float MIN_INTENSITY = 0.0f;

    void Start()
    {
        _bloom = GetComponent<Bloom>();
        StartCoroutine(Light());
    }

    /// <summary>
    /// 雷が光る演出
    /// </summary>
    /// <returns></returns>
    IEnumerator Light()
    {
        float time = 0.0f;
        const float LIGHTNING_MOVE_TIME = 2.0f;

        _bloom.bloomIntensity = MIN_INTENSITY;
        while (time <= LIGHTNING_MOVE_TIME)
        {
            time += Time.deltaTime;
            _bloom.bloomIntensity = Mathf.Lerp(MAX_INTENSITY, MIN_INTENSITY, time / LIGHTNING_MOVE_TIME);
            yield return null;
        }

        time = 0.0f;

        const float FLASH_MOVE_TIME = 1.0f * 0.8f;
        int count = 0;

        while (count != 2)
        {
            _bloom.bloomIntensity = MAX_INTENSITY;
            while (time <= FLASH_MOVE_TIME)
            {
                time += Time.deltaTime;
                _bloom.bloomIntensity = Mathf.Lerp(MAX_INTENSITY, MIN_INTENSITY, time / FLASH_MOVE_TIME);
                yield return null;
            }

            count++;
            time = 0.0f;
            yield return null;
        }

        yield return new WaitForSeconds(10.0f);

        yield return Light();
    }
}