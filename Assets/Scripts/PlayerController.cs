using System.Collections ;
using System.Collections.Generic ;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem ;

public class PlayerController : MonoBehaviour{
    public Vector2 moveValue;
    public float speed = 700;
    private int count;
    private int numPickups = 12;

    // Score and win text
    public Text scoreText;
    public Text winText;

    // Velocity and position text
    public Text positionText;
    public Text velocityText;

    // variables to find velocity
    private Vector3 previous;
    private Vector3 velocity;


    void Start(){
        this.count = 0;
        winText.text = "";
        SetCountText();

        velocityText.text = "Velocity: " + updateVelocity().ToString("0.00") +"m/s";


        positionText.text = "Pos: " + transform.position.ToString();

    }

    // When moving get the value of Vector2
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    // Update movement, position text, and velocity text
    void FixedUpdate(){
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        GetComponent<Rigidbody>().AddForce(movement*speed*Time.fixedDeltaTime);

        updatePosText();

        velocityText.text = "Velocity: " + updateVelocity().ToString("0.00") +"m/s";
    }


    // Interactions with pickups
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            
            count++;

            this.speed+=25;

            SetCountText();


        }
    }



    // Updating counter and win
    private void SetCountText(){
        scoreText.text = "Score: " + count.ToString();

        if(count >= numPickups){
            winText.text="You win!";
        }

    }


    // Updating Positioning Text
    private void updatePosText(){
        positionText.text = "Pos: " + transform.position.ToString();
    }



    // Updating Velocity
    private float updateVelocity(){
        velocity = (transform.position - previous)/Time.fixedDeltaTime;
        previous = transform.position;        

        return velocity.magnitude;
    }
   
}


