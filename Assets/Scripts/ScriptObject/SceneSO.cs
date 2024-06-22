using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
/// <summary>
/// 场景本地资源管理
/// </summary>
[CreateAssetMenu(fileName = "New SceneSO", menuName = "Scene/SceneSO")]
public class SceneSO : ScriptableObject
{
    public AssetReference sceneReference;
    public SceneType sceneType;
}
