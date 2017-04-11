using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace DAL
{
    public class Common
    {
        public List<T> GetEntityList<T>(DataTable dt) where T : class, new()
        {
            List<T> list = new List<T>();
            try
            {
                //第一步：创建对象实例||||||||Activator.CreateInstance
                System.Type obType = typeof(T);//获取指定名称的类型
                object Instance = Activator.CreateInstance(obType, null);//创建指定类型实例
                                                                         //第二步：获取指定对象的所有公共属性 ||||对象.GetType().GetProperties();
                PropertyInfo[] fields = Instance.GetType().GetProperties();
                //第三步：在DataTable中遍历所有行，将值赋值给相应的属性
                foreach (DataRow dr in dt.Rows)
                {
                    object obj = Activator.CreateInstance(obType, null);
                    foreach (DataColumn dc in dt.Columns)
                    {
                        foreach (PropertyInfo t in fields)
                        {
                            //根据条件DataColumn列名称与属性名称相等
                            if (dc.ColumnName == t.Name)
                            {

                                if (dr[dc.ColumnName] != null && dr[dc.ColumnName] != DBNull.Value)
                                {
                                    //属性类将值复制个相应的属性即可
                                    t.SetValue(obj, dr[dc.ColumnName], null);//给对象赋值
                                }
                                continue;
                            }
                        }
                    }
                    list.Add((T)obj);//将对象填充到list集合
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public List<T> DataToList<T>(DataTable dt) {
            List<T> list = new List<T>();
            var type = typeof(T);
            var obj = Activator.CreateInstance(type, null);
            PropertyInfo [] properties = obj.GetType().GetProperties();
            
            foreach (DataRow row in dt.Rows)
            {
                var model = Activator.CreateInstance(type, null);
                foreach (DataColumn colum in dt.Columns)
                {
                    foreach (var info in properties)
                    {
                        if (colum.ColumnName == info.Name)
                        {
                            if (row[colum.ColumnName] != null && row[colum.ColumnName] != DBNull.Value)
                            {
                                info.SetValue(model, row[colum.ColumnName], null);
                            }
                            continue;
                        }
                    }
                }
                list.Add((T)model);
            }
            return list;
        }
    }
}
