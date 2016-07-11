using UnityEngine;
using System.Collections;

/// <summary>
/// 集中型の時にエネルギーが移動してほしい場所を決める
/// </summary>
public class EnergyConcentrationPos : MonoBehaviour {

    [SerializeField]
    private Vector3 _concentrationPos;

    public Vector3 concentrationPos
    {
        get { return _concentrationPos; }
    }

    [SerializeField]
    private Vector3 _rotatePos;

    public Vector3 rotateStartPos
    {
        get { return _rotatePos; }
    }

    [SerializeField]
    private Vector3 _normalPos;

    public Vector3 normalPos
    {
        get { return _normalPos; }
    }

}
