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
   git clone https://github.com/your-username/cryptographical-key-lifecycle-management.git
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
   Open your browser and navigate to `http://localhost:5000/swagger` to access the Swagger UI.

## Usage

Here is a brief overview of how to use the key lifecycle management functions:

### Create a Key

```csharp
string CreateKey(string keyType, int keySize);
```

### Encrypt Data

```csharp
string Encrypt(string data, string objectType);
```

### Decrypt Data

```csharp
string Decrypt(string data, string keyId);
```

### Activate a Key

```csharp
void ActivateKey(string objectId);
```

### Deactivate a Key

```csharp
void DeactivateKey(string objectId);
```

### Destroy a Key

```csharp
void DestroyKey(string objectId);
```

### Revoke a Key

```csharp
void RevokeKey(string objectId);
```

### Archive a Key

```csharp
void ArchiveKey(string objectId);
```

### Recover a Key

```csharp
void RecoverKey(string objectId);
```

### Get Key Information

```csharp
byte[] GetKeyInfo(string objectId);
```

## Contributing

We welcome contributions to enhance the functionality and capabilities of this API. Please fork the repository and submit pull requests with your improvements.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## Contact

For any questions or support, please open an issue on the repository or contact the maintainer at [your-email@example.com].

---

Thank you for using the Cryptographical Key Lifecycle Management API. We hope it meets your security and cryptographic needs.