using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //handle to text
    [SerializeField]
    private Text _scoreText;

    [Tooltip("Image varibale to change lives")]
    [SerializeField]
    private Image _livesImg;

    [Tooltip("Ship Lives")]
    [SerializeField]
    private Sprite[] _livesSprite;

    [Tooltip("Game Over Text")]
    [SerializeField]
    private Text _uiGameOverText;

    [Tooltip("Restart Level Text")]
    [SerializeField]
    private Text _uiRestartLevelText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {

        //assign text component to handle
        _scoreText.text = "Score: " + 0;
        _uiGameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {

            Debug.Log("Game Manager is NULL");


        }
    }

    public void UpdateScore(int playerScore)
    {

        _scoreText.text = "Score: " + playerScore.ToString();

    }

    public void UpdateLives(int currentLives)
    {

        //display img sprite
        //give it a new one based on the currentLives index
        _livesImg.sprite = _livesSprite[currentLives];

        //Display game over text if live are all gone
        if(currentLives == 0)
        {

            GameOverSquence();

        }

    }

    void GameOverSquence()
    {

        _uiGameOverText.gameObject.SetActive(true);
        _uiRestartLevelText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());

        _gameManager.GameOver();
        

    }

    IEnumerator GameOverFlickerRoutine()
    {

        while(true)
        {

            _uiGameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _uiGameOverText.text = "";
            yield return new WaitForSeconds(0.5f);

        }

    }
}
