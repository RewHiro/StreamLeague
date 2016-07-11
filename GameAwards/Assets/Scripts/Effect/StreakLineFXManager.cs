/// <author>
/// 新井大一
/// </author>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakLineFXManager : MonoBehaviour
{
    [SerializeField]
    TrailRenderer _trailRenderer = null;

    [SerializeField]
    Color _color = new Color(0, 0, 0);

    public Color color
    {
        get
        {
            return _color;
        }

        set
        {
            _color = value;
        }
    }

    public float delay
    {
        get; set;
    }

    List<GameObject> _particleObject = new List<GameObject>();
    ParticleSystem.Particle[] _particles = null;
    ParticleSystem _particleSystem = null;
    float _time = 0.0f;
    float _playTime = 0.0f;
    bool _isStart = false;

    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[_particleSystem.maxParticles];

        foreach (var particle in _particles)
        {
            var game_object = Instantiate(_trailRenderer.gameObject, particle.position, Quaternion.identity) as GameObject;
            game_object.transform.SetParent(transform, true);
            _particleObject.Add(game_object);
        }
    }

    void Start()
    {
        StartCoroutine(Wait());
    }

    void Update()
    {
        if (!_isStart) return;

        _time += Time.deltaTime;
        if (_time >= _playTime)
        {
            _time = 0.0f;
            _particleSystem.Play();
        }

        _particleSystem.GetParticles(_particles);
        for (int i = 0; i < _particles.Length; i++)
        {
            _particleObject[i].transform.localPosition = _particles[i].position;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay * 1.0f);

        foreach (var particle in _particleObject)
        {
            var traileRenderer = particle.GetComponent<TrailRenderer>();
            traileRenderer.material.SetColor("_TintColor", color);
            traileRenderer.time = _particleSystem.startLifetime * 0.8f;
        }

        _playTime = 2.0f;

        _time = _playTime;

        _isStart = true;
    }
}
