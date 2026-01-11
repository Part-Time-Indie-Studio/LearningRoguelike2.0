using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionManager : Singleton<QuestionManager>
{
    [SerializeField] private List<string> questions;
    [SerializeField] private List<string> discardQuestions;
    [SerializeField] private List<string> currentDisplayedQuestions;

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

            // Reshuffle: only add questions that aren't currently displayed
            foreach (string discardedQuestion in discardQuestions)
            {
                if (!currentDisplayedQuestions.Contains(discardedQuestion))
                {
                    questions.Add(discardedQuestion);
                }
            }
            
            discardQuestions.Clear();
            
            // If all questions are currently displayed
            if (questions.Count == 0)
            {
                Debug.LogWarning("All questions are currently displayed.");
                return null;
            }
        }
        
        // Pick a random question that isn't already displayed
        string selectedQuestion;
        int attempts = 0;
        int maxAttempts = questions.Count * 2; // Prevent infinite loop
        
        do
        {
            int randomIndex = Random.Range(0, questions.Count);
            selectedQuestion = questions[randomIndex];
            attempts++;
            
            if (!currentDisplayedQuestions.Contains(selectedQuestion))
            {
                questions.RemoveAt(randomIndex);
                discardQuestions.Add(selectedQuestion);
                currentDisplayedQuestions.Add(selectedQuestion);
                return selectedQuestion;
            }
            
        } while (attempts < maxAttempts);
        
        // Fallback: shouldn't reach here if logic is correct
        Debug.LogWarning("Could not find a non-displayed question.");
        return null;
    }
    
    // Call this when a card is dismissed/removed from display
    public void RemoveDisplayedQuestion(string question)
    {
        currentDisplayedQuestions.Remove(question);
    }
}