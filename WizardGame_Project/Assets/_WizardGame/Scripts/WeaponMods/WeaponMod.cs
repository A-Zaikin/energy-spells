﻿using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace WizardGame
{
    public class WeaponMod
    {
        private readonly List<Modifier> modifiers = new();

        public WeaponMod(Quality quality)
        {
            var count = quality switch
            {
                Quality.Common => 1,
                Quality.Uncommon => 2,
                Quality.Rare => 3,
                _ => 0
            };

            for (var i = 0; i < count; i++)
            {
                var modifier = new Modifier(GetRandomParameter(), Random.Range(0.01f, 0.5f));
                modifiers.Add(modifier);
            }
        }

        public IReadOnlyList<Modifier> Modifiers => modifiers;

        private static ParameterType GetRandomParameter()
        {
            var random = Random.Range(0, (int)ParameterType.NUM);
            return (ParameterType)random;
        }

        public readonly struct Modifier
        {
            public readonly ParameterType Parameter;
            public readonly float Value;

            public Modifier(ParameterType parameter, float value)
            {
                Parameter = parameter;
                Value = value;
            }
        }
    }
}