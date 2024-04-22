using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物理检测
/// </summary>
public class PhysicsCheck : MonoBehaviour
{
    CapsuleCollider2D colli;
    [Header("物理检测")]
    public bool isGround;
    public bool isLeftWall;
    public bool isRightWall;
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    private Vector2 _bottomOffset;
    //是否手动设置
    public bool manual;
    public Vector2 leftOffset;
    public Vector2 rightOffset;


    private void Awake()
    {
        colli = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            ChangeDirection(transform,false);
        }
    }
    /// <summary>
    /// 物理引擎更新检测结果
    /// 0.02s/次
    /// </summary>
    private void FixedUpdate()
    {
        // isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }
    /// <summary>
    /// 绘制辅助线
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Gizmos.DrawRay(transform.position, Vector2.down*checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
    /// <summary>
    /// 模型改变方向后，更新判断点的位置
    /// </summary>
    /// <param name="transform">人物模型</param>
    /// <param name="changeBottom">是否改变底部判断点</param>
    public void ChangeDirection(Transform transform, bool changeBottom = true)
    {
        if (transform.localScale.x > 0)
        {
            leftOffset = new Vector2(-colli.bounds.size.x / 2 + colli.offset.x, colli.offset.y);
            rightOffset = new Vector2(colli.bounds.size.x / 2 + colli.offset.x, colli.offset.y);
        }
        else
        {
            rightOffset = new Vector2(colli.bounds.size.x / 2 - colli.offset.x, colli.offset.y);
            leftOffset = new Vector2(-colli.bounds.size.x / 2 - colli.offset.x, colli.offset.y);
        }
        if (changeBottom)
        {
            bottomOffset = new Vector2(-bottomOffset.x, bottomOffset.y);
        }
    }

}
