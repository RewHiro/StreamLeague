using UnityEngine;
using System.Collections;

public class ResultLinesMover : MonoBehaviour
{

    public enum Direction
    {
        LEFT,
        RIGHT
    }

    [SerializeField]
    private Direction _direction = Direction.LEFT;
    public Direction moveDirection
    {
        get { return _direction; }
    }

    //動かすスピード、それぞれのラインはここから参照させる
    private float _moveSpeed = 5.0f;
    public float moveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    //[SerializeField] //動かすラインのオブジェクト
    //private GameObject[] _lineObj = null;

    //時間をカウントする
    private float _timeCount = 0.0f;

    //エンドカウントまではスピードが速い
    [SerializeField]
    private float END_COUNT = 0.5f;

    [SerializeField] //最初のスピード
    private float START_SPEED = 10.0f;

    [SerializeField] //通常時のスピード
    private float NORMAL_SPEED = 0.5f;

    void Start()
    {
        _moveSpeed = START_SPEED;
    }

    void Update()
    {
        if(_timeCount == END_COUNT) { return; }
        _timeCount += Time.deltaTime;
        if (_timeCount > END_COUNT)
        {
           _timeCount = END_COUNT;
            _moveSpeed = NORMAL_SPEED;
        }
    }

}
