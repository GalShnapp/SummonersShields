using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldStatus
{
    Off,
    On,
    Out,
    Charging
}

public class shieldBehaviour : MonoBehaviour
{
    public ShieldStatus shieldStatus;
    public int chargeMeter;
    public float weight;

    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
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
    }

    void releaseShield(int chargeMeter)
    {
        Debug.Log("release");
        animator.SetBool("isCharging", false);
        animator.SetBool("playerHasShield", false);
        transform.Translate(0,1,0);
    }
}