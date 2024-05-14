using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boar : Enemy
{
    
    protected override void Awake() {
        base.Awake();
        currentState = new BoarPotrolState();
    }
    
}
