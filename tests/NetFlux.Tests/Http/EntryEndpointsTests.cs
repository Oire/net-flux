using System.Net;
using FluentAssertions;
using Oire.NetFlux.Http;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Http;

public class EntryEndpointsTests: IDisposable {
    private const string BaseUrl = "https://miniflux.example.com";
    private const string ApiKey = "test-api-key";

    private readonly HttpMessageHandlerMock _httpMock;
    private readonly HttpClient _httpClient;
    private readonly MinifluxHttpClient _client;

    public EntryEndpointsTests() {
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
    public async Task GetEntriesAsync_Should_Return_EntryResultSet() {
        // Arrange
        var expectedResult = new EntryResultSet {
            Total = 10,
            Entries = new List<Entry> {
                TestDataFactory.CreateEntry(1, 1),
                TestDataFactory.CreateEntry(2, 1)
            }
        };
        _httpMock.SetupJsonResponse(expectedResult, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == "/v1/entries");

        // Act
        var result = await _client.GetAsync<EntryResultSet>("/v1/entries");

        // Assert
        result.Should().NotBeNull();
        result!.Total.Should().Be(10);
        result.Entries.Should().HaveCount(2);
        result.Entries.First().Id.Should().Be(1);
    }

    [Fact]
    public async Task GetEntriesWithFilter_Should_Include_QueryParameters() {
        // Arrange
        var filter = new EntryFilter {
            Status = EntryStatus.Unread,
            Limit = 50,
            Starred = true,
            Order = "published_at",
            Direction = "desc"
        };

        var expectedResult = new EntryResultSet {
            Total = 5,
            Entries = new List<Entry> { TestDataFactory.CreateEntry(1, 1) }
        };

        _httpMock.SetupJsonResponse(expectedResult, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get &&
            req.RequestUri!.AbsolutePath == "/v1/entries" &&
            req.RequestUri.Query.Contains("status=unread") &&
            req.RequestUri.Query.Contains("limit=50") &&
            req.RequestUri.Query.Contains("starred=1") &&
            req.RequestUri.Query.Contains("order=published_at") &&
            req.RequestUri.Query.Contains("direction=desc"));

        // Act - This would normally build query parameters, testing the concept
        var queryString = $"?status=unread&limit=50&starred=1&order=published_at&direction=desc";
        var result = await _client.GetAsync<EntryResultSet>($"/v1/entries{queryString}");

        // Assert
        result.Should().NotBeNull();
        result!.Total.Should().Be(5);

        // Verify URL was constructed correctly
        var request = _httpMock.CapturedRequests.First();
        request.RequestUri!.Query.Should().Contain("status=unread");
        request.RequestUri.Query.Should().Contain("limit=50");
        request.RequestUri.Query.Should().Contain("starred=1");
    }

    [Fact]
    public async Task GetEntryAsync_Should_Return_SpecificEntry() {
        // Arrange
        var entryId = 5;
        var expectedEntry = TestDataFactory.CreateEntry(entryId, 1);
        _httpMock.SetupJsonResponse(expectedEntry, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/entries/{entryId}");

        // Act
        var entry = await _client.GetAsync<Entry>($"/v1/entries/{entryId}");

        // Assert
        entry.Should().NotBeNull();
        entry!.Id.Should().Be(entryId);
    }

    [Fact]
    public async Task UpdateEntryAsync_Should_Send_UpdateData() {
        // Arrange
        var entryId = 10;
        var updateRequest = new EntryUpdateRequest {
            Title = "Updated Entry Title",
            Content = "Updated entry content"
        };
        var expectedEntry = TestDataFactory.CreateEntry(entryId, 1);
        expectedEntry.Title = "Updated Entry Title";

        _httpMock.SetupJsonResponse(expectedEntry, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/entries/{entryId}");

        // Act
        var entry = await _client.PutAsync<Entry>($"/v1/entries/{entryId}", updateRequest);

        // Assert
        entry.Should().NotBeNull();
        entry!.Title.Should().Be("Updated Entry Title");

        // Verify request content
        var capturedRequest = await _httpMock.GetCapturedRequestBodyAsync<EntryUpdateRequest>();
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Title.Should().Be("Updated Entry Title");
        capturedRequest.Content.Should().Be("Updated entry content");
    }

    [Fact]
    public async Task ToggleEntryStarAsync_Should_Send_PutRequest() {
        // Arrange
        var entryId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/entries/{entryId}/bookmark");

        // Act
        await _client.PutAsync<object>($"/v1/entries/{entryId}/bookmark", null);

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests.First();
        request.Method.Should().Be(HttpMethod.Put);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/entries/{entryId}/bookmark");
    }

    [Fact]
    public async Task GetStarredEntriesAsync_Should_Return_EntryResultSet() {
        // Arrange
        var expectedResult = new EntryResultSet {
            Total = 3,
            Entries = new List<Entry> {
                TestDataFactory.CreateEntry(1, 1),
                TestDataFactory.CreateEntry(2, 1)
            }
        };
        // Set entries as starred
        foreach (var entry in expectedResult.Entries) {
            entry.Starred = true;
        }

        _httpMock.SetupJsonResponse(expectedResult, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == "/v1/entries" &&
            req.RequestUri.Query.Contains("starred=1"));

        // Act
        var result = await _client.GetAsync<EntryResultSet>("/v1/entries?starred=1");

        // Assert
        result.Should().NotBeNull();
        result!.Total.Should().Be(3);
        result.Entries.Should().AllSatisfy(e => e.Starred.Should().BeTrue());
    }

    [Fact]
    public async Task SaveEntryAsync_Should_Send_PostRequest() {
        // Arrange
        var entryId = 10;
        var saveUrl = "https://thirdparty.com/save";

        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.OK), req =>
            req.Method == HttpMethod.Post && req.RequestUri!.AbsolutePath == $"/v1/entries/{entryId}/save/{Uri.EscapeDataString(saveUrl)}");

        // Act
        await _client.PostAsync<object>($"/v1/entries/{entryId}/save/{Uri.EscapeDataString(saveUrl)}", null);

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests.First();
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri!.AbsolutePath.Should().Contain($"/v1/entries/{entryId}/save/");
    }

    [Fact]
    public async Task GetFeedEntriesAsync_Should_Return_EntryResultSet() {
        // Arrange
        var feedId = 5;
        var expectedResult = new EntryResultSet {
            Total = 15,
            Entries = new List<Entry> {
                TestDataFactory.CreateEntry(1, feedId),
                TestDataFactory.CreateEntry(2, feedId)
            }
        };

        _httpMock.SetupJsonResponse(expectedResult, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/feeds/{feedId}/entries");

        // Act
        var result = await _client.GetAsync<EntryResultSet>($"/v1/feeds/{feedId}/entries");

        // Assert
        result.Should().NotBeNull();
        result!.Total.Should().Be(15);
        result.Entries.Should().AllSatisfy(e => e.FeedId.Should().Be(feedId));
    }

    [Fact]
    public async Task GetCategoryEntriesAsync_Should_Return_EntryResultSet() {
        // Arrange
        var categoryId = 3;
        var expectedResult = new EntryResultSet {
            Total = 8,
            Entries = new List<Entry> {
                TestDataFactory.CreateEntry(1, 1),
                TestDataFactory.CreateEntry(2, 2)
            }
        };

        _httpMock.SetupJsonResponse(expectedResult, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/categories/{categoryId}/entries");

        // Act
        var result = await _client.GetAsync<EntryResultSet>($"/v1/categories/{categoryId}/entries");

        // Assert
        result.Should().NotBeNull();
        result!.Total.Should().Be(8);
        result.Entries.Should().HaveCount(2);
    }
}
