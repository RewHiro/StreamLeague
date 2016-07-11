using UnityEngine;
using System.Collections;

public class WhoHaveEnergy : MonoBehaviour {

    // GamePadManager にある Type だと誰のものでもないという表現ができなかったので新しく作った
    // Type.ONE == WhoHave.ONE　数値が一致するようにしたのでキャストして使ってください
    /*       
        [Type → WhoHave]
        _whoHave = (WhoHave)type;
        
        [WhoHave → Type]
        (WhoHave.NONE が -1 なので NONE でないことを確認してください)
        _type = _whoHave == WhoHave.NONE ? _type : (GamePadManager.Type)_whoHave;

    */
    public enum WhoHave
    {
        NONE = -1,  // 誰のものでもない
        ALL,  
        ONE,        // プレイヤー1 のもの
        TWO,        // プレイヤー2 のもの
        THREE,      // プレイヤー3 のもの
        FOUR,       // プレイヤー4 のもの
    }

    // デバック用に目視用の SerializeField
    //[SerializeField]
    public WhoHave _whoHave = WhoHave.NONE;

    public int num
    {
        get; set;
    }
}
