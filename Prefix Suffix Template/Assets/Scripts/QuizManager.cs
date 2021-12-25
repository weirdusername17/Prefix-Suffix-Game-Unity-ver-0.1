using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<NewBehaviourScript> QnA;
    public GameObject[] options;
    public int CurrentQuestion;

    public GameObject Quizpanel;
    public GameObject GoPanel;

    public Text QuestionTxt;
    public Text ScoreTxt;

    int totalQuestions = 0;
    public int score;

    private void Start()
    {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        generateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;
    }
    public void correct()
    {
        //when you answer correct
        score += 1;
        QnA.RemoveAt(CurrentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        //when you answer wrong
        QnA.RemoveAt(CurrentQuestion);
        generateQuestion();
    }

    void setAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[CurrentQuestion].Answers[i];

            if(QnA[CurrentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if(QnA.Count > 0)
        {
            CurrentQuestion = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[CurrentQuestion].Question;
            setAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
        

      
    }
}
