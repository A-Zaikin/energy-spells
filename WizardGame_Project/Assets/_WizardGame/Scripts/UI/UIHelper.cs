using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.UI
{
    public static class UIHelper
    {
        public static void SetupWidgets<TModel>(Transform container, IReadOnlyList<TModel> models)
        {
            if (container == null || container.childCount == 0)
                return;

            for (int i = 0, count = container.childCount; i < count; i++)
                container.GetChild(i).gameObject.SetActive(false);

            if (container.GetChild(0).TryGetComponent<OrderedWidget<TModel>>(out var orderedWidget))
                SetupWidgets(container, orderedWidget, models);
            else if (container.GetChild(0).TryGetComponent<Widget<TModel>>(out var normalWidget))
                SetupWidgets(container, normalWidget, models);
        }

        private static void SetupWidgets<TModel>(Transform container, Widget<TModel> widget, IReadOnlyList<TModel> models)
        {
            for (int i = 0, count = models.Count; i < count; i++)
            {
                var model = models[i];

                if (container.childCount > i)
                {
                    if (container.GetChild(i).TryGetComponent<Widget<TModel>>(out var newWidget))
                        newWidget.Setup(model);

                    newWidget.gameObject.SetActive(true);
                }
                else
                {
                    var newWidgetObject = Object.Instantiate(widget.gameObject, container);
                    if (newWidgetObject.TryGetComponent<Widget<TModel>>(out var newWidget))
                        newWidget.Setup(model);
                }
            }
        }

        private static void SetupWidgets<TModel>(Transform container, OrderedWidget<TModel> widget, IReadOnlyList<TModel> models)
        {
            for (int i = 0, count = models.Count; i < count; i++)
            {
                var model = models[i];

                if (container.childCount > i)
                {
                    if (container.GetChild(i).TryGetComponent<OrderedWidget<TModel>>(out var newWidget))
                        newWidget.Setup(model, i);

                    newWidget.gameObject.SetActive(true);
                }
                else
                {
                    var newWidgetObject = Object.Instantiate(widget.gameObject, container);
                    if (newWidgetObject.TryGetComponent<OrderedWidget<TModel>>(out var newWidget))
                        newWidget.Setup(model, i);
                }
            }
        }
    }
}