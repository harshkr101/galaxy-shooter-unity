using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 12.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    
    public float fireRate = 0.5f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _life = 3;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update

    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (!_spawnManager)
        {
            Debug.LogError("Spawn Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >_nextFire)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0,1.05f,0), Quaternion.identity);
    }

    void CalculateMovement()
    {
        // input controls
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        
        // control direction
        var direction = new Vector3(horizontalInput, verticalInput,0);
        
        // update object position
        transform.Translate(direction*speed*Time.deltaTime);
        
        // restrict object position on vertical axis
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0),0);

        // loop object position on horizontal axis
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    public void Damage()
    {
        _life--;
        if (_life < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
