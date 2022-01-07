using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    
    // powerupID 0 = Triple Shot, 1 = Speed, 2 = Shields
    [SerializeField] private int powerupID;     
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //activate powerUP based on their ID
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
                     default: Debug.Log("default");
                         break;
                }
            }
            Destroy(this.gameObject);  // destroy power up object
        }
    }
    
}
