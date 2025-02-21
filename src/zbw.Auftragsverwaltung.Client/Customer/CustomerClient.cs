﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Client.Common.Configuration;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Extensions;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client.Customer
{
    public class CustomerClient : ICustomerClient
    {

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IContextDataService _contextDataService;
        private readonly IExceptionMapper<HttpResponseMessage> _exceptionMapper;

        private UriBuilder GetDefaultPath => new UriBuilder(_baseUrl) { Path = "api/customer" };

        public CustomerClient(HttpClient httpClient, string baseUrl, IContextDataService contextDataService, IExceptionMapper<HttpResponseMessage> exceptionMapper)
        {
            _httpClient = httpClient;
            _contextDataService = contextDataService;
            _exceptionMapper = exceptionMapper;
            _baseUrl = baseUrl;
        }

        public async Task<CustomerDto> Get(Guid id)
        {
            var builder = GetDefaultPath;

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("id", id.ToString());
            builder.Query = query.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            await request.AddAuthenticationHeaders(_contextDataService);
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            
            await response.EnsureSuccess(_exceptionMapper);
            
            return await response.Content.ReadFromJsonAsync<CustomerDto>();

        }

        public async Task<PaginatedList<CustomerDto>> List(int size = 10, int page = 1, bool deleted = false)
        {
            var builder = GetDefaultPath;

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("size", size.ToString());
            query.Add("page", page.ToString());
            query.Add("deleted", deleted.ToString());
            builder.Query = query.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            await request.AddAuthenticationHeaders(_contextDataService);
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            await response.EnsureSuccess(_exceptionMapper);

            return await response.Content.ReadFromJsonAsync<PaginatedList<CustomerDto>>();
        }

        public async Task<CustomerDto> Add(CustomerDto customer)
        {
            var builder = GetDefaultPath;

            var request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            await request.AddAuthenticationHeaders(_contextDataService);
            request.Content = JsonContent.Create(customer);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            await response.EnsureSuccess(_exceptionMapper);

            return await response.Content.ReadFromJsonAsync<CustomerDto>();
        }

        public async Task<CustomerDto> Update(CustomerDto customer)
        {
            var builder = GetDefaultPath;

            var request = new HttpRequestMessage(HttpMethod.Patch, builder.Uri);
            await request.AddAuthenticationHeaders(_contextDataService);
            request.Content = JsonContent.Create(customer);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            await response.EnsureSuccess(_exceptionMapper);

            return await response.Content.ReadFromJsonAsync<CustomerDto>();
        }

        public async Task<bool> Delete(Guid id)
        {
            var builder = GetDefaultPath;

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("id", id.ToString());
            builder.Query = query.ToString();

            var request = new HttpRequestMessage(HttpMethod.Delete, builder.Uri);
            await request.AddAuthenticationHeaders(_contextDataService);
            
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            await response.EnsureSuccess(_exceptionMapper);

            return true;
        }
    }
}
