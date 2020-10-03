using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [Tooltip("Speed Of The Powerup")]
    [SerializeField]
    float _powerupSpeed = 3.0f;

    [Tooltip("Each Powerup HasOwn ID")] //ID for power ups, 0 for Triple Shot, 1 for Speed, 2 for Sheilds
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

   
    // Update is called once per frame
    void Update()
    {

        //Move down at the speed of three
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);
        //When we leave the screen. destroy this object
        if (transform.position.y < -7f)
        {

            Destroy(this.gameObject);

        }

    }

    //OnTriggerCollision
    //Only be collectable by the player (use player tag)
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            //comminicate with player script
            //handle to the component you want
            //assign the handle to the component
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            //after getting access to the component, then null check
            if (player != null)
            {
                ////If ID is 0 triple shot
                //if (powerupID == 0)
                //{
                //    player.TripleShotActive();
                //}
                ////else if ID is 1 Speed
                ////Play speed power up
                //else if (powerupID == 1)
                //{
                //    Debug.Log("Speed is active");
                //}
                ////else if ID is 2 sheild
                ////Play sheild powerup
                //else if (powerupID == 2)
                //{
                //    Debug.Log("Shields are active");
                //}

                //more effienct to write a switch statement
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;

                }
                          
            }

            Destroy(this.gameObject);

        }


    }


}
