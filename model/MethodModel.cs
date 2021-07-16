using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkModeling.model
{
    public class MethodModel
    {
        public bool IsSelectedByDefault { get; set; }
        public List<string> Properties { get; set; }

        public MethodModel(bool isSelectedByDefault, List<string> properties)
        {
            IsSelectedByDefault = isSelectedByDefault;
            Properties = properties;
        }
    }
}
