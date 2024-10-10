using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Core
{
    public class DynamicCounter
    {
        private bool flag = false;
        private float velocity = 0;
        private readonly Action<float> ValueSetter;
        private readonly Func<float> ValueGetter;
        private float Duration { get; set; }
        private float Target { get; set; }

        public DynamicCounter(Action<float> valueSetter, Func<float> valueGetter)
        {
            ValueGetter = valueGetter;
            ValueSetter = valueSetter;
        }

        public void Start(float duration, float target)
        {
            Duration = duration;
            Target = target;
            flag = true;
        }

        public void Update()
        {
            if (flag && ValueGetter() != Target)
            {
                var newValue = Mathf.SmoothDamp(ValueGetter(), Target, ref velocity, Duration);
                ValueSetter?.Invoke(newValue);
                return;
            }
            else
            {
                flag = false;
            }
        }
    }
}
