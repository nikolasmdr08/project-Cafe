using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitLevel: MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject img;

    private void Start() {
        transition = GetComponent<Animator>();
        AudioManager.S_PlayMusic(SFXID.MUSIC_UI_DEFAULT, 0);
    }
    public void initGame() {
        //int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        AudioManager.S_PlayMusic(SFXID.MUSIC_SCENE, 0);
        StartCoroutine(SceneLoad(1));
    }

    public IEnumerator SceneLoad(int sceneIndex) {
        img.SetActive(true);
        transition.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    public void DiseableImg() {
        img.SetActive(false);
    }
}
