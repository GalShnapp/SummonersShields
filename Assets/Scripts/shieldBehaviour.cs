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
    private Transform player;
    public int chargeMeter;
    public int weightPenaltyPercent;

    public Animator animator;
    public Vector3 whereDidILeaveMyShield;
    public Vector3 whereDidIThrowMyShield;
    
    void Start()
    {
        player = this.transform.parent;
        shieldStatus = ShieldStatus.Off;
        chargeMeter = 0;
        animator = GetComponent<Animator>();
        animator.SetBool("isOn", true);
    }

    // Update is called once per frame
    void Update()
    {
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

        // Start shield charge
        if (Input.GetKey(KeyCode.Space) && shieldStatus == ShieldStatus.On)
        {
            shieldStatus = ShieldStatus.Charging;
            chargeMeter++;
           // Debug.Log(chargeMeter);
        } 
        // Throw shield
        else if (Input.GetButtonUp("Space") == true && shieldStatus == ShieldStatus.Charging)
        {
            shieldStatus = ShieldStatus.Stuck;
            ShieldThrow();
        }
        
        // Reset shield
        if (Input.GetKeyDown(KeyCode.R))
        {
            shieldStatus = ShieldStatus.On;
        }
        
        if (shieldStatus == ShieldStatus.Stuck)
        {
            transform.position = whereDidIThrowMyShield;
        }

        SetNewState();
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
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return weightPenaltyPercent;
    }
    
    private void ShieldThrow()
    {
        whereDidIThrowMyShield = new Vector3(-15, 0, 0);
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
        //transform.SetParent(player);
        shieldStatus = ShieldStatus.On;
    }
}