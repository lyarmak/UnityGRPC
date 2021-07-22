using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Helloworld;

public class HelloRequest : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private Greeter.GreeterClient _client;
    [SerializeField] private Channel _channel;

    public void Start()
    {
        _channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
        _client = new Greeter.GreeterClient(_channel);
        Debug.Log("Channel and Client established");
    }

    public void ChangeColor()
    {
        SendRequest();
        Debug.Log("Hello Request Sent");
    }

    public void SendRequest()
    {
        var clientRequest = new Helloworld.HelloRequest() { Name = "Sir" };
        var result = _client.SayHello(clientRequest);
        Debug.Log($"Returned Value: { result.Message }");
    }


    private void OnDisable()
    {
        _channel.ShutdownAsync().Wait();
    }
}
