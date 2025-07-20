using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    public int powerUpID;
    [SerializeField]
    private AudioClip _audioclip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided with: " +other.name);
        
        if (other.tag=="Player")
        {
            //Acceder a informacion de otro script o clase
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioclip,Camera.main.transform.position,1f);
            if (player != null)
            {
                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotPowerUpOn();
                        break;
                    case 1:
                        //_speed *= 10;
                        player.SpeedBoostPowerUpOn();
                        break;
                    case 2:
                        //Enable Shields
                        player.EnableShield();
                        break;
                }





            }
   

            Destroy(this.gameObject);
        }
       
    }
}
