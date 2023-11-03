using UnityEngine;

namespace WizardGame.UI
{
    public abstract class Widget<TModel> : MonoBehaviour
    {
        public abstract void Setup(TModel mod);
    }
}