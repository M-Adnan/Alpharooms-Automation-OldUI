using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Log
{
    public class LogAction
    {
        private ActionTypes action;
        private string description;
        private Parameter[] parameters;

        public LogAction(ActionTypes action, string description)
            : this(action, description, new Parameter[0])
        {
        }

        public LogAction(ActionTypes action, string description, Parameter[] parameters)
        {
            this.action = action;
            this.description = description;
            this.parameters = parameters;
        }

        public void WriteTo(StringBuilder builder)
        {
            builder.Append(this.action);
            builder.Append(' ');
            builder.Append(this.description);
            this.ParametersWriteTo(builder);
        }
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            this.WriteTo(builder);
            return builder.ToString();
        }

        private void ParametersWriteTo(StringBuilder builder)
        {
            if (!string.IsNullOrEmpty(this.description) && this.parameters.Length > 0) builder.Append(" ");
            foreach (Parameter parameter in this.parameters)
            {
                parameter.WriteTo(builder);
                builder.Append(" ");
            }
        }

        public ActionTypes Type { get { return this.action; } }
        public string Description { get { return this.description; } }
        public Parameter[] Parameters { get { return this.parameters; } }
    }
}
