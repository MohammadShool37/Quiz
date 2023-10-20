using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public List<Question> questions;

    [System.Serializable]
    public struct Question {
        public string question;
        public List<QuestionAnswer> answers;
    }

    [System.Serializable]
    public struct QuestionAnswer {
        public string answer;
        public bool isTrue;
    }
}
