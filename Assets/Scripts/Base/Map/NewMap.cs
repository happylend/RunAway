using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewMap 
{
    public int Map_X { get => 15;}
    public int Map_Y { get => 10;}

    public int Map_Num { get => Map_Num; set => Map_Num = value; }
    public enum GroundType
    {
        floor,
        ice
    }
    public enum UpperType
    {
        player,
        treasure,
        stone,
        icestone,
        blower
    }

    //读取关卡文件
    public List<List<string>> ReadFile(string Map_name)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(Map_name);//载入关卡

        string[] map_rowString = textAsset.text.Trim().Split('\n');//Trim表示清除空格，然后在以换行符作为一行 范例为["1,1,1" "2,3,2,2" "4,4,4" "6,6,6"]
        var MapList = new List<List<string>>(); //用来储存的容器
        
        //存入容器
        for (int i = 0; i < map_rowString.Length; i++)
        {
            var map_row = new List<string>(map_rowString[i].Split(','));//将逗号作为分割点 将此时的行 传入一个一维数组
            MapList.Add(map_row);//将这个一维数组加入储存的容器
        }
        return MapList;
    }

    //生成关卡


}
