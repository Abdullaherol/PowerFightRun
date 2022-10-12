using System.Collections.Generic;
using UnityEngine;

public class PodiumHelper : MonoBehaviour
{
    public GameObject prefab;

    public Transform parent;
    
    public Vector3 space;
    public List<int> line = new List<int>();

    public List<GameObject> enemies = new List<GameObject>();

    public void Spawn()
    {
        Remove();
        
        for (int i = 0; i < line.Count; i++)
        {
            var enemy1 = Instantiate(prefab, parent);
            var enemy2 = Instantiate(prefab, parent);
            var enemy3 = Instantiate(prefab, parent);

            var position = transform.position;
            enemy1.transform.position = position + new Vector3(space.x, space.y, space.z * i);
            enemy2.transform.position = position + new Vector3(0, space.y, space.z * i);
            enemy3.transform.position = position + new Vector3(-space.x, space.y, space.z * i);

            enemy1.GetComponent<Enemy>().health = line[i];
            enemy2.GetComponent<Enemy>().health = line[i];
            enemy3.GetComponent<Enemy>().health = line[i];
                
            enemies.Add(enemy1);
            enemies.Add(enemy2);
            enemies.Add(enemy3);
            
        }
    }

    public void Remove()
    {
        for (var index = 0; index < enemies.Count;)
        {
            var enemy = enemies[index];
            enemies.Remove(enemy);
            DestroyImmediate(enemy);
        }
    }

}