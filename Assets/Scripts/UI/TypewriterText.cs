using UnityEngine;
using TMPro;
using System.Collections;
using System.Text;

public class TypewriterText : MonoBehaviour
{
    public float typingSpeed = 0.1f;
    public string fullText;

    public TMP_Text textMeshPro;
    private StringBuilder currentText;
    public bool isTyping;

    public static bool AnyKeyDown = false;


    private void OnEnable()
    {
        fullText = textMeshPro.text;


        currentText = new StringBuilder();
    }



    public void Init()
    {
        StopAllCoroutines();
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        while (!Cinematic.canRunText) yield return null;
        isTyping = true;
        currentText.Length = 0; // Clear the current text
        int totalCharacters = fullText.Length;

        textMeshPro.text = "";

        yield return new WaitForSeconds(typingSpeed);

        for (int i = 0; i < totalCharacters; i++)
        {

            if (Input.anyKeyDown || AnyKeyDown)
            {

               
                AnyKeyDown = false;
                textMeshPro.text = fullText;
                break;
            }

           


            currentText.Append(fullText[i]);

            textMeshPro.text = currentText.ToString();

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
