using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LaunchProjectile : MonoBehaviour
{
    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private GameObject waterBallon;

    [SerializeField]
    public int totalThrows;

    [SerializeField]
    private float throwCooldown;

    [SerializeField]
    private KeyCode throwKey = KeyCode.Space;

    [SerializeField]
    private float throwForce;

    [SerializeField]
    private float throwUpdwardForce;

    bool readyToThrow;

    PhotonView view;

    Rigidbody projectileRb;
    GameObject projectile;

    private Vector3 scaleChange;

    //IMPORTANT!!!!!!!!!!
    private Vector3 maxScaleChange = new Vector3(5,0.1f,5);
    private int maxThrows = 5;
    private int maxThrowForce = 7;
    private int maxUpdwardForce = 4;

    [SerializeField]
    private WaterBalloon wb;

    private void Start() 
    {
        wb.splashZone.transform.localScale = new Vector3(1,0.1f,1);
        scaleChange = new Vector3(1,0,1);
        readyToThrow = true;
        view = GetComponent<PhotonView>();
        if(!view.IsMine)
        {
            Destroy(projectileRb);
        }
    }

    private void Update() 
    {
        
    }

    private void Throw()
    {
        readyToThrow = false;

        projectile = PhotonNetwork.Instantiate(waterBallon.name, attackPoint.position, Quaternion.identity);

        projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = transform.forward * throwForce + transform.up * throwUpdwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    private void TestThrow()
    {
        if(readyToThrow && totalThrows > 0)
        {
            if(view.IsMine)
            {
                Throw();
                StartCoroutine(AddWaterBalloons());
            }
        }
    }

    IEnumerator AddWaterBalloons()
    {
        yield return new WaitForSeconds(3);
        totalThrows++;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "WaterBalloon" && totalThrows != maxThrows)
        {
            totalThrows += 1;
        }
        if(other.gameObject.tag == "Muscle" && throwForce != maxThrowForce && throwUpdwardForce != maxUpdwardForce)
        {
            throwForce += 0.5f;
            throwUpdwardForce += 0.5f;
        }
        if(other.gameObject.tag == "WaterBottle" && wb.splashZone.transform.localScale != maxScaleChange)
        {
            if(!view.IsMine)
            {
                return;
            }
            wb.splashZone.transform.localScale += scaleChange;
        }
    }
}
