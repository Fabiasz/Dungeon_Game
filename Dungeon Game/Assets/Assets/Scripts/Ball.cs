using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] audio_array;

    Vector2 paddleToBallDistance;
    Rigidbody2D rigidbody2D;
    private bool hasStarted = false;
    
   void Start()
    {

        paddleToBallDistance = transform.position - paddle1.transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<AudioSource>().playOnAwake = false;
    }
    void Update()
    {
       
        if (!hasStarted)
        {
            LaunchBallOnMouseClick();
            PaddleStickToBall();
        }
       
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            rigidbody2D.velocity = new Vector2(xPush,yPush);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float randomFactor = 1f;
        Vector2 velocityTweak = new Vector2(
            Random.Range(0,randomFactor),
            Random.Range(0, randomFactor));
        if (hasStarted)
        {
            AudioClip current_clip = audio_array[UnityEngine.Random.Range(0, audio_array.Length-1)];
         //   GetComponent<AudioSource>().Play();

            GetComponent<AudioSource>().PlayOneShot(current_clip);
            rigidbody2D.velocity += velocityTweak;
            
        }
    }
    

    private void PaddleStickToBall()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallDistance;
    }
}
