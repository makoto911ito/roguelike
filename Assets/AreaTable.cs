using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaObj
{
    walk = 0,
    obj2 = 1,
    obj3 = 2,
}

public class AreaTable : MonoBehaviour
{
    [SerializeField] AreaObj _areaObj;

    public AreaObj AreaObj
    {
        get => _areaObj;
        set
        {
            _areaObj = value;
        }
    }
}
