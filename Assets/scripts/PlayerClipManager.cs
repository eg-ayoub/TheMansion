 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClipManager : MonoBehaviour {


    bool paused;
    Vector2 deltaPosition;
    BoxCollider2D PlayerBox;
    Rectangle PlayerRect;
    const float skinWidth = .015f;
    float XW;
    float YW;

    public int HorizontalRayCount;
    public int VerticalRayCount;

    float HorizontalRaySpacing;
    float VerticalRaySpacing;
    public LayerMask layerMask;




	void Start () {
        PlayerBox = GetComponentInParent<BoxCollider2D>();
        PlayerRect = new Rectangle(PlayerBox, 0);
        XW = PlayerRect.B.x;
        YW = PlayerRect.B.y;
        PlayerRect.Reset(PlayerBox, skinWidth);
        XW -= PlayerRect.B.x;
        YW -= PlayerRect.B.y;
        HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
        VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);
        paused = false;
	}
	


	void FixedUpdate () {
        if (!paused)
        {
            PlayerRect.Reset(PlayerBox, skinWidth);
            VerticalMove(ref deltaPosition);
            HorizontalMove(ref deltaPosition);
            PlayerInstanciationScript.Player.transform.Translate(deltaPosition);
        }
	}
    

    void HorizontalMove(ref Vector2 deltaPosition) {
        int directionX = (int) Mathf.Sign(deltaPosition.x);
        float RayCastLenght = Mathf.Abs(deltaPosition.x);
        Vector2 RayCastOrigin = (directionX > 0 ? PlayerRect.C : PlayerRect.D);
        HorizontalRaySpacing = Mathf.Abs((PlayerRect.B - PlayerRect.C).y) / (HorizontalRayCount - 1);
        for (int i = 0; i < HorizontalRayCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(RayCastOrigin + HorizontalRaySpacing * i * Vector2.up, directionX * Vector2.right, RayCastLenght, layerMask);
            if (hit)
            {
                deltaPosition.x = directionX * (hit.distance - XW);
                RayCastLenght = hit.distance;
            }
        }

    }
    void VerticalMove(ref Vector2 deltaPosition) {
        int directionY = (int) Mathf.Sign(deltaPosition.y);
        float RayCastLenght = Mathf.Abs(deltaPosition.y);
        Vector2 RayCastOrigin = (directionY > 0 ? PlayerRect.A : PlayerRect.D );
        VerticalRaySpacing = Mathf.Abs((PlayerRect.B - PlayerRect.A).x) / (VerticalRayCount - 1);
        for(int i = 0; i < VerticalRayCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(RayCastOrigin + VerticalRaySpacing * i * Vector2.right, directionY * Vector2.up, RayCastLenght, layerMask);
            if (hit)
            {
                deltaPosition.y = directionY * (hit.distance - YW);
                RayCastLenght = hit.distance;
            }
        }

    }
    public void SetDeltaPosition(Vector2 move)
    {
        deltaPosition = move;
    }

    public void OnPauseGame() {
        paused = true;
    }
    public void OnResumeGame()
    {
        paused = false;
    }

    // public void Dive(){
    //     PlayerBox.size = new Vector2(10, 10);
    //     PlayerRect = new Rectangle(PlayerBox, 0);

    //     XW = PlayerRect.B.x;
    //     YW = PlayerRect.B.y;
    //     PlayerRect.Reset(PlayerBox, skinWidth);
    //     XW -= PlayerRect.B.x;
    //     YW -= PlayerRect.B.y;

    // }

    // public void JumpOut(){
    //     PlayerBox.size = new Vector2(10, 30);
    //     PlayerRect = new Rectangle(PlayerBox, 0);

    //     XW = PlayerRect.B.x;
    //     YW = PlayerRect.B.y;
    //     PlayerRect.Reset(PlayerBox, skinWidth);
    //     XW -= PlayerRect.B.x;
    //     YW -= PlayerRect.B.y;
    // }
}
