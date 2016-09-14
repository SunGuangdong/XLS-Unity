using Excel;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;
using UnityEngine;
              
// Excel文件读取和转换List格式
public class ExcelAccess
{

    public static List<T> SelectMenuTable<T>(string excelName, string tableName) where T : class , new()
    {
        DataRowCollection collect = ExcelAccess.ReadExcel(excelName, tableName);
        List<T> menuArray = new List<T>();

        // 从第1行开始， 0行是Title介绍
        for (int i = 1; i < collect.Count; i++)
        {
            if (collect[i][1].ToString() == "") continue;

            T menu = new T();

            // C# 类在定义的时候 和 表的字段顺序一一对应， 否则很难维护

            FieldInfo[] fieldInfos = menu.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            for (int j = 0; j < fieldInfos.Length; j++)
            {

                Debug.Log(collect[i][j]);
                //fieldInfos[j].SetValue(fieldInfos[j], collect[i][j]);   
                fieldInfos[j].SetValue(fieldInfos[j], SetFieldValue(fieldInfos[j].FieldType, collect[i][j]));
                Debug.Log(collect[i][j]);
            }

            menuArray.Add(menu);
        }
        return menuArray;
    }

    static object SetFieldValue(Type type, object value)
    {
        // 关于各种类型    http://blog.csdn.net/zjdxsunyan/article/details/7539316
        Debug.Log("   " + type.ToString());
        if (type.IsPrimitive)   // 是基元类型 基元类型是 Boolean、 Byte、 SByte、 Int16、 UInt16、 Int32、 UInt32、 Int64、 UInt64、 Char、 Double和 Single。
        {
            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)           // 代替  if (type == typeof(int))
            {
                case TypeCode.Boolean:
                    return (bool)value;
                case TypeCode.Int32:
                    return (int)value;
                case TypeCode.Single:
                    return (float)value;
                case TypeCode.Double:
                    return (double)value;
                case TypeCode.String:
                    return value.ToString();
                case TypeCode.Empty:
                case TypeCode.Object:
                case TypeCode.DBNull:
                case TypeCode.Decimal:
                case TypeCode.DateTime:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                default:
                    break;
            }
        }
        else if (type.IsArray)             // todo 如果是 数组 结构， 需要自己解析
        {
            return null;
        }
        else if(type.IsEnum)
        {
            return 0;
        }

        return 0;
    }


    /// <summary>
    /// 读取 Excel 需要添加 Excel; System.Data;
    /// </summary>
    /// <param name="sheet"></param>
    /// <returns></returns>
    static DataRowCollection ReadExcel(string exelName, string sheet)
    {
        FileStream stream = File.Open(FilePath(exelName), FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        //int columns = result.Tables[0].Columns.Count;
        //int rows = result.Tables[0].Rows.Count;
        return result.Tables[sheet].Rows;
    }

    private static string FilePath(string excelName)
    {
        Debug.Log(UnityEngine.Application.dataPath + "/Excel/" + excelName);
        return UnityEngine.Application.dataPath + "/Excel/" + excelName;
    }
}
