using System.Data;

namespace MarCorp.DemoBack.Application.Interface.Persistence
{
    public interface IDapperContext
    {
        IDbConnection Createconnection();
    }
}
