using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    private bool isInRange;
    public Animator fadeSystem;
    public Text nextLevel;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        nextLevel = GameObject.FindGameObjectWithTag("NextLevel").GetComponent<Text>();
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(loadNextScene());
            nextLevel.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            nextLevel.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
        nextLevel.enabled = false;
    }

    public IEnumerator loadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
