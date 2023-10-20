using UnityEngine;
using BestHTTP.SocketIO3;
using UnityEngine.Events;

public class Connection : MonoBehaviour
{
    private static Connection insc;
    public static Connection instance
    {
        get { return insc; }
        set
        {
            if (insc)
            {
                Destroy(value);
                return;
            }

            insc = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public string socketAddress = "http://127.0.0.1:3000";
    SocketManager socketManager;
    public Socket socket;

    public UnityEvent onConnection;

    void Start()
    {
        socketManager = new SocketManager(new System.Uri(socketAddress));
        socket = socketManager.Socket;

        socket.On("connect", () => onConnection?.Invoke());
    }
}
