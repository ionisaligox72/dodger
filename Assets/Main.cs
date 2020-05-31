using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using TMPro;

public enum GameRunningStatus {
    PAUSED,
    RUNNING
};

public enum GameState {
    SHOWING_DIALOG,
    START_LEVEL,
    IN_LEVEL,
    END_LEVEL,
    EXIT
}

public class GameContext {
    public GameRunningStatus RunningStatus;
    public GameState GameState;
}

public class Main : MonoBehaviour
{
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI YouLastedText;
    public AsteroidMovement AsteroidPrefab;
    public Boolean SpawnAsteroids = true;
    public Boolean Timer = true;
    public GameObject WelcomeDialog;
    public GameObject EndGameDialog;

    public static Boolean IsCollision = false;
    public static int Collisions = 0;
    public static Boolean InWall = false;
    public static GameContext GameContext = new GameContext();

    private float timer;
    private List<AsteroidMovement> asteroids = new List<AsteroidMovement>();
    private Dialogs dialogs;
    private GameObject _player;
    private Vector3 _playerStartingPosition;

    private void spawnAsteroids() {
        var topWall = GameObject.Find("Top wall");
        var collider = topWall.GetComponent<BoxCollider2D>();
        var topOffset = collider.size.y / 2;
        var v3 = Camera.main.ViewportToWorldPoint(new Vector3(0, topOffset, 0));
        var infos = new List<AsteroidInfo>() {
            new AsteroidInfo(location: new Vector3(0.1f, 0.1f, 10), direction: new Vector3(1, 1.5f, 0), speed: 2, scale: 0.1f)
            , new AsteroidInfo(new Vector3(0, 1 - topOffset, 10), new Vector3(3, 1, 0), 3, 0.15f)
            , new AsteroidInfo(new Vector3(1, 1 - topOffset, 10), new Vector3(2, 3, 0), 4, 0.2f)
        };

        foreach(AsteroidInfo info in infos) {
            var a = Instantiate(AsteroidPrefab, Camera.main.ViewportToWorldPoint(info.Location), Quaternion.identity);
            a.Configure(info.Direction, info.Speed, info.Scale);
            asteroids.Add(a);
        }
    }

    private void Start() {
        InvokeRepeating("Accelerate", 0, 4);
        PauseGame();
        GameContext.RunningStatus = GameRunningStatus.PAUSED;
        GameContext.GameState = GameState.SHOWING_DIALOG;
        _player = GameObject.Find("Player");
        _playerStartingPosition = _player.transform.position;
        _player.SetActive(false);
        WelcomeDialog.SetActive(true);
    }

    private void Accelerate() {
        foreach(AsteroidMovement m in asteroids) {
            m.Accelerate(1.1f);
        }
    }

    private Boolean IsPaused() {
        return GameContext.RunningStatus == GameRunningStatus.PAUSED;
    }

    private void PauseGame() {
        GameContext.RunningStatus = GameRunningStatus.PAUSED;
    }

    private void UnpauseGame() {
        GameContext.RunningStatus = GameRunningStatus.RUNNING;
    }
    
    private void State(GameState newState) {
        Debug.Log("State: " + GameContext.GameState + " -> " + newState);
        GameContext.GameState = newState;
    }

    private void ClosestVirus() {
        AsteroidMovement closest = null;
        var playerPosition = transform.position;
        foreach (var ai in asteroids) {
            if (closest == null) closest = ai;
            else if (Vector3.Distance(ai.transform.position, playerPosition) < Vector3.Distance(closest.transform.position, playerPosition)) {
                closest = ai;
            }
        }
        Debug.DrawLine(Vector3.zero, new Vector3(5, 0, 0), Color.white, 2.5f);
        Debug.DrawLine(playerPosition, closest.transform.position, Color.red, 1);
    }

    private void Update()
    {
        //Vector3 p = UtilsClass.GetMouseWorldPosition();
        //// always draw a 5-unit colored line from the origin
        //Color color = new Color(q, q, 1.0f);
        //Debug.DrawLine(Vector3.zero, new Vector3(0, 5, 0), color);
        //q = q + 0.01f;

        //if (q > 1.0f) {
        //    q = 0.0f;
        //}


        switch (GameContext.GameState) {
            case GameState.SHOWING_DIALOG:
                PauseGame();
                break;
            case GameState.START_LEVEL:
                IsCollision = false;
                EndGameDialog.SetActive(false);
                WelcomeDialog.SetActive(false);
                _player.transform.position = _playerStartingPosition;
                _player.SetActive(true);
                UnpauseGame();
                if (SpawnAsteroids) spawnAsteroids();
                timer = 0f;
                IsCollision = false;
                Collisions = 0;
                State(GameState.IN_LEVEL);
                break;
            case GameState.IN_LEVEL:
                ClosestVirus();
                if (Timer) timer += Time.deltaTime;
                if (IsCollision) {
                    State(GameState.END_LEVEL);
                }
                break;
            case GameState.END_LEVEL:
                PauseGame();
                foreach (AsteroidMovement asteroid in asteroids) {
                    Destroy(asteroid.gameObject);
                }
                asteroids.Clear();
                _player.SetActive(false);
                YouLastedText.text = "You lasted " + timer.ToString("F2") + " seconds";
                EndGameDialog.SetActive(true);
                break;
            case GameState.EXIT:
                Debug.Log("Goodbye!");
                break;
            //default:
            //    EditorUtility.DisplayDialog("Unknown case", "Unknown state: " + GameContext.GameState, "Ok");
        }

    }

    private float q = 0.0f;

    private void FixedUpdate()
    {
        var seconds = System.Convert.ToInt32(timer);
        TimeText.text = timer.ToString("F2");
    }
}

public class AsteroidInfo
{
    public AsteroidInfo(Vector3 location, Vector3 direction, float speed, float scale = 0.5f)
    {
        Location = location;
        Direction = direction;
        Speed = speed;
        Scale = scale;
    }
    public Vector3 Location { get; }
    public Vector3 Direction { get; }
    public float Speed { get; }
    public float Scale { get; }
}
