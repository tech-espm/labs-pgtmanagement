using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json.Serialization;

namespace PGTManagement.Common.WebClient
{
    public static class WebClientOfT<TReturn>
    {
        private const string contentType = "application/json";
        private static HttpClient _client = new HttpClient();

        public static TReturn DeserializeTObject(string content)
        {

            if (content.Contains("statusCode") || content.Contains("Code"))
            {
                var obj = JsonConvert.DeserializeObject<ObjectResult>(content);

                if (obj.StatusCode == 200)
                {
                    var IsBool = false;

                    if (obj.Result != null && !bool.TryParse(obj.Result.ToString(), out IsBool))
                        return JsonConvert.DeserializeObject<TReturn>(obj.Result.ToString());
                    else
                        return JsonConvert.DeserializeObject<TReturn>(content);
                }
                else
                {
                    throw new WebClientOfTException(obj);
                }
            }
            else
            {
                return JsonConvert.DeserializeObject<TReturn>(content);
            }

        }

        public static TReturn DeserializeObjectThrowError(HttpResponseMessage response, string content)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new WebClientOfTException(new ObjectResult
                {
                    StatusCode = (int)response.StatusCode,
                    Message = response.ReasonPhrase,
                    Errors = new List<string> { response.ReasonPhrase }
                });

            }
            else
            {
                if (content.Contains("statusCode") || content.Contains("Code"))
                {
                    var obj = JsonConvert.DeserializeObject<ObjectResult>(content);
                    throw new WebClientOfTException(obj);
                }
                else
                {
                    throw new Exception(content);
                }
            }

        }

        public static async Task<TReturn> PostFileAsync(string url, byte[] file)
        {
            //_client.SetHttpClientHeaders(token);
            _client.DefaultRequestHeaders.Accept.Clear();

            using (var fileContent = new MultipartFormDataContent())
            {
                fileContent.Add(new StreamContent(new MemoryStream(file)), "file", "upload.jpg");

                var response = await _client.PostAsync(url, fileContent);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return DeserializeTObject(content);

                }
                else
                {
                    return DeserializeObjectThrowError(response, content);
                }
            }

        }


        public static async Task<TReturn> PostJsonAsync<TObj>(string url, TObj obj)
        {
            var newObj = new StringContent(JsonConvert.SerializeObject(obj,
                                                                        new JsonSerializerSettings
                                                                        {
                                                                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                                        }), Encoding.UTF8, contentType);

            var response = await _client.PostAsync(url, newObj);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return DeserializeTObject(content);
            }
            else
            {
                return DeserializeObjectThrowError(response, content);

            }

        }

        public static async Task<TReturn> PostAsync(string url)
        {
            var response = await _client.PostAsync(url, null);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return DeserializeTObject(content);
            }
            else
            {
                return DeserializeObjectThrowError(response, content);

            }

        }

        public static async Task<TReturn> PostAsync(string url, MultipartFormDataContent obj)
        {
            var response = await _client.PostAsync(url, obj);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return DeserializeTObject(content);
            }
            else
            {
                return DeserializeObjectThrowError(response, content);

            }

        }
        public static async Task<TReturn> PostXFormAsync<TObj>(string url, TObj obj)
        {
            var colecao = obj.ToNameValueCollection();
            var dict = new Dictionary<string, string>();
            foreach (var item in colecao)
            {
                dict.Add(item.ToString(), colecao[item.ToString()]);
            }

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(dict)
            };
            
            var response = await _client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return DeserializeTObject(content);
            }
            else
            {
                return DeserializeObjectThrowError(response, content);

            }

        }
        public static async Task<TReturn> GetByIdAsync(string url, string id, string token = null)
        {
            return await GetAsync(string.Concat(url, "/", id));
        }

        public static async Task<TReturn> GetByIdAsync(string url, int id, string token = null)
        {
            return await GetAsync(string.Concat(url, "/", id));
        }

        public static async Task<FileResult> GetFileAsync(string url, string token = null)
        {
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {

                var result = new FileResult
                {
                    Bytes = await response.Content.ReadAsByteArrayAsync(),
                    ContentType = response.Content.Headers.ContentType.ToString(),
                    FileName = response.Content.Headers.ContentDisposition.FileName
                };

                return result;

            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<ObjectResult>(content);
                throw new WebClientOfTException(obj);

            }

        }

        public static async Task<TReturn> GetAsync(string url)
        {

            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                return DeserializeTObject(content);
            }
            else
            {

                return DeserializeObjectThrowError(response, content);
            }

        }


        public static async Task<TReturn> PutAsJsonAsync<TObj>(string url, TObj obj)
        {
            var response = await _client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, contentType));
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return DeserializeTObject(content);
            }
            else
            {
                return DeserializeObjectThrowError(response, content);

            }

        }

        public static async Task<TReturn> DeleteAsync(string url, int id)
        {
            var response = await _client.DeleteAsync(string.Concat(url, "/", id));
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                return DeserializeTObject(content);
            }
            else
            {
                return DeserializeObjectThrowError(response, content);

            }

        }
        public static async Task<TReturn> DeleteAsync(string url)
        {
            var response = await _client.DeleteAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                return DeserializeTObject(content);
            }
            else
            {
                return DeserializeObjectThrowError(response, content);
            }

        }

    }
}
