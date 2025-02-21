﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Invoices;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts;
using zbw.Auftragsverwaltung.Lib.HttpClient.Extensions;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client.Invoices
{
   public class InvoiceClient : IInvoiceClient
   {
       private readonly HttpClient _httpClient;
       private readonly string _baseUrl;
       private readonly IContextDataService _contextDataService;
       private readonly IExceptionMapper<HttpResponseMessage> _exceptionMapper;

       private UriBuilder GetDefaultPath => new UriBuilder(_baseUrl) { Path = "api/invoice" };

       public InvoiceClient(HttpClient httpClient, string baseUrl, IContextDataService contextDataService, IExceptionMapper<HttpResponseMessage> exceptionMapper)
       {
           _httpClient = httpClient;
           _baseUrl = baseUrl;
           _contextDataService = contextDataService;
           _exceptionMapper = exceptionMapper;
       }


       public async Task<InvoiceDto> Get(Guid id)
       {
           var builder = GetDefaultPath;

           var query = HttpUtility.ParseQueryString(builder.Query);
           query.Add("id", id.ToString());
           builder.Query = query.ToString();
           var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
           await request.AddAuthenticationHeaders(_contextDataService);
           var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

           await response.EnsureSuccess(_exceptionMapper);

           return await response.Content.ReadFromJsonAsync<InvoiceDto>();
        }

       public async Task<PaginatedList<InvoiceDto>> List(int size = 10, int page = 1, bool deleted = false)
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

           return await response.Content.ReadFromJsonAsync<PaginatedList<InvoiceDto>>();
        }

       public async Task<InvoiceDto> Add(InvoiceDto invoice)
       {
           var builder = GetDefaultPath;

           var request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
           await request.AddAuthenticationHeaders(_contextDataService);
           request.Content = JsonContent.Create(invoice);

           var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

           await response.EnsureSuccess(_exceptionMapper);

           return await response.Content.ReadFromJsonAsync<InvoiceDto>();
        }

       public async Task<InvoiceDto> Update(InvoiceDto invoice)
       {
           var builder = GetDefaultPath;

           var request = new HttpRequestMessage(HttpMethod.Patch, builder.Uri);
           await request.AddAuthenticationHeaders(_contextDataService);
           request.Content = JsonContent.Create(invoice);

           var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

           await response.EnsureSuccess(_exceptionMapper);

           return await response.Content.ReadFromJsonAsync<InvoiceDto>();
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
