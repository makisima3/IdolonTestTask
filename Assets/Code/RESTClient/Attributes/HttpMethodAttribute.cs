using System;

namespace Code.RESTClient.Attributes
{
    public enum MethodType { GET, POST, PATCH, UPDATE, DELETE, PUT }

    [AttributeUsage(AttributeTargets.Class)]
    public class HttpMethodAttribute : Attribute
    {
        public string method;
        public HttpMethodAttribute(MethodType methodType)
        {
            method = methodType.ToString();
        }
    }
}