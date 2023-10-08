using UnityEngine;

namespace WizardGame
{
    public static class VectorHelper
    {
        public static Vector3 AsXz(this Vector2 vector) => new(vector.x, 0, vector.y);
    }
}