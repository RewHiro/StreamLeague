/// <author>
/// 新井大一
/// </author>


using UnityEngine;

/// <summary>
/// UIを表示する機能
/// </summary>
public class UIActiver : MonoBehaviour
{
    [SerializeField]
    GameObject[] _gameObjects = null;

    public void ActiveUI()
    {
        _gameObjects.ExecuteAction
            (
            obj => obj.SetActive(true)
            );
    }
}
