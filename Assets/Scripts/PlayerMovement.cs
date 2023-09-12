using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;
    [SerializeField]
    public Animator animator;
    private float currentSpeed;

    [SerializeField]
    private Ladder ladder;

    [SerializeField]
    public MountMovement mountMovement;

    [SerializeField]
    private Mount mount;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject floatThing;

    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private GameObject flag;

    //Test Mount
    [SerializeField]
    private GameObject car;

    private bool isMounted = false;

    PhotonView view;

    private Vector3 move;

    private bool isHit = false;

    [SerializeField]
    private GameObject heartsContainer;

    [SerializeField]
    private GameObject[] hearts;

    [SerializeField]
    private  ParticleSystem[] heartParticles;

    [SerializeField]
    private  ParticleSystem playerParticles;

    private int health = 3;

    private void Awake() 
    {
        heartParticles[0].Pause();
        heartParticles[1].Pause();
        heartParticles[2].Pause();
        playerParticles.Pause();
    }


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
        StartCoroutine(ShowHearts());
    }

    void Update()
    {
        OnlineMovement();
    }

    public void Sitting()
    {
        controller.enabled = false;
        ladder.enabled = false;
        animator.SetBool("isSitting", true);
    }

    private void OnlineMovement()
    {
        if(view.IsMine)
        {   groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                animator.SetBool("isWalking", true);
                //Shoe powerup
                if(playerSpeed == 5)
                {
                    animator.SetBool("isRunning", true);
                }
            }
            if (move == Vector3.zero)
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isThrowing", false);
                animator.SetBool("isClimbing", false);
            }
            //UNCOMMENT TO BRING BACK SHIFT RUNNING!!
            // if(animator.GetBool("isRunning") == false)
            // {
            //     playerSpeed = 2;
            // }
            // if(Input.GetKey(KeyCode.LeftShift))
            // {
            //     animator.SetBool("isRunning", true);
            //     playerSpeed = 5;
            // }
            if(Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("isThrowing", true);
            }
            // if(Input.GetKeyDown(KeyCode.LeftControl))
            // {
            //     player.transform.position = new Vector3(transform.position.x, 0.1f, mount.mountPrefab.transform.position.z + 1f);
            //     controller.enabled = true;
            //     ladder.enabled = true;
            //     mount.mountPrefab.transform.GetChild(0).DetachChildren();
            //     mountMovement.controller.enabled = false;
            //     mountMovement.enabled = false;
            //     animator.SetBool("isSitting", false);
            // }
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!view.IsMine)
        {
            return;
        }
        if(other.gameObject.tag == "Water" && animator.GetBool("isIdle"))
        {
            // floatThing.SetActive(true);
            // animator.SetBool("isWalking", false);
            // animator.SetBool("isTreading", true);
            view.RPC("ApplyFloatThing", RpcTarget.All);
        }
        if(other.gameObject.tag == "BubbleShield")
        {
            view.RPC("ApplyShield", RpcTarget.All);
        }
        if(other.gameObject.tag == "Splash" && isHit == false)
        {
            view.RPC("SplashDamage", RpcTarget.All);
            SoundManagerScript.PlaySound("sfx_sound_neutral11");
        }
        if(other.gameObject.tag == "Heart")
        {
            view.RPC("ApplyHealth", RpcTarget.All);
        }
        if(other.gameObject.tag == "Shoes")
        {
            view.RPC("ApplyShoes", RpcTarget.All);
        }
        if(other.gameObject.tag == "Flag")
        {
            view.RPC("ApplyFlag", RpcTarget.All);
        }
        if(other.gameObject.tag == "Car")
        {
            view.RPC("ApplyCar", RpcTarget.All);
        }
        if(other.gameObject.tag == "Splash")
        {
            view.RPC("RemoveCar", RpcTarget.All);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        floatThing.SetActive(false);
        animator.SetBool("isIdle", true);
        animator.SetBool("isTreading", false);
    }

    [PunRPC]
    void ApplyFloatThing()
    {
        floatThing.SetActive(true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isTreading", true);
    }

    [PunRPC]
    void ApplyShield()
    {
        shield.SetActive(true);
        StartCoroutine(ShieldTimer());
        isHit = true;
    }

    [PunRPC]
    void ApplyHealth()
    {
        if(health < 3)
        {
            health++;
            StartCoroutine(ShowUpdatedHearts());
        }
    }

    [PunRPC]
    void ApplyShoes()
    {
        if(isMounted == false)
        {
            playerSpeed = 5;
            StartCoroutine(ShoesTimer());
        }
    }

    [PunRPC]
    void ApplyFlag()
    {
        flag.SetActive(true);
    }

    [PunRPC]
    void ApplyCar()
    {
        isMounted = true;
        car.SetActive(true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isSitting", true);
        controller.height = 2.3f;
        playerSpeed = 10;
        isHit = true;
    }

    [PunRPC]
    void RemoveCar()
    {
        isMounted = false;
        isHit = true;
        StartCoroutine(ResetHit());
        car.SetActive(false);
        animator.SetBool("isIdle", true);
        animator.SetBool("isSitting", false);
        controller.height = 1.3f;
        playerSpeed = 2;
    }

    [PunRPC]
    void SplashDamage()
    {
        if(isHit == false)
        {
            //IMPORTANT!!!!! Take damage here
            //Debug.Log("Hit " + gameObject.name);
            health--;
            if(health == 2)
            {
                StartCoroutine(ShowHearts());
                heartParticles[2].gameObject.SetActive(true);
                heartParticles[2].Play();
                hearts[2].transform.GetChild(0).gameObject.SetActive(false);
                //heartsContainer.transform.position = new Vector3(0.45f, 0.5f, -0.5f);
            }
            if(health == 1)
            {
                StartCoroutine(ShowHearts());
                heartParticles[1].gameObject.SetActive(true);
                heartParticles[1].Play();
                hearts[1].transform.GetChild(0).gameObject.SetActive(false);
                //heartsContainer.transform.position = new Vector3(0.9f, 0.5f, -0.5f);
            }
            if(health == 0)
            {
                StartCoroutine(ShowHearts());
                heartParticles[0].gameObject.SetActive(true);
                heartParticles[0].Play();
                hearts[0].transform.GetChild(0).gameObject.SetActive(false);
                this.enabled = false;
                controller.enabled = false;
                animator.SetBool("isDead", true);
                StartCoroutine(DelayDeath());
                StartCoroutine(DisablePlayer());
            }
        }
        isHit = true;
        StartCoroutine(ResetHit());
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(3);
        isHit = false;
    }

    IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(10);
        shield.SetActive(false);
        isHit = false;
    }

    IEnumerator ShoesTimer()
    {
        yield return new WaitForSeconds(10);
        playerSpeed = 2;
        animator.SetBool("isRunning", false);
        if(isMounted == true)
        {
            playerSpeed = 10;
        }
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        playerParticles.gameObject.SetActive(true);
        playerParticles.Play();
        SoundManagerScript.PlaySound("sfx_exp_odd7");
    }

    IEnumerator DisablePlayer()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }

    IEnumerator ShowHearts()
    {
        heartsContainer.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        heartsContainer.gameObject.SetActive(false);
        if(health == 2)
        {
            heartParticles[2].gameObject.SetActive(false);
        }
        if(health == 1)
        {
            heartParticles[1].gameObject.SetActive(false);
        }
        if(health == 0)
        {
            heartParticles[0].gameObject.SetActive(false);
        }
    }

    IEnumerator ShowUpdatedHearts()
    {
        if(health == 3)
        {
            hearts[2].transform.GetChild(0).gameObject.SetActive(true);
        }
        if(health == 2)
        {
            hearts[1].transform.GetChild(0).gameObject.SetActive(true);
        }
        if(health == 1)
        {
            hearts[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        heartsContainer.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        heartsContainer.gameObject.SetActive(false);
    }
}