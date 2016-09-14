using UnityEngine;
using System.Collections.Generic;

// 对应一个  Excel        
// 注意： 你也一个Tab表就是一个ScriptableObject对象这样设计！    将同一个Tab表对应的 C#字段类和  ScriptableObject类放在一个文件管理方式也是很不错的呦
public class StudentScriptableObj : ScriptableObject
{
    //  Excel 的所有表！        ， 如果一个Tab对应一个 ScriptableObject 那就是一个List 了
    public List<Student> StudentTab1;
    public List<Student> StudentTab2;
    public List<Reward> RewardTab1;
}
