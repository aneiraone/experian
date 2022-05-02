using System.Collections.Generic;

namespace Common
{
    public class EmailConfiguration
    {
        public Smtp Smtp { get; set; }
        public List<Template> Template { get; set; }
    }
}