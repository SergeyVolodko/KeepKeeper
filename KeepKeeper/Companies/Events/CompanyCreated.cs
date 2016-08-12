using System;
using KeepKeeper.Common;

namespace KeepKeeper.Companies.Events
{
    public class CompanyCreated: Event
    {
        public string Name { get; set; }
    }
}
