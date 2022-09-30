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

    }

    public class MessageModel
    {
        public int code { get; set; } = 200;
        public string message { get; set; } = "ok";
        public string type { get; set; } = "sucess";
        public object result { get; set; }
    }
}
