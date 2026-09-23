using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHO;

namespace PluginApi
{
    public sealed class EditorTarget
    {
        public object Value { get; }

        /// <summary>
        /// Archive this object belongs to, if applicable.
        /// </summary>
        public Handler? Archive { get; }

        public EditorTarget(object value, Handler? archive = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Value = value;
            Archive = archive;
        }
    }

}
