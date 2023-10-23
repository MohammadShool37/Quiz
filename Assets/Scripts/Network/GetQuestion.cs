using UnityEngine;

namespace Quiz
{
    
    [RequireComponent(typeof(QuestionManager))]
    public class GetQuestion : MonoBehaviour
    {

        QuestionManager questionManager;
        
        void Start()
        {
            questionManager = GetComponent<QuestionManager>();

            Connection.instance.onConnection.AddListener(() =>
            {
                Connection.instance.socket.Emit("getQuestions");

                Connection.instance.socket.On<QuestionStructure[]>("getQuestions", (QuestionStructure[] questions) =>
                {
                    questionManager.questions = questions;
                    questionManager.ShowCurrentQuestion();
                });
            });

            Connection.instance.onDisconnect.AddListener(() =>
            {
                Connection.instance.socket.Off("getQuestions");
            });
        }
    }
    
}