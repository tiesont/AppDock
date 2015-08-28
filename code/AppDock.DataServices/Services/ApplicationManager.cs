using System;
using System.Collections.Generic;
using System.Linq;

using AppDock.Entities;

namespace AppDock.DataServices
{
    public class ApplicationManager : IApplicationManager
    {
        DataContext database;
        DataContext Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataContext(AppSettings.ConnectionString);
                }

                return database;
            }
        }


        #region IDisposable Members

        bool disposed = false;

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeAll)
        {
            if (!disposed)
            {
                if (disposeAll)
                {
                    database.Dispose();
                    database = null;
                }

                disposed = true;
            }
        }

        #endregion


        public ILabApplication GetApplication(int id)
        {
            ILabApplication app = null;

            string query =
                "SELECT app.ID AS ApplicationID, app.Label, app.Path, app.StartInPath, app.TypeID, app.IsActive, t.Name  " + 
                "FROM LabApplication app " +
                "LEFT JOIN ApplicationType t ON t.ID = app.TypeID " + 
                "WHERE ID = @id";

            app = Database.Context
                .Select<LabApplication>(query)
                .Parameter("id", id)
                .QuerySingle();

            return app;
        }


        public IEnumerable<ILabApplication> GetApplications(bool activeOnly = true)
        {
            string query =
                "SELECT app.ID AS ApplicationID, app.Label, app.Path, app.StartInPath, app.TypeID, app.IsActive, t.Name " +
                "FROM LabApplication app " +
                "LEFT JOIN ApplicationType t ON t.ID = app.TypeID ";

            if (activeOnly)
            {
                query += "WHERE app.IsActive = True";
            }

            return Database.Context
                .Sql(query)
                .QueryMany((LabApplication application, dynamic row) =>
                {
                    application.ID = row.ApplicationID;
                    application.Path = row.Path;
                    application.Label = row.Label;
                    application.StartInPath = row.StartInPath;
                    application.TypeID = row.TypeID;
                    application.IsActive = row.IsActive;
                    application.Type = new TypeEntity
                    {
                        ID = row.TypeID,
                        Name = row.Name
                    };
                })
                .AsQueryable();
        }


        public bool InsertOrUpdateApplication(Entities.ILabApplication application)
        {
            bool success = false;
            int rows = 0;

            if (application.ID > 0)
            {
                rows = Database.Context
                    .Update("LapApplication", application)
                    .Execute();
            }
            else
            {
                rows = Database.Context
                    .Insert("LabApplication", application)
                    .Execute();
            }

            success = rows > 0;

            return success;
        }


        public bool RemoveApplication(int id)
        {
            bool success = false;

            int rows = Database.Context
                .Delete("LapApplication")
                .Where("ID", id)
                .Execute();

            success = rows == 1;

            return success;
        }
    }
}
