using UnityEngine;

public class ContactDamageSystem : MonoBehaviour
{
    [SerializeField] private int damageToDeal;
    [SerializeField] private LayerMask targetLayers;

    [Space(10)]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private bool destroySelfOnContact = false;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayers) != 0)
        {
            // Deal damage to object hit (if it has health)
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damageToDeal);
            }

            // Self destruct if necessary
            if (destroySelfOnContact && healthSystem != null)
            {
                healthSystem.TakeDamage(healthSystem.MaxHealth);
            }
        }
    }
}
