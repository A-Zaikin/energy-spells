using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardGame.UI
{
    public class MainMenuPlayButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(1);
        }
    }
}
