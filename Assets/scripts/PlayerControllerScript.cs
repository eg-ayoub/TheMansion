using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

    bool paused;

    bool JumpButtonDown;
    bool JumpButtonUp;
    bool JumpButton;
    float horizontal;
    float vertical;
    bool WeaponSelect;
    bool WeaponSelectUp;
    bool OrbAttackDown;
    bool OrbAttack;
    bool StateChange;
    bool Next;
    bool Previous;

    PlayerMovementModifier MovementModifier;
	// Use this for initialization
	void Start () {
        MovementModifier = transform.parent.GetComponentInChildren<PlayerMovementModifier>();
        paused = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            JumpButtonUp = Input.GetButtonUp("Jump");
            JumpButtonDown = Input.GetButtonDown("Jump");
            JumpButton = Input.GetButton("Jump");
            horizontal = Input.GetAxis("Horizontal");
            //vertical = Input.GetAxis("Vertical");

            if (horizontal > 0)
            {
                PlayerInstanciationScript.Player.direction = PlayerInstanciationScript.DIRECTION.RIGHT;
            }else if (horizontal < 0)
            {
                PlayerInstanciationScript.Player.direction = PlayerInstanciationScript.DIRECTION.LEFT;
            }
            if (!OrbAttack)
            {
                MovementModifier.SetControls(JumpButtonUp, JumpButtonDown, horizontal, vertical);
            }
            else
            {
                //... why ? 
                MovementModifier.SetControls(true, false, 0, 0);
            }

            


        }
	}




    public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }
}
