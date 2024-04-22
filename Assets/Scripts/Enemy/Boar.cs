using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    override public void Move()
    {
        base.Move();
        animator.SetBool("isWalk", true);
    }
}
