namespace Dragon.Core.WebApi
{
    /// <summary>
    /// 事务特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionalAttribute : Attribute
    {
    }
}
