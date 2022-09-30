using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{

    public class MenuInput
    {
        public int id { get; set; }
        public int menuType { get; set; }
        public string? createdName { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public int pid { get; set; }
        public string order { get; set; }
        public int status { get; set; }
        public string? icon { get; set; }
        public string path { get; set; }
        public string component { get; set; }
        public string? redirect { get; set; }
        public string? frameSrc { get; set; }

        public string? permission { get; set; }
        public bool ignoreKeepAlive { get; set; }
        public bool hideMenu { get; set; }

        public int mid { get; set; }
    }

    public class MenuListItem : MenuInput
    {
        public string? apiName { get; set; }

        public DateTime createdTime { get; set; }

        public List<MenuListItem> children { get; set; } = new List<MenuListItem>();
    }

    public class MenuParams
    {
        public string? title { get; set; }
        public int menuType { get; set; } = -1;
    }
}
