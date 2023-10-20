using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public List<Question> questions;
    private int currentQuestionLength;

    [System.Serializable]
    public struct Question
    {
        public string question;
        public string[] answers;
        public int answerIndex;
    }

    [SerializeField, Space(5), Header("UI :")]
    private TextMeshProUGUI questionText;

    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField, Tooltip("text who want to show current answer count and all question count")]
    private TextMeshProUGUI answerLengthInforamtionText;

    public void ShowQuestionInformation()
    {
        Question currentQuestion = questions[currentQuestionLength];
        questionText.text = currentQuestion.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].answerText.text = currentQuestion.answers[i];
        }
        
        answerLengthInforamtionText.text = (currentQuestionLength + 1) + "/" + (questions.Count);
    }

    private void NextQuestion()
    {
        currentQuestionLength++;

        if (currentQuestionLength >= questions.Count)
        {
            Debug.Log("questions end know is time to win");
            return;
        }

        isAnswerInProcess = false;
        ResetAnswerButtons();
        ShowQuestionInformation();
    }

    private void ResetAnswerButtons()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].ResetAnswerButton();
        }
    }

    private void Start()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int temp = i;
            answerButtons[i].answerButton.onClick.AddListener(() => Answer(temp));
        }

        ShowQuestionInformation();
    }

    bool isAnswerInProcess = false;

    public void Answer(int ansIndex = 0)
    {
        if (isAnswerInProcess) return;

        if (questions[currentQuestionLength].answerIndex == ansIndex)
            answerButtons[ansIndex].TrueEffect(OnEndAnswering);
        else
            answerButtons[ansIndex].NotTrueEffect(OnEndAnswering);

        isAnswerInProcess = true;
    }

    void OnEndAnswering(bool isTrue)
    {
        if (!isTrue)
        {
            Debug.Log("time to fail");
            return;
        }

        StartCoroutine("DelayBeforeNextQuestion");
    }

    IEnumerator DelayBeforeNextQuestion()
    {
        yield return new WaitForSeconds(3f);
        NextQuestion();
    }

}
