using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum GameState { menu, singleplay, multiplay, scores} 
public enum Direction { left, right, empty}

//public class GameManager : MonoBehaviour {
public class GameManager : NetworkManager
{

    [SerializeField] private GameObject mainMenu;
    //  [SerializeField] private Undead player1;
    //  [SerializeField] private Undead player2;
    [SerializeField] private GameObject player;
    public Vector3 GetPlayerPosition
    {
        get {
          //  Debug.Log(player.transform.position);
            return player.transform.position; }

        set
        {
            player.transform.position = value;
        }
    }
   [SerializeField] private GameObject [] spawnPoints;
    [SerializeField] private Camera [] cameras;
    [SerializeField] private GameObject networkManager;
       




    private Vector3 startCameraPostition = new Vector3(-72.784f, 132.9f, -390.7f);
    private Vector3 playCameraPostition = new Vector3(-190f, 500f, -800.7f);
    private float startCameraSize = 200f;
    private float playCameraSize = 300f;
    private float cameradistance = 50f;
    private Vector3 startCameraRotation = new Vector3(0, 0, 0);
    private Vector3 playCameraRotation = new Vector3(45, 0, 0);


    private bool gameRun = false;
    private GameState gamestate;
    public GameState GameState
    {
        get { return gamestate; }
        set { gamestate = value;  }
    }

	// Use this for initialization

	void Start () {
        gamestate = GameState.menu;
        

      //  mainMenu.SetActive(true);
      //  cameras[0].transform.position = startCameraPostition;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

      

        if ( gamestate == GameState.menu)
        {
          //  mainMenu.SetActive(true);
         //   cameras[0].transform.position = startCameraPostition;  //new Vector3(351, 266, 0);
            openMenu();
        }




       /* if (gamestate == GameState.singleplay && !gameRun)
        {
          //  startGame();
            
        } */

     


    }

    void openMenu()
    {
        //gamestate = GameState.singleplay;
        mainMenu.SetActive(true);
        cameras[0].transform.position = startCameraPostition;
        cameras[0].transform.eulerAngles = startCameraRotation;
        cameras[0].orthographicSize = startCameraSize;

    }

    public void startGame()
    {
      //  Debug.Log("Clicking");
        mainMenu.SetActive (false);
       
        gamestate = GameState.singleplay;

       
        player = Instantiate(player) as GameObject ;
       // player.spawnPosition = spawnPoints[0].transform.position;
        player.transform.Rotate (0, 180, 0);
        player.gameObject.transform.position = spawnPoints[0].transform.position;


        
       GameObject playerBot = Instantiate(player) as GameObject;
       // player2.spawnPosition = spawnPoints[1].transform.position;
        playerBot.transform.Rotate(0, 0, 0);
        playerBot.gameObject.transform.position = spawnPoints[1].transform.position;
        playerBot.tag = "Bot";

        cameras[0].orthographicSize = playCameraSize;
        cameras[0].transform.position = playCameraPostition;
        cameras[0].transform.eulerAngles = playCameraRotation;

    }



    public void createHost()
    {
        //  Debug.Log("Clicking");
        mainMenu.SetActive(false);
      
        gamestate = GameState.multiplay;

        StartHost();
       

        cameras[0].orthographicSize = playCameraSize;
        cameras[0].transform.position = playCameraPostition;
        cameras[0].transform.eulerAngles = playCameraRotation;

    }

    public void connectToHost()
    {
        mainMenu.SetActive(false);
        gameRun = true;
        gamestate = GameState.multiplay;

        StartClient();


        cameras[0].orthographicSize = playCameraSize;
        cameras[0].transform.position = playCameraPostition;
        cameras[0].transform.eulerAngles = playCameraRotation;
    }


    public void StartHost()
    {
        setPort();
        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        SetIPAddress();
        setPort();
        NetworkManager.singleton.StartClient();
    }

    private void SetIPAddress()
    {

        string ipAddress = "localhost";
        NetworkManager.singleton.networkAddress = ipAddress;

    }

    private void setPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }


    public void endGame()
    {
        Debug.Log("End game");
        gamestate = GameState.scores;
        // mainMenu.SetActive(true);
        openMenu();

    }


    public void quit()
    {

        Application.Quit();

        
    }


}
