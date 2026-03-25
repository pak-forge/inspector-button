using System;
using System.Diagnostics;

namespace YoWorld.Core.Utilities.InspectorButton
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public class InspectorButtonAttribute : Attribute
    {
        public string ButtonName { get; }

        public InspectorButtonAttribute(string buttonName = "")
        {
            ButtonName = buttonName;
        }
    }
}
