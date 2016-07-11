using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialChangeText : MonoBehaviour
{

    public void ChnageText()
    {
        var name = _eventSystem.currentSelectedGameObject.name.Substring(10,1);
        var num = int.Parse(name);
        _actions[num]();
    }

    //----------------------------------------------------------
    List<Action> _actions = new List<Action>();
    EventSystem _eventSystem = null;
    Text _text = null;

    void Start()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        _text = GetComponent<Text>();

        _actions.Add(() => { _text.text = "対戦での基本となる移動・回避の操作を学べるチュートリアルです"; });
        _actions.Add(() => { _text.text = "対戦での勝敗・攻撃するためのルール操作を学べるチュートリアルです"; });
        _actions.Add(() => { _text.text = "速度タイプの加速スキルとマッハスキルについて学べるチュートリアルです"; });
        _actions.Add(() => { _text.text = "超能力タイプのグラビティスキルとトルネードについて学べるチュートリアルです"; });
        _actions.Add(() => { _text.text = "回避タイプの受け流しスキルとインビジブルスキルについて学べるチュートリアルです"; });
        _actions.Add(() => { _text.text = "相手に奪われたエネルギーを奪うための説明"; });
    }
}
