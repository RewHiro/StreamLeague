using UnityEngine;
using System.Collections;

public class EnergyRotater : MonoBehaviour
{

    public bool rotateFlug
    {
        get; set;
    }

    [SerializeField]
    private float _speed = 1.0f;

    void Start()
    {
        rotateFlug = false;
    }

    void Update()
    {
        if (rotateFlug)
        {
            transform.Rotate(0, _speed, 0);
        }
        else
        {
            if(transform.eulerAngles.y > 5.0f)
            {
                transform.Rotate(0, _speed, 0);
            }
        }
    }
}
