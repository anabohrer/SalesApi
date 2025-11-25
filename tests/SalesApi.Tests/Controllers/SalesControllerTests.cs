using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesApi.Application;
using SalesApi.Controllers;
using SalesApi.Domain.Models;

namespace SalesApi.Tests.Controllers
{
    public sealed class SalesControllerTests
    {
        [Fact]
        public async Task GivenValidFile_WhenUploadAndSummarize_ThenReturnsOkResultWithSummary()
        {
            // Given
            var expectedSummary = new SummaryResult
            {
                MedianUnitCost = 125.50m,
                MostCommonRegion = "North America",
                FirstOrderDate = new DateTime(2023, 1, 1),
                LastOrderDate = new DateTime(2023, 12, 31),
                DaysBetweenFirstAndLast = 364,
                TotalRevenue = 1_000_000.00m
            };

            var mockUseCase = new Mock<ISalesSummaryUseCase>();
            mockUseCase.Setup(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(expectedSummary);

            var controller = new SalesController(mockUseCase.Object);
            var file = CreateMockFormFile("test.csv", "Region,Country\nNorth America,USA");

            // When
            var result = await controller.UploadAndSummarize(file, CancellationToken.None);

            // Then
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().Be(expectedSummary);
            mockUseCase.Verify(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GivenNullFile_WhenUploadAndSummarize_ThenReturnsBadRequest()
        {
            // Given
            var mockUseCase = new Mock<ISalesSummaryUseCase>();
            var controller = new SalesController(mockUseCase.Object);
            IFormFile? file = null;

            // When
            var result = await controller.UploadAndSummarize(file!, CancellationToken.None);

            // Then
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult!.Value.Should().Be("Please upload a CSV file in form field 'file'.");
            mockUseCase.Verify(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GivenEmptyFile_WhenUploadAndSummarize_ThenReturnsBadRequest()
        {
            // Given
            var mockUseCase = new Mock<ISalesSummaryUseCase>();
            var controller = new SalesController(mockUseCase.Object);
            var file = CreateMockFormFile("empty.csv", "");

            // When
            var result = await controller.UploadAndSummarize(file, CancellationToken.None);

            // Then
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult!.Value.Should().Be("Please upload a CSV file in form field 'file'.");
            mockUseCase.Verify(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GivenValidFile_WhenUseCaseThrowsException_ThenExceptionIsPropagated()
        {
            // Given
            var mockUseCase = new Mock<ISalesSummaryUseCase>();
            mockUseCase.Setup(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                       .ThrowsAsync(new InvalidOperationException("Test exception from use case"));

            var controller = new SalesController(mockUseCase.Object);
            var file = CreateMockFormFile("test.csv", "Region,Country\nEurope,Germany");

            // When
            Func<Task> act = async () => await controller.UploadAndSummarize(file, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Test exception from use case");
            mockUseCase.Verify(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GivenValidFile_WhenUploadAndSummarize_ThenPassesCancellationTokenToUseCase()
        {
            // Given
            var expectedSummary = new SummaryResult { TotalRevenue = 500.00m };
            var cancellationToken = new CancellationToken();
            var mockUseCase = new Mock<ISalesSummaryUseCase>();
            mockUseCase.Setup(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), cancellationToken))
                       .ReturnsAsync(expectedSummary);

            var controller = new SalesController(mockUseCase.Object);
            var file = CreateMockFormFile("test.csv", "Region\nAsia");

            // When
            await controller.UploadAndSummarize(file, cancellationToken);

            // Then
            mockUseCase.Verify(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task GivenFileWithContent_WhenUploadAndSummarize_ThenStreamIsPassedToUseCase()
        {
            // Given
            var expectedSummary = new SummaryResult { MostCommonRegion = "Europe" };
            Stream? receivedStream = null;
            var mockUseCase = new Mock<ISalesSummaryUseCase>();
            mockUseCase.Setup(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                       .Callback<Stream, CancellationToken>((stream, _) => receivedStream = stream)
                       .ReturnsAsync(expectedSummary);

            var controller = new SalesController(mockUseCase.Object);
            var fileContent = "Region,Country,ItemType\nEurope,France,Electronics";
            var file = CreateMockFormFile("data.csv", fileContent);

            // When
            await controller.UploadAndSummarize(file, CancellationToken.None);

            // Then
            receivedStream.Should().NotBeNull();
            mockUseCase.Verify(x => x.ComputeSummaryAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private static IFormFile CreateMockFormFile(string fileName, string content)
        {
            var mockFile = new Mock<IFormFile>();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.Length).Returns(content.Length);
            mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
            mockFile.Setup(f => f.ContentType).Returns("text/csv");

            return mockFile.Object;
        }
    }
}
