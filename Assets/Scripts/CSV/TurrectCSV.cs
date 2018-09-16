﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCSV : CSVBaseData
{
    public int _ID;
    public string _Name;
    public int _Attack;
    public int _Defense;
    public string _Picture;
    public int _Price;
    public Dictionary<int,int> _Materials = new Dictionary<int, int>();
    public int _TechLevel;

    public override string GetPrimaryKey()
    {
        return _ID.ToString();
    }

    public override void ParseData(long index, int fieldCount, string[] headers, string[] values)
    {
        _ID = ReadInt("ID", headers, values);
        _Name = ReadString("Name", headers, values);
        _Attack = ReadInt("Attack", headers, values); 
        _Defense = ReadInt("Defense", headers, values);
        _Picture = ReadString("Picture", headers, values);
        _Price = ReadInt("Price", headers, values);
        var ms = ReadString("Materials", headers, values);
        var array = ms.Split(';');
        for (int i = 0, length = array.Length; i < length; i++)
        {
            var kv = array[i].Split('-'); 
            _Materials.Add(int.Parse(kv[0]), int.Parse(kv[1])); 
        }
        _TechLevel = ReadInt("TechLevel", headers, values);
    }
}
