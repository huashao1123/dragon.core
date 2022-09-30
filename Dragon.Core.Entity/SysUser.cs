namespace Dragon.Core.Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class SysUser:BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别true=男。false=女
        /// </summary>
        public bool Sex { get; set; } = true;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string? WeChat { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; } = "";
        /// <summary>
        /// 工号=登陆账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 历史密码
        /// </summary>
        public string? HistroyPsw { get; set; }

        /// <summary>
        ///错误次数 
        /// </summary>
        public int ErrorCount { get; set; } = 0;

        /// <summary>
        ///最后登录时间 
        /// </summary>
        public DateTime LastErrTime { get; set; } = DateTime.Now;
        // <summary>
        // 组织Id{用户所属组织 Or 公司}
        // </summary>
        //public int? OrganizeId { get; set; }
        /// <summary>
        /// 部门Id{用户所属部门}
        /// </summary>
        public int DepartmentId { get; set; } = 0;
        /// <summary>
        /// 员工类型
        /// </summary>
        public int EmpliyeeType { get; set; } = 0;

        /// <summary>
        /// 职务名称
        /// </summary>
        public string? JobName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? Avater { get; set; }
    }
}
