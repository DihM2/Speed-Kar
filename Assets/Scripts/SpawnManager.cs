using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> carsPrefabs;
    [SerializeField] List<GameObject> powerupPrefabs;

    //Vector3 spawnPosition = new Vector3(-2, 4);

    float timerCount = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SpawnStructGenerator());
        
        
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
        int powerupCount = 0;

        string spawnStruct = null;

        for(int i = 0; i < 5; i++)
        {
            posType = Random.Range(0, 3);
            
            if (posType == 2 && powerupCount > 1)
            {
                posType = 1;
            }

            if (i == 4 && !hasEmpty)
            {
                spawnStruct += "0";
            }
            else
            {
                if(posType == 0)
                {
                    hasEmpty = true;
                }

                if(posType == 2)
                {
                    powerupCount++;
                }
                
                spawnStruct += posType;
                
            }
        }

        return spawnStruct;

    }

    void SpawnObjects(string spawnStruct)
    {
        int index = 0;
        int prefabIndex;

        foreach(char spawnPos in spawnStruct)
        {
            if (spawnPos.ToString().Equals("1"))
            {
                prefabIndex = Random.Range(0, carsPrefabs.Count);
                Instantiate(carsPrefabs[prefabIndex], new Vector3(-2 + index, 4, 0), carsPrefabs[prefabIndex].transform.rotation);
            }
            else if (spawnPos.ToString().Equals("2"))
            {
                prefabIndex = Random.Range(0, powerupPrefabs.Count);
                Instantiate(powerupPrefabs[prefabIndex], new Vector3(-2 + index, 4, 0), powerupPrefabs[prefabIndex].transform.rotation);
            }

            index++;
        }
    }
}
