using System.Net;
using FluentAssertions;
using Oire.NetFlux.Http;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Http;

public class FeedEndpointsTests: IDisposable {
    private const string BaseUrl = "https://miniflux.example.com";
    private const string ApiKey = "test-api-key";

    private readonly HttpMessageHandlerMock _httpMock;
    private readonly HttpClient _httpClient;
    private readonly MinifluxHttpClient _client;

    public FeedEndpointsTests() {
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
    public async Task GetFeedsAsync_Should_Return_FeedList() {
        // Arrange
        var expectedFeeds = new[] {
            TestDataFactory.CreateFeed(1, 1),
            TestDataFactory.CreateFeed(2, 1)
        };
        _httpMock.SetupJsonResponse(expectedFeeds, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == "/v1/feeds");

        // Act
        var feeds = await _client.GetAsync<Feed[]>("/v1/feeds");

        // Assert
        feeds.Should().NotBeNull();
        feeds!.Should().HaveCount(2);
        feeds[0].Id.Should().Be(1);
        feeds[1].Id.Should().Be(2);
    }

    [Fact]
    public async Task GetFeedAsync_Should_Return_SpecificFeed() {
        // Arrange
        var feedId = 5;
        var expectedFeed = TestDataFactory.CreateFeed(feedId, 1);
        _httpMock.SetupJsonResponse(expectedFeed, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/feeds/{feedId}");

        // Act
        var feed = await _client.GetAsync<Feed>($"/v1/feeds/{feedId}");

        // Assert
        feed.Should().NotBeNull();
        feed!.Id.Should().Be(feedId);
    }

    [Fact]
    public async Task CreateFeedAsync_Should_Send_FeedData() {
        // Arrange
        var createRequest = new FeedCreateRequest {
            FeedUrl = "https://example.com/feed.xml",
            CategoryId = 1
        };
        var expectedFeed = TestDataFactory.CreateFeed(10, 1);

        _httpMock.SetupJsonResponse(expectedFeed, HttpStatusCode.Created, req =>
            req.Method == HttpMethod.Post && req.RequestUri!.AbsolutePath == "/v1/feeds");

        // Act
        var feed = await _client.PostAsync<Feed>("/v1/feeds", createRequest);

        // Assert
        feed.Should().NotBeNull();
        feed!.Id.Should().Be(10);

        // Verify request content
        var capturedRequest = await _httpMock.GetCapturedRequestBodyAsync<FeedCreateRequest>();
        capturedRequest.Should().NotBeNull();
        capturedRequest!.FeedUrl.Should().Be(createRequest.FeedUrl);
        capturedRequest.CategoryId.Should().Be(createRequest.CategoryId);
    }

    [Fact]
    public async Task UpdateFeedAsync_Should_Send_UpdateData() {
        // Arrange
        var feedId = 10;
        var updateRequest = new FeedUpdateRequest {
            FeedUrl = "https://updated.com/feed.xml",
            Title = "Updated Feed"
        };
        var expectedFeed = TestDataFactory.CreateFeed(feedId, 1);
        expectedFeed.Title = "Updated Feed";

        _httpMock.SetupJsonResponse(expectedFeed, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/feeds/{feedId}");

        // Act
        var feed = await _client.PutAsync<Feed>($"/v1/feeds/{feedId}", updateRequest);

        // Assert
        feed.Should().NotBeNull();
        feed!.Title.Should().Be("Updated Feed");

        // Verify request content
        var capturedRequest = await _httpMock.GetCapturedRequestBodyAsync<FeedUpdateRequest>();
        capturedRequest.Should().NotBeNull();
        capturedRequest!.FeedUrl.Should().Be(updateRequest.FeedUrl);
        capturedRequest.Title.Should().Be(updateRequest.Title);
    }

    [Fact]
    public async Task DeleteFeedAsync_Should_Send_DeleteRequest() {
        // Arrange
        var feedId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Delete && req.RequestUri!.AbsolutePath == $"/v1/feeds/{feedId}");

        // Act
        await _client.DeleteAsync($"/v1/feeds/{feedId}");

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests[0];
        request.Method.Should().Be(HttpMethod.Delete);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/feeds/{feedId}");
    }

    [Fact]
    public async Task RefreshFeedAsync_Should_Send_PutRequest() {
        // Arrange
        var feedId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/feeds/{feedId}/refresh");

        // Act
        await _client.PutAsync<object>($"/v1/feeds/{feedId}/refresh", null);

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests[0];
        request.Method.Should().Be(HttpMethod.Put);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/feeds/{feedId}/refresh");
    }

    [Fact]
    public async Task MarkFeedAsReadAsync_Should_Send_PutRequest() {
        // Arrange
        var feedId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/feeds/{feedId}/mark-all-as-read");

        // Act
        await _client.PutAsync<object>($"/v1/feeds/{feedId}/mark-all-as-read", null);

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests[0];
        request.Method.Should().Be(HttpMethod.Put);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/feeds/{feedId}/mark-all-as-read");
    }

    [Fact]
    public async Task GetFeedIconAsync_Should_Return_Icon() {
        // Arrange
        var feedId = 5;
        var expectedIcon = new FeedIcon {
            Id = 1,
            Data = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==",
            MimeType = "image/png"
        };

        _httpMock.SetupJsonResponse(expectedIcon, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/feeds/{feedId}/icon");

        // Act
        var icon = await _client.GetAsync<FeedIcon>($"/v1/feeds/{feedId}/icon");

        // Assert
        icon.Should().NotBeNull();
        icon!.Id.Should().Be(1);
        icon.MimeType.Should().Be("image/png");
        icon.Data.Should().StartWith("data:image/png;base64,");
    }

    [Fact]
    public async Task GetFeedEntriesAsync_Should_Return_EntryList() {
        // Arrange
        var feedId = 5;
        var expectedEntries = new[] {
            TestDataFactory.CreateEntry(1, feedId),
            TestDataFactory.CreateEntry(2, feedId)
        };

        _httpMock.SetupJsonResponse(expectedEntries, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/feeds/{feedId}/entries");

        // Act
        var entries = await _client.GetAsync<Entry[]>($"/v1/feeds/{feedId}/entries");

        // Assert
        entries.Should().NotBeNull();
        entries!.Should().HaveCount(2);
        entries[0].FeedId.Should().Be(feedId);
        entries[1].FeedId.Should().Be(feedId);
    }
}
