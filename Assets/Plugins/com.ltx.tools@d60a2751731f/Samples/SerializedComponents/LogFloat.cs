﻿using LTX.Tools.SerializedComponent;
using UnityEngine;

namespace LTX.Tools.Samples.SerializedComponents
{
    [System.Serializable, AddSerializedComponentMenu("LTX/Samples/LogFloat")]
    public struct LogFloat : ISampleSComponent
    {
        [SerializeField, Range(0, 2)]
        private float number;

        public void Log()
        {
            Debug.Log(number);
        }
    }
}