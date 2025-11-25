using FluentAssertions;
using SalesApi.Domain.Models;
using System;
using Xunit;

namespace SalesApi.Tests.Domain.Models
{
    public class SalesRecordTests
    {
        [Fact]
        public void GivenNewSalesRecord_WhenCreated_ThenAllPropertiesHaveDefaultValues()
        {
            // Given & When
            var salesRecord = new SalesRecord();

            // Then
            salesRecord.Region.Should().Be(string.Empty);
            salesRecord.Country.Should().Be(string.Empty);
            salesRecord.ItemType.Should().Be(string.Empty);
            salesRecord.SalesChannel.Should().Be(string.Empty);
            salesRecord.OrderPriority.Should().Be(string.Empty);
            salesRecord.OrderDate.Should().Be(default(DateTime));
            salesRecord.OrderID.Should().Be(0);
            salesRecord.ShipDate.Should().Be(default(DateTime));
            salesRecord.UnitsSold.Should().Be(0);
            salesRecord.UnitPrice.Should().Be(0);
            salesRecord.UnitCost.Should().Be(0);
            salesRecord.TotalRevenue.Should().Be(0);
            salesRecord.TotalCost.Should().Be(0);
            salesRecord.TotalProfit.Should().Be(0);
        }

        [Fact]
        public void GivenSalesRecord_WhenSettingStringProperties_ThenPropertiesAreSetCorrectly()
        {
            // Given
            var salesRecord = new SalesRecord();
            const string region = "North America";
            const string country = "United States";
            const string itemType = "Office Supplies";
            const string salesChannel = "Online";
            const string orderPriority = "High";

            // When
            salesRecord.Region = region;
            salesRecord.Country = country;
            salesRecord.ItemType = itemType;
            salesRecord.SalesChannel = salesChannel;
            salesRecord.OrderPriority = orderPriority;

            // Then
            salesRecord.Region.Should().Be(region);
            salesRecord.Country.Should().Be(country);
            salesRecord.ItemType.Should().Be(itemType);
            salesRecord.SalesChannel.Should().Be(salesChannel);
            salesRecord.OrderPriority.Should().Be(orderPriority);
        }

        [Fact]
        public void GivenSalesRecord_WhenSettingDateProperties_ThenPropertiesAreSetCorrectly()
        {
            // Given
            var salesRecord = new SalesRecord();
            var orderDate = new DateTime(2023, 6, 15);
            var shipDate = new DateTime(2023, 6, 20);

            // When
            salesRecord.OrderDate = orderDate;
            salesRecord.ShipDate = shipDate;

            // Then
            salesRecord.OrderDate.Should().Be(orderDate);
            salesRecord.ShipDate.Should().Be(shipDate);
        }

        [Fact]
        public void GivenSalesRecord_WhenSettingNumericProperties_ThenPropertiesAreSetCorrectly()
        {
            // Given
            var salesRecord = new SalesRecord();
            const long orderId = 123456789;
            const int unitsSold = 100;
            const decimal unitPrice = 25.50m;
            const decimal unitCost = 15.75m;
            const decimal totalRevenue = 2550.00m;
            const decimal totalCost = 1575.00m;
            const decimal totalProfit = 975.00m;

            // When
            salesRecord.OrderID = orderId;
            salesRecord.UnitsSold = unitsSold;
            salesRecord.UnitPrice = unitPrice;
            salesRecord.UnitCost = unitCost;
            salesRecord.TotalRevenue = totalRevenue;
            salesRecord.TotalCost = totalCost;
            salesRecord.TotalProfit = totalProfit;

            // Then
            salesRecord.OrderID.Should().Be(orderId);
            salesRecord.UnitsSold.Should().Be(unitsSold);
            salesRecord.UnitPrice.Should().Be(unitPrice);
            salesRecord.UnitCost.Should().Be(unitCost);
            salesRecord.TotalRevenue.Should().Be(totalRevenue);
            salesRecord.TotalCost.Should().Be(totalCost);
            salesRecord.TotalProfit.Should().Be(totalProfit);
        }

        [Fact]
        public void GivenSalesRecord_WhenSettingAllProperties_ThenAllPropertiesAreSetCorrectly()
        {
            // Given
            var salesRecord = new SalesRecord();
            const string region = "Europe";
            const string country = "Germany";
            const string itemType = "Electronics";
            const string salesChannel = "Retail";
            const string orderPriority = "Medium";
            var orderDate = new DateTime(2023, 8, 10);
            const long orderId = 987654321;
            var shipDate = new DateTime(2023, 8, 15);
            const int unitsSold = 50;
            const decimal unitPrice = 199.99m;
            const decimal unitCost = 120.00m;
            const decimal totalRevenue = 9999.50m;
            const decimal totalCost = 6000.00m;
            const decimal totalProfit = 3999.50m;

            // When
            salesRecord.Region = region;
            salesRecord.Country = country;
            salesRecord.ItemType = itemType;
            salesRecord.SalesChannel = salesChannel;
            salesRecord.OrderPriority = orderPriority;
            salesRecord.OrderDate = orderDate;
            salesRecord.OrderID = orderId;
            salesRecord.ShipDate = shipDate;
            salesRecord.UnitsSold = unitsSold;
            salesRecord.UnitPrice = unitPrice;
            salesRecord.UnitCost = unitCost;
            salesRecord.TotalRevenue = totalRevenue;
            salesRecord.TotalCost = totalCost;
            salesRecord.TotalProfit = totalProfit;

            // Then
            salesRecord.Region.Should().Be(region);
            salesRecord.Country.Should().Be(country);
            salesRecord.ItemType.Should().Be(itemType);
            salesRecord.SalesChannel.Should().Be(salesChannel);
            salesRecord.OrderPriority.Should().Be(orderPriority);
            salesRecord.OrderDate.Should().Be(orderDate);
            salesRecord.OrderID.Should().Be(orderId);
            salesRecord.ShipDate.Should().Be(shipDate);
            salesRecord.UnitsSold.Should().Be(unitsSold);
            salesRecord.UnitPrice.Should().Be(unitPrice);
            salesRecord.UnitCost.Should().Be(unitCost);
            salesRecord.TotalRevenue.Should().Be(totalRevenue);
            salesRecord.TotalCost.Should().Be(totalCost);
            salesRecord.TotalProfit.Should().Be(totalProfit);
        }
    }
}
