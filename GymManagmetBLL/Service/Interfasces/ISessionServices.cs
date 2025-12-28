using GymManagmetBLL.ViewModels.SessionViewModel;
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
        SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionVeiwModel createSession);
        UpdateSessionVeiwModel? GetSessionForUpdate(int id);
        bool UpdateSession(int id, UpdateSessionVeiwModel updateSession);
        bool RemoveSession(int id);
    }
}
