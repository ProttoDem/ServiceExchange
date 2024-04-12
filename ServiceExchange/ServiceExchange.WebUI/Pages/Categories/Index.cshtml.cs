﻿using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.ViewModels;

namespace ServiceExchange.WebUI.Pages.Categories;

public class Index : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public Index(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    // Property to hold categories
    public List<CategoryViewModel> CategoriesList { get; set; } = new List<CategoryViewModel>();
    
    public async Task OnGet()
    {
        // Create the HTTP client using the FruitAPI named factory
        var httpClient = _httpClientFactory.CreateClient("ServiceExchangeAPI");

        // Execute the GET operation and store the response, the empty parameter
        // in GetAsync doesn't modify the base address set in the client factory 
        using HttpResponseMessage response = await httpClient.GetAsync("/Categories");

        // If the operation is successful deserialize the results into the data model
        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            CategoriesList = await JsonSerializer.DeserializeAsync<List<CategoryViewModel>>(contentStream);
        }
    }
}