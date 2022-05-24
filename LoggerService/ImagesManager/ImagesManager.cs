using Contracts.ImagesManager;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService.ImagesManager
{
    /// <summary>
    /// The images manager.
    /// </summary>
    public class ImagesManager: IImagesManager
    {
        private readonly IOptions<AppDomainSettings> _domainSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagesManager"/> class.
        /// </summary>
        /// <param name="domainSettings">The domain settings.</param>
        public ImagesManager(IOptions<AppDomainSettings> domainSettings)
        {
            _domainSettings = domainSettings;
        }

        /// <summary>
        /// Urls the.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A string.</returns>
        public string Url(string path)
        {
            var url = path != "" ? path.Contains("http") ? path : _domainSettings?.Value?.domain_images + path : "";
            return url;
        }
    }
}
