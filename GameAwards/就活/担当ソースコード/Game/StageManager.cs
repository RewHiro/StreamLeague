/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム画面に遷移したらプレイヤーを生成する機能
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField]
    Transform _playerOne;

    [SerializeField]
    Transform _playerTwo;

    void Awake()
    {
        ThenOnePlayerCreate();
        ThenTwoPlayerCreate();
    }

    void Start()
    {

        var sceneHash = SceneManager.GetActiveScene().name.GetHashCode();

        int[] stagesHash = new int[3];
        stagesHash[0] = SceneName.Stage01.GetHashCode();
        stagesHash[1] = SceneName.Stage02.GetHashCode();
        stagesHash[2] = SceneName.Stage03.GetHashCode();

        if (sceneHash == stagesHash[0])
        {
            AudioManager.instance.PlayBgm(SoundName.BgmName.stage02);
        }
        else if (sceneHash == stagesHash[1])
        {
            AudioManager.instance.PlayBgm(SoundName.BgmName.stage01);
        }
        else
        {
            AudioManager.instance.PlayBgm(SoundName.BgmName.stage03);
        }
    }

    /// <summary>
    /// １人プレイ時の処理
    /// </summary>
    void ThenOnePlayerCreate()
    {
        if (!TitleSelect.isSelectOnePlayer) return;
        var oneModels = Resources.LoadAll("Character/Prefabs/1P");
        var twoModels = Resources.LoadAll("Character/Prefabs/AI");

        var onePlayer = Instantiate(oneModels[(int)SelectManager.characterSkillType[0]]) as GameObject;
        var twoPlayer = Instantiate(twoModels[(int)SelectManager.characterSkillType[1]]) as GameObject;

        onePlayer.name = "Player1";
        twoPlayer.name = "Player2";

        onePlayer.transform.position = _playerOne.position;
        onePlayer.transform.rotation = _playerOne.rotation;

        twoPlayer.transform.position = _playerTwo.position;
        twoPlayer.transform.rotation = _playerTwo.rotation;

        if (SceneManager.GetActiveScene().name == SceneName.Stage03)
        {
            onePlayer.transform.localScale = Vector3.one * 1.06f;
            twoPlayer.transform.localScale = Vector3.one * 1.06f;
        }

        switch (TitleSelect.aiLevel)
        {
            case TitleSelect.AILevel.Easy:
                twoPlayer.AddComponent<WeakAI>();
                break;

            case TitleSelect.AILevel.Normal:
                twoPlayer.AddComponent<NormalAI>();
                break;

            case TitleSelect.AILevel.Hard:
                twoPlayer.AddComponent<StrongAI>();
                break;
        }


    }

    /// <summary>
    ///　２人プレイ時の処理
    /// </summary>
    void ThenTwoPlayerCreate()
    {
        if (TitleSelect.isSelectOnePlayer) return;
        var oneModels = Resources.LoadAll("Character/Prefabs/1P");
        var twoModels = Resources.LoadAll("Character/Prefabs/2P");

        var onePlayer = Instantiate(oneModels[(int)SelectManager.characterSkillType[0]]) as GameObject;
        var twoPlayer = Instantiate(twoModels[(int)SelectManager.characterSkillType[1]]) as GameObject;

        onePlayer.name = "Player1";
        twoPlayer.name = "Player2";

        onePlayer.transform.position = _playerOne.position;
        onePlayer.transform.rotation = _playerOne.rotation;

        twoPlayer.transform.position = _playerTwo.position;
        twoPlayer.transform.rotation = _playerTwo.rotation;

        if (SceneManager.GetActiveScene().name == SceneName.Stage03)
        {
            onePlayer.transform.localScale = Vector3.one * 1.06f;
            twoPlayer.transform.localScale = Vector3.one * 1.06f;
        }
    }
}
