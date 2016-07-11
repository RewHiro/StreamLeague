using UnityEngine;
using System.Collections;

public class StageView : MonoBehaviour {

    [SerializeField, Range(0.1f, 1.0f)]
    private float ROTATE_SPEED = 0.5f;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, ROTATE_SPEED, 0);
    }

}
