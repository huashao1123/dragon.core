using Dragon.Core.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 基类
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class BaseEntity<TPrimaryKey>
    {
        /// <summary>
        /// ID
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public virtual TPrimaryKey? CreatedId { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public virtual TPrimaryKey? UpdateId { get; set; }
    }
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class BaseEntity : BaseEntity<int>
    {
        /// <summary>
        /// 创建时间//   类型后面加问号代表可以为空
        /// </summary>
        public DateTime? CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string? CreatedName { get; set; } = "Admin";

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string? UpdateName { get; set; }

        /// <summary>
        /// 删除标记,软删除
        /// </summary>
        public bool? IsDrop { get; set; } = false;

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public StateEnum Status { get; set; } = StateEnum.Normal;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}
