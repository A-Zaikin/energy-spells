using System;
using System.Collections.Generic;

namespace WizardGame.Utility
{
    public class EventCounter
    {
        public event Action OnCompleted;

        private readonly List<Action> actions = new();
        private int counter;

        public Action Subscribe()
        {
            var action = new Action(Completed);
            actions.Add(action);
            return action;
        }

        private void Completed()
        {
            counter++;

            if (counter == actions.Count)
                OnCompleted?.Invoke();
        }
    }
}