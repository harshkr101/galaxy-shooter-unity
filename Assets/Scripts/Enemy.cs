using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private float _speed = 6.0f;
    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    
    [SerializeField] private GameObject laserPrefab;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator is null");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is null");
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
            GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].ApplyEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // destroy enemy and damage player if player collides with enemy
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            // trigger enemy destruction animation
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(GetComponent<Collider2D>()); // destroy the collider 
            // destroy enemy object
            Destroy(this.gameObject,2.8f);
            _audioSource.Play();
        }
        
        // destroy enemy and laser if laser collides with enemy
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10); // add player score on after destroying enemy
            }
            // trigger enemy destruction animation
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(GetComponent<Collider2D>()); // destroy the collider 
            // destroy enemy object
            Destroy(this.gameObject,2.8f);
            _audioSource.Play();
        }
    }
}
