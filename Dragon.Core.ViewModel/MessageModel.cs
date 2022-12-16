using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class MessageModel<T>
    {
        public int code { get; set; } = 200;
        public string message { get; set; } = "ok";
        public string type { get; set; } = "success";
        public T result { get; set; }

        public bool success { get; set; } = true;

        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="success">失败/成功</param>
        /// <param name="msg">消息</param>
        /// <param name="response">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Message(bool success, string msg, T response)
        {
            return new MessageModel<T>() { message = msg, result = response, success = success };
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static MessageModel<T> Success(string msg)
        {
            return Message(true, msg, default);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="response">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Success(string msg, T response)
        {
            return Message(true, msg, response);
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(string msg)
        {
            return Message(false, msg, default);
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="response">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(string msg, T response)
        {
            return Message(false, msg, response);
        }
    }

    public class MessageModel
    {
        public int code { get; set; } = 200;
        public string message { get; set; } = "ok";
        public string type { get; set; } = "sucess";
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; } = true;
        public object result { get; set; }
    }
}
