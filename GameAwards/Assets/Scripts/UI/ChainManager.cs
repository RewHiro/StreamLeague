using UnityEngine;
using UnityEngine.UI;

public class ChainManager : MonoBehaviour
{
    [SerializeField]
    GameObject _chainCanvas = null;

    void OnTriggerEnter(Collider collider)
    {
        if (_chainCanvas == null) return;
        if (collider.tag.GetHashCode() != HashTagName.Player) return;
        if (!collider.GetComponent<Attack>().isCanAttack) return;
        if (collider.GetComponent<PlayerState>().state != PlayerState.State.ATTACK) return;
        var playerType = (int)collider.GetComponent<InputBase>().getPlayerType;
        var ownType = (int)GetComponent<WhoHaveEnergy>()._whoHave;
        if (playerType != ownType) return;

        var canvas = Instantiate(_chainCanvas);
        canvas.transform.position = transform.position;

        var text = _chainCanvas.GetComponentInChildren<Text>();
        text.text = "Chain" + GetComponent<WhoHaveEnergy>().num.ToString();

    }
}