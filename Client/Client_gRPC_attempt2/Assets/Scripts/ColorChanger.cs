using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Protocolor;

public class ColorChanger : MonoBehaviour
{
    // Public Fields
    public Material material;
    public readonly Color defaultColor = Color.white;

    // Serialized Fields
    [SerializeField] private ColorGenerator.ColorGeneratorClient _client;
    [SerializeField] private Channel _channel;

    public void Start()
    {
        _channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
        _client = new ColorGenerator.ColorGeneratorClient(_channel);
        Debug.Log("Channel and Client established");
    }

    public void ChangeColor()
    {
        material.color = GetColor(material.color);
        Debug.Log("Color Change Request");
    }

    public Color GetColor(Color currentColor)
    {
        var currentColorSend = ColorUtility.ToHtmlStringRGBA(currentColor);
        var newColor = GetNewColor(currentColorSend);
        return Color.red;
    }

    public string GetNewColor(string currentColor)
    {
        var clientRequest = new CurrentColor { Color = currentColor };
        var result = _client.GetRandomColor(clientRequest);
        Debug.Log($"Returned Value: { result.Color }");
        return result.Color;
    }

    private void OnDisable()
    {
        _channel.ShutdownAsync().Wait();
    }
}
