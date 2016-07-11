using System.Collections;
using UnityEngine;

public class TutorialSelectAnimator : MonoBehaviour
{

    [SerializeField]
    Vector2 START_POSITION = Vector2.zero;

    [SerializeField]
    Vector2 END_POSITION = Vector2.zero;

    [SerializeField]
    float DELAY = 0.0f;

    [SerializeField]
    float EASE_TIME = 5.0f;

    [SerializeField]
    float s = 1.0f;


    //-------------------------------------------------------------------
    float _time = 0.0f;

    void Start()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = START_POSITION;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        var rectTransform = GetComponent<RectTransform>();
        yield return null;
        rectTransform.anchoredPosition = new Vector2((float)Easing.OutBack(_time, EASE_TIME, END_POSITION.x, START_POSITION.x, s), END_POSITION.y);
        yield return new WaitForSeconds(DELAY);

        while (_time < EASE_TIME)
        {
            _time += Time.deltaTime;
            rectTransform.anchoredPosition = new Vector2((float)Easing.OutBack(_time, EASE_TIME, END_POSITION.x, START_POSITION.x, s), END_POSITION.y);
            yield return null;
        }
    }
}
