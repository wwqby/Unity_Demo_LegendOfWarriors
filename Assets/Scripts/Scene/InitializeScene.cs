using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// 初始化脚本
/// </summary>
public class InitializeScene : MonoBehaviour
{

    public AssetReference scene;
    private void Start() {
        Addressables.LoadSceneAsync(scene);
    }
}
