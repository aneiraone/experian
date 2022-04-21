using System.Collections.Generic;

public class EmailConfiguration
{
    public Smtp Smtp { get; set; }
    public List<Template> Template { get; set; }
}