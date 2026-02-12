using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationCycle : AnimatedEntity
{
    private float RangeX = 20, RangeY = 20;

    private Vector3 savedDirection;

    // Start is called before the first frame update
    void Start()
    {
        AnimationSetup();
    }

    // Update is called once per frame
    void Update()
    {
         AnimationUpdate();
         transform.position += savedDirection;
    
    }
}
