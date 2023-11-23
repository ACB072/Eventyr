using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public GameObject trap;                         // Trampa que hará aparición
    private Vector2 whereToSpawn;                   // Posicion donde aparecerá la trampa

    public float spawnRate, rangeIni, rangeEnd;     // Ratio de apariciones
    private float randY, nextSpawn;                 // Proxima aparición

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randY = Random.Range(rangeIni, rangeEnd);
            whereToSpawn = new Vector2(transform.position.x, randY);
            Instantiate(trap, whereToSpawn, Quaternion.identity);
        }
    }
}
