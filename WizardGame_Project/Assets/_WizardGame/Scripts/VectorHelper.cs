﻿using UnityEngine;

namespace WizardGame.Extensions
{
    public static class VectorHelper
    {
        public static Vector3 AsXz(this Vector2 vector) => new(vector.x, 0, vector.y);

        public static Vector3 AsXy(this Vector2 vector) => new(vector.x, vector.y, 0);

        public static Vector2 Xz(this Vector3 vector) => new(vector.x, vector.z);

        public static Vector3 WithY(this Vector3 vector, float y) => new(vector.x, y, vector.z);

        public static Vector3 WithZ(this Vector3 vector, float z) => new(vector.x, vector.y, z);

        public static Vector3 WithXz(this Vector3 vector, Vector2 xz) => new(xz.x, vector.y, xz.y);

        public static Vector3 WithLength(this Vector3 vector, float length) => vector.normalized * length;

        public static Vector3 Create(float all) => new(all, all, all);

        public static void ChangeLength(this ref Vector2 vector, float value) =>
            vector = vector.normalized * MathHelper.Clamp0(vector.magnitude + value);

        public static Vector2 CreateFromAngle(float angle)
        {
            var angleInRadians = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
        }
    }
}