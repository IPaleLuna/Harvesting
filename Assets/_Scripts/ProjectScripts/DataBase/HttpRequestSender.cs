using Cysharp.Threading.Tasks;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PaleLuna.httpRequests
{
    public static class HttpRequestSender
    {
        private const string API_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzNzA2ODAiLCJuYW1lIjoiUGFsZUx1bmEiLCJpYXQiOjE3MzA0NDg4MTV9.u1QfZ6IMDhrHBl1pRbTBXM1EYJtfOFDq0oB0s_RyjC4";

        public static void Report(float value)
        {
            Debug.Log(value);
        }

        public static async UniTask<string> GetSend(string url)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Authorization", $"Bearer {API_TOKEN}");

            await request.SendWebRequest();

            // ��������� ������
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }

            return request.downloadHandler.text;
        }

        public static async UniTask PostSend(string url, string body)
        {
            WWWForm formData = new WWWForm();

            UnityWebRequest request = UnityWebRequest.Post(url, formData);
            request.SetRequestHeader("Authorization", $"Bearer {API_TOKEN}");

            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);

            UploadHandler uploadHandler = new UploadHandlerRaw(bodyBytes);

            request.uploadHandler = uploadHandler;

            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

            // �������� ������� � �������� ������
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError(request.error);
        }
    }
}

