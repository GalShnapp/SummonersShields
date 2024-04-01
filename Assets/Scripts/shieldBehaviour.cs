using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Serialization;

public enum ShieldStatus
{
    Off,
    On,
    Charging,
    Flying,
    Stuck
}

public class ShieldBehaviour : MonoBehaviour
{
    public ShieldStatus shieldStatus;
    public float maxChargeTime = 1.5f;
    public float throwSpeed;
    private Transform player;
    public float chargeMeter;
    public int weightPenaltyPercent;
    private Rigidbody2D rigidbody;
    public float throwPower;
    private float throwTimer;

    public Animator animator;
    public Vector3 whereDidILeaveMyShield;
    public Vector3 whereDidIThrowMyShield;
    
    void Start()
    {
        player = this.transform.parent;
        shieldStatus = ShieldStatus.Off;
        chargeMeter = 0;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        SetNewState();
        GameManager position;
    }

    // Update is called once per frame
    void Update()
    {
        ShieldStatus oldStatus = shieldStatus;
        
        // Equip unEquip shield
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (shieldStatus == ShieldStatus.On)
            {
                shieldStatus = ShieldStatus.Off;
            }
            else if (shieldStatus == ShieldStatus.Off)
            {
                shieldStatus = ShieldStatus.On;
            }
        }
        
        if (shieldStatus == ShieldStatus.Flying)
        {
            //rigidbody.AddForce(transform.up * throwPower *- Time.deltaTime * 0.75f, ForceMode2D.Force);
            throwTimer -= Time.deltaTime;
            if (throwTimer <= 0)
            {
                Debug.Log("Done flying");
                Debug.Log(throwTimer);
                shieldStatus = ShieldStatus.Stuck;
            }
        }
        
        // Start shield charge
        if (Input.GetKey(KeyCode.Space))
        {
            if (shieldStatus == ShieldStatus.On)
            {
                shieldStatus = ShieldStatus.Charging;
            }
            else if (shieldStatus == ShieldStatus.Charging)
            {
                chargeMeter += Time.deltaTime;
            }
        } 
        
        // Throw shield
        else if (Input.GetButtonUp("Space") == true && shieldStatus == ShieldStatus.Charging)
        {
            shieldStatus = ShieldStatus.Flying;
            ShieldThrow(chargeMeter);
            Debug.Log(chargeMeter);
        }

        if (shieldStatus != oldStatus)
        {
            SetNewState();
        }
    }
    
    public float SetNewState()
    {
        switch (shieldStatus)
        {
            case ShieldStatus.Off:
                weightPenaltyPercent = 0;
                animator.SetBool("isOn", false);
                this.gameObject.GetComponent<Collider2D>().enabled = false;
                transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                break;
            case ShieldStatus.On:
                weightPenaltyPercent = 50;
                animator.SetBool("isOn", true);
                animator.SetBool("playerHasShield", true);
                this.gameObject.GetComponent<Collider2D>().enabled = true;
                transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                break;
            case ShieldStatus.Charging:
                weightPenaltyPercent = 70;
                animator.SetBool("isCharging", true);
                break;
            case ShieldStatus.Flying:
                weightPenaltyPercent = 0;
                animator.SetBool("isFlying", true);
                animator.SetBool("isCharging", false);
                animator.SetBool("playerHasShield", false);
                animator.SetBool("isOn", false);
                break;
            case ShieldStatus.Stuck:
                weightPenaltyPercent = 0;
                animator.SetBool("isFlying", false);
                animator.SetBool("isCharging", false);
                animator.SetBool("playerHasShield", false);
                animator.SetBool("isOn", false);
                rigidbody.isKinematic = true;
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return weightPenaltyPercent;
    }
    
    private void ShieldThrow(float chargeTime)
    {
        transform.parent = null;
        /*
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        whereDidIThrowMyShield = mousePosition;
        */
        rigidbody.isKinematic = false;
        rigidbody.constraints = RigidbodyConstraints2D.None;
        if (chargeTime >= maxChargeTime)
        {
            chargeTime = maxChargeTime;
        }
        throwTimer = chargeTime;
        rigidbody.AddForce(transform.up * throwPower, ForceMode2D.Impulse);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            PickupShield();
        }
    }

    private void PickupShield()
    {
        Debug.Log("pickup");
        transform.SetParent(player);
        transform.position = player.GetChild(0).position;
        transform.up = player.transform.up;
        shieldStatus = ShieldStatus.On;
        SetNewState();
    }
}