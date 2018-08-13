using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClipManager : MonoBehaviour {
	bool paused;
	Vector2 deltaPosition;

	BoxCollider2D box;
	Rectangle rect;
	const float skinWidth = .15f;

	float XW, YW;
	public int HorizontalRayCount;
    public int VerticalRayCount;

    float HorizontalRaySpacing;
    float VerticalRaySpacing;
    public LayerMask layerMask;


	private void Start() {
		box = GetComponent<BoxCollider2D>();
		rect = new Rectangle(box, 0);
		XW = rect.B.x;
		YW = rect.B.y;
		rect.Reset(box, skinWidth);
		XW -= rect.B.x;
		YW -= rect.B.y;
		HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
        VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);
        paused = false;
	}
    internal void SetDeltaPosition(Vector2 DeltaPosition)
    {
        deltaPosition = DeltaPosition;
    }

	private void FixedUpdate() {
		if(!paused){
			rect.Reset(box, skinWidth);
			
            VerticalMove(ref deltaPosition);
            HorizontalMove(ref deltaPosition);

			transform.Translate(deltaPosition);
		}
	}
	void HorizontalMove(ref Vector2 deltaPosition) {
        int directionX = (int) Mathf.Sign(deltaPosition.x);
        float RayCastLenght = Mathf.Abs(deltaPosition.x);
        Vector2 RayCastOrigin = (directionX > 0 ? rect.C : rect.D);
        HorizontalRaySpacing = Mathf.Abs((rect.B - rect.C).y) / (HorizontalRayCount - 1);
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
        Vector2 RayCastOrigin = (directionY > 0 ? rect.A : rect.D );
        VerticalRaySpacing = Mathf.Abs((rect.B - rect.A).x) / (VerticalRayCount - 1);
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
	public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }
}
