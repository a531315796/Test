using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BuildingDAL
    {
        public DataTable GetAll() {
            string sql = "select top 100 * from building";
            var list = new Common().GetEntityList<Model.Building> (new SqlHelp().ExecuteReader(null, sql));
            var list1= new Common().DataToList<Model.Building>(new SqlHelp().ExecuteReader(null, sql));
            return new SqlHelp().ExecuteReader(null, sql);
        }
    }
}
