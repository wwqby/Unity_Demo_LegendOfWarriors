using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDefination : MonoBehaviour
{   
    [Header("是否重新生成GUID")]
    public bool clear;
    [Header("GUID")]
    public string ID;

    private void OnValidate()
    {
        if (clear)
        {
            clear = false;
            ID = string.Empty;
        }
        if (ID == "")
            ID = System.Guid.NewGuid().ToString();
    }
}
