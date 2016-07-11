using UnityEngine;
using System.Collections;

public class ChainMover : MonoBehaviour
{
    RectTransform _rectTransform = null;

    const float DESTORY_TIME = 3.0f;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        Destroy(gameObject, DESTORY_TIME);
    }

    void Update()
    {
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y + Time.deltaTime);
    }
}
