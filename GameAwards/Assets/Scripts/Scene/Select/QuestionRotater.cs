using UnityEngine;
using System.Collections;

public class QuestionRotater : MonoBehaviour {

    [SerializeField]
    private float speed = 0.5f;

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
