// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Oire.NetFlux.Exceptions;
using Oire.NetFlux.Models;

namespace Oire.NetFlux.Http;

internal class MinifluxHttpClient: IDisposable {
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string? _username;
    private readonly string? _password;
    private readonly string? _apiKey;
    private readonly JsonSerializerOptions _jsonOptions;
    private bool _disposed;

    private const string UserAgent = "NetFlux Client Library";
    private const int DefaultTimeoutSeconds = 80;

    public MinifluxHttpClient(string endpoint, string? username = null, string? password = null, string? apiKey = null) {
        if (string.IsNullOrWhiteSpace(endpoint)) {
            throw new MinifluxConfigurationException("Endpoint cannot be empty.");
        }

        _baseUrl = endpoint.TrimEnd('/').TrimEnd("/v1".ToCharArray());
        _username = username;
        _password = password;
        _apiKey = apiKey;

        _httpClient = new HttpClient {
            Timeout = TimeSpan.FromSeconds(DefaultTimeoutSeconds)
        };

        _jsonOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        ConfigureHeaders();
    }

    private void ConfigureHeaders() {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (!string.IsNullOrEmpty(_apiKey)) {
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _apiKey);
        } else if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password)) {
            var authBytes = Encoding.UTF8.GetBytes($"{_username}:{_password}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
        }
    }

    public async Task<T?> GetAsync<T>(string path, CancellationToken cancellationToken = default) {
        using var response = await SendRequestAsync(HttpMethod.Get, path, null, cancellationToken);

        return await DeserializeResponseAsync<T>(response, cancellationToken);
    }

    public async Task<T?> PostAsync<T>(string path, object? data, CancellationToken cancellationToken = default) {
        using var response = await SendRequestAsync(HttpMethod.Post, path, data, cancellationToken);

        return await DeserializeResponseAsync<T>(response, cancellationToken);
    }

    public async Task<T?> PutAsync<T>(string path, object? data, CancellationToken cancellationToken = default) {
        using var response = await SendRequestAsync(HttpMethod.Put, path, data, cancellationToken);

        return await DeserializeResponseAsync<T>(response, cancellationToken);
    }

    public async Task DeleteAsync(string path, CancellationToken cancellationToken = default) {
        using var response = await SendRequestAsync(HttpMethod.Delete, path, null, cancellationToken);
        // DELETE operations typically don't return content
    }

    public async Task<byte[]> GetBytesAsync(string path, CancellationToken cancellationToken = default) {
        using var response = await SendRequestAsync(HttpMethod.Get, path, null, cancellationToken);

        return await response.Content.ReadAsByteArrayAsync(cancellationToken);
    }

    public async Task PostFileAsync(string path, Stream fileStream, CancellationToken cancellationToken = default) {
        var url = $"{_baseUrl}{path}";
        using var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        using var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
        using var response = await _httpClient.SendAsync(request, cancellationToken);
        await HandleResponseAsync(response, cancellationToken);
    }

    private async Task<HttpResponseMessage> SendRequestAsync(
        HttpMethod method,
        string path,
        object? data,
        CancellationToken cancellationToken
    ) {
        var url = $"{_baseUrl}{path}";
        using var request = new HttpRequestMessage(method, url);

        if (data is not null) {
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        await HandleResponseAsync(response, cancellationToken);

        return response;
    }

    private async Task HandleResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken) {
        if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent) {
            return;
        }

        var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var errorMessage = TryParseErrorMessage(errorContent);

        throw response.StatusCode switch {
            HttpStatusCode.Unauthorized => new MinifluxAuthenticationException(errorMessage ?? "Unauthorized"),
            HttpStatusCode.Forbidden => new MinifluxForbiddenException(errorMessage ?? "Forbidden"),
            HttpStatusCode.NotFound => new MinifluxNotFoundException(errorMessage ?? "Not found"),
            HttpStatusCode.BadRequest => new MinifluxBadRequestException(errorMessage ?? "Bad request"),
            HttpStatusCode.InternalServerError => new MinifluxServerException(errorMessage ?? "Internal server error"),
            _ => new MinifluxException($"Request failed with status {response.StatusCode}: {errorMessage}")
        };
    }

    private string? TryParseErrorMessage(string content) {
        if (string.IsNullOrWhiteSpace(content)) {
            return null;
        }

        try {
            using var doc = JsonDocument.Parse(content);

            if (doc.RootElement.TryGetProperty("error_message", out var errorMessage)) {
                return errorMessage.GetString();
            }
        } catch {
            // If parsing fails, return the raw content
            return content;
        }

        return content;
    }

    private async Task<T?> DeserializeResponseAsync<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken
    ) {
        if (response.StatusCode == HttpStatusCode.NoContent) {
            return default;
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(content)) {
            return default;
        }

        return JsonSerializer.Deserialize<T>(content, _jsonOptions);
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposed) {
            if (disposing) {
                _httpClient?.Dispose();
            }
            _disposed = true;
        }
    }
}
