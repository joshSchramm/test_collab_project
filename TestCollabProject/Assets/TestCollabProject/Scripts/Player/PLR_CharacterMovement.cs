/* -----------------------------------------------------------------------------------
 * Class Name: PLR_CharacterMovement
 * -----------------------------------------------------------------------------------
 * Purpose: Controls character movement based on WASD key presses.
 * -----------------------------------------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PLR_CharacterMovement : MonoBehaviour
{
    public GameObject[] players;

    public float moveSpeed = 10f, maxDeltaV = 10f, turnSpeed = 2f, rayDistance = .2f, verticalOffset = .5f, jumpSpeed = 3f;
    public AudioMixer mainAudioMixer;
    public AudioClip jumpSound;
    //public Transform lookPoint;

    int currentPlayer = 0;
    Rigidbody rb;
    bool isFrozen;
    GameObject seeNoEvilOverlay;
    AudioSource aS;

    // ------------------------------------------------------------------------------
    // Function Name: Awake
    // ------------------------------------------------------------------------------
    // Purpose: Runs on startup before any other code. Runs prior to start.
    // ------------------------------------------------------------------------------

    void Awake()
    {
        for (int i = 0; i < players.Length; i++)
        {   
            players[i].GetComponent<Rigidbody>().isKinematic = false;
            players[i].GetComponent<Rigidbody>().useGravity = true;
            players[i].GetComponent<Rigidbody>().freezeRotation = true;
            players[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        rb = players[currentPlayer].GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.freezeRotation = true;

        isFrozen = false;

        seeNoEvilOverlay = GameObject.Find("SeeNoEvilOverlay");
        seeNoEvilOverlay.SetActive(false);

        aS = GetComponent<AudioSource>();
        mainAudioMixer.SetFloat("masterVol", 0);
    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // ------------------------------------------------------------------------------
    // Purpose: Runs each frame. Used to perform frame based checks and actions.
    // ------------------------------------------------------------------------------

    void Update ()
    {
        SwitchPlayer();
        Move();
        Jump();
		
	}

    // ------------------------------------------------------------------------------
    // Function Name: Move
    // ------------------------------------------------------------------------------
    // Purpose: Regulates the movement of the current slected player
    // ------------------------------------------------------------------------------

    void Move()
    {

        float v = Input.GetAxis(UNA_Tags.vertical);
        float h = Input.GetAxis(UNA_Tags.horizontal);

        Vector3 targetDirection = transform.forward * v + transform.right * h;

        Vector3 targetVelocity = targetDirection * moveSpeed;
        Vector3 deltaVelocity = targetVelocity - rb.velocity;

        deltaVelocity.x = Mathf.Clamp(deltaVelocity.x, -maxDeltaV, maxDeltaV);
        deltaVelocity.z = Mathf.Clamp(deltaVelocity.z, -maxDeltaV, maxDeltaV);
        deltaVelocity.y = 0f;

        rb.AddForce(deltaVelocity, ForceMode.VelocityChange);

        //lookPoint.position = new Vector3(transform.position.x, transform.position.y + verticalOffset, transform.position.z);

        //if (v != 0 || h != 0)
        //{
        //    Quaternion turnAngle = Quaternion.Euler(0, lookPoint.eulerAngles.y, 0);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, turnSpeed * Time.deltaTime);

        //}

    }

    // ------------------------------------------------------------------------------
    // Function Name: Jump
    // ------------------------------------------------------------------------------
    // Purpose: Allows the player to jump if on the ground
    // ------------------------------------------------------------------------------

    void Jump()
    {
        if (Physics.Raycast(players[currentPlayer].transform.position, -transform.up, rayDistance))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
                aS.PlayOneShot(jumpSound);
            }

        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: SwitchPlayer
    // ------------------------------------------------------------------------------
    // Purpose: Handles the behavior for switching between the three different characters.
    // ------------------------------------------------------------------------------

    void SwitchPlayer()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            currentPlayer++;

            if(currentPlayer >= players.Length)
            {
                currentPlayer = 0;
            }

            rb = players[currentPlayer].GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.freezeRotation = true;

            if (players[currentPlayer].name == "SpeakNoEvil")
            {
                seeNoEvilOverlay.SetActive(false);
                mainAudioMixer.SetFloat("masterVol", 0);
            }

            else if (players[currentPlayer].name == "HearNoEvil")
            {
                seeNoEvilOverlay.SetActive(false);
                mainAudioMixer.SetFloat("masterVol", -20);
            }

            else if (players[currentPlayer].name == "SeeNoEvil")
            {
                seeNoEvilOverlay.SetActive(true);
                mainAudioMixer.SetFloat("masterVol", 0);
            }
        }
    }

}
