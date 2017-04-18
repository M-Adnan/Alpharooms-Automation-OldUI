using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Log
{
    public class Parameter
    {
        private string property;
        private object value;

        public Parameter(string property, object value)
        {
            this.property = property;
            this.value = value;
        }

        public void WriteTo(StringBuilder builder)
        {
            builder.AppendFormat("{0} \"{1}\"", this.property, this.value);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            this.WriteTo(builder);
            return builder.ToString();
        }

        public string Property { get { return this.property; } }
        public object Value { get { return this.value; } }
    }
}
