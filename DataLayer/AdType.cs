//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdType
    {
        public AdType()
        {
            this.Cars = new HashSet<Car>();
        }
    
        public System.Guid AdTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<int> SortOrder { get; set; }
    
        public virtual ICollection<Car> Cars { get; set; }
    }
}
