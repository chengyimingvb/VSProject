using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace CYM
{
    [Unobfus]
    public class AsyncWebRequest
    {
        public UnityWebRequest Request;
        public Exception UploadException;

        public int Timeout = 20; // how long until the webrequest will attempt to abort

        public bool RequestIsError
        {
            get
            {
                return Request.isNetworkError;
            }
        }
        public bool RequestTimedOut
        {
            get
            {
                return Request.error == "Request timeout";

            }
        }

        public IEnumerator Post(string uri, WWWForm data, Action<UnityWebRequest, Exception> onFinished = null)
        {
            Request = UnityWebRequest.Post(uri, data);
            Request.chunkedTransfer = false; // required so the request sends the content-length header

            yield return sendRequest();

            if (onFinished != null)
                onFinished(Request, UploadException);
        }

        public IEnumerator Get(string uri, Action<UnityWebRequest, Exception> onFinished = null)
        {
            Request = UnityWebRequest.Get(uri);

            yield return sendRequest();

            if (onFinished != null)
                onFinished(Request, UploadException);
        }

        private IEnumerator sendRequest()
        {
            Request.timeout = Timeout;

            // send the request
            AsyncOperation op = null;
            try
            {
                op = Request.SendWebRequest();
            }
            catch (Exception e)
            {
                UploadException = e;
                yield break;
            }

            // block until request is finished
            while (!op.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }

    }
}
