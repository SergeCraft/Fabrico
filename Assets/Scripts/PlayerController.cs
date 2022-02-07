using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Capsule object rigidbody
    /// <summary>
    private Rigidbody _rb;
    

    public PlayerSettings Settings;

    public float RotYDeg = 0.0f;

    public Backpack Backpack;

    public FixedJoystick Joystick;



    /// <summary>
    /// Start is called before the first frame update
    /// <summary>
    void Start()
    {
        Settings = new PlayerSettings();
        _rb = GetComponent<Rigidbody>();
        Backpack = new Backpack(Settings.BackpackSize, transform);
        Joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
    }

    /// <summary>
    // Update is called once per frame
    /// <summary>
    void Update()
    {
        MoveBody();
        MoveBackpack();
    }


    /// <summary>
    /// Moving body method dependent from input
    /// </summary>
    /// <param name="input"></param>
    void MoveBody()
    {
        //applying force 
        _rb.AddForce(new Vector3(Joystick.Horizontal, 0.0f, Joystick.Vertical) * Settings.ForceMultiplier);

        //speed limitation with counterforce
        if (_rb.velocity.magnitude > Settings.MovespeedMax ) _rb.AddForce(_rb.velocity * -0.2f);
        
        //apply rotation
        RotYDeg = Vector3.SignedAngle(Vector3.forward, _rb.velocity, Vector3.up);
        transform.rotation = Quaternion.Euler(new Vector3(
                 0.0f,
                 RotYDeg,
                 0.0f));
    }

    private void MoveBackpack()
	{
        Backpack.UpdateBackpackStack();
	}
}
