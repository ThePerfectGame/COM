using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameCompleteZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") GameController.Instance.CompleteGame();
    }
}
