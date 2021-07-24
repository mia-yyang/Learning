using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    public class RepositorySimpleFactory
    {
        public static Repository GetRepository(string type)
        {
            Repository repository = null;
            if (type.Equals("sugar"))
            {
                repository = new RepositorySqlsugar();
            }
            else if (type.Equals("efcore"))
            {
                repository = new RepositoryEFCore();
            }
            return repository;
        }
    }
}
