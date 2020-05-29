using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    public float MouseSpeed;                //Floating point variable to store the player's movement speed.
    public float KeyboardSpeed;                //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Vector3 startPosition;
    public Camera Camera;
    public GameObject TopWall;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
        startPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        MouseSpeed = 20;
        KeyboardSpeed = 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
            if (Time.timeScale > 0) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }

    void Translate(float x, float y) {
        this.transform.position += new Vector3(x, y, 0);
    }

    private void MoveWithMouse() {
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");
        Translate(MouseSpeed * x * Time.deltaTime, MouseSpeed * y * Time.deltaTime);
    }

    private void MoveWithArrows() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            Translate(0, KeyboardSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            Translate(0, -KeyboardSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            Translate(-KeyboardSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            Translate(KeyboardSpeed * Time.deltaTime, 0);
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        // Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis ("Horizontal");

        // Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis ("Vertical");

        // Use the two store floats to create a new Vector2 variable movement.
        Vector2 keyboardMovement = new Vector2 (moveHorizontal, moveVertical);
        Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        var movement = mouseMovement;
        //rb2d.AddForce(movement * 10);

        //MoveWithMouse();
        MoveWithArrows();

        // Absolute
        //if (! Main.InWall) {
        //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //this.transform.position = worldPosition;
        //}
        //Main.InWall = false;

        //collider.bounds.Intersects(worldPosition);
        //Debug.Log("top wall: " + pos + " player: " + worldPosition);
    }
}