using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConfig : ScriptableObject
{
    public string ServerHttp_Url = "http://192.168.1.23:20000/auth";
    public string Server_IP = "192.168.1.23";
    public int Server_Port = 2000;
}
