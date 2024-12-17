using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCorp.DemoBack.Support.Common
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
