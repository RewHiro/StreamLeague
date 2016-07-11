using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.GetHashCode() != HashTagName.Player) return;
        
    }
}
