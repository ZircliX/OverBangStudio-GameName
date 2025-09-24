using System;
using UnityEngine;

namespace LTX.Tools.Settings.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class LTXSettingsTitleAttribute : Attribute
    {
        public static Color DefaultColor { get; } = new Color(0.2f, 0.54f, 0.8f);

        public readonly string title;
        public readonly Color color;

        public LTXSettingsTitleAttribute(string title)
        {
            this.title = title;
            color = DefaultColor;
        }

        public LTXSettingsTitleAttribute(string title, string color)
        {
            this.title = title;
            if (ColorUtility.TryParseHtmlString(color, out Color c))
                this.color = c;
            else
                this.color = DefaultColor;
        }
    }
}