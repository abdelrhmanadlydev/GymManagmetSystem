using GymManagmetBLL.Service.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.Service.Interfasces
{
    public interface ISessionServices
    {
        IEnumerable<SessionViewModel> GetAllSessions();
    }
}
