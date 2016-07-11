/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

/// <summary>
/// 警報エフェクト
/// </summary>
public class Warninger : MonoBehaviour
{

    /// <summary>
    /// 遷移する色
    /// </summary>
    [SerializeField]
    Color TRANSITION_COLOR = Color.white;

    /// <summary>
    /// 点滅する時間
    /// </summary>
    [SerializeField]
    float BLINK_TIME = 1.0f;

    /// <summary>
    /// 点滅する明るさ
    /// </summary>
    [SerializeField]
    float BLINK_INTENSITY = 1.0f;

    Color _startColor = Color.white;
    Light _light = null;
    EnergyMoveManager _energyMoveManager = null;

    float _startIntensity = 0.0f;
    bool _isDirector = false;

    void Start()
    {
        _light = GetComponent<Light>();
        _energyMoveManager = FindObjectOfType<EnergyMoveManager>();

        _startColor = _light.color;
        _startIntensity = _light.intensity;
    }

    void LateUpdate()
    {
        if (_isDirector) return;
        if (!_energyMoveManager.isChange) return;
        StartCoroutine(Blink());
    }

    /// <summary>
    /// 点滅
    /// </summary>
    /// <returns></returns>
    IEnumerator Blink()
    {
        _isDirector = true;
        int count = 0;
        while (count != 2)
        {
            float time = 0.0f;

            while (time <= BLINK_TIME)
            {
                time += Time.deltaTime;
                _light.color = Color.Lerp(_light.color, TRANSITION_COLOR, time / BLINK_TIME);
                _light.intensity = Mathf.Lerp(_light.intensity, BLINK_INTENSITY, time / BLINK_TIME);
                yield return null;
            }

            time = 0.0f;

            while (time <= BLINK_TIME)
            {
                time += Time.deltaTime;
                _light.color = Color.Lerp(TRANSITION_COLOR, _startColor, time / BLINK_TIME);
                _light.intensity = Mathf.Lerp(BLINK_INTENSITY, _startIntensity, time / BLINK_TIME);
                yield return null;
            }
            count++;
        }

        _isDirector = false;
    }
}
