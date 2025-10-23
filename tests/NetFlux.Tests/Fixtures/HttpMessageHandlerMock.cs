using System.Net;
using System.Text;
using System.Text.Json;

namespace Oire.NetFlux.Tests.Fixtures;

public class HttpMessageHandlerMock : HttpMessageHandler
{
    private readonly Queue<(Func<HttpRequestMessage, bool> matcher, HttpResponseMessage response)> _responses = new();
    private readonly List<HttpRequestMessage> _capturedRequests = new();

    public IReadOnlyList<HttpRequestMessage> CapturedRequests => _capturedRequests;

    public void SetupResponse(HttpResponseMessage response, Func<HttpRequestMessage, bool>? matcher = null)
    {
        _responses.Enqueue((matcher ?? (_ => true), response));
    }

    public void SetupJsonResponse<T>(T content, HttpStatusCode statusCode = HttpStatusCode.OK, Func<HttpRequestMessage, bool>? matcher = null)
    {
        var json = JsonSerializer.Serialize(content, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });
        
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        
        SetupResponse(response, matcher);
    }

    public void SetupStringResponse(string content, HttpStatusCode statusCode = HttpStatusCode.OK, string mediaType = "text/plain", Func<HttpRequestMessage, bool>? matcher = null)
    {
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, mediaType)
        };
        
        SetupResponse(response, matcher);
    }

    public void SetupByteResponse(byte[] content, HttpStatusCode statusCode = HttpStatusCode.OK, string mediaType = "application/octet-stream", Func<HttpRequestMessage, bool>? matcher = null)
    {
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new ByteArrayContent(content)
        };
        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
        
        SetupResponse(response, matcher);
    }

    public void SetupErrorResponse(HttpStatusCode statusCode, string? reasonPhrase = null, Func<HttpRequestMessage, bool>? matcher = null)
    {
        var response = new HttpResponseMessage(statusCode)
        {
            ReasonPhrase = reasonPhrase,
            Content = new StringContent(string.Empty)
        };
        
        SetupResponse(response, matcher);
    }

    public void VerifyRequest(Func<HttpRequestMessage, bool> predicate)
    {
        if (!_capturedRequests.Any(predicate))
        {
            throw new InvalidOperationException("No matching request was found.");
        }
    }

    public void VerifyNoOutstandingRequests()
    {
        if (_responses.Any())
        {
            throw new InvalidOperationException($"There are {_responses.Count} unused response setups.");
        }
    }

    public async Task<T?> GetCapturedRequestBodyAsync<T>(int index = 0)
    {
        if (index >= _capturedRequests.Count)
            return default;
            
        var request = _capturedRequests[index];
        if (request.Content == null)
            return default;
            
        var json = await request.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _capturedRequests.Add(request);

        while (_responses.Count > 0)
        {
            var (matcher, response) = _responses.Dequeue();
            if (matcher(request))
            {
                return await Task.FromResult(response);
            }
        }

        throw new InvalidOperationException($"No response setup found for {request.Method} {request.RequestUri}");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (var request in _capturedRequests)
            {
                request.Dispose();
            }
            
            while (_responses.Count > 0)
            {
                var (_, response) = _responses.Dequeue();
                response.Dispose();
            }
        }
        
        base.Dispose(disposing);
    }
}