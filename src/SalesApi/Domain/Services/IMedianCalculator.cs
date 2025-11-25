using System;

namespace SalesApi.Domain.Services;

public interface IMedianCalculator
{
    decimal ComputeMedian(IEnumerable<decimal> values);
}
