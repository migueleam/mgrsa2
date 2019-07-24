namespace mgrsa2.Models
{
    using System;

    public class Login
    {
        public int loginID { get; set; }

        public DateTime? LoginStamp { get; set; }

        //[Required]
        //[StringLength(31)]
        public string LoginUserID { get; set; }

        //[StringLength(20)]
        public string LoginPassword { get; set; }

        //[Required]
        //[StringLength(7)]
        public string LoginCagCodigo { get; set; }

        //[StringLength(30)]
        public string LoginFullName { get; set; }

        public short? LoginCount { get; set; }

        public DateTime? LoginLast { get; set; }

        public int LoginLevelID { get; set; }

        //[StringLength(30)]
        public string LoginPhoneExtension { get; set; }

        //[StringLength(30)]
        public string LoginPhonePassword { get; set; }

        //[Required]
        //[StringLength(1)]
        public string LoginActive { get; set; }

        public bool? LoginOt { get; set; }

        public int? LoginSupervisor { get; set; }

        public bool? LoginSupervisorB { get; set; }

        public bool? LoginDesign { get; set; }

        public bool? LoginCollections { get; set; }

        public bool? LoginTelemarketing { get; set; }

        //[StringLength(50)]
        public string LoginTelemarketingArea { get; set; }

        //[StringLength(50)]
        public string LoginArea { get; set; }

        public bool? LoginTelemarketingSupervisor { get; set; }

        //[StringLength(20)]
        public string LoginCellular { get; set; }

        public int? LoginProvider { get; set; }

        public bool? LoginAdminRpts { get; set; }

        public bool? LoginAPAdmin { get; set; }

        public bool? LoginAdmbAdmin { get; set; }

        public bool? LoginCC { get; set; }

        //[StringLength(50)]
        public string LoginEmail { get; set; }

        public int? LoginNumCtPerm { get; set; }

        //[StringLength(1)]
        public string LoginLevelCtPerm { get; set; }

        public bool? LoginConfirm { get; set; }

        //[StringLength(20)]
        public string LoginDept { get; set; }

        //[StringLength(100)]
        public string LoginContrasena { get; set; }

        public string LoginPhone { get; set; }

    }
}
