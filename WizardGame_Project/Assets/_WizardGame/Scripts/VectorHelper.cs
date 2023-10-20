using UnityEngine;

namespace WizardGame.Extensions
{
    public static class VectorHelper
    {
        public static Vector3 AsXz(this Vector2 vector) => new(vector.x, 0, vector.y);

        public static Vector3 AsXy(this Vector2 vector) => new(vector.x, vector.y, 0);

        public static Vector2 Xz(this Vector3 vector) => new(vector.x, vector.z);

        public static Vector3 WithY(this Vector3 vector, float y) => new(vector.x, y, vector.z);

        public static Vector3 WithZ(this Vector3 vector, float z) => new(vector.x, vector.y, z);

        public static Vector3 WithLength(this Vector3 vector, float length) => vector.normalized * length;

        public static Vector3 Create(float all) => new(all, all, all);
    }
}