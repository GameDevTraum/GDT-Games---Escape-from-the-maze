using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    /* El Script GameControl se encarga del control durante todo el juego, iniciar un nuevo juego,
     * colocar al personaje, distribuir las piezas en el laberinto, iniciar el timer, etc.
     * 
     */

    [SerializeField]
    private GameObject[] objectsToFindPrefabs;

    private GameObject[] objectsToFindInstances;

    public int lPieces;
    public int[] selectedPiecesIndex;

    [SerializeField]
    private GameObject menuCamera;

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject playerInstance;
    private string appearPositionsTag = "AppearPosition";
    [SerializeField]
    private bool keyFound;
    private UserInterface userInterface;

    public enum GameState { MAINMENU,PLAYING,PAUSED,ENDSCREEN};
    private GameState gameState;
    [SerializeField]
    private bool gamePaused;

    private int objectsFound;

    private AudioManager audioManager;

    private Timer timer;
    private Gate gate;

    public AudioSource backgrounMusic;

    public bool useStartButton = false;
    public GameObject startWindow;

    void Start()
    {
        userInterface = FindFirstObjectByType<UserInterface>();
        audioManager = FindFirstObjectByType<AudioManager>();
        timer = FindFirstObjectByType<Timer>();
        gate = FindFirstObjectByType<Gate>();
        gameState = GameState.MAINMENU;

        if (!useStartButton)
        {
            backgrounMusic.Play();
            startWindow.SetActive(false);
        }
        else
        {
            startWindow.SetActive(true);
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            NewGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (gameState == GameState.PLAYING || gameState == GameState.PAUSED))
        {
            SetPauseState(!gamePaused);
        }
    }

    public void NewGame()
    {

        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gate.NewGame();
        userInterface.NewGame();
        gameState = GameState.PLAYING;
        keyFound = false;
        objectsFound = 0;
        PlacePlayer();
        PlaceObjectsToFind();
        timer.StartTimer();
    }

    public void SetMainMenu()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameState = GameState.MAINMENU;
        userInterface.SetMenuScreen();
        Destroy(playerInstance);
        timer.StopTimer();
    }

    public void SetEndScreen(bool victory)
    {
        
        Destroy(playerInstance);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameState = GameState.ENDSCREEN;
        userInterface.SetEndScreen(victory);
        timer.StopTimer();
    }

    public void GameWon()
    {
        SetEndScreen(true);
    }

    public void GameLost()
    {
        SetEndScreen(false);
    }

    public void SetPauseState(bool pause)
    {
        gamePaused = pause;
        
        if (gamePaused)
        {
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            userInterface.SetPauseScreen();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            userInterface.SetGameScreen();
        }

    }

    private void PlacePlayer()
    {
        
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }
        GameObject[] positions = GameObject.FindGameObjectsWithTag(appearPositionsTag);
        GameObject p = positions[Random.Range(0, positions.Length)];

        playerInstance = Instantiate(playerPrefab,p.transform.position,playerPrefab.transform.rotation);
    }


    private void PlaceObjectsToFind()
    {
        if (objectsToFindInstances != null)
        {
            foreach(GameObject g in objectsToFindInstances)
            {
                Destroy(g);
            }
        }
        LabyrinthPiece[] labyrinthPieces = FindObjectsByType<LabyrinthPiece>(FindObjectsSortMode.None);
        int[] randomPieceIndex = new int[labyrinthPieces.Length];
        int n = labyrinthPieces.Length;
        lPieces = n;
        selectedPiecesIndex=new int[objectsToFindPrefabs.Length];
        for (int i = 0; i < n; i++)
        {
            randomPieceIndex[i] = i;
        }
        for (int i = 0; i < objectsToFindPrefabs.Length; i++)
        {
            int index = Random.Range(0, n - i);
            selectedPiecesIndex[i] = randomPieceIndex[index];
            //swap
            int aux = randomPieceIndex[n - i - 1];
            randomPieceIndex[n - i - 1] = selectedPiecesIndex[i];
            randomPieceIndex[index] = aux;
        }

        objectsToFindInstances = new GameObject[objectsToFindPrefabs.Length];

        for (int i = 0; i < objectsToFindInstances.Length; i++)
        {
            objectsToFindInstances[i] = Instantiate(objectsToFindPrefabs[i],labyrinthPieces[selectedPiecesIndex[i]].GetRandomPosition(), objectsToFindPrefabs[i].transform.rotation);
            objectsToFindInstances[i].transform.position += 0.5f * Vector3.up;
        }
        objectsToFindInstances[objectsToFindInstances.Length - 1].SetActive(false);
    }

    public void ItemFound(Collectable.Item item)
    {
        audioManager.ItemCollected();
        objectsFound++;
        if (objectsFound==objectsToFindInstances.Length-1)
        {
            objectsToFindInstances[objectsToFindInstances.Length - 1].SetActive(true);
            userInterface.KeyReleased();
        }
        userInterface.ItemFound(item);
        if (item == Collectable.Item.KEY)
        {
            keyFound = true;
        }
    }

    public bool PlayerHasKey()
    {
        return keyFound;
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }
    
    public void TimeIsUp()
    {
        //timer reached 0
        GameLost();
    }

    public void ClickToStartButton()
    {
        backgrounMusic.Play();
    }



}
