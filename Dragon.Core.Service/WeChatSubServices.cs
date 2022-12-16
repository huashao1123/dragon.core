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
    public class WeChatSubServices : BaseService<WeChatSub>, IWeChatSubServices
    {
        public WeChatSubServices(IBaseRepository<WeChatSub> baseRepository) : base(baseRepository)
        {
        }
    }
}
