using System;

namespace KeepKeeper.Companies
{
    public class CreateCompanyData
    {
        public Guid CompanyId { get; set; }

        public string Name { get; set; }
    }

    public class ChangeCompanyNameData
    {
        public string NewName { get; set; }
    }
}
