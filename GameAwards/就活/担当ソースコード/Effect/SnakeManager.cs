/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System.Collections;

public class SnakeManager : MonoBehaviour
{
    [SerializeField]
    Vector2 MAX_SWING_WIDTH = Vector2.zero;

    [SerializeField]
    float SWING_SPEED = 1.0f;

    [SerializeField]
    float LIFE_TIME = 1.0f;

    [SerializeField]
    GameObject _streakLinePrefab = null;

    [SerializeField]
    GameObject _snakeManagerPrefab = null;

    GameObject _streakLine = null;
    GameObject _snakeManager = null;

    EnergyConnect _energyConnect = null;

    int _count = 0;

    void Start()
    {

        _energyConnect = GetComponent<EnergyConnect>();

        _snakeManager = Instantiate(_snakeManagerPrefab);

        _streakLine = Instantiate(_streakLinePrefab);
        _streakLine.transform.SetParent(_snakeManager.transform);
        _streakLine.transform.position = _snakeManager.transform.position;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {

        while (_energyConnect.connectNum < 2)
        {
            yield return null;
        }

        if (_count >= _energyConnect.connectNum - 1) _count = 0;

        float theta = 0.0f;
        float time = 0.0f;
        float distance = 0.0f;

        var trailRenderer = _streakLine.GetComponent<TrailRenderer>();
        trailRenderer.material.SetColor("_TintColor", GetComponent<CharacterParameter>().getParameter.color);

        if (
    _energyConnect.connectList[_count + 1] == null ||
    _energyConnect.connectList[_count] == null
    )
        {
            _count = 0;
            yield return null;
            yield return Move();
        }

        _snakeManager.transform.SetParent(_energyConnect.connectList[_count].transform.parent);
        _snakeManager.transform.position = _energyConnect.connectList[_count].transform.parent.position;

        if (_count == 0) trailRenderer.Clear();

        while (time < LIFE_TIME)
        {

            if (_count >= _energyConnect.connectNum - 1) yield return Move();

            if (
                _energyConnect.connectList[_count + 1] == null ||
                _energyConnect.connectList[_count] == null
                )
            {
                _count = 0;
                yield return null;
                yield return Move();
            }

            var end_position = _energyConnect.connectList[_count + 1].transform.position;
            var start_position = _energyConnect.connectList[_count].transform.position;

            distance = Vector3.Distance(start_position, end_position);

            var swingXWidth = Random.Range(0.0f, MAX_SWING_WIDTH.x);
            var swingYWidth = Random.Range(0.0f, MAX_SWING_WIDTH.y);

            var x = Mathf.Sin(theta) * swingXWidth;
            var y = Mathf.Cos(theta) * swingYWidth;
            var z = distance * (time / LIFE_TIME);

            time += Time.deltaTime;
            theta += Time.deltaTime * SWING_SPEED;

            _snakeManager.transform.LookAt(end_position);
            _streakLine.transform.localPosition = new Vector3(x, y, z);

            yield return null;
        }

        time = 0.0f;

        _count++;

        while (time < trailRenderer.time && _count >= _energyConnect.connectNum - 1)
        {
            time += Time.deltaTime;
            _streakLine.transform.localPosition = new Vector3(0, 0, distance);
            yield return null;
        }

        yield return Move();
    }
}