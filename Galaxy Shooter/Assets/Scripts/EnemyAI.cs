using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float _speed = 3f;
    [SerializeField]
    public GameObject enemyexplosionPrefab;
    private Animator animator;
    private UIManager _manager;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _audioclip;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        //Enemy movement
        //tansform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //Limit screen Enemy
        if (transform.position.y < -6.2f)
        {
            float randomX = Random.Range(-7.5f, 7.5f);
            transform.position = new Vector3(randomX, 6.2f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(enemyexplosionPrefab,transform.position, Quaternion.identity);
            _manager.UpdateScore();
            AudioSource.PlayClipAtPoint(_audioclip,Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            //Acceder a informacion de otro script o clase
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
               
                    player.Damage();
                    Instantiate(enemyexplosionPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(_audioclip, Camera.main.transform.position);
                //Destroy(enemyexplosionPrefab);
                Destroy(this.gameObject);
                
            }
        }
    }
}
