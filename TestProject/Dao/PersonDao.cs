using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public interface IPersonDao
    {
        void Save();
    }

    public class PersonDao : IPersonDao
    {
        public class Person
        {
            public int Key = 552;
            public class PersonModel {
                public int Key { get; set; }
            }
        }
        public void Save()
        {
            Console.WriteLine("保存 Person");
        }
    }
}
