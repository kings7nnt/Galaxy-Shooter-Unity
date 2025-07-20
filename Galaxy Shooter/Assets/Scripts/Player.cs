using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false, shieldActive = false;
    public Transform shootPoint;
    private float _speed = 5.0f, _speedBoost = 5.0f;
    public float horizontalInput;
    public float verticalInput;
    [SerializeField]
    private GameObject _laserPrefab, _tripleLaserPrefab, playerExplosionPrefab, _shieldGameObject;
    [SerializeField]
    private float _fireRate = 0.20f;
    private float _canFire = 0.0f;
    public int lives = 3;
    private UIManager _uimanager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject[] _engines;

    private int _hitCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uimanager != null)
        {
            _uimanager.UpdateLives(lives);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }

    }

    public void Move()
    {
        //Limit screen
        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 8.3f)
        {
            transform.position = new Vector3(-8.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.3f)
        {
            transform.position = new Vector3(8.2f, transform.position.y, 0);
        }

        //Player Movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);

        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

    }

    public void Damage()
    {
        //Sustraemos vidas del player
        //Validamos si hay escudos activos
        
        if (shieldActive == true)
        {
            shieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }
        //activar fallas en motores
        _hitCount++;
        if (_hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (_hitCount == 2)
        {
            _engines[1].SetActive(true);
        }

        lives--;
        _uimanager.UpdateLives(lives);
        if (lives == 0)
        {
            Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uimanager.ShowTitleScreen();
            Destroy(this.gameObject);

        }
    }

    public void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShot == true)
            {
                Instantiate(_tripleLaserPrefab, shootPoint.position, Quaternion.identity);
                //Debug.Log("Triple SHOOT!!!");
            }
            else
            {
                Instantiate(_laserPrefab, shootPoint.position, Quaternion.identity);

            }
            _canFire = Time.time + _fireRate;

        }


    }
    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotDownRoutine());
    }

    public void SpeedBoostPowerUpOn()
    {
        StartCoroutine(SpeedBoostRoutine());
    }

    public void EnableShield()
    {
        shieldActive = true;
        _shieldGameObject.SetActive(true);
    }

    IEnumerator SpeedBoostRoutine()
    {
        _speed *= _speedBoost;
        yield return new WaitForSeconds(5.0f);
        _speed = 5.0f;
    }

    public IEnumerator TripleShotDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }


}
