using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Cores.Extension.Services.CSP
{
    internal static class CSPService
    {
        /// <summary>
        /// Apps the csp.
        /// </summary>
        /// <param name="app">The app.</param>
        public static void AppCsp(this IApplicationBuilder app)
        {
            //https://letienthanh0212.medium.com/how-to-secure-your-net-core-web-application-with-nwebsec-c705fb5daf4b
            app.UseCsp(options =>
            {
                options.BlockAllMixedContent()
                //.ScriptSources(s => s.Self())
                .StyleSources(s => s.Self())
                //.StyleSources(s => s.UnsafeInline())
                .FontSources(s => s.Self())
                .FormActions(s => s.Self())
                .FrameAncestors(s => s.Self())
                .ImageSources(s => s.Self());
            });
            app.UseXfo(option =>
            {
                option.SameOrigin();
            });
            app.UseXXssProtection(option =>
            {
                option.EnabledWithBlockMode();
            });
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.Use(async (context, next) =>
            {
                if (!context.Response.Headers.ContainsKey("Contact"))
                {
                    context.Response.Headers.Add("Contact", "Vuong Tran Kim My");
                }
                if (!context.Response.Headers.ContainsKey("Tel"))
                {
                    context.Response.Headers.Add("Tel", "(084) 70-778-2689");
                }
                if (!context.Response.Headers.ContainsKey("Email"))
                {
                    context.Response.Headers.Add("Email", "trankimmyvuong@gmail.com");
                }
                if (!context.Response.Headers.ContainsKey("Skype"))
                {
                    context.Response.Headers.Add("Skype", "vuong.trankimmy");
                }
                await next();
            });
        }
    }
}
