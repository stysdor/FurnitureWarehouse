using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NHibernate
{
    public class NHibernateBase
    {
        #region Fields
        private static Configuration configuration { get; set; }
        private static ISession session { get; set; }
        private static IStatelessSession statelessSession { get; set; }
        #endregion

        #region Properties
        protected static ISessionFactory SessionFactory { get; set; }

        public static ISession Session => session = SessionFactory.OpenSession();
        public static IStatelessSession StatelessSession => (statelessSession = SessionFactory.OpenStatelessSession());
        #endregion

        #region Methods

        private static Configuration ConfigureNHibernate()
        {
            try
            {
                configuration = new Configuration();
                configuration.Configure();
                return configuration;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void Initialize()
        {
            configuration = ConfigureNHibernate();
            SessionFactory = configuration.BuildSessionFactory();
            new SchemaExport(configuration);
        }

        #endregion
    }
}
