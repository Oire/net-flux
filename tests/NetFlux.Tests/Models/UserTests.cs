using System.Text.Json;
using FluentAssertions;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Models;

public class UserTests
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Fact]
    public void Should_Serialize_User_To_Json()
    {
        // Arrange
        var user = TestDataFactory.CreateUser();

        // Act
        var json = JsonSerializer.Serialize(user, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<User>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(user.Id);
        deserialized.Username.Should().Be(user.Username);
        deserialized.IsAdmin.Should().Be(user.IsAdmin);
        deserialized.Theme.Should().Be(user.Theme);
        deserialized.Language.Should().Be(user.Language);
        deserialized.Timezone.Should().Be(user.Timezone);
        deserialized.EntrySortingDirection.Should().Be(user.EntrySortingDirection);
        deserialized.EntrySortingOrder.Should().Be(user.EntrySortingOrder);
        deserialized.EntriesPerPage.Should().Be(user.EntriesPerPage);
        deserialized.KeyboardShortcuts.Should().Be(user.KeyboardShortcuts);
        deserialized.ShowReadingTime.Should().Be(user.ShowReadingTime);
        deserialized.EntrySwipe.Should().Be(user.EntrySwipe);
        deserialized.GestureNav.Should().Be(user.GestureNav);
        deserialized.LastLoginAt.Should().Be(user.LastLoginAt);
        deserialized.DisplayMode.Should().Be(user.DisplayMode);
        deserialized.DefaultReadingSpeed.Should().Be(user.DefaultReadingSpeed);
        deserialized.CjkReadingSpeed.Should().Be(user.CjkReadingSpeed);
        deserialized.DefaultHomePage.Should().Be(user.DefaultHomePage);
        deserialized.CategoriesSortingOrder.Should().Be(user.CategoriesSortingOrder);
        deserialized.MarkReadOnView.Should().Be(user.MarkReadOnView);
        deserialized.MediaPlaybackRate.Should().Be(user.MediaPlaybackRate);
        deserialized.ExternalFontHosts.Should().Be(user.ExternalFontHosts);
        deserialized.AlwaysOpenExternalLinks.Should().Be(user.AlwaysOpenExternalLinks);
        deserialized.OpenExternalLinksInNewTab.Should().Be(user.OpenExternalLinksInNewTab);
    }

    [Fact]
    public void Should_Deserialize_User_From_Json()
    {
        // Arrange
        var json = """
        {
            "id": 123,
            "username": "john_doe",
            "is_admin": true,
            "theme": "dark_serif",
            "language": "fr_FR",
            "timezone": "Europe/Paris",
            "entry_sorting_direction": "asc",
            "entry_sorting_order": "created_at",
            "stylesheet": ".custom { color: red; }",
            "google_id": "google123",
            "openid_connect_id": "oidc456",
            "entries_per_page": 50,
            "keyboard_shortcuts": false,
            "show_reading_time": false,
            "entry_swipe": false,
            "gesture_nav": "swipe",
            "last_login_at": "2024-03-15T14:30:00Z",
            "display_mode": "fullscreen",
            "default_reading_speed": 300,
            "cjk_reading_speed": 600,
            "default_home_page": "starred",
            "categories_sorting_order": "count",
            "mark_read_on_view": true,
            "media_playback_rate": 1.5,
            "block_filter_entry_rules": "ads|spam",
            "keep_filter_entry_rules": "important",
            "external_font_hosts": "custom-fonts.com",
            "always_open_external_links": true,
            "open_external_links_in_new_tab": false
        }
        """;

        // Act
        var user = JsonSerializer.Deserialize<User>(json, _jsonOptions);

        // Assert
        user.Should().NotBeNull();
        user!.Id.Should().Be(123);
        user.Username.Should().Be("john_doe");
        user.IsAdmin.Should().BeTrue();
        user.Theme.Should().Be("dark_serif");
        user.Language.Should().Be("fr_FR");
        user.Timezone.Should().Be("Europe/Paris");
        user.EntrySortingDirection.Should().Be("asc");
        user.EntrySortingOrder.Should().Be("created_at");
        user.Stylesheet.Should().Be(".custom { color: red; }");
        user.GoogleId.Should().Be("google123");
        user.OpenIdConnectId.Should().Be("oidc456");
        user.EntriesPerPage.Should().Be(50);
        user.KeyboardShortcuts.Should().BeFalse();
        user.ShowReadingTime.Should().BeFalse();
        user.EntrySwipe.Should().BeFalse();
        user.GestureNav.Should().Be("swipe");
        user.LastLoginAt.Should().Be(new DateTime(2024, 3, 15, 14, 30, 0, DateTimeKind.Utc));
        user.DisplayMode.Should().Be("fullscreen");
        user.DefaultReadingSpeed.Should().Be(300);
        user.CjkReadingSpeed.Should().Be(600);
        user.DefaultHomePage.Should().Be("starred");
        user.CategoriesSortingOrder.Should().Be("count");
        user.MarkReadOnView.Should().BeTrue();
        user.MediaPlaybackRate.Should().Be(1.5);
        user.BlockFilterEntryRules.Should().Be("ads|spam");
        user.KeepFilterEntryRules.Should().Be("important");
        user.ExternalFontHosts.Should().Be("custom-fonts.com");
        user.AlwaysOpenExternalLinks.Should().BeTrue();
        user.OpenExternalLinksInNewTab.Should().BeFalse();
    }

    [Fact]
    public void Should_Format_ToString_Correctly()
    {
        // Arrange
        var user = new User { Id = 42, Username = "testuser", IsAdmin = false };

        // Act
        var result = user.ToString();

        // Assert
        result.Should().Be("#42 - testuser (admin=False)");
    }

    [Fact]
    public void UserCreateRequest_Should_Serialize_Correctly()
    {
        // Arrange
        var request = new UserCreateRequest
        {
            Username = "newuser",
            Password = "secure123",
            IsAdmin = true,
            GoogleId = "google789",
            OpenIdConnectId = "oidc101"
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<UserCreateRequest>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Username.Should().Be(request.Username);
        deserialized.Password.Should().Be(request.Password);
        deserialized.IsAdmin.Should().Be(request.IsAdmin);
        deserialized.GoogleId.Should().Be(request.GoogleId);
        deserialized.OpenIdConnectId.Should().Be(request.OpenIdConnectId);
    }

    [Fact]
    public void UserUpdateRequest_Should_Handle_Nullable_Properties()
    {
        // Arrange
        var request = new UserUpdateRequest
        {
            Username = "updateduser",
            IsAdmin = false,
            Theme = null,
            EntriesPerPage = 75
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        
        // Assert
        json.Should().Contain("\"username\":\"updateduser\"");
        json.Should().Contain("\"is_admin\":false");
        json.Should().NotContain("\"theme\"");
        json.Should().Contain("\"entries_per_page\":75");
    }
}