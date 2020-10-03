using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [Tooltip("Speed the laser travels")]
    [SerializeField]
    float _laserSpeed = 8;

    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {

            MoveUp();

        }
        else 
        {

            MoveDown();

        }

    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 10f)
        {

            //check to see the object has a parent
            //destroy parent too
            if (transform.parent != null)
            {

                Destroy(transform.parent.gameObject);

            }
            else
            {

                Destroy(this.gameObject);

            }
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);

        if (transform.position.y < -10f)
        {

            //check to see the object has a parent
            //destroy parent too
            if (transform.parent != null)
            {

                Destroy(transform.parent.gameObject);

            }
            else
            {

                Destroy(this.gameObject);

            }
        }
    }

    public void AssignEnemyLaser()
    {

        _isEnemyLaser = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && _isEnemyLaser == true)
        {

            Player player = other.GetComponent<Player>();
            if (player != null)
            {

                player.PlayerDamage();

            }

        }

    }
}
