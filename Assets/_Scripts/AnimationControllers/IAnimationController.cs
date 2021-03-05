using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationController 
{
    void SetVelocity(float v);

    void SetAttack(Transform playerTransform);

    void SetDeath();

    bool GetAttacking();
}
