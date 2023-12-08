using System.Collections.Generic;
using UnityEngine;
using WizardGame.Data;
using WizardGame.Utility;

namespace WizardGame
{
    public class Weapon : MonoBehaviour
    {
        public static Weapon Current { get; private set; }

        [SerializeField] private WeaponCore core;
        [SerializeField] private ParameterContainer parameters;
        

        public Dictionary<ParameterType, Modifiable> Parameters { get; } = new();

        public OrderedContainer<WeaponMod> Mods { get; } = new(5);

        private readonly List<Modification> modifications = new();

        public void Setup(WeaponData data, ManaContainer manaContainer)
        {
            foreach (var (type, value) in data.StartingParameters)
            {
                Parameters[type] = new Modifiable(value);
            }

            if (parameters != null)
                parameters.Setup(Parameters);

            if (core != null)
                core.Setup(manaContainer);
        }

        private void Awake()
        {
            Current = this;
        }

        private void OnEnable()
        {
            Mods.Observe += ObserveMods;
        }

        private void OnDisable()
        {
            Mods.Observe -= ObserveMods;
        }

        private void ObserveMods(OrderedContainer<WeaponMod> mods)
        {
            foreach (var modification in modifications)
                modification.Dispose();

            foreach (var mod in mods)
            {
                if (mod == null)
                    continue;

                foreach (var modifier in mod.Modifiers)
                {
                    if (Parameters.TryGetValue(modifier.Parameter, out var parameter))
                        modifications.Add(parameter.AddModifier(modifier.Value));
                }
            }

            if (parameters != null)
                parameters.Setup(Parameters);
        }

        private void Update()
        {
            if (core != null)
                core.CanShoot = InputManager.GetMouseButton(0);
        }
    }
}