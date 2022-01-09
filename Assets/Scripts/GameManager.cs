using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // load scene when game is over and R key is pressed by player
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(0); // load current game scene
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
