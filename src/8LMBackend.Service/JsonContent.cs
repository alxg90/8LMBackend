using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
namespace _8LMBackend.Service{
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
}