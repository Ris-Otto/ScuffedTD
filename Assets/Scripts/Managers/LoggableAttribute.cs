using System;

namespace Managers
{
    public class LoggableAttribute : Attribute
    {
        public string name { get; }

        public LoggableAttribute(string name) {
            this.name = name;
        }
    }
}