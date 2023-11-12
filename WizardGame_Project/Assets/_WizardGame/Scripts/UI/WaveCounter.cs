using TMPro;
using UnityEngine;

namespace WizardGame.UI
{
    public class WaveCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        private void Update()
        {
            if (EnemySpawner.Current != null && text != null)
                text.text = $"Wave {EnemySpawner.Current.WaveCount}";
        }
    }
}