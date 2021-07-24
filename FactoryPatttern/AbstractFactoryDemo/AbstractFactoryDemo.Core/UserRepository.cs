using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDemo.Core
{
    /// <summary>
    /// 定义抽象用户仓储，继承抽象基类仓储
    /// 抽象的目的，是为了给UserRepositoryEFCore、UserRepositorySugar、
    /// 做父类
    /// </summary>
    public abstract class UserRepository : BaseRepository
    {
    }
}
