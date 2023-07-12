using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Resources;

namespace VinEcomViewModel.Base
{
    public static class GlobalRule
    {
        public static IRuleBuilderOptions<T, string> IsImageUrlAsync<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MustAsync(async (imageUrl, c) =>
            {
                if (!Uri.TryCreate(imageUrl, UriKind.Absolute, out var validateResult) || (validateResult.Scheme != Uri.UriSchemeHttp && validateResult.Scheme != Uri.UriSchemeHttps)) return false;
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(imageUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                string contentType = response.Content.Headers.ContentType.MediaType;
                return contentType.StartsWith("image/");
            }).WithMessage(VinEcom.VINECOM_IMAGE_URL_FORMAT_ERROR);
        }
    }
}
