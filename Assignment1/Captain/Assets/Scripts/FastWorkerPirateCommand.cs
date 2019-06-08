using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Captain.Command;

namespace Captain.Command
{
    public class FastWorkPirateCommand : ScriptableObject, IPirateCommand
    {
        private float TotalWorkDuration;
        private float TotalWorkDone = 0;
        private float CurrentWork = 0;
        private const float PRODUCTION_TIME = 2.0f;
        private bool Exhausted = false;

        public FastWorkPirateCommand()
        {

        }

        public bool Execute(GameObject pirate, Object productPrefab)
        {
            if (!Exhausted)
            {
                TotalWorkDuration = 5 + Random.Range(0, 5);
                CurrentWork += Time.deltaTime;
                if (CurrentWork >= PRODUCTION_TIME)
                {
                    CurrentWork = 0;
                    TotalWorkDone += PRODUCTION_TIME;
                    Instantiate(productPrefab);
                }
                if (TotalWorkDone >= TotalWorkDuration)
                {
                    Exhausted = true;
                    return false;
                }
                return true;
            } return false;
        }
    }
}
