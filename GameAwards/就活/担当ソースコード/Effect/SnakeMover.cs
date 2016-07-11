/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

public class SnakeMover : MonoBehaviour
{

    [SerializeField]
    Vector2 MAX_SWING_WIDTH = Vector2.zero;

    [SerializeField]
    float SWING_SPEED = 1.0f;

    [SerializeField]
    float LIFE_TIME = 1.0f;

    [SerializeField]
    GameObject _streakLinePrefab = null;

    public EnergyConnect energyConnect { get; set; }

    GameObject _streakLine = null;

    int _count = 0;

    void Start()
    {

        transform.SetParent(energyConnect.connectList[0].transform.parent, true);
        transform.position = energyConnect.connectList[0].transform.parent.position;

        _streakLine = Instantiate(_streakLinePrefab);
        _streakLine.transform.SetParent(energyConnect.connectList[_count].transform.parent, false);
        _streakLine.transform.position = energyConnect.connectList[_count].transform.parent.position;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {

        while (energyConnect.connectNum < 2)
        {
            yield return null;
        }

        float theta = 0.0f;
        float time = 0.0f;
        float distance = 0.0f;
        float energyDistance = 10.0f;

        var trailRenderer = _streakLine.GetComponent<TrailRenderer>();
        trailRenderer.material.SetColor("_TintColor", GetComponent<CharacterParameter>().getParameter.color);

        while (energyDistance > 0.5f)
        {

            if (_count >= energyConnect.connectNum - 1) yield return Move();

            var end_position = energyConnect.connectList[_count + 1].transform.position;
            var start_position = energyConnect.connectList[_count].transform.position;

            distance = Vector3.Distance(start_position, end_position);

            var swingXWidth = Random.Range(0.0f, MAX_SWING_WIDTH.x);
            var swingYWidth = Random.Range(0.0f, MAX_SWING_WIDTH.y);

            var x = Mathf.Sin(theta) * swingXWidth;
            var y = Mathf.Cos(theta) * swingYWidth;
            var z = distance * (time / LIFE_TIME);

            time += Time.deltaTime;
            theta += Time.deltaTime * SWING_SPEED;

            _streakLine.transform.LookAt(end_position);
            _streakLine.transform.localPosition = new Vector3(x, y, z);

            energyDistance = Vector3.Distance(_streakLine.transform.position, end_position);

            yield return null;
        }

        time = 0.0f;

        _count++;

        while (time < trailRenderer.time && _count >= energyConnect.connectNum - 1)
        {
            if (_count >= energyConnect.connectNum - 1) yield return Move();

            time += Time.deltaTime;
            _streakLine.transform.localPosition = new Vector3(0, 0, distance);
            yield return null;
        }

        if (_count >= energyConnect.connectNum - 1) _count = 0;

        _streakLine.transform.SetParent(energyConnect.connectList[_count].transform.parent, false);
        _streakLine.transform.position = energyConnect.connectList[_count].transform.parent.position;

        if (_count == 0) trailRenderer.Clear();


        yield return Move();
    }
}