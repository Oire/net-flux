using System.Text.Json;
using FluentAssertions;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Models;

public class CategoryTests
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Fact]
    public void Should_Serialize_Category_To_Json()
    {
        // Arrange
        var category = TestDataFactory.CreateCategory();

        // Act
        var json = JsonSerializer.Serialize(category, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<Category>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(category.Id);
        deserialized.Title.Should().Be(category.Title);
        deserialized.UserId.Should().Be(category.UserId);
        deserialized.HideGlobally.Should().Be(category.HideGlobally);
        deserialized.FeedCount.Should().Be(category.FeedCount);
        deserialized.TotalUnread.Should().Be(category.TotalUnread);
    }

    [Fact]
    public void Should_Deserialize_Category_From_Json()
    {
        // Arrange
        var json = """
        {
            "id": 42,
            "title": "Programming",
            "user_id": 5,
            "hide_globally": true,
            "feed_count": 10,
            "total_unread": 25
        }
        """;

        // Act
        var category = JsonSerializer.Deserialize<Category>(json, _jsonOptions);

        // Assert
        category.Should().NotBeNull();
        category!.Id.Should().Be(42);
        category.Title.Should().Be("Programming");
        category.UserId.Should().Be(5);
        category.HideGlobally.Should().BeTrue();
        category.FeedCount.Should().Be(10);
        category.TotalUnread.Should().Be(25);
    }

    [Fact]
    public void Should_Format_ToString_Correctly()
    {
        // Arrange
        var category = new Category { Id = 10, Title = "Tech News" };

        // Act
        var result = category.ToString();

        // Assert
        result.Should().Be("#10 Tech News");
    }

    [Fact]
    public void CategoryCreateRequest_Should_Serialize_Correctly()
    {
        // Arrange
        var request = new CategoryCreateRequest
        {
            Title = "New Category",
            HideGlobally = true
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<CategoryCreateRequest>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Title.Should().Be(request.Title);
        deserialized.HideGlobally.Should().Be(request.HideGlobally);
    }

    [Fact]
    public void CategoryUpdateRequest_Should_Handle_Nullable_Properties()
    {
        // Arrange
        var request = new CategoryUpdateRequest
        {
            Title = "Updated Title",
            HideGlobally = null
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);

        // Assert
        json.Should().Contain("\"title\":\"Updated Title\"");
        json.Should().NotContain("\"hide_globally\"");
    }
}