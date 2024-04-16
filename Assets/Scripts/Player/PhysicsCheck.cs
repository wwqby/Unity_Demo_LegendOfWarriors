using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物理检测
/// </summary>
public class PhysicsCheck : MonoBehaviour
{
    [Header("地面检测")]
    public bool isGround;
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    /// <summary>
    /// 物理引擎更新检测结果
    /// 0.02s/次
    /// </summary>
    private void FixedUpdate()
    {
        // isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
    }
    /// <summary>
    /// 绘制辅助线
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Gizmos.DrawRay(transform.position, Vector2.down*checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }

}
