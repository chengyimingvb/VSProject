using System;

namespace CYM
{
    [Unobfus]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class Unobfus : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DynStr : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DynIcon : Attribute
    {

    }
    public class TextAttribute : Attribute
    {
        public TextAttribute(string value)
        {
            Value = value;
        }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Value { get; set; }
    }
}
