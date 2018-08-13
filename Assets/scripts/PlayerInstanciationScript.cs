using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerInstanciationScript : MonoBehaviour {

    public static PlayerInstanciationScript Player;
    public enum DIRECTION { LEFT, RIGHT};
    public DIRECTION direction;
    private void Awake()
    {
        if (Player == null)
        {
            DontDestroyOnLoad(gameObject);
            Player = this;
        }
        else if (Player != this)
        {
            Player.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            Player.GetComponentInChildren<PlayerPhysics>().Freeze();
        }
    }
    
}
