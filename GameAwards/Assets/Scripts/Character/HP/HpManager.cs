using UnityEngine;
using System.Collections;

/// <summary>
/// HPについて管理するClass
/// </summary>
public class HpManager : MonoBehaviour {

    [SerializeField, Tooltip("キャラごとの最大HP")]
    private float _maxHp = 0.0f;

    float[] _damage = new float[9] {
            0,
            1,
            2,
            5,
            7,
            16,
            20,
            35,
            50,
    };

    [SerializeField]
    float IMPULSE_VALUE = 1000.0f;

    const float DAMAGE_COOL_TIME = 0.5f;
    int PLAYER_HASH_CODE = 0;

    /// <summary>
    /// 最大HP所得
    /// </summary>
    public float getMaxHp
    {
        get { return _maxHp; }
    }

    public float setMaxHP
    {
        set { _maxHp = value; }
    }

    private float _nowHp;

    public bool isDamaged
    {
        get
        {
            return _isDameged;
        }
    }

    bool _isDameged = false;


    bool _isHit = false;
    public bool isHit
    {
        get
        {
            return _isHit;
        }
    }

    /// <summary>
    /// 現在のHP所得
    /// </summary>
    public float getNowHp
    {
        get { return _nowHp; }
    }

    /// <summary>
    /// 相手が死ぬ領域にいたら
    /// </summary>
    public bool youWillDie
    {
        get
        {
            var myPlayerState = GetComponent<PlayerState>();
            var rivalPlayerState = _rivalHpManager.gameObject.GetComponent<PlayerState>();
            if (rivalPlayerState.state == PlayerState.State.AVOIDANCE) { return false; }

            var rivalConnect = _rivalHpManager.gameObject.GetComponent<EnergyConnect>();
            var rivalDefense = _rivalHpManager.gameObject.GetComponent<Defense>();
          
            var damage = _damage[_energyConnect.connectNum - 1];

            if (rivalPlayerState.state == PlayerState.State.DEFENSE) { damage -= rivalDefense.damageCut[rivalConnect.connectNum]; }

            return _rivalHpManager.getNowHp <= damage;
        }
    }

    //------------------------------------------------------------------------

    HpManager _rivalHpManager = null;
    EnergyConnect _energyConnect = null;

    void Awake()
    {
        _nowHp = _maxHp;
        PLAYER_HASH_CODE = "Player".GetHashCode();
    }

    void Start()
    {
        foreach (var player in FindObjectsOfType<InputBase>())
        {
            if (player.getPlayerType == GetComponent<InputBase>().getPlayerType) continue;
            _rivalHpManager = player.GetComponent<HpManager>();
        }
        _energyConnect = GetComponent<EnergyConnect>();
    }

    //デバッグ用
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) { _nowHp -= 10; }
    }

    void OnCollisionEnter(Collision col)
    {
        if (_isDameged) return;
        //HPが減る処理を書く
        if (col.gameObject.tag.GetHashCode() != PLAYER_HASH_CODE) return;

        var rival_input = col.gameObject.GetComponent<InputBase>();
        var own_input = GetComponent<InputBase>();

        if (rival_input.getPlayerType == own_input.getPlayerType) return;
        var rivalPlayerState = col.gameObject.GetComponent<PlayerState>();

        if (rivalPlayerState.state != PlayerState.State.ATTACK) return;
        var rivalEnergyConnect = col.gameObject.GetComponent<EnergyConnect>();
        if (rivalEnergyConnect.NextObj() != gameObject) return;

        var damage = _damage[rivalEnergyConnect.connectNum - 1];

        var rivalDefense = col.gameObject.GetComponent<Defense>();
        var myDefense = GetComponent<Defense>();
        var myPlayerState = GetComponent<PlayerState>();

        if (myPlayerState.state == PlayerState.State.AVOIDANCE) { return; }

        if (myPlayerState.state == PlayerState.State.DEFENSE) {
            damage -= myDefense.damageCut[_energyConnect.connectNum];
            if(damage < 0) { damage = 0; }
            AudioManager.instance.PlaySe(SoundName.SeName.reflection);
        }

        _nowHp -= damage;
        _isDameged = true;
        _isHit = true;

        var vector = col.transform.position - transform.position;

        //GetComponent<Rigidbody>().AddForce(-vector.normalized * IMPULSE_VALUE, ForceMode.Impulse);

        StartCoroutine(Damage());

        if (_nowHp < 0) _nowHp = 0;
    }

    IEnumerator Damage()
    {
        yield return null;
        _isHit = false;

        yield return new WaitForSeconds(DAMAGE_COOL_TIME);
        _isDameged = false;
    }
}
