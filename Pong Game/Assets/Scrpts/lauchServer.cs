using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class lauchServer : NetworkBehaviour
{
    public NetworkManager y;
    // Start is called before the first frame update
    public void Start()
    {
        y.StartServer();
        Debug.Log("LOL");
    }
}
