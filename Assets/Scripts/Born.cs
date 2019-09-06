using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject tankPrefab;

    public GameObject[] enemyPrefabList;

    public bool spawnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", 1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame

    private void Spawn()
    {
        if (spawnPlayer)
        {
            Instantiate(tankPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, transform.rotation);
        }
    }
}
