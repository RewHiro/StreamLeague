/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using System.Linq;

/// <summary>
/// ゲームパッドが差さっていなかったら警告表示する機能
/// </summary>
public class GamepadAnouncer : MonoBehaviour
{
    [SerializeField]
    GameObject _gamepadAnounce = null;

    /// <summary>
    /// ゲームパッドが差さっているか
    /// </summary>
    public bool isConnectedGamePad
    {
        get
        {
            return !Input.GetJoystickNames().All(name => name == "");
        }
    }

    void Update()
    {
        _gamepadAnounce.SetActive(!isConnectedGamePad);
    }
}
