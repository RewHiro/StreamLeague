using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnePlayTextChange : MonoBehaviour{

    private Text _text = null;

    void Start()
    {
        _text = GetComponent<Text>();
        if (TitleSelect.isSelectOnePlayer)
        {
            _text.text = "CPU";
            _text.color = Color.grey;
        }
    }

}
