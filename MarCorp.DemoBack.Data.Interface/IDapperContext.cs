using System.Data;

namespace MarCorp.DemoBack.Data.Interface
{
    public interface IDapperContext
    {
        IDbConnection Createconnection();
    }
}
