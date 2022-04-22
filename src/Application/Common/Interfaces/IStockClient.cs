﻿namespace Application.Common.Interfaces;

public interface IStockClient
{
    Task UpdateStock(Guid productId, int unitsChange);
}
