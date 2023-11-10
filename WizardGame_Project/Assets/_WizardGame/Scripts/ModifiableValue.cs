using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardGame
{
    public class Modifiable
    {
        public float BaseValue;

        public readonly List<Modification> Modifiers = new();

        public Modifiable(float baseValue)
        {
            BaseValue = baseValue;
        }

        public Modification AddModifier(float value)
        {
            return new Modification(this, value);
        }

        public float Value
        {
            get
            {
                var totalMultiplier = 1 + Modifiers.Sum(modifier => modifier.Value);
                return BaseValue * totalMultiplier;
            }
        }

        public static implicit operator float(Modifiable modifiable) => modifiable.Value;
    }

    public class Modification : IDisposable
    {
        public float Value;

        private readonly Modifiable modifiable;

        public Modification(Modifiable modifiable, float value)
        {
            Value = value;
            modifiable.Modifiers.Add(this);
            this.modifiable = modifiable;
        }

        public void Dispose()
        {
            modifiable?.Modifiers.Remove(this);
        }
    }
}