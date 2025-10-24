using System.Net;
using FluentAssertions;
using Oire.NetFlux.Http;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Http;

public class CategoryEndpointsTests: IDisposable {
    private const string BaseUrl = "https://miniflux.example.com";
    private const string ApiKey = "test-api-key";

    private readonly HttpMessageHandlerMock _httpMock;
    private readonly HttpClient _httpClient;
    private readonly MinifluxHttpClient _client;

    public CategoryEndpointsTests() {
        _httpMock = new HttpMessageHandlerMock();
        _httpClient = new HttpClient(_httpMock) { BaseAddress = new Uri(BaseUrl) };
        _client = new MinifluxHttpClient(BaseUrl, apiKey: ApiKey, httpClient: _httpClient);
    }

    public void Dispose() {
        _client?.Dispose();
        _httpClient?.Dispose();
        _httpMock?.Dispose();
    }

    [Fact]
    public async Task GetCategoriesAsync_Should_Return_CategoryList() {
        // Arrange
        var expectedCategories = new[] {
            TestDataFactory.CreateCategory(1, "Technology"),
            TestDataFactory.CreateCategory(2, "Business")
        };
        _httpMock.SetupJsonResponse(expectedCategories, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == "/v1/categories");

        // Act
        var categories = await _client.GetAsync<Category[]>("/v1/categories");

        // Assert
        categories.Should().NotBeNull();
        categories!.Should().HaveCount(2);
        categories[0].Title.Should().Be("Technology");
        categories[1].Title.Should().Be("Business");
    }

    [Fact]
    public async Task GetCategoryAsync_Should_Return_SpecificCategory() {
        // Arrange
        var categoryId = 5;
        var expectedCategory = TestDataFactory.CreateCategory(categoryId, "Science");
        _httpMock.SetupJsonResponse(expectedCategory, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/categories/{categoryId}");

        // Act
        var category = await _client.GetAsync<Category>($"/v1/categories/{categoryId}");

        // Assert
        category.Should().NotBeNull();
        category!.Id.Should().Be(categoryId);
        category.Title.Should().Be("Science");
    }

    [Fact]
    public async Task CreateCategoryAsync_Should_Send_CategoryData() {
        // Arrange
        var createRequest = new CategoryCreateRequest {
            Title = "New Category"
        };
        var expectedCategory = TestDataFactory.CreateCategory(10, "New Category");

        _httpMock.SetupJsonResponse(expectedCategory, HttpStatusCode.Created, req =>
            req.Method == HttpMethod.Post && req.RequestUri!.AbsolutePath == "/v1/categories");

        // Act
        var category = await _client.PostAsync<Category>("/v1/categories", createRequest);

        // Assert
        category.Should().NotBeNull();
        category!.Title.Should().Be("New Category");

        // Verify request content
        var capturedRequest = await _httpMock.GetCapturedRequestBodyAsync<CategoryCreateRequest>();
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Title.Should().Be(createRequest.Title);
    }

    [Fact]
    public async Task UpdateCategoryAsync_Should_Send_UpdateData() {
        // Arrange
        var categoryId = 10;
        var updateRequest = new CategoryUpdateRequest {
            Title = "Updated Category"
        };
        var expectedCategory = TestDataFactory.CreateCategory(categoryId, "Updated Category");

        _httpMock.SetupJsonResponse(expectedCategory, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/categories/{categoryId}");

        // Act
        var category = await _client.PutAsync<Category>($"/v1/categories/{categoryId}", updateRequest);

        // Assert
        category.Should().NotBeNull();
        category!.Title.Should().Be("Updated Category");

        // Verify request content
        var capturedRequest = await _httpMock.GetCapturedRequestBodyAsync<CategoryUpdateRequest>();
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Title.Should().Be(updateRequest.Title);
    }

    [Fact]
    public async Task DeleteCategoryAsync_Should_Send_DeleteRequest() {
        // Arrange
        var categoryId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Delete && req.RequestUri!.AbsolutePath == $"/v1/categories/{categoryId}");

        // Act
        await _client.DeleteAsync($"/v1/categories/{categoryId}");

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests[0];
        request.Method.Should().Be(HttpMethod.Delete);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/categories/{categoryId}");
    }

    [Fact]
    public async Task GetCategoryCountersAsync_Should_Return_Counters() {
        // Arrange
        var categoryId = 5;
        var expectedCounters = new FeedCounters {
            ReadCounters = new Dictionary<long, int> { { categoryId, 100 } },
            UnreadCounters = new Dictionary<long, int> { { categoryId, 25 } }
        };

        _httpMock.SetupJsonResponse(expectedCounters, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/categories/{categoryId}/counters");

        // Act
        var counters = await _client.GetAsync<FeedCounters>($"/v1/categories/{categoryId}/counters");

        // Assert
        counters.Should().NotBeNull();
        counters!.ReadCounters.Should().ContainKey(categoryId);
        counters.ReadCounters[categoryId].Should().Be(100);
        counters.UnreadCounters.Should().ContainKey(categoryId);
        counters.UnreadCounters[categoryId].Should().Be(25);
    }

    [Fact]
    public async Task MarkCategoryAsReadAsync_Should_Send_PutRequest() {
        // Arrange
        var categoryId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/categories/{categoryId}/mark-all-as-read");

        // Act
        await _client.PutAsync<object>($"/v1/categories/{categoryId}/mark-all-as-read", null);

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests[0];
        request.Method.Should().Be(HttpMethod.Put);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/categories/{categoryId}/mark-all-as-read");
    }

    [Fact]
    public async Task RefreshCategoryAsync_Should_Send_PutRequest() {
        // Arrange
        var categoryId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/categories/{categoryId}/refresh");

        // Act
        await _client.PutAsync<object>($"/v1/categories/{categoryId}/refresh", null);

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests[0];
        request.Method.Should().Be(HttpMethod.Put);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/categories/{categoryId}/refresh");
    }

    [Fact]
    public async Task GetCategoryFeedsAsync_Should_Return_FeedList() {
        // Arrange
        var categoryId = 5;
        var feed1 = TestDataFactory.CreateFeed(1, categoryId);
        feed1.Title = "Feed 1";
        var feed2 = TestDataFactory.CreateFeed(2, categoryId);
        feed2.Title = "Feed 2";
        var expectedFeeds = new[] { feed1, feed2 };

        _httpMock.SetupJsonResponse(expectedFeeds, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/categories/{categoryId}/feeds");

        // Act
        var feeds = await _client.GetAsync<Feed[]>($"/v1/categories/{categoryId}/feeds");

        // Assert
        feeds.Should().NotBeNull();
        feeds!.Should().HaveCount(2);
        feeds[0].Title.Should().Be("Feed 1");
        feeds[1].Title.Should().Be("Feed 2");
        // Feeds have Category object, not CategoryId property
        feeds.Should().AllSatisfy(f => f.Category?.Id.Should().Be(categoryId));
    }
}
