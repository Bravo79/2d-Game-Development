using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Tooltip("Enemy Movement Speed")]
    [SerializeField]
    float _enemySpeed = 4f;

    [Tooltip("Enemy Laser")]
    [SerializeField]
    private GameObject _enemyLaserPrefab;

    [Tooltip("Enemy On Screen")]
    [SerializeField]
    GameObject _enemyPrefab;

    //#1 create varibale
    private Player _player;

    //#1 handle to animator
    private Animator _anim;

    //Audio sorce varibale
    private AudioSource _audioSource;

    private float _fireRate = 3.0f;
    private float _canFire = -1f;

    private void Start()
    {
        //#2 cache a referance in start
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>(); //do not have to grab component becuase its already on the game object
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {

            Debug.Log("The Player is NULL");

        }


        //Assign the assign the animator component
       
        if (_anim == null)
        {

            Debug.LogError("The Animator is NULL");

        }

        if (_audioSource == null)
        {

            Debug.LogError("Audio Source on enemy is NULL");

        }    

    }


    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {

            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {

                lasers[i].AssignEnemyLaser();

            }

        }

    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);


        if (transform.position.y < -7f)
        {

            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7, 0);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //if other is player
        //damage player
        //Destory enemy
            
        if (other.tag == "Player")
        {

            //damage player
            //Null Check
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {

                player.PlayerDamage();

            }

            //Play eney destroy anim
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;

            _audioSource.Play();

            //Destroy the enemy
            Destroy(this.gameObject, 2.8f);

        }

        //if other is laser
        //destroy laser
        //destroy enemy 
        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);

            //#3 check if player is alive 
            if (_player != null)
            {

                //#4 call addscore meathod and add to score
                _player.AddScore(Random.Range(5, 15));

            }

            //Play enemy destroy anim
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;

            _audioSource.Play();

            //destroy the collider to laser makes sound only once when hitting the enemy
            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2.8f);

        }

    }


}
