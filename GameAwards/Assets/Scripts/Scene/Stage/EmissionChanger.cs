/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using System.Collections;

/// <summary>
/// マテリアルのエミッションを変更するテスト
/// </summary>
public class EmissionChanger : MonoBehaviour
{
    [SerializeField]
    Color TRANSITION_COLOR = Color.red;

    [SerializeField]
    float BLINK_TIME = 1.0f;

    Color _startColor = Color.white;
    Renderer _renderer = null;
    EnergyMoveManager _energyMoveManager = null;

    bool _isDirector = false;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _energyMoveManager = FindObjectOfType<EnergyMoveManager>();
        _startColor = _renderer.material.GetColor("_EmissionColor");
    }

    void LateUpdate()
    {
        if (_isDirector) return;
        if (!_energyMoveManager.isChange) return;
        StartCoroutine(Blink());
    }

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
                var color = Color.Lerp(_startColor, TRANSITION_COLOR, time / BLINK_TIME);
                _renderer.material.SetColor("_EmissionColor", color);
                yield return null;
            }

            time = 0.0f;

            while (time <= BLINK_TIME)
            {
                time += Time.deltaTime;
                var color = Color.Lerp(TRANSITION_COLOR, _startColor, time / BLINK_TIME);
                _renderer.material.SetColor("_EmissionColor", color);
                yield return null;
            }
            count++;
        }

        _isDirector = false;
    }
}
