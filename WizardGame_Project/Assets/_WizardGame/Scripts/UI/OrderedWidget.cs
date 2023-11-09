namespace WizardGame.UI
{
    public abstract class OrderedWidget<TModel> : Widget<TModel>
    {
        public int Index { get; private set; }

        public void Setup(TModel mod, int index)
        {
            Index = index;
            Setup(mod);
        }
    }
}