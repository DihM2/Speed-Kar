using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> carsPrefab;

    //Vector3 spawnPosition = new Vector3(-2, 4);

    float timerCount = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerCount += Time.deltaTime;
        //Debug.Log(timerCount);

        if(timerCount > 3.5)
        {
            string structSpawn = SpawnStructGenerator();
            Debug.Log(structSpawn);
            timerCount = 0;

            SpawnObjects(structSpawn);
        }
    }

    string SpawnStructGenerator()
    {
        bool hasEmpty = false;
        int posType;

        string spawnStruct = null;

        for(int i = 0; i < 5; i++)
        {
            posType = Random.Range(0, 2);

            if(i == 4 && !hasEmpty)
            {
                spawnStruct += "0";
            }
            else
            {
                if(posType == 0)
                {
                    hasEmpty = true;
                }
                spawnStruct += posType;
            }
        }

        return spawnStruct;

    }

    void SpawnObjects(string spawnStruct)
    {
        int index = 0;
        int carIndex;

        foreach(char spawnPos in spawnStruct)
        {
            carIndex = Random.Range(0, carsPrefab.Count);
            if (spawnPos.ToString().Equals("1"))
            {
                Instantiate(carsPrefab[carIndex], new Vector3(-2 + index, 4, 0), carsPrefab[carIndex].transform.rotation);
            }

            index++;
        }
    }
}
