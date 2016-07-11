using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// スキル「トルネード」
/// エネルギーと他プレイヤーを引き寄せたり引き離したりする
/// </summary>
public class Tornado : SkillBase {

    //引き離すか吸い寄せるかを決める
    enum Type
    {
        Suction = 1, //吸いつける
        Separate = -1 //引き離す
    }

    /// <summary>
    /// 動かしたいObjectをListに追加する
    /// </summary>
    [SerializeField]
    private List<GameObject> _list = null;

    [SerializeField]
    private int POWER = 10;
    public int power
    {
        get { return POWER; }
        set { POWER = value; }
    }

    [SerializeField]
    PlayerState _playerState = null;    // プレイヤーの状態
    public PlayerState playerState
    {
        get
        {
            if (_playerState == null)
            {
                _playerState = GetComponent<PlayerState>();
            }
            return _playerState;
        }
    }

    PlayerState _state = null;

    //あとで考える
    override public void Start()
    {
        base.Start();
        PrefabsLoad();
        _list = new List<GameObject>();

        //以下の処理で自分をリストに追加しないようにする
        gameObject.tag = "Untagged"; //一度tagを外す
        _list.AddRange(GameObject.FindGameObjectsWithTag("Player")); //プレイヤーtagのオブジェクトをトルネードの対象にする
        gameObject.tag = "Player"; //自分のtagをプレイヤーに戻す
    }

    void Update()
    {
        ////回転方向のboolをもらったら変える
        //if(_input.isRightStickClockwiseRotate && !_skillActive)
        //{
        //    base.SkillStart();
        //    StartCoroutine(Move(Type.Suction));
        //}
        //else if(_input.isRightStickCounterClockwiseRotate && !_skillActive)
        //{
        //    base.SkillStart();
        //    StartCoroutine(Move(Type.Separate));
        //}

        //回転方向のboolをもらったら変える
        //if (_input.isSkill && !_skillActive)
        //{
        //    base.SkillStart();
        //    StartCoroutine(Move(Type.Suction));
        //}
        if (_input.isSkill && !_skillActive && playerState.state == PlayerState.State.NORMAL && playerState.state != PlayerState.State.ATTACK)
        {
            base.SkillStart();
            StartCoroutine(Move(Type.Separate));
        }


        var _playerList = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in _playerList)
        {
            if (_input.getPlayerType != player.GetComponent<InputBase>().getPlayerType)
            {
                _state = player.GetComponent<PlayerState>();
            }
        }
    }

    public override void SkillStart()
    {
        base.SkillStart();
        StartCoroutine(Move(Type.Suction));
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator Move(Type type)
    {
        AudioManager.instance.PlaySe(SoundName.SeName.tornade);
        var tornadoPos = transform.localPosition;
        var effect = Instantiate(_skilleffect[5], transform.localPosition, _skilleffect[5].transform.localRotation) as GameObject;
        effect.name = "TornadoEffect";
        effect.tag = "Effect";
        while (_activeTime < SKILL_ACTIVE_TIME)
        {
            _activeTime += Time.deltaTime;
            //foreach (var list in _list)
            //{
            //    //座標の移動
            //    var localPosition = list.transform.localPosition;
            //    list.GetComponent<Rigidbody>().AddForce(
            //        Vector3.Normalize(playerPos - localPosition) * 
            //        (1 /Vector3.Distance(playerPos, localPosition)) 
            //        * POWER * (int)type);
            //    //回転
            //    list.transform.Rotate(0, (int)type, 0);
            //}

            if (_state.state != PlayerState.State.AVOIDANCE)
            {
                var length = _state.transform.position - tornadoPos;
                _state.gameObject.GetComponent<Rigidbody>().AddForce(length.normalized * POWER * Time.deltaTime * 60);
            }
            yield return null;
        }
        StartCoroutine(SKillCoolTime());
        //Destroy(effect);
        yield return null;
    }

}
