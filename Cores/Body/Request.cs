using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Helpers.Sort;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cores.Body
{
    public static class Request
    {
        public static async Task<T> GetRawBodyAsync<T>(
            this HttpRequest request,bool encrypto=false,
            Encoding encoding = null) where T:new()
        {
            if (!request.Body.CanSeek)
            {
                // We only do this if the stream isn't *already* seekable,
                // as EnableBuffering will create a new stream instance
                // each time it's called
                request.EnableBuffering();
            }

            request.Body.Position = 0;
            var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);
            var body = await reader.ReadToEndAsync().ConfigureAwait(false);
            request.Body.Position = 0;            
            var t = new T();
            if (body == ""|| body.ToLower() == "null") return t;
            if (encrypto)
            {
                var enBody = body.Replace("\"", "");
                body = Encrypto.Encrypto.DecryptByAES(enBody);
            }
            if (body != "")
            {
                var decode = HttpUtility.UrlDecode(body);//lỗi html - kiểm tra cách cho phép html lưu xuống
                var jObj = (JObject)JsonConvert.DeserializeObject(decode);
                await SortExtension.Sort(jObj);
                t = JsonConvert.DeserializeObject<T>(jObj?.ToString() ?? "");
            }
            return t;
        }
    }
}
