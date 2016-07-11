using UnityEngine;
using System.Collections;

public class AfterImager : MonoBehaviour
{

    [SerializeField]
    GameObject _afterImage = null;

    PlayerState _playerState = null;

    // Use this for initialization
    void Start()
    {
        _playerState = GetComponent<PlayerState>();
        _afterImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerState.state == PlayerState.State.ATTACK)
        {
            _afterImage.SetActive(true);
        }
        else
        {
            _afterImage.SetActive(true);
        }
    }
}
