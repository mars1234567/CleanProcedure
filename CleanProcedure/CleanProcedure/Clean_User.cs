//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanProcedure
{
    using System;
    using System.Collections.Generic;
    
    public partial class Clean_User
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string LoginName { get; set; }
        public string CardID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<short> RoleNum { get; set; }
        public Nullable<short> WorkGroupNum { get; set; }
        public string Department { get; set; }
        public byte[] Signature { get; set; }
    }
}
