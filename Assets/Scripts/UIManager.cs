using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Text messageText;
    [SerializeField] private float waitTime;
    [SerializeField] private Text score;
    private int scorevar=0;
    void Start()
    {
        messageText.gameObject.SetActive(false);
        if(messageText==null)
        {
            Debug.LogError("message text is missing from uimanager script");
            return;
        }
        if(score==null)
        {
            Debug.LogError("The score text or memory crystal text is missing in uiscript");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        score.text = scorevar.ToString();
    }

    public void ShowMessage(string msg)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        scorevar+=1;
        StartCoroutine(MessageCooldown());
    }

    IEnumerator MessageCooldown()
    {
        yield return new WaitForSeconds(waitTime);
        messageText.gameObject.SetActive(false);
    }
}
