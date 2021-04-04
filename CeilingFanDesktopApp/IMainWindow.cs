using Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeilingFanDesktopApp
{
    /// <summary>
    /// Interface for Main Window due to dependency injection purpose 
    /// </summary>
    public interface IMainWindow
    {
        public IUnitOfWork Uow { get; }
    }
}
