using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialImageDraw : MonoBehaviour {

    [SerializeField]
    PlayerInput _playerInput = null;
    PlayerInput playerInput
    {
        get
        {
            if(_playerInput == null)
            {
                _playerInput = GetComponent<PlayerInput>();
            }
            return _playerInput;
        }
    }

    [SerializeField]
    TitleSelect _titleSelect = null;

    [SerializeField]
    Image[] _tutorialImages;

    [SerializeField]
    Image _backImage;

    const int INIT_PAGE = 0;
    int _nowPage = INIT_PAGE;

    [SerializeField]
    private GameObject _animator = null;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (playerInput.IsPush(GamePadManager.ButtonType.B))
        {
            Reset();
            return;
        }

        if (_nowPage == _tutorialImages.Length - 1)
        {
            return;
        }

        if (playerInput.IsPush(GamePadManager.ButtonType.A))
        {
            if (_nowPage == _tutorialImages.Length - 1)
            {
                Reset();
                return;
            }


            _tutorialImages[_nowPage].enabled = false;
            ++_nowPage;
            if (_nowPage != _tutorialImages.Length)
            {
                _tutorialImages[_nowPage].enabled = true;
            }
        }
    }

    public void StartImage()
    {
        _tutorialImages[_nowPage].enabled = true;
        _backImage.enabled = true;
    }

    void Reset()
    {
        _animator.SetActive(false);
        _backImage.enabled = false;
        _tutorialImages[_nowPage].enabled = false;
        _nowPage = INIT_PAGE;
        _titleSelect.enabled = true;
        this.enabled = false;
    }
}
