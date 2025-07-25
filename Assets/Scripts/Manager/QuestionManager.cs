using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : Singleton<QuestionManager>
{
    [SerializeField] private List<string> questions;
    [SerializeField] private List<string> discardQuestions;

    public void SetQuestions(List<string> receivedQuestions)
    {
        if (receivedQuestions == null) return;
        questions.AddRange(receivedQuestions);
    }
    
    public string GetQuestion()
    {
        if (questions.Count == 0)
        {
            if (discardQuestions.Count == 0)
            {
                Debug.LogWarning("No questions available to retrieve.");
                return null;
            }

            questions.AddRange(discardQuestions);
            discardQuestions.Clear();
        }
        
        int randomIndex = Random.Range(0, questions.Count);
        string selectedQuestion = questions[randomIndex];
        
        questions.RemoveAt(randomIndex);
        discardQuestions.Add(selectedQuestion);

        return selectedQuestion;
    }
}
