using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerControllerTest : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;
    private GameController gameController;
    public Text countText;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        // Set the count to zero
        count = 0;
        // Run the SetCountText function to update the UI (see below)
        SetCountText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            gameController.PickupCollected();
            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }
        else if (other.CompareTag("TimeBoost"))
        {
            other.gameObject.SetActive(false);
            gameController.AddTime(5f);
        }
        else if (other.CompareTag("SpeedBoost"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(SpeedBoost());
        }
    }
    void SetCountText()
    {
        // Update the text field of our 'countText' variable
        countText.text = "Count: " + count.ToString();

    }

    IEnumerator SpeedBoost()
    {
        speed *= 2;
        yield return new WaitForSeconds(5f);
        speed /= 2;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MazeWall"))
        {
            gameController.AddTime(-5f);
        }
    }
}