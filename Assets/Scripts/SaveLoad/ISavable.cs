using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可保存数据
/// </summary>
public interface ISavable
{
    //确保有资源唯一ID
    DataDefination GetDataDefination();
    //数据注册
    void RigisterData(){
        DataManager.instance.RegisterISavableData(this);
    }
    //数据注销
    void UnRigisterData(){
        DataManager.instance.UnRegisterISavableData(this);
    }
    //数据保存
    void SaveData(DataModel dataModel);
    //数据加载
    void LoadData(DataModel dataModel);

}
