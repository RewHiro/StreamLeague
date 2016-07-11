/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using System.Linq;

/// <summary>
/// リザルトの演出
/// </summary>
public class ResultDirector : MonoBehaviour
{


    float _time = 0.0f;
    GameObject _winPlayer = null;
    Animator _animator = null;

    /// <summary>
    /// 勝利アニメーションが終わったか
    /// </summary>
    public bool isFinishAnimation
    {
        get
        {
            return _animator.GetCurrentAnimatorStateInfo(0).IsName("WinEnd");
        }
    }

    void Start()
    {
        foreach (var player in FindObjectsOfType<InputBase>())
        {
            if (player.GetComponent<HpManager>().getNowHp > 0)
            {
                _winPlayer = player.gameObject;
                _animator = player.GetComponent<Animator>();
                _animator.SetTrigger("Win");
                break;
            }
        }

        var hpManagers = FindObjectsOfType<HpManager>();

        if (hpManagers.All(player => player.getNowHp <= 0))
        {
            var random = Random.Range(0, hpManagers.Length);
            _winPlayer = hpManagers[random].gameObject;
            _animator = _winPlayer.GetComponent<Animator>();
            _animator.SetTrigger("Win");
        }
    }

    /// <summary>
    /// 勝ったキャラをカメラの方向に向ける
    /// </summary>
    void Update()
    {
        const float STOP_ROTATE_TIME = 1.0f;
        if (_time >= STOP_ROTATE_TIME) return;
        _time += Time.deltaTime;

        var oldRotation = _winPlayer.transform.rotation;

        _winPlayer.transform.LookAt(Camera.main.transform);

        var angle = _winPlayer.transform.rotation.eulerAngles;
        angle.x = 0;
        angle.z = 0;

        _winPlayer.transform.rotation = oldRotation;
        _winPlayer.transform.rotation = Quaternion.Lerp(_winPlayer.transform.rotation, Quaternion.Euler(angle), _time / STOP_ROTATE_TIME);
    }
}