using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;

namespace MyselfIoC
{
    public class MyXmlFactory
    {
        private Dictionary<string, object> objectDefine = new Dictionary<string, object>();
        public MyXmlFactory(string fileName,string objectName= "object")
        {
            InstanceObjects(fileName, objectName);
        }

        private void InstanceObjects(string fileName,string objectName)
        {
            XElement xml = XElement.Load(fileName);
            var objects = from obj in xml.Elements(objectName) select obj;
            objectDefine = objects.ToDictionary(
                key => key.Attribute("id").Value,
                value =>
                {
                    string typeName = value.Attribute("type").Value;
                    Type type = Type.GetType(typeName);
                    return Activator.CreateInstance(type);
                }
            );
        }

        public object GetObject(string key)
        {
            object result = null;
            if (objectDefine.ContainsKey(key))
            {
                result = objectDefine[key];
            }
            return result;
        }
    }
}
