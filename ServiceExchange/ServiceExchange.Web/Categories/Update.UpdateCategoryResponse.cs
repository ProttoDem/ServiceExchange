﻿namespace ServiceExchange.Categories;

public class UpdateCategoryResponse(CategoryRecord category)
{
    public CategoryRecord Category { get; set; } = category;
}