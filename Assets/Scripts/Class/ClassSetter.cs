using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClassSetter : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> classPrefab;
    
    // void Start()
    // {
    //     for (int i = 0; i < zombieDatas.Count; i++)
    //     {
    //         var zombie = SpawnZombie((ZombieType)i);
    //         zombie.WatchZombieInfo();
    //     }
    // }
    
    // public PlayerClass SpawnZombie(ClassType type)
    // {
    //     var newZombie = Instantiate(zombiePrefab).GetComponent<Zombie>();
    //     newZombie.ZombieData = zombieDatas[(int)type];
    //     return newZombie;
    // }
}
