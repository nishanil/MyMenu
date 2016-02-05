using System;
using System.Collections.Generic;
using System.Text;

namespace MyMenu
{
    public interface IAzureEntity
    {
        string Id { get; set; }
        string Version { get; set; }
    }

}
