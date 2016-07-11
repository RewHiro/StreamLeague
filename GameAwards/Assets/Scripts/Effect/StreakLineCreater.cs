/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// 流脈を生成する機能
/// </summary>
public class StreakLineCreater : MonoBehaviour
{
    [SerializeField]
    GameObject _snakeManager = null;

    /// <summary>
    /// エネルギーの最大個数の半分を生成する
    /// </summary>
    void Start()
    {
        var halfMaxNum = FindObjectOfType<EnergyCreater>().energyMaxNum;
        for (var i = 0; i < halfMaxNum; i++)
        {
            var snakeManager = Instantiate(_snakeManager);
            var streakLineManager = snakeManager.GetComponent<StreakLineManager>();
            streakLineManager.energyConnect = GetComponent<EnergyConnect>();
            streakLineManager.num = i;
        }
    }
}