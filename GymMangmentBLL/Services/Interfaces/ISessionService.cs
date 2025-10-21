using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymMangmentBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionById(int sessionId);

        bool CreateSession(CreateSessionViewModel CreatedSession);

        UpdateSessionViewModel? GetSessionForUpdate(int sessionId);
        bool UpdateSession(int sessionId, UpdateSessionViewModel UpdatedSession);

        bool DeleteSession(int sessionId);

        IEnumerable<TrainerSelectViewModel> GetTrainersForDropDown();
        IEnumerable<CategorySelectViewModel> GetCategoryForDropDown();

    }
}
