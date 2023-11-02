using UnityEngine;

namespace WizardGame
{
    public class LootCollector : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Loot>(out var loot))
            {
                var mod = new WeaponMod(loot.Quality);
                inventory.AddMod(mod);
                Destroy(loot.gameObject);
            }
        }
    }
}
