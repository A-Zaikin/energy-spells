using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.UI
{
    public static class UIHelper
    {
        public static void SetupWidgets<TModel>(Transform container, IReadOnlyList<TModel> models)
        {
            if (models == null ||
                container == null ||
                container.childCount == 0 ||
                !container.GetChild(0).TryGetComponent<Widget<TModel>>(out var widget))
            {
                return;
            }

            for (int i = 0, count = container.childCount; i < count; i++)
                container.GetChild(i).gameObject.SetActive(false);

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
                    var newWidget = Object.Instantiate(widget, container);
                    newWidget.Setup(model);
                }
            }
        }
    }
}