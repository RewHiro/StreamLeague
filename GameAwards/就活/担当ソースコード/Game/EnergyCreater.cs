/// <author>
/// 新井大一
/// </author>


using UnityEngine;


/// <summary>
/// エネルギー生成する機能
/// </summary>
public class EnergyCreater : MonoBehaviour
{
    /// <summary>
    /// エネルギーが存在する最大数
    /// </summary>
    [SerializeField]
    int MAX_NUM = 10;
    public int energyMaxNum
    {
        get { return MAX_NUM; }
    }

    /// <summary>
    /// 生成する時間
    /// </summary>
    [SerializeField]
    int CREATE_TIME = 2;

    /// <summary>
    /// 生成するエネルギーのプレハブ
    /// </summary>
    [SerializeField]
    GameObject _energyPrefab = null;

    /// <summary>
    /// エネルギーを生成する位置情報
    /// </summary>
    [SerializeField]
    Transform[] _enemySpawners = null;


    //--------------------------------------------------------------

    float _count = 0.0f;

    void Update()
    {
        _count += Time.deltaTime;

        if ((int)_count % CREATE_TIME != 0) return;

        if (GetComponentsInChildren<WhoHaveEnergy>().Length >= MAX_NUM) return;

        Create();
    }

    /// <summary>
    /// 生成
    /// </summary>
    void Create()
    {
        var num = 0;
        var spawn = GetNotChildSpawn(ref num);
        if (spawn == null) return;
        var energy = Instantiate(_energyPrefab);
        energy.name = _energyPrefab.name;
        energy.transform.SetParent(spawn, false);
    }

    /// <summary>
    /// 子供を取得
    /// </summary>
    /// <param name="num"></param>
    /// <returns>子供を返す(子供がいなかったらnullを返す)</returns>
    Transform GetNotChildSpawn(ref int num)
    {
        foreach (var child in _enemySpawners)
        {
            if (child.GetComponentInChildren<WhoHaveEnergy>() != null) continue;
            return child.transform;
        }
        return null;
    }
}
