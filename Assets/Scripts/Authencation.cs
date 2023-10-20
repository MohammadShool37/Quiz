using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authencation : MonoBehaviour
{
    private void Start() {
        Connection.instance.onConnection.AddListener(()=>{
            Debug.Log("Connectin To Server");

            Connection.instance.socket.Emit ("GetAuthKey");
        });
    }
}
