using UnityEngine.Networking;

namespace Code.RESTClient
{
    public class Response<T>
    {
        public bool HasError => Error != null;
        public HttpError Error;

        public T Data;
        public string JSONContent;
        public UnityWebRequest UnityWebRequest;
    }
}
