namespace DataModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name ="User name")]
        public string UserName { get; set; }

        [StringLength(32)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "Full name")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Full address")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Active In System or NOT")]
        public bool Status { get; set; }
    }
}
