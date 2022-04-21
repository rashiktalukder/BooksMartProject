using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksMartProject.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoryRepository Category { get; }

        ICoverTypeRepository CoverType{ get; }

        IStore_Procedure_Calls SP_Call { get; }

        /*ICoverTypeRepository CoverType { get; }*/

        void Save();
    }
}
