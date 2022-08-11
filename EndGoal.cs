using UnityEngine;

public class EndGoal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagConstants.Player)
        {
            GameManager.Instance.ReachGoal();
            // SoundManager.Instance.StartCheering();
        }
    }
}