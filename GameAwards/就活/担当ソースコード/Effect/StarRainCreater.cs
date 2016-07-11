/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using System.Collections.Generic;


public class StarRainCreater : MonoBehaviour
{
    [SerializeField]
    GameObject _starRainPrefab = null;

    List<HpManager> _hpManagers = new List<HpManager>();

    void Start()
    {
        _hpManagers.AddRange(FindObjectsOfType<HpManager>());
    }

    void Update()
    {
        foreach (var hp in _hpManagers)
        {
            if (!hp.isHit) continue;
            var effect = Instantiate(_starRainPrefab);
            effect.transform.position = transform.position;
            break;
        }
    }
}
