using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public AnimationClip popupAnimation;

    void Start()
    {
        StartCoroutine(ShowPopupAfterDelay());
    }

    IEnumerator ShowPopupAfterDelay()
    {
        yield return new WaitForSeconds(popupAnimation.length);

        // Enable the TextMeshPro UI Text element to show the pop-up message
        popupText.enabled = true;

        // Play the pop-up animation
        Animation anim = popupText.GetComponent<Animation>();
        if (anim == null)
        {
            Debug.LogWarning("No Animation component found on the TextMeshPro UI Text element.");
            yield break;
        }
        anim.clip = popupAnimation;
        anim.Play();
    }
}
