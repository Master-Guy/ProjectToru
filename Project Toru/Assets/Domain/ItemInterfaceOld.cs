using System;
using System.Collections.Generic;

namespace Assets.Domain
{
    interface ItemInterfaceOld
    {
        string sprite { get; set; }
        string name { get; set; }
        bool uncovered { get; set; }
        Dictionary<string, Delegate> options { get; set; }

        Dictionary<int, Delegate> getOptions();
    }
}
