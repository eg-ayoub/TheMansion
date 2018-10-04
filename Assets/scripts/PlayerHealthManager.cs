using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour {

    
    bool paused;
    bool canTogglepause;
    [SerializeField]
    int HP;
    int maxHP;
    [SerializeField]
    Image[] barElements;
    [SerializeField]
    Color full, empty;

    int sceneNow;

    [SerializeField]
    GameObject gameOver, Pause;
    Object[] objects;
    int checkpoint;
 	private void Start() {

        maxHP = 10;
        HP = 4;
        updateHPHUD();
        sceneNow = 0;
        paused = false;
        canTogglepause = true;
        checkpoint = 0;
    }
	// Update is called once per frame
	void TakeDamage(int damage)
    {
        if(!paused){
            
			GetComponent<AudioSource>().Play();
			CameraShakerScript.cam.RandomShake();
            if (damage >= HP)
            {
                StartCoroutine("ReturnToCheckpoint");
            }
            else
            {
                
                HP -= damage;
                SceneManager.LoadSceneAsync(sceneNow);
            }
            updateHPHUD();
        }
    }

    void Heal(int health){
        HP = Mathf.Clamp(HP + health, 0, maxHP);
        updateHPHUD();
    }

    void AddMaxHP(int health){
        // maxHP = Mathf.Clamp(maxHP + health, 0, 10);
        // updateHPHUD();
    }
    public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }

    public int GetHP(){
        return HP;
    }

    void SetHP(int health){
        HP = Mathf.Clamp(health, 0, maxHP);
        updateHPHUD();
    }
    
    void SetMaxHP(int maxhealth){
        maxHP = Mathf.Clamp(maxhealth, 1, 20);
        updateHPHUD();
    }

    public int  GetMaxHP(){
        return maxHP;
    }
    
    void updateHPHUD(){
        for(int i = 0; i < 10; i++){
            if(i < HP){
                barElements[i].color = full;
            }else if(i < maxHP){
                barElements[i].color = empty;
            }else{
                barElements[i].color = Color.clear;
            }
        }
    }

    public void IncrementScene(){
        sceneNow ++;
        if(sceneNow == 10){
            checkpoint = 10;
        }
        if(sceneNow == 20){
            checkpoint = 20;
        }
    }

    IEnumerator ReturnToCheckpoint(){
        sceneNow = checkpoint;
        gameOver.SetActive(true);
        gameOver.GetComponent<AudioSource>().Play();
        ToggleGamePaused();
        for(int i =0; i < 120; i++){
            yield return null;
        }
        SceneManager.LoadScene(sceneNow);
        ToggleGamePaused();
        gameOver.SetActive(false);
        SetHP(3);
    }

    public void ToggleGamePaused(){
        objects = FindObjectsOfType(typeof(GameObject));
        paused = !paused;
        foreach(GameObject obj in objects){
            obj.SendMessage((paused ? "OnPauseGame" : "OnResumeGame"), SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Update() {
        //Heal(5);
        if(Input.GetButtonDown("Pause") && canTogglepause){
            Debug.Log("pause!");
            ToggleGamePaused();
            Pause.SetActive(paused);
            canTogglepause = false;
            StartCoroutine("ResetPause");
        }
    }

    IEnumerator ResetPause(){
        for(int i = 0; i < 10; i++){
            yield return null;
        }
        canTogglepause = true;
    }

}
