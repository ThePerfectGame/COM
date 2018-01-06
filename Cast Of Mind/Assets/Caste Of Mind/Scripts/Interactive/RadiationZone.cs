using UnityEngine;

public class RadiationZone : MonoBehaviour
{
    public int damage = 5;
    public float freq = 0.5f;

    private float lastHitTime;

    void OnTriggerStay(Collider other)
    {
        if (Time.time - lastHitTime > freq && other.gameObject.tag == "Player")
        {
            PlayerController.Instance.InRadiationZone = true;
            if (PlayerController.Instance.RadiationProtection == false) PlayerController.Instance.Hit(damage);
            lastHitTime = Time.time;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") PlayerController.Instance.InRadiationZone = false;
    }
}
