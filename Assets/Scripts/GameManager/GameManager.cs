using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] int score = 0;
    [SerializeField] bool paused = false;
    public TMP_Text scoreText;

    public GameObject panelEnd;
    public GameObject saveConfirm;
    public TMP_Text panelScore;
    public TMP_InputField registerUsername;

    private Hero hero;

    private GameObject enemySpawnManager;

    float tiempoDelFrame = 0;
    float tiempoEnSegAMostrar = 0;

    void Awake() 
    {
        hero = GameObject.Find("Player").GetComponent<Hero>(); 
        enemySpawnManager = GameObject.Find("EnemySpawnManager"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        score = SumarMatros();
        if (paused) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }

        if(hero.IsDeath && !paused) {
            GameOver();
        }
    }

    void GameOver()
    {
        enemySpawnManager.SetActive(false);
        AudioManager.S_PlayMusic(SFXID.NONE, 0);
        PausarJuego();
        panelEnd.SetActive(true);
        panelScore.text = "Score: " + score;
    }

    public void PausarJuego() {
        paused = !paused;
    }

    public int SumarMatros() {
        tiempoDelFrame = Time.deltaTime;
        tiempoEnSegAMostrar += tiempoDelFrame;
        return ((int)tiempoEnSegAMostrar);
    }

    public void Reset() {
        Debug.Log("Reset");
        saveConfirm.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void Backmenu() {
        Debug.Log("Back");
        PausarJuego();
        SceneManager.LoadScene(0);
    }

    public void SaveData() {
        Debug.Log("Save");
        string user = "AAAA";
        if(registerUsername.text != "") {
            user = registerUsername.text;
        }
        SaveManager.RegistrarScore(score, user);
        saveConfirm.SetActive(true);
    }
}
