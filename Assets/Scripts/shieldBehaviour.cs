using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ShieldStatus
{
    Off,
    On,
    Out,
    Charging
}

public class ShieldBehaviour : MonoBehaviour
{
    public ShieldStatus shieldStatus;
    public int chargeMeter;
    public int weightPenaltyPercent;

    public Animator animator;
    public Vector3 whereDidILeaveMyShield;
    
    void Start()
    {
        shieldStatus = ShieldStatus.Off;
        chargeMeter = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool equipStatus = animator.GetBool("isOn");
        bool chargingStatus = animator.GetBool("isCharging");
        bool playerHasShield = animator.GetBool("playerHasShield");
        
        if (Input.GetKeyDown(KeyCode.Q) && playerHasShield)
        {
            animator.SetBool("isOn", !equipStatus);
            Debug.Log(animator.GetBool("isOn"));
        }

        if (Input.GetKey(KeyCode.Space) && equipStatus && playerHasShield)
        {
            animator.SetBool("isCharging", true);
            Debug.Log("is charging: ");
            Debug.Log(animator.GetBool("isCharging"));
            chargeMeter++;
            Debug.Log(chargeMeter);
        } 
        else if (chargingStatus)
        {
            releaseShield(chargeMeter);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("isOn", true);
            animator.SetBool("isCharging", false);
            animator.SetBool("playerHasShield", true);
        }

        if (!playerHasShield)
        {
            transform.position = whereDidILeaveMyShield;
        }
        
        equipStatus = animator.GetBool("isOn");
        chargingStatus = animator.GetBool("isCharging");
        playerHasShield = animator.GetBool("playerHasShield");

        if (equipStatus)
        {
            shieldStatus = ShieldStatus.On;
        }
        else if (chargingStatus)
        {
            shieldStatus = ShieldStatus.Charging;
        }
        else if (!playerHasShield)
        {
            shieldStatus = ShieldStatus.Out;
        }
        else if (!equipStatus)
        {
            shieldStatus = ShieldStatus.Off;
        }
    }

    public float SetNewState()
    {
        switch (shieldStatus)
        {
            case ShieldStatus.Off:
                weightPenaltyPercent = 0;
                break;
            case ShieldStatus.On:
                weightPenaltyPercent = 50;
                break;
            case ShieldStatus.Out:
                weightPenaltyPercent = 0;
                break;
            case ShieldStatus.Charging:
                weightPenaltyPercent = 70;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return weightPenaltyPercent;
    }
    
    private void releaseShield(int chargeMeter)
    {
        Debug.Log("release");
        animator.SetBool("isCharging", false);
        animator.SetBool("playerHasShield", false);
        transform.Translate(0,1,0);
        whereDidILeaveMyShield = transform.position;
    }
}