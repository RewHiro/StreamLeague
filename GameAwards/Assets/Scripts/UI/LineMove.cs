using UnityEngine;
using System;
using System.Collections.Generic;

public class LineMove : MonoBehaviour {

    //親に入ってる管理スクリプト
    private ResultLinesMover _manager = null;

    //向きを決める
    private ResultLinesMover.Direction _directionType;

    //親が設定した向きにする
    private Vector3 _direction = Vector3.zero;

    private float ReversePosX = 1920.0f;

    private Dictionary<ResultLinesMover.Direction, Action> _update;

    void Start()
    {
        _update = new Dictionary<ResultLinesMover.Direction, Action>();
        _update.Add(ResultLinesMover.Direction.LEFT, Left);
        _update.Add(ResultLinesMover.Direction.RIGHT, Right);
        _manager = GetComponentInParent<ResultLinesMover>();
        _directionType = _manager.moveDirection;
        if(_directionType == ResultLinesMover.Direction.LEFT)
        {
            _direction = Vector3.left;
            ReversePosX = -1920.0f;
        }
        else if(_directionType == ResultLinesMover.Direction.RIGHT)
        {
            _direction = Vector3.right;
            ReversePosX = 1920.0f;
        }
    }

    void Update()
    {
        transform.Translate(_direction * _manager.moveSpeed * Time.deltaTime);
        _update[_directionType]();
    }

    void Left()
    {
        if (transform.localPosition.x < ReversePosX)
        {
            var pos = transform.localPosition;
            pos.x = -ReversePosX;
            transform.localPosition = pos;
        }
    }

    void Right()
    {
        if (transform.localPosition.x > ReversePosX)
        {
            var pos = transform.localPosition;
            pos.x = -ReversePosX;
            transform.localPosition = pos;
        }
    }
}
