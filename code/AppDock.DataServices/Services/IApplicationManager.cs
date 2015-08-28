using System;
using System.Collections.Generic;

namespace AppDock.DataServices
{
    public interface IApplicationManager : IDisposable
    {
        Entities.ILabApplication GetApplication(int id);
        IEnumerable<Entities.ILabApplication> GetApplications(bool activeOnly = true);
        bool InsertOrUpdateApplication(Entities.ILabApplication application);
        bool RemoveApplication(int id);
    }
}
