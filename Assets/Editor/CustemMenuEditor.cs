using UnityEngine;
using System.Collections;
using UnityEditor;

public class CustemMenuEditor
{
    public static string testExcelName = "Student.xlsx";
    public static string[] testSheetNames = { "sheet1", "sheet2", "sheet3" };   // 一班、二班

    [MenuItem("Assetbundles/Create Assetbundles")]
    public static void ExcuteBuild()
    {
        StudentScriptableObj holder = ScriptableObject.CreateInstance<StudentScriptableObj>();

        //  把 Excel 文件的所有表都  读到 BookElementHolder 中
        holder.StudentTab1 = ExcelAccess.SelectMenuTable<Student>(testExcelName, testSheetNames[0]);
        holder.StudentTab2 = ExcelAccess.SelectMenuTable<Student>(testExcelName, testSheetNames[2]);
        holder.RewardTab1 = ExcelAccess.SelectMenuTable<Reward>(testExcelName, testSheetNames[3]);

        AssetDatabase.CreateAsset(holder, Application.dataPath + "/student.asset");

        // todo 打成 AB
        //AssetImporter import = AssetImporter.GetAtPath(HolderPath);
        //import.assetBundleName = "stutents";
        //BuildPipeline.BuildAssetBundles("Assets/Abs");

        Debug.Log("BuildAsset Success!");
    }

}
