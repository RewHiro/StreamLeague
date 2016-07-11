using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class TitleSpriteChange : MonoBehaviour {

    [SerializeField]
    private TitleSelect.Mode _mode = TitleSelect.Mode.GameMode;

    private TitleSelect _titleSelect = null;

    private Image _image = null;

    [SerializeField]
    private Sprite[] _sprite = null;

    private int _spriteNum = 0;
    public int spriteNumber
    {
        get { return _spriteNum; }
    }

    private Dictionary<TitleSelect.Mode, Action> _selectUpdate = null;

    void Start()
    {
        _titleSelect = FindObjectOfType<TitleSelect>();
        _image = GetComponent<Image>();
        _selectUpdate = new Dictionary<TitleSelect.Mode, Action>();
        _selectUpdate.Add(TitleSelect.Mode.GameMode, GameMode);
        _selectUpdate.Add(TitleSelect.Mode.PlayerNum, PlayerNum);
        _selectUpdate.Add(TitleSelect.Mode.Level, Level);
    }

    void Update()
    {
        _selectUpdate[_mode]();
    }

    void GameMode()
    {
        if(_titleSelect.mode != TitleSelect.Mode.GameMode)
        {
            _image.sprite = _sprite[2];
            _spriteNum = 1;
        }
        else if(_titleSelect.isSelectButtle)
        {
            _image.sprite = _sprite[1];
            _spriteNum = 1;
        }
        else
        {
            _image.sprite = _sprite[0];
            _spriteNum = 0;
        }
    }

    void PlayerNum()
    {
        if(TitleSelect.isSelectOnePlayer)
        {
            _image.sprite = _sprite[1];
            _spriteNum = 4;
        }
        else
        {
            _image.sprite = _sprite[0];
            _spriteNum = 3;
        }
    }

    void Level()
    {
        if(TitleSelect.aiLevel == TitleSelect.AILevel.Easy)
        {
            _image.sprite = _sprite[0];
            _spriteNum = 5;
        }
        else if(TitleSelect.aiLevel == TitleSelect.AILevel.Normal)
        {
            _image.sprite = _sprite[1];
            _spriteNum = 6;
        }
        else if(TitleSelect.aiLevel == TitleSelect.AILevel.Hard)
        {
            _image.sprite = _sprite[2];
            _spriteNum = 7;
        }
    }

}
