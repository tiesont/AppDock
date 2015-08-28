using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppDock.Entities;

using FluentData;

namespace AppDock.DataServices
{
    public class DataContext : IDisposable
    {
        string connection = string.Empty;
        IDbProvider provider;

        private IDbContext context;
        public IDbContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new DbContext().ConnectionString(connection, provider);
                }

                return context;
            }
        }

        public DataContext(string connection = "")
        {
            this.connection = connection;
            this.provider = new FluentData.AccessProvider();
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
                    context.Dispose();
                    context = null;
                }

                disposed = true;
            }
        }

        #endregion
    }
}
