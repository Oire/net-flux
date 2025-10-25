# NetFlux Samples

This directory contains sample applications demonstrating how to use the NetFlux library.

## Available Samples

### 1. NetFlux.Samples.Console

A comprehensive console application that demonstrates most NetFlux features in an interactive menu-driven interface.

**Features demonstrated:**
- Both authentication methods (API key and basic auth)
- Feed management (list, add, refresh, delete)
- Category management (list with counters, create, rename)
- Entry browsing and filtering
- Search functionality
- OPML export
- Feed discovery
- Server version information

**To run:**
```bash
cd samples/NetFlux.Samples.Console
dotnet run
```

The application will prompt you for:
1. Your Miniflux instance URL
2. Authentication method (API key or username/password)
3. Credentials

Then you'll see an interactive menu with various options to explore the API functionality.

## Adding Your Own Samples

Feel free to contribute additional samples demonstrating specific use cases:

- Web application integration
- Background service for monitoring feeds
- Migration tools from other RSS readers
- Custom feed processing workflows
- Integration with notification systems

## Tips for Running Samples

1. **Set up a test Miniflux instance**: It's recommended to use a test instance of Miniflux to avoid affecting your production data.

2. **API Key Authentication**: For production use, API keys are recommended over basic authentication. You can create an API key in Miniflux under Settings → API Keys.

3. **Error Handling**: The samples include basic error handling, but in production applications you should implement more comprehensive error handling and retry logic.

4. **Configuration**: In real applications, store your Miniflux URL and credentials in configuration files or environment variables rather than prompting for them.

## Sample Configuration

For production applications, consider using configuration like:

```json
{
  "Miniflux": {
    "BaseUrl": "https://miniflux.example.com",
    "ApiKey": "your-api-key-here"
  }
}
```

Or environment variables:
```bash
MINIFLUX_URL=https://miniflux.example.com
MINIFLUX_API_KEY=your-api-key-here
```