using System;

namespace Code.RESTClient.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UrlAttribute : Attribute
    {
        public string url;

        public UrlAttribute(string url)
        {
            this.url = url;
        }
    }
}