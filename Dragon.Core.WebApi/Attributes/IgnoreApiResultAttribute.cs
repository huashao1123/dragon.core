namespace Dragon.Core.WebApi
{
    /// <summary>
    /// 是否忽略统一返回格式
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IgnoreApiResultAttribute : Attribute
    {
       public bool ignore { get; set; } = false;
    }
}
