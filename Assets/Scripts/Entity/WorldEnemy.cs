using UnityEngine;

public class WorldEnemy : MonoBehaviour,IEntity
{
    public EntityType GetEntityType()
    {
        return EntityType.Enemy;
    }
}