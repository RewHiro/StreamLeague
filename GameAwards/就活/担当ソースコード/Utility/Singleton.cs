/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// シングルトン
/// <Tips>
/// 継承して使用してください。
/// 必ず
/// void Awake()
/// {
///     base.Awake();
/// }
/// の記述をお願いします
/// </Tips>
/// </summary>
/// <typeparam name="Type"></typeparam>
/// 
public class Singleton<Type> : MonoBehaviour where Type : MonoBehaviour
{
    public static Type instance
    {
        get
        {
            return _instance;
        }
    }

    //------------------------------------------------------------

    protected static Type _instance;

    protected virtual void Awake()
    {
        int length;
        Destory(out length);
        if (length >= 2) return;

        _instance = FindObjectOfType<Type>();

        Assert.IsNotNull(_instance);
        DontDestroyOnLoad(gameObject);
    }

    void Destory(out int length)
    {
        var types = FindObjectsOfType<Type>();
        length = types.Length;
        if (length < 2) return;

        for (int i = 1; i < length; i++)
        {
            Destroy(types[i].gameObject);
        }
    }
}