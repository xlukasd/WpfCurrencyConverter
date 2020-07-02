using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.Shared.CurrencyConversion;

namespace Application.Services.CurrencyConversion.Converters
{
    public abstract class AbstractOnlineConverter<TResponseObject> : IOnlineCurrencyConverter
    {
        protected abstract string ApiBaseUri { get; }

        protected abstract IEnumerable<KeyValuePair<string, string>> GetQueryParameters(InputObject inputObject);

        public async Task<Result<OutputObject>> Convert(InputObject inputObject)
        {
            HttpClient httpClient = new HttpClient();

            IEnumerable<KeyValuePair<string, string>> queryParameters = GetQueryParameters(inputObject);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(queryParameters);

            UriBuilder uriBuilder = new UriBuilder(ApiBaseUri)
            {
                Query = await formUrlEncodedContent.ReadAsStringAsync()
            };

            HttpResponseMessage response = await httpClient.GetAsync(uriBuilder.Uri);

            return response.IsSuccessStatusCode
                ? Result.Ok(await GetFromContentAsync(response.Content, inputObject))
                : Result.Failure<OutputObject>($"HTTP Error: {response.StatusCode}");
        }

        protected async Task<OutputObject> GetFromContentAsync(HttpContent content, InputObject inputObject)
        {
            TResponseObject deserializedContent = await content.ReadAsAsync<TResponseObject>();
            return ConvertResponseToCommonOutput(deserializedContent, inputObject);
        }

        protected abstract OutputObject ConvertResponseToCommonOutput(TResponseObject responseObject, InputObject inputObject);
    }
}
