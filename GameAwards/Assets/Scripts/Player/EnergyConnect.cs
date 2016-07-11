using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnergyConnect : MonoBehaviour
{

    [SerializeField]
    CharacterParameter _characterParameter = null;
    public CharacterParameter characterParameter
    {
        get
        {
            if (_characterParameter == null)
            {
                _characterParameter = GetComponent<CharacterParameter>();
            }
            return _characterParameter;
        }
    }

    [SerializeField]
    InputBase _input = null;    // ゲームパッドの処理
    public InputBase input
    {
        get
        {
            if (_input == null)
            {
                _input = GetComponent<InputBase>();
            }
            return _input;
        }
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

    // 攻撃の情報が入っている
    [SerializeField]
    Attack _attack = null;
    public Attack attack
    {
        get { return _attack; }
    }

    // 敵(エネルギー)につなぐときのエフェクト
    [SerializeField]
    GameObject _connectParticle = null;

    // 繋いだ敵同士を結ぶエフェクト
    [SerializeField]
    GameObject _lineObj = null;

    // 敵(エネルギー)のタグ
    [SerializeField]
    string _energyTagName = "Energy";
    public string energyTagName
    {
        get { return _energyTagName; }
    }

    // 相手プレイヤーのタグ
    [SerializeField]
    string _playerTagName = "Player";
    public string playerTagName
    {
        get { return _playerTagName; }
    }

    // 他プレイヤーの情報
    List<Attack> _playerList = new List<Attack>();
    public List<Attack> playerList
    {
        get
        {
            return _playerList;
        }
    }

    // 繋いだオブジェクトを格納する
    List<GameObject> _connectList = new List<GameObject>();
    public List<GameObject> connectList
    {
        get { return _connectList; }
        set { _connectList = value; }
    }

    // 繋いだオブジェクトを結ぶ線(龍脈)のリスト
    List<GameObject> _lineList = new List<GameObject>();

    // _connectListのイテレータ
    IEnumerator<GameObject> _connectEnumerator;

    // いまいくつ繋いだかを返すプロパティ
    public int connectNum
    {
        get
        {
            return _connectList.Count;
        }
    }

    // どちらか多く繋いでるか(こっちから見て)
    // 多かったら true を返す
    public bool isConnectMany
    {
        get { return _connectList.Count > playerList[0].GetComponent<EnergyConnect>().connectNum; }
    }


    // Use this for initialization
    void Start()
    {
        var players = GameObject.FindObjectsOfType<Attack>();
        foreach (var player in players)
        {
            if (player.gameObject != gameObject)
            {
                _playerList.Add(player);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.state == PlayerState.State.NORMAL)
        {
            NormalUpdate();
        }
    }

    // 移動状態時の処理
    void NormalUpdate()
    {


        ////////////////////////////////////////////////////////////

        // 攻撃可能かつゲームパッドのボタンを押したら
        //if (_attack.isCanAttack && input.isAttack)
        //{
        //}
    }

    // 呼ぶと攻撃スタートする。
    public void AttackStart()
    {
        AudioManager.instance.PlaySe(SoundName.SeName.dash);

        // プレイヤーの状態を攻撃状態にする
        playerState.state = PlayerState.State.ATTACK;

        // 順番に攻撃するためのイテレータを生成
        _connectEnumerator = _connectList.GetEnumerator();

        // イテレータチェック
        CheckIter();
    }

    // 次のイテレータが存在するかしないかを調べる
    // true : 存在する、false : 存在しない
    public bool IsPresence()
    {
        return !(_connectEnumerator == null || _connectEnumerator.Current == null);
    }

    // イテレータの次のオブジェクトが存在するか調べる
    // true :存在する、false : 存在しない
    // (デバック及び他の用途などで絶対に呼ばないでください。)
    // 理由 : MoveNext() を呼んでいるため
    public bool IsNextObj()
    {
        return _connectEnumerator == null || !_connectEnumerator.MoveNext();
    }

    // イテレータの次のオブジェクトが存在するか調べる
    // (攻撃時に使用するので、デバック及び他の用途などで絶対に呼ばないでください、動かなくなります。)
    // 理由 : _energyConnect.isNextObj() で MoveNext() を呼んでいるため
    public void CheckIter()
    {
        // 次のオブジェクトが存在しなかったら
        if (IsNextObj())
        {
            // プレイヤーの状態をすべてリセットする
            _attack.ResetPlayerState();
        }
    }

    // 現在のイテレータの位置にあるオブジェクトを取り出す
    public GameObject NextObj()
    {
        return _connectEnumerator.Current;
    }

    // 繋いでる情報をリセットする
    public void ConnectReset(bool energyReset)
    {

        if (energyReset)
        {
            foreach (var energy in GameObject.FindGameObjectsWithTag(TagName.Energy))
            {
                if (energy.GetComponent<WhoHaveEnergy>()._whoHave == (WhoHaveEnergy.WhoHave)input.getPlayerType)
                {
                    energy.GetComponent<HitChangeColor>().Reset();
                    energy.GetComponent<WhoHaveEnergy>()._whoHave = WhoHaveEnergy.WhoHave.NONE;
                }
            }
            _connectList.Clear();
            _connectList = new List<GameObject>();
            _connectEnumerator = null;
        }
        
        // 残ったラインを消す
        foreach (var line in _lineList)
        {
            Destroy(line);
        }
        _lineList.Clear();
    }

    // 順番に攻撃するためのイテレータを生成
    public void IterCreate()
    {
        _connectEnumerator = _connectList.GetEnumerator();
    }

    //public void OnCollisionStay(Collision collision)
    //{
    //    // ぶつかった相手がプレイヤーなら
    //    if (collision.gameObject.tag == _playerTagName)
    //    {
    //        // 相手の攻撃情報
    //        var attackPlayer = collision.gameObject.GetComponent<Attack>();

    //        // 相手が攻撃可能なら奪えないので抜ける
    //        if (attackPlayer.isCanAttack) { return; }

    //        // プレイヤーが攻撃状態なら
    //        if (playerState.state == PlayerState.State.NORMAL)
    //        {
    //            // 攻撃可能状態なら計算する必要がないので抜ける
    //            if (_attack.isCanAttack) { return; }

    //            // 相手プレイヤーが攻撃状態のときにつなぐわけにはいかないので抜ける
    //            foreach (var player in _playerList)
    //            {
    //                if (player.playerState.state == PlayerState.State.ATTACK)
    //                {
    //                    return;
    //                }
    //            }

    //            // 相手よりつなげてる数が多かったら相手を追加して攻撃できるようにする
    //            if (attackPlayer.energyConnect.connectNum < connectNum)
    //            {
    //                // すでにリストに同じものがないか調べる
    //                foreach (var obj in _connectList)
    //                {
    //                    // 一致していたらメソッドを抜ける
    //                    if (obj == collision.gameObject)
    //                    {
    //                        return;
    //                    }
    //                }

    //                // 相手プレイヤーを追加
    //                _connectList.Add(collision.gameObject);

    //                // 攻撃可能にする
    //                _attack.isCanAttack = true;

    //                // 相手も攻撃可能の場合のフラグを切ってラインを作り直す
    //                if (attackPlayer.isCanAttack)
    //                {
    //                    attackPlayer.isCanAttack = false;
    //                    attackPlayer.energyConnect.connectList.RemoveAt(attackPlayer.energyConnect.connectList.Count - 1);
    //                    attackPlayer.energyConnect.AllLineUpdate();
    //                }

    //                // 繋ぐエフェクト生成
    //                var particle = Instantiate(_connectParticle);
    //                particle.transform.position = attackPlayer.gameObject.transform.position;

    //                // ラインを引いてつなぐ
    //                CreateLine();
    //            }
    //        }
    //    }
    //}

    public void OnTriggerEnter(Collider other)
    {
        // ぶつかった物体が敵(エネルギー)か判定
        if (other.tag.GetHashCode() == _energyTagName.GetHashCode())
        {
            // プレイヤーが攻撃状態以外なら
            if (playerState.state == PlayerState.State.NORMAL ||
                playerState.state == PlayerState.State.SPEED_SKILL ||
                playerState.state == PlayerState.State.AVOIDANCE)
            {
                AudioManager.instance.PlaySe(SoundName.SeName.energychange);

                //// 攻撃可能状態なら計算する必要がないので抜ける
                //if (_attack.isCanAttack) { return; }

                // 相手のつないだリストにつなごうとしてるオブジェクトがあるかないか調べる
                Attack playerEnemy = null;
                foreach (var player in _playerList)
                {
                    foreach (var connect in player.energyConnect.connectList)
                    {
                        if (connect == other.gameObject)
                        {
                            playerEnemy = player;
                            break;
                        }
                    }
                }

                // 相手のつないだリストになかったら
                if (playerEnemy == null)
                {
                    // すでにリストに同じものがないか調べる
                    foreach (var obj in _connectList)
                    {
                        // 一致していたらメソッドを抜ける
                        if (obj == other.gameObject)
                        {
                            return;
                        }
                    }

                    // リストに追加
                    _connectList.Add(other.gameObject);

                    // 追加した敵(エネルギー)にそのエネルギーは誰のものかを入れる
                    var whoHaveEnergy = other.gameObject.GetComponent<WhoHaveEnergy>();
                    whoHaveEnergy._whoHave = (WhoHaveEnergy.WhoHave)input.getPlayerType;
                    whoHaveEnergy.num = connectNum;


                    // 繋ぐエフェクト生成
                    var particle = Instantiate(_connectParticle);
                    particle.transform.position = other.gameObject.transform.position;

                    // ラインを引いてつなぐ
                    CreateLine();
                }
                // 相手のつないだリストにあったら
                else
                {
                    // 相手が攻撃中なら奪えないようにする
                    foreach (var player in _playerList)
                    {
                        if (player.playerState.state == PlayerState.State.ATTACK)
                        {
                            return;
                        }
                    }

                    //// 相手が攻撃可能なら奪えないようにぬける
                    //if (playerEnemy.isCanAttack) { return; }

                    // 相手のつないだリストの一部を奪うので奪えなかった分をいれるリスト
                    List<GameObject> newConnectList = new List<GameObject>();

                    // 相手のつないだリストの範囲外のオブジェクトは追加しないで相手のままにするためのbool
                    bool isOutRange = false;

                    // PlayerInputに何Pかの情報が入ってるのでそのエネルギーが誰のものかのしきに使う
                    //PlayerInput playerInput = (PlayerInput)_input;

                    // 0～ 回すために foreach を使ってない(foreach も 0～ らしいけど怖いので for文)
                    for (int i = 0, max = playerEnemy.energyConnect.connectList.Count; i < max; ++i)
                    {

                        // 範囲外なら
                        if (isOutRange)
                        {
                            // 相手のつないだリストに残すオブジェクトをいれる
                            newConnectList.Add(playerEnemy.energyConnect.connectList[i]);
                        }
                        // 範囲内なら
                        else
                        {
                            // 相手のつないだリストの範囲内を自分の物にする
                            _connectList.Add(playerEnemy.energyConnect.connectList[i]);

                            // 追加した敵(エネルギー)にそのエネルギーは誰のものかを入れる
                            if (playerEnemy.energyConnect.connectList[i] == null) continue;
                            // プレイヤも格納されてたので回避
                            if (playerEnemy.energyConnect.connectList[i].tag.GetHashCode() != HashTagName.Energy) continue;
                            playerEnemy.energyConnect.connectList[i].gameObject.GetComponent<WhoHaveEnergy>()._whoHave = (WhoHaveEnergy.WhoHave)input.getPlayerType;

                            // 繋ぐエフェクト生成
                            var particle = Instantiate(_connectParticle);
                            particle.transform.position = playerEnemy.energyConnect.connectList[i].gameObject.transform.position;

                            // 次のオブジェクトから範囲外にでるなら追加しないようにする
                            if (playerEnemy.energyConnect._connectList[i] == other.gameObject)
                            {
                                isOutRange = true;
                            }
                        }
                    }

                    //for (int i = 0; i < playerEnemy.energyConnect.connectList.Count; i++)
                    //{
                    //    if (playerEnemy.energyConnect.connectList[i] == null) continue;
                    //    if (other.gameObject != playerEnemy.energyConnect.connectList[i]) continue;

                    //    for (int j = i; j < playerEnemy.energyConnect.connectList.Count; j++)
                    //    {
                    //        if (playerEnemy.energyConnect.connectList[j] == null) continue;
                    //        var whoHaveEnergy = playerEnemy.energyConnect.connectList[j].GetComponent<WhoHaveEnergy>();
                    //        if (whoHaveEnergy == null) continue;
                    //        _connectList.Add(playerEnemy.energyConnect.connectList[j]);
                    //        playerEnemy.energyConnect.connectList[j].GetComponent<WhoHaveEnergy>()._whoHave = (WhoHaveEnergy.WhoHave)input.getPlayerType;

                    //        // 繋ぐエフェクト生成
                    //        var particle = Instantiate(_connectParticle);
                    //        particle.transform.position = playerEnemy.energyConnect.connectList[j].gameObject.transform.position;
                    //    }
                    //    for (int j = 0; j < i; j++)
                    //    {
                    //        newConnectList.Add(playerEnemy.energyConnect.connectList[j]);
                    //    }
                    //    break;
                    //}

                    // 相手のつないだリストを更新
                    playerEnemy.energyConnect.connectList = newConnectList;

                    // ラインを作り直す
                    playerEnemy.energyConnect.AllLineUpdate();
                    AllLineUpdate();
                }
            }
        }
    }

    // 繋ぐリストの新しいものから２つ取り出してラインを作る
    public void CreateLine()
    {
        //if (_connectList.Count >= 2)
        //{
        //    var before = _connectList[_connectList.Count - 2];
        //    var after = _connectList[_connectList.Count - 1];
        //    var color = GetComponent<CharacterParameter>().getParameter.color;

        //    var lineObj = Instantiate(_lineObj);
        //    lineObj.transform.position = before.transform.position;
        //    lineObj.transform.LookAt(after.transform.position);

        //    var streakLineMover = lineObj.GetComponentInChildren<StreakLineLookAt>();
        //    streakLineMover.targetTransform = after.transform;
        //    streakLineMover.color = color;

        //    _lineList.Add(lineObj);

        //}
    }

    // ラインをすべて作り直す
    public void AllLineUpdate()
    {
        foreach (var line in _lineList)
        {
            Destroy(line);
        }
        _lineList.Clear();
        if (_connectList.Count < 2) { return; }
        for (int i = 0, max = _connectList.Count - 1; i < max; ++i)
        {

            var before = _connectList[i];
            var after = _connectList[i + 1];

            if (before == null || after == null) continue;


            var color = GetComponent<CharacterParameter>().getParameter.color;

            var playerType = input.getPlayerType;
            var skillType = characterParameter.getParameter.skillType;

            if (before.tag.GetHashCode() == _energyTagName.GetHashCode())
            {
                var beforeColor = before.GetComponent<HitChangeColor>();
                var afterColor = after.GetComponent<HitChangeColor>();

                if (beforeColor == null || afterColor == null) continue;

                before.GetComponent<HitChangeColor>().ChangeColor(playerType, skillType);
                after.GetComponent<HitChangeColor>().ChangeColor(playerType, skillType);
            }

            //{
            //    var lineObj = Instantiate(_lineObj);
            //    lineObj.transform.position = before.transform.position;
            //    lineObj.transform.LookAt(after.transform.position);

            //    var streakLineMover = lineObj.GetComponentInChildren<StreakLineLookAt>();
            //    streakLineMover.targetTransform = after.transform;
            //    streakLineMover.color = color;

            //    _lineList.Add(lineObj);
            //}

            //var lineDraw = lineObj.GetComponent<LineDraw>();
            //lineDraw._beforeObj = _connectList[_connectList.Count - 2];
            //lineDraw._afterObj = _connectList[_connectList.Count - 1];
        }
    }
}
