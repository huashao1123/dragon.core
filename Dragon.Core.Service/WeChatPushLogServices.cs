using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class WeChatPushLogServices : BaseService<WeChatPushLog>, IWeChatPushLogServices
    {
        public WeChatPushLogServices(IBaseRepository<WeChatPushLog> baseRepository) : base(baseRepository)
        {
        }
    }
}
