using System;
using UnityEngine;

namespace Game.Generic
{
    [Serializable]
    public class FloatReference
    {
        [SerializeField] private bool _useConstant;
        [SerializeField] private float _constantValue;
        [SerializeField] private FloatVariableSo variableSo;

        public float Value
        {
            get { return _useConstant ? _constantValue : variableSo.Value; }
        }
    }
}
