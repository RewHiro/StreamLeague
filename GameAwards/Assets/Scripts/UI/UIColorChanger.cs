using UnityEngine;
using UnityEngine.UI;

public class UIColorChanger : MonoBehaviour
{
    [SerializeField]
    GamePadManager.Type _type = GamePadManager.Type.ONE;

    void Start()
    {
        foreach (var player in FindObjectsOfType<InputBase>())
        {
            if (_type != player.getPlayerType) continue;
            var color = player.GetComponent<CharacterParameter>().getParameter.color;
            foreach (var image in GetComponentsInChildren<Image>())
            {
                image.color = color;
            }
            break;
        }
    }
}
