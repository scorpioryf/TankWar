using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    //0.老盖 1.红墙 2.白墙 3.出生动画 4.水 5.草 6.空气墙
    public GameObject[] item;

    private List<Vector3> itemPositionList = new List<Vector3>();

    public void Awake()
    {
        initMap();
    }

    private void initMap()
    {
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);

        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(0, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -7, 0), Quaternion.identity);

        CreateItem(item[6], new Vector3(0, 9, 0), Quaternion.identity);
        CreateItem(item[6], new Vector3(0, -9, 0), Quaternion.identity);
        CreateItem(item[6], new Vector3(-11, 0, 0), Quaternion.Euler(0, 0, -90));
        CreateItem(item[6], new Vector3(11, 0, 0), Quaternion.Euler(0, 0, -90));

        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().spawnPlayer = true;

        for (int i = 0; i < 60; i++)
        {
            CreateItem(item[1], creatRandomPosition(), Quaternion.identity);
        }

        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[2], creatRandomPosition(), Quaternion.identity);
        }

        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[4], creatRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[5], creatRandomPosition(), Quaternion.identity);
        }

        InvokeRepeating("spawnEnemy", 0, 5);
    }

    private void CreateItem(GameObject createGameObject,Vector3 creatPosition,Quaternion createRotation)
    {
        GameObject itemGO = Instantiate(createGameObject, creatPosition, createRotation);
        itemGO.transform.SetParent(gameObject.transform);
        itemPositionList.Add(creatPosition);
    }

    private Vector3 creatRandomPosition() {
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    private bool HasThePosition(Vector3 createPos)
    {
        for(int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    private void spawnEnemy()
    {
        int enemyPosition;
        enemyPosition = Random.Range(-10, 11);
        CreateItem(item[3], new Vector3(enemyPosition, 8, 0), Quaternion.identity);
    }
} 
