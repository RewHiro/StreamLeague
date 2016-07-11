using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour {

    [SerializeField]
    float _speed = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Input.GetAxis("Horizontal") * _speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * _speed * Time.deltaTime);
	}
}
