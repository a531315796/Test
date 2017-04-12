using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dao;


namespace DaoFactory
{
    public static class DataAccess
    {
        public static IPersonDao CreatePersonDao()
        {
            return new PersonDao();
        }
    }
}
