using UnityEngine;

public class WorldPodiumEnemy : MonoBehaviour,IEntity
{
    public EntityType GetEntityType()
    {
        return EntityType.PodiumEnemy;
    }
}