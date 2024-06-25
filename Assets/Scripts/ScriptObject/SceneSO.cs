using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
/// <summary>
/// 场景本地资源管理
/// </summary>
[CreateAssetMenu(fileName = "New SceneSO", menuName = "SceneAsset/SceneSO")]
public class SceneSO : ScriptableObject
{   
    //资源文件
    public AssetReference sceneReference;
    //场景类型
    public SceneType sceneType;
    //玩家初始位置
    public Vector3 playerPosition;
}
