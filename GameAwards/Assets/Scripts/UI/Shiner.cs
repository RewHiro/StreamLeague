using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shiner : MonoBehaviour
{
    [SerializeField,Tooltip("0~255")]
    Vector2 ALPHA_MIN_MAX = Vector2.zero;

    [SerializeField]
    float SHINE_TIME = 1.0f;

    Image _image = null;

    void Start()
    {
        _image = GetComponent<Image>();

        StartCoroutine(Shine());
    }

    IEnumerator Shine()
    {
        var time = 0.0f;

        //MIN
        while (time < SHINE_TIME)
        {
            var color = _image.color;
            color.a = Mathf.Lerp(ALPHA_MIN_MAX.y, ALPHA_MIN_MAX.x, time / SHINE_TIME) / 255.0f;
            _image.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        time = 0.0f;

        //MAX
        while (time < SHINE_TIME)
        {
            var color = _image.color;
            color.a = Mathf.Lerp(ALPHA_MIN_MAX.x, ALPHA_MIN_MAX.y, time / SHINE_TIME) / 255.0f;
            _image.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        yield return Shine();
    }
}
