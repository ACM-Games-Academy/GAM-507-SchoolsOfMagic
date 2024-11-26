using UnityEngine;
using UnityEngine.Rendering;

public class RootDamageHandler : MonoBehaviour
{

    [SerializeField] float damage = 20f;

    public void Initialize(float damageValue)
    {
        damage = damageValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            ApplyDamageToPlayer(playerController);
        }
    }

    private void ApplyDamageToPlayer(PlayerController playerController)
    {
        playerController.AddReduceValue(PlayerController.ValueType.Health, -damage, false);
        Debug.Log("Damage applied to player: " + damage);
    }
}
