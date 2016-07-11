using UnityEngine;
using System.Collections;
using System;

public class StartPanelRotation : MonoBehaviour {

    private float END_TIME = 3.0f;

    void Start()
    {
        StartCoroutine(StartCharacterSelect());
    }

    private IEnumerator StartCharacterSelect()
    {
        var time = 0.0f;
        while(time < END_TIME)
        {
            time += Time.deltaTime;
            var localRotation = transform.localRotation;
            localRotation = Quaternion.Euler((float)Easing.OutCirc(time, END_TIME, 360 * 10, 0), 0, 0);
            transform.localRotation = localRotation;
            yield return null;
        }
    }
}
