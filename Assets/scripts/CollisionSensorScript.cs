using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSensorScript : MonoBehaviour {

    bool paused;
    public CollisionInfo info;
    [SerializeField]
    IntermediateSensorManager up, down, left, right;


    

    public struct CollisionInfo{
        public bool left, right;
        public bool up, down;
        public Vector2 groundVector;

        public void Reset()
        {
            left = right = false;
            up = down = false;

        }

        }
    private void Start()
    {
        info.Reset();

    }
    private void Update()
    {
        if (!paused)
        {
            info.Reset();
            info.up = up.on;
            info.down = down.on;
            info.left = left.on;
            info.right = right.on;
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
