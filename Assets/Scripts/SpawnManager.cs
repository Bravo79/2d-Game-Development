using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [Tooltip("Eneny Prefab")]
    [SerializeField]
    GameObject _enemyPrefab;

    [Tooltip("Spawning Time")]
    [SerializeField]
    float _spawnTime = 5.0f;

    [Tooltip("Spawn Parent")]
    [SerializeField]
    GameObject _enemyContainer;

    private bool _stopSpawning;

    [Tooltip("Powerup Array")]
    [SerializeField]
    GameObject[] _powerups;

 
    // Start is called before the first frame update
    void Start()
    {

        

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUps());
    }


    //Spawn game objects every 5 seconds
    //create a coroutine of type IEmumerator, it allows us to yeild events
    //while loop so it runs while a condition is true
    IEnumerator SpawnEnemyRoutine()
    {

        yield return new WaitForSeconds(3.0f);

        //while loop (infinite loop)
            //Instatiate enemy prefab
            //wait for 5 seconds

        while (_stopSpawning == false)
        {

            Vector3 positionToSpawn = new Vector3(Random.Range(-9.5f, 9.5f), 6.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);

            //Assigning enemy clone to enemy container in UNITY Hierarchy
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_spawnTime);

        }

    }

    IEnumerator SpawnPowerUps()
    {

        yield return new WaitForSeconds(3.0f);


        while (_stopSpawning == false)
        {

            Vector3 positionToSpawn = new Vector3(Random.Range(-9.5f, 9.5f), 6.5f, 0);
            int randomPowerups = Random.RandomRange(0, 3);
            Instantiate(_powerups[randomPowerups], positionToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));

        }

    }

    public void OnPlayerDeath()
    {

        _stopSpawning = true;

    }
}
