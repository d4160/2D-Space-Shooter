using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using UnityEngine;

public class DestroyByLimitsAuthoring : AuthoringBehaviourBase<Limits>
{
    private void Update()
    {
        if (transform.position.y > _data.yUpperLimit)
        {
            Destroy(gameObject);
        }
    }
}
