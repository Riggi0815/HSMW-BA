using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    
    public void Fire() 
    { 
        SprayEnemy parent = GetComponentInParent<SprayEnemy>();
        parent.Attack();
    }

}
