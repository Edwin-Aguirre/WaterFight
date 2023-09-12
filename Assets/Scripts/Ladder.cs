using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerScript;
    [SerializeField]
    private float speedUpDown = 3.2f;
    [SerializeField]
    private Transform chController;
    private bool inside = false;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject player;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
        inside = false;
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Ladder")
        {
            playerScript.enabled = false;
            inside = !inside;
            animator.SetBool("isClimbing", true);
            player.transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Ladder")
        {
            playerScript.enabled = true;
            inside = !inside;
            animator.SetBool("isClimbing", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            animator.speed = 0;
            if(inside == true && Input.GetKey(KeyCode.UpArrow))
            {
                chController.transform.position += Vector3.up * speedUpDown * Time.deltaTime;
                animator.speed = 1;
            }
            if(inside == true && Input.GetKey(KeyCode.DownArrow))
            {
                chController.transform.position  += Vector3.down * speedUpDown * Time.deltaTime;
                animator.speed = 1;
            }
            if(inside == false)
            {
                animator.speed = 1;
            }
        }
    }
}
