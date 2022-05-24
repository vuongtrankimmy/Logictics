using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cores.AntiXss
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;

        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // enable buffering so that the request can be read by the model binders next
            httpContext.Request.EnableBuffering();

            // leaveOpen: true to leave the stream open after disposing,
            // so it can be read by the model binders
            using (var streamReader = new StreamReader
                  (httpContext.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var raw = await streamReader.ReadToEndAsync();
                if (raw != "")
                {
                    raw = raw.Trim().Replace("\n", "").Replace("\r", "");
                    var decode = HttpUtility.HtmlDecode(raw);//lỗi html - kiểm tra cách cho phép html lưu xuống
                    var sanitiser = new HtmlSanitizer();
                    sanitiser.AllowedAttributes.Add("class");
                    sanitiser.AllowedCssProperties.Add("style");
                    var sanitised = sanitiser.Sanitize(decode);
                    //Tạm khóa - cần phải kiểm tra kỹ và nhiều hơn chỗ này. đang không ổn.
                    //20220522
                    //if (decode != sanitised&&decode.Length-sanitised.Length>5)
                    //{
                    //    throw new BadHttpRequestException("XSS injection detected from middleware.");
                    //}
                }
            }

            // rewind the stream for the next middleware
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next.Invoke(httpContext);
        }
    }
}
