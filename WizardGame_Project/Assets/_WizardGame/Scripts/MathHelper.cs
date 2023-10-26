namespace WizardGame.Extensions
{
    public static class MathHelper
    {
        public static float Clamp0(float value) => value < 0 ? 0 : value;
    }
}