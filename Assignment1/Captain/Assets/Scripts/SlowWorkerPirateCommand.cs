using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Captain.Command;

namespace Captain.Command
{
    public class SlowWorkPirateCommand : ScriptableObject, IPirateCommand
    {
        private float TotalWorkDuration;
        private float TotalWorkDone = 0;
        private float CurrentWork = 0;
        private const float PRODUCTION_TIME = 8.0f;
        private bool Exhausted = false;

        public SlowWorkPirateCommand()
        {

        }

        public bool Execute(GameObject pirate, Object productPrefab)
        {
            if (!Exhausted)
            {
                TotalWorkDuration = 20 + Random.Range(0, 20);
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
            }
            return false;
        }
    }
}
