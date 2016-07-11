using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextFlower : MonoBehaviour
{

    [SerializeField]
    float FLOW_SPEED = 0.2f;

    [Multiline]
    [SerializeField]
    string[] _textData = null;

    public int getTextDataCount
    {
        get
        {
            return _textDataCount;
        }
    }

    public bool isNext
    {
        get
        {
            return _isNext;
        }
    }

    public void StartText()
    {
        StartCoroutine(TextChange());
    }

    //-------------------------------------------------------------------
    Text _text = null;
    InputBase _inputBase = null;
    int _textDataCount = 0;
    bool _isNext = false;

    void Start()
    {
        _text = GetComponent<Text>();
        foreach (var input in FindObjectsOfType<InputBase>())
        {
            if (input.getPlayerType != GamePadManager.Type.ONE) continue;
            _inputBase = input;
        }
        StartText();
    }

    IEnumerator TextChange()
    {
        yield return null;
        _isNext = false;

        if (_textData.Length <= _textDataCount) yield break;

        var length = 0;
        var time = 0.0f;
        var text = _textData[_textDataCount];

        while (length < text.Length)
        {
            if (time >= FLOW_SPEED)
            {
                length++;
                _text.text = text.Substring(0, length);
                time = 0.0f;
            }

            if (_inputBase.IsPush(GamePadManager.ButtonType.A))
            {
                length = text.Length;
                _text.text = text;
            }

            time += Time.deltaTime;
            yield return null;
        }

        yield return null;

        while (_textDataCount < _textData.Length)
        {

            if (Time.frameCount % 60 == 0)
            {
                _text.text = text;
            }
            else if (Time.frameCount % 60 == 30)
            {
                _text.text = text.Substring(0, text.Length - 1);
            }

            if (_inputBase.IsPush(GamePadManager.ButtonType.A))
            {
                _isNext = true;
                _textDataCount++;
                if (_textData.Length > _textDataCount)
                {
                    _text.text = "";
                    yield return TextChange();
                }

                SceneManagerUtility.instance.StartTransition(1.0f, SceneName.Name.TutorialSelect);
                yield break;
            }
            yield return null;
        }
    }
}
