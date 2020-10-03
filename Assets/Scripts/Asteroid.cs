using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 6.0f;


    [Tooltip("The Explosion Animation")]
    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;

    

    // Start is called before the first frame update
    void Start()
    {

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
       

        if (_spawnManager == null)
        {

            Debug.LogError("Spawn manager on Asteriod is NULL");

        }
                

    }

    // Update is called once per frame
    void Update()
    {
        //Torotate an object
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);


    }

    //check for laser collission (Trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {

        //instantiate explosion at the position of the Astroid (us)
        if (other.tag == "Laser")
        {


            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
                       
            _spawnManager.StartSpawning();

            //destroy the explosion after 3 seconds
            Destroy(this.gameObject, 0.25f);

        }

    }
                   
}
