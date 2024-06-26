using System.Collections;
using UnityEngine;
using TMPro;

public class BlinkingTMPText : MonoBehaviour
{
    public TMP_Text textToBlink;
    public float blinkInterval = 0.5f;

    private bool isBlinking = false;

    private void Start()
    {
        if (textToBlink == null)
        {
            textToBlink = GetComponent<TMP_Text>();
        }
        StartBlinking();
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkText());
        }
    }

    public void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopCoroutine(BlinkText());
            if (textToBlink != null)
            {
                textToBlink.enabled = true;
            }
        }
    }

    private IEnumerator BlinkText()
    {
        while (isBlinking)
        {
            if (textToBlink != null)
            {
                textToBlink.enabled = !textToBlink.enabled;
            }
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
