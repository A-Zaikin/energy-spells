using System.Collections.Generic;
using UnityEngine;
using WizardGame.Data;

namespace WizardGame
{
    public class TeamContainer : MonoBehaviour
    {
        public Team Value { get; private set; }

        public void Setup(Team team)
        {
            Value = team;
        }
    }
}