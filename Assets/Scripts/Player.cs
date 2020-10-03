using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Speed Of Ship")]
    [SerializeField]
    private float _speed = 5f;

    [Tooltip("Speed Powerup Multiplier")]
    [SerializeField]
    private float _speedMultiplier = 2f;

    [Tooltip("Thrusters Increase amount")]
    [SerializeField]
    private float _increaseThrusters = 4f;

    [Tooltip("Thruster Game Objects On Player")]
    [SerializeField]
    GameObject _boostersOff, _boostersON;

    [Tooltip("Laser to fire")]
    [SerializeField]
    GameObject _laserPrefab;

    [Tooltip("Player Lives")]
    [SerializeField]
    int _lives = 3;

    [Tooltip("Fire Cool Down")]
    [SerializeField]
    float _fireRate = 0.15f;
    float _canFire = -1f;

    private SpawnManager _spawnManager;

    [Tooltip("Triple Shot Power Up")]
    [SerializeField]
    GameObject _tripleShotPrefab;

    [Tooltip("Shield Prefab")]
    [SerializeField]
    GameObject _ShieldVisualizerPrefab;
        

    [Tooltip("Toggle To Activate Triple Shot")]
    [SerializeField]
    private bool _isTripleShotActive = false;
    [Tooltip("Toggle To Activate Speed Boost")]
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [Tooltip("Toggle To Activate Speed Boost")]
    [SerializeField]
    private bool _isShieldstActive = false;

    [Tooltip("Toggle To Activate Right Engine Damage")]
    [SerializeField]
    private GameObject _isRightEngineDamage;
    [Tooltip("Toggle To Activate Left Engine Damage")]
    [SerializeField]
    private GameObject _isLeftEngineDamage;

    [Tooltip("Score Variable")]
    [SerializeField]
    private int _score;

    [Tooltip("Handle for UI Access")]
    private UIManager _uiManager;

    //varibale to store audio clip
    [Tooltip("Sound Clip")]
    [SerializeField]
    private AudioClip _laserSoundClip;

    [Tooltip("Audio Source")]
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
                
        transform.position = new Vector3(0, -4.5f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {

            Debug.LogError("The Spawn Manager is NULL");

        }

        if (_uiManager == null)
        {

            Debug.LogError("The UIManager is NULL");

        }

        if(_audioSource == null)
        {

            Debug.LogError("The Audio Source on the player is NULL");

        }
        else
        {

            _audioSource.clip = _laserSoundClip;

        }
        
    }

    // Update is called once per frame
    void Update()
    {

        ShipMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
                      
    }
     
    private void ShipMovement()
    {
        //variable for movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
                
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {

            transform.Translate(direction * (_speed * _increaseThrusters) * Time.deltaTime);
            _boostersOff.SetActive(false);
            _boostersON.SetActive(true);

        }
        else
        {

            transform.Translate(direction * _speed * Time.deltaTime);
            _boostersOff.SetActive(true);
            _boostersON.SetActive(false);
        }


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5, 1.5f), 0);


        //Keep player in bounds on x and wrap
        if (transform.position.x >= 11.5f)
        {

            transform.position = new Vector3(-11.5f, transform.position.y);

        }
        else if (transform.position.x <= -11.5f)
        {

            transform.position = new Vector3(11.5f, transform.position.y, 0);

        }

    }

    private void FireLaser()
    {

        _canFire = Time.time + _fireRate;


        //is space key pressed & if triple shot active is true
        //fire the (triple shot prefab)
        if (_isTripleShotActive == true)
        {

            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

        }
        //else FireLaser 1 laser
        else
        {

            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        }

        //Play laser audio clip
        _audioSource.Play();

    }            

    public void PlayerDamage()
    {
                
        if (_isShieldstActive == true)
        {
            _isShieldstActive = false;
            _ShieldVisualizerPrefab.SetActive(false);
            return; 

        }             
        
        _lives--;

        EngineDamage();

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
                        
            _spawnManager.OnPlayerDeath();
            
            Destroy(this.gameObject);
                      
        }

    }
      
    public void TripleShotActive()
    {
                
        _isTripleShotActive = true;
        
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    
    IEnumerator TripleShotPowerDownRoutine()
    {

        yield return new WaitForSeconds(5.0f); //Wait for 5 seconds then set to false
        _isTripleShotActive = false;
        
    }

    public void SpeedBoostActive()
    {

        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {

        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;

    }

    public void ShieldsActive()
    {

        _isShieldstActive = true;
        _ShieldVisualizerPrefab.SetActive(true);

    }

    public void AddScore(int points)
    {
                
        _score += points;
        
        _uiManager.UpdateScore(_score);

    }

    private void EngineDamage()
    {

        if (_lives == 2)
        {

            _isRightEngineDamage.SetActive(true);

        }
        else if(_lives == 1)
        {

            _isLeftEngineDamage.SetActive(true);

        }

    }
      

}
