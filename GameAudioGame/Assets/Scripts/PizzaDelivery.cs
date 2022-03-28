using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PizzaDelivery : MonoBehaviour
{
    public List<GameObject> targets;
    GameObject target;
    float timer = 60f;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        target = chooseTarget();
        target.gameObject.tag = "Target";
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        timerText.text = timer.ToString();
        if (timer <= 0)
        {
            endGame();
        }
    }

    GameObject chooseTarget()
    {
        return targets[Random.Range(0, targets.Count)];
    }

    void reachTarget()
    {
        targets.Remove(target);
        resetTimer();
        chooseTarget();
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("pls");
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("we did it");
            reachTarget();
        }
    }

    void resetTimer()
    {
        timer = 60f;
    }

    void endGame()
    {
        SceneManager.LoadScene("Fail");
    }
}
