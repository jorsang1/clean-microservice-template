﻿namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Models;

internal record UpdateStockRequest(Guid ProductId, int UnitsChange);