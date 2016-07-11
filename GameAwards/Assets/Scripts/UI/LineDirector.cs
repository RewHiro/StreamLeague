using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineDirector : MonoBehaviour
{

    [SerializeField]
    float TARGET = 0.0f;

    [SerializeField]
    float MOVE_TIME = 1.0f;

    RectTransform _rectTransform = null;
    Vector2 START_POSITION = Vector2.zero;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        START_POSITION = _rectTransform.anchoredPosition;
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        float time = 0.0f;

        var position = Vector2.zero;
        position = START_POSITION;

        while (time < MOVE_TIME)
        {
            time += Time.deltaTime;
            position.x = Mathf.Lerp(_rectTransform.anchoredPosition.x, TARGET, time / MOVE_TIME); 
            yield return null;
        }

        yield return Move();
    }
}
