//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WBL_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_user
    {
        public int u_id { get; set; }
        public string u_username { get; set; }
        public string u_pwd { get; set; }
        public string u_name { get; set; }
        public string u_email { get; set; }
        public Nullable<int> u_type { get; set; }
        public string u_resetpwdCode { get; set; }
    }
}
