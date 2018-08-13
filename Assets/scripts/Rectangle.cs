using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle  {

    BoxCollider2D box;
    private Vector2 d;
    Vector2[] Corners;
    Vector2 center;
    private Vector2 a;
    private Vector2 b;
    private Vector2 c;

    public Vector2 A
    {
        get
        {
            return a;
        }

        set
        {
            a = value;
        }
    }

    public Vector2 B
    {
        get
        {
            return b;
        }

        set
        {
            b = value;
        }
    }

    public Vector2 C
    {
        get
        {
            return c;
        }

        set
        {
            c = value;
        }
    }

    public Vector2 D
    {
        get
        {
            return d;
        }

        set
        {
            d = value;
        }
    }

    public Rectangle(BoxCollider2D collider, float lambda)
    {
        box = collider;

        center = box.bounds.center;

        A = (box.transform.TransformPoint(new Vector2(-box.size.x / 2, box.size.y / 2)) - box.bounds.center) *(1- lambda) + box.bounds.center;

        B = (box.transform.TransformPoint(new Vector2(box.size.x / 2, box.size.y / 2)) - box.bounds.center) *(1 - lambda) + box.bounds.center;

        C = (box.transform.TransformPoint(new Vector2(box.size.x / 2, -box.size.y / 2)) - box.bounds.center) *(1 - lambda) + box.bounds.center;

        D = (box.transform.TransformPoint(new Vector2(-box.size.x / 2, -box.size.y / 2)) - box.bounds.center) *(1 - lambda) + box.bounds.center;

        
        Corners = new Vector2[4];
        Corners[0] = A;
        Corners[1] = B;
        Corners[2] = C;
        Corners[3] = D;
    }

    public void Reset(BoxCollider2D collider, float lambda)
    {
        box = collider;

        center = box.bounds.center;

        A = (box.transform.TransformPoint(new Vector2(-box.size.x / 2, box.size.y / 2)) - box.bounds.center) * (1 - lambda) + box.bounds.center;

        B = (box.transform.TransformPoint(new Vector2(box.size.x / 2, box.size.y / 2)) - box.bounds.center) * (1 - lambda) + box.bounds.center;

        C = (box.transform.TransformPoint(new Vector2(box.size.x / 2, -box.size.y / 2)) - box.bounds.center) * (1 - lambda) + box.bounds.center;

        D = (box.transform.TransformPoint(new Vector2(-box.size.x / 2, -box.size.y / 2)) - box.bounds.center) * (1 - lambda) + box.bounds.center;


        Corners = new Vector2[4];
        Corners[0] = A;
        Corners[1] = B;
        Corners[2] = C;
        Corners[3] = D;
    }

}
