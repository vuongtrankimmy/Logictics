using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.Gizp
{
    internal static class GzipService
    {
        #region Gzip
        /// <summary>
        /// Configures the gzip.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureGzip(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                            {
                               "image/svg+xml",
                               "application/atom+xml"
                            });
            });
        }
        #endregion
    }
}
