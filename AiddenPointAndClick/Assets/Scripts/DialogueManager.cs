using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText; // the text on screen
    private Queue<string> sentences; // queue of sentences we take from our objects
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); // make sure we're starting with an empty queue
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue) // function to help us start the dialogue
    {
        sentences.Clear(); // clear any old sentences

        foreach (string sentence in dialogue.senteces)
        {
            sentences.Enqueue(sentence); // putting all the sentences from the object into the queue
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) // when we run out of sentences
        {
            // end diolague function
            return;
        }

        string sentence = sentences.Dequeue(); // get the sentence at the top
        StopAllCoroutines(); // make sure we're not printing text anymore
        StartCoroutine(TypeSentence(sentence)); // start a coroutine to type out the sentence
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // type each letter out
            yield return new WaitForSeconds(0.1f); // wait time per letter
        }
    }
}
