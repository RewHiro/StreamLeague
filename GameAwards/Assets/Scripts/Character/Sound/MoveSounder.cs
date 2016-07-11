/// <author>
/// 新井大一
/// </author>

using UnityEngine;

public class MoveSounder : MonoBehaviour
{
    [SerializeField]
    SoundName.SeName _seName = SoundName.SeName.footstepspeedavoidance;

    InputBase _inputBase = null;
    AudioPlayer _audioPlayer = null;
    Move _move = null;

    void Awake()
    {
        _inputBase = GetComponent<InputBase>();
        _audioPlayer = AudioManager.instance.GetSe(_seName);
        _move = GetComponent<Move>();
    }

    void Update()
    {
        if (!_move.enabled) return;
        if (!_inputBase.isMoved) return;
        if (_audioPlayer.audioSource.isPlaying) return;
        _audioPlayer.Play();
    }
}