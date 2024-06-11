# Cryptographical Key Lifecycle Management API

## Overview

Welcome to the Cryptographical Key Lifecycle Management API repository. This API, built with .NET Core 8.0, is designed to manage the lifecycle of cryptographic keys using RSA and AES algorithms. The API supports key creation, activation, deactivation, destruction, revocation, archiving, and recovery.

## Features

- **Algorithms Supported**:
  - AES: 128, 192, 256 bits
  - RSA: 1024, 2048, 3072, 4096 bits
- **Key Lifecycle Actions**:
  - **Create Key**: Generates a new cryptographic key.
  - **Encrypt**: Encrypts data using the specified cryptographic key.
  - **Decrypt**: Decrypts data using the specified cryptographic key.
  - **Activate Key**: Activates a cryptographic key for use.
  - **Deactivate Key**: Deactivates a cryptographic key, making it unavailable.
  - **Destroy Key**: Marks a cryptographic key for destruction.
  - **Revoke Key**: Revokes a cryptographic key, typically used in case of compromise.
  - **Archive Key**: Archives a cryptographic key, taking it out of operational use.
  - **Recover Key**: Recovers an archived cryptographic key.
  - **Get Key Info**: Retrieves information about a cryptographic key.

## Architecture

- **Factory Design Pattern**: Utilized for creating cryptographic key objects based on the specified algorithm and key size.
- **Dependency Injection**: Ensures loose coupling and easier testing of components.
- **Singleton Pattern**: Implements an online ephemeral store for cryptographic keys to ensure only one instance of the store exists during runtime.

## Swagger Documentation

This API includes integrated Swagger documentation to provide an interactive interface for exploring and testing the API endpoints. You can access the Swagger UI at `/swagger`.

## Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/hamza-eljaouhari/klms-net-core.git
   cd cryptographical-key-lifecycle-management
   ```

2. **Build the Project**:
   ```bash
   dotnet build
   ```

3. **Run the Project**:
   ```bash
   dotnet run
   ```

4. **Access the API**:
   Open your browser and navigate to `http://localhost:YOUR_OWN_PORT/swagger` to access the Swagger UI.

## Usage

Here is a brief overview of how to use the key lifecycle management functions:

### Create a Key

```csharp
[HttpPost("create")]
public IActionResult CreateKey([FromBody] CreateKeyRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        var keyId = provider.CreateKey(request.Algorithm, request.KeySize);
        return Ok(keyId);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Encrypt Data

```csharp
[HttpPost("encrypt")]
public IActionResult Encrypt([FromBody] EncryptRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        var encryptedData = provider.Encrypt(request.Data, request.KeyId);
        return Ok(encryptedData);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Decrypt Data

```csharp
[HttpPost("decrypt")]
public IActionResult Decrypt([FromBody] DecryptRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        var decryptedData = provider.Decrypt(request.Data, request.KeyId);
        return Ok(decryptedData);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Activate a Key

```csharp
[HttpPost("activate")]
public IActionResult ActivateKey([FromBody] KeyActionRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        provider.ActivateKey(request.KeyId);
        return Ok($"Key {request.KeyId} activated.");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Deactivate a Key

```csharp
[HttpPost("deactivate")]
public IActionResult DeactivateKey([FromBody] KeyActionRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        provider.DeactivateKey(request.KeyId);
        return Ok($"Key {request.KeyId} deactivated.");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Destroy a Key

```csharp
[HttpPost("destroy")]
public IActionResult DestroyKey([FromBody] KeyActionRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        provider.DestroyKey(request.KeyId);
        return Ok($"Key {request.KeyId} destroyed.");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Revoke a Key

```csharp
[HttpPost("revoke")]
public IActionResult RevokeKey([FromBody] KeyActionRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        provider.RevokeKey(request.KeyId);
        return Ok($"Key {request.KeyId} revoked.");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Archive a Key

```csharp
[HttpPost("archive")]
public IActionResult ArchiveKey([FromBody] KeyActionRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        provider.ArchiveKey(request.KeyId);
        return Ok($"Key {request.KeyId} archived.");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Recover a Key

```csharp
[HttpPost("recover")]
public IActionResult RecoverKey([FromBody] KeyActionRequest request)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(request.Algorithm);
        provider.RecoverKey(request.KeyId);
        return Ok($"Key {request.KeyId} recovered.");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Get Key Information

```csharp
[HttpGet("info")]
public IActionResult GetKeyInfo(string algorithm, string keyId)
{
    try
    {
        var provider = _factory.GetCryptographyProvider(algorithm);
        var keyInfo = provider.GetKeyInfo(keyId);
        return Ok(keyInfo);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

## React Client

Please find a minimal React client on this URL [https://github.com/hamza-eljaouhari/kms-react-gui]

## Contributing

We welcome contributions to enhance the functionality and capabilities of this API. Please fork the repository and submit pull requests with your improvements.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## Contact

For any questions or support, please open an issue on the repository or contact the maintainer at [hamza.eljaouhari.1995@gmail.com].

---

Thank you for using the Cryptographical Key Lifecycle Management API. We hope it meets your security and cryptographic needs.