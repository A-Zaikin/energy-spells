using System.Collections.Generic;
using UnityEngine;

namespace WizardGame
{
    public class ParameterContainer : MonoBehaviour
    {
        private readonly Dictionary<ParameterType, float> parameters = new();

        public void Setup(Dictionary<ParameterType, Modifiable> parameters)
        {
            if (parameters == null)
                return;

            foreach (var (type, value) in parameters)
                this.parameters[type] = value;
        }

        public void Setup(ParameterContainer container)
        {
            if (parameters == null)
                return;

            foreach (var (type, value) in container.parameters)
                parameters[type] = value;
        }

        public void SetupWithValues(Dictionary<ParameterType, float> parameters)
        {
            if (parameters == null)
                return;

            foreach (var (type, value) in parameters)
                this.parameters[type] = value;
        }

        public bool Get(ParameterType type, out float value) => parameters.TryGetValue(type, out value);
    }
}