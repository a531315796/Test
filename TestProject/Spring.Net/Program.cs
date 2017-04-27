using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dao;
using DaoFactory;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;
using MyselfIoC;

namespace Spring.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            //Function();
            XmlFactory();
            //FactoryMethod();
            //IoCMethod();
            //NormalMethod();
        }

        private static void Function() {
            string a=null, b;
            b = (a ?? "6") == null ? "66" : "666";
            Console.WriteLine(b);
        }

        private static void XmlFactory()
        {
            var path =System.Environment.CurrentDirectory+ @"\SpringObjects.xml";
            MyXmlFactory xml = new MyXmlFactory(path);
            Console.WriteLine(xml.GetObject("staticObjectsFactory").ToString());
        }

        private static void NormalMethod()
        {
            IPersonDao dao = new PersonDao();
            dao.Save();
            Console.WriteLine("我是一般方法");
        }

        private static void FactoryMethod()
        {
            IPersonDao dao = DataAccess.CreatePersonDao();
            dao.Save();
            Console.WriteLine("我是工厂方法");
        }

        private static void IoCMethod()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            IPersonDao dao = ctx.GetObject("PersonDao") as IPersonDao;
            PersonDao.Person person = ctx.GetObject("Person") as PersonDao.Person;
            PersonDao.Person.PersonModel personModel = ctx.GetObject("PersonModel") as PersonDao.Person.PersonModel;
            if (dao != null)
            {
                dao.Save();
                Console.WriteLine("我是IoC方法");
            }
        }


        private static void IoCMethod1()
        {
            string[] xmlFiles = new string[]
            {
                "assembly://Spring.Net/Spring.Net/SpringObjects.xml"
            };
            IApplicationContext context = new XmlApplicationContext(xmlFiles);

            IObjectFactory factory = (IObjectFactory)context;
            var personDao = factory.GetObject("PersonDao") as IPersonDao;
            if (personDao != null)
            {
                personDao.Save();
                Console.WriteLine("我是IoC方法");
            }
            Console.ReadLine();
        }
    }
}
