using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HTTP : Singleton<HTTP> { // TODO: add a request builder

    private HTTP() {}

    public void Post(string url, Action<UploadHandler, DownloadHandler> requestHandler) {
        Post(url, data: null, requestHandler);
    }

    public void Post(string url, string data, Action<UploadHandler, DownloadHandler> requestHandler) {
        UnityWebRequest request = new UnityWebRequest(url);
        
        if (data != null) {
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(data));
        }
        request.downloadHandler = new DownloadHandlerBuffer();
        request.method = UnityWebRequest.kHttpVerbPOST;
        StartCoroutine(HTTPCoroutine(request, requestHandler));
    }

    public void Post(string url, Dictionary<string, string> parameters, Action<UploadHandler, DownloadHandler> requestHandler) {
        UnityWebRequest request = UnityWebRequest.Post(url, parameters);

        StartCoroutine(HTTPCoroutine(request, requestHandler));
    }

    IEnumerator HTTPCoroutine(UnityWebRequest request, Action<UploadHandler, DownloadHandler> requestHandler) {
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            Debug.Log("HTTP request error: " + request.error);
        } else {
            requestHandler.Invoke(request.uploadHandler, request.downloadHandler);
        }
    }

}
