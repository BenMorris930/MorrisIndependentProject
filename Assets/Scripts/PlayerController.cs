using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpheight;
    public float buff;
    public float fanStrength;
    public float timeLeft = 45;
    public Text countText;
    public Text winText;
    public Text restartText;
    public Text escapeText;
    private Rigidbody rb;
    private int count;
    private bool isGrounded;
    private bool isSlimed;
    private bool jumpBuff;
    private bool allCollected;
    public bool gameOver = true;
    public bool isWon = false;
    public bool endGame = false;
    private Vector3 jump = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 respawn = new Vector3(0, 0.26f, 0);
    public GameObject CameraPivot;
    public ParticleSystem goalLight;

    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public Button restartButton;
    public Button startButton;


    public Transform StartPoint;
    public ParticleSystem playerExplosion;
    public AudioClip explosionAudio;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        count = 0;
        SetCountText();
        winText.text = "";
        isSlimed = false;
        isGrounded = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        respawn = transform.position;
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    public void StartGame()
    {
        gameOver = false;
        titleText.gameObject.SetActive(false);
        descText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        rb.useGravity = true;
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void OnCollisionExit()
    {
        if (isSlimed == true)
        {

        }
        else
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 relativeMovement = CameraPivot.transform.TransformVector(movement);

        if (isGrounded == false || isSlimed == true)
        {
            rb.AddForce(relativeMovement * 0.1f * speed, ForceMode.Force);
        }
        else if (!gameOver)
        {
            rb.AddForce(relativeMovement * speed, ForceMode.Force);
        }
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 slimeJump = new Vector3(moveHorizontal, 1.0f, moveVertical);
        Vector3 relativeSlimeJump = CameraPivot.transform.TransformVector(slimeJump);

        if (!gameOver)
        {
            timeLeft = timeLeft - Time.deltaTime;
            timerText.text = "Time Left: " + Mathf.Round(timeLeft);
        }
        
        if (timeLeft <= 0)
        {
            gameOver = true;
            endGame = true;
        }
        if (gameOver)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            
        }
        if (isWon)
        {
            victoryText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
        if (endGame)
        {
            gameoverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isSlimed)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(relativeSlimeJump * 0.5f * jumpheight, ForceMode.Impulse);
            isGrounded = false;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpBuff)
        {
            rb.AddForce(jump * buff * jumpheight, ForceMode.Impulse);
            isGrounded = false;
            jumpBuff = false;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameOver)
        {
            rb.AddForce(jump * jumpheight, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            isWon = true;
            gameOver = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
        }

        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("DoubleJump"))
        {
            isGrounded = true;
            jumpBuff = true;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("FanWind"))
        {
            rb.AddForce(Vector3.up * fanStrength, ForceMode.Impulse);
        }


        if (other.gameObject.CompareTag("Goal") && allCollected)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            winText.text = "You Win!";
            restartText.text = "Press 'R' to Restart";
            escapeText.text = "Press 'Escape' to Exit";
        }

        if (other.gameObject.CompareTag("Slime"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            isSlimed = true;
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Kill"))
        {
            //transform.position = respawn;
            gameOver = true;
            endGame = true;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            playerExplosion.Play();
            audioSource.PlayOneShot(explosionAudio, 1.5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("FanWind"))
        {
            rb.AddForce(Vector3.up * 1.5f * fanStrength, ForceMode.Force);
        }

        if (other.gameObject.CompareTag("Slime"))
        {
            rb.AddForce(Vector3.down * 0.4f, ForceMode.Acceleration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Slime"))
        {
            rb.constraints = RigidbodyConstraints.None;
            isSlimed = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 4)
        {
            var main = goalLight.main;
            main.startColor = Color.green;
            allCollected = true;
        }
    }
}
