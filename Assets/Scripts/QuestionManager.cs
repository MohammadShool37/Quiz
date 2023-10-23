using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Quiz
{
    public class QuestionManager : MonoBehaviour
    {

        #region Get UI Components

        [SerializeField]
        private UIComponentData uiComponents;

        [System.Serializable]
        public class UIComponentData
        {
            public TextMeshProUGUI questionText;
            public AnswerButton[] answerButtons;
            public TextMeshProUGUI numberOfQuestionsText;
        }

        #endregion

        public QuestionStructure[] questions;
        int currentQuestion = 0;

        void NextQuestion()
        {
            currentQuestion++;

            if (currentQuestion >= questions.Length)
            {
                Debug.Log("Time To Win");
                return;
            }
            
            isAnswerInProcess = false;

            ResetAnswerButtons();
            ShowCurrentQuestion();
        }

        public void ShowCurrentQuestion()
        {
            uiComponents.questionText.text = questions[currentQuestion].question;

            for (int i = 0; i < questions[currentQuestion].answers.Length; i++)
            {
                uiComponents.answerButtons[i].answerText.text = questions[currentQuestion].answers[i];
            }

            uiComponents.numberOfQuestionsText.text = currentQuestion + "/" + questions.Length;
        }

        public void ResetAnswerButtons()
        {
            foreach (AnswerButton ansBtn in uiComponents.answerButtons)
            {
                ansBtn.ResetAnswerButton();
            }
        }

        bool isAnswerInProcess = false;
        public void Answer(int ans)
        {
            if (isAnswerInProcess) return;
            bool isWin = questions[currentQuestion].real_answer == questions[currentQuestion].answers[ans];

            void EffectEnd()
            {
                if (!isWin)
                {
                    Debug.Log("Time To Fail.");
                    return;
                }

                StartCoroutine(enumerator());

                IEnumerator enumerator()
                {
                    yield return new WaitForSeconds(3f);
                    NextQuestion();
                }
            }

            if (isWin)
                uiComponents.answerButtons[ans].TrueEffect(EffectEnd);
            else
                uiComponents.answerButtons[ans].NotTrueEffect(EffectEnd);

            isAnswerInProcess = true;
        }

        private void Start()
        {
            // add listener to ui answer buttons
            for (int i = 0; i < uiComponents.answerButtons.Length; i++)
            {
                int temp = i;

                uiComponents.answerButtons[i].answerButton.onClick.AddListener(() => Answer(temp));
            }
        }

    }

}