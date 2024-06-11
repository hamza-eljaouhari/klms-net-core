using Microsoft.AspNetCore.Mvc;
using KMS.Contracts;
using KMS.Factory;
using KMS.CryptographyProviders;
using System;

namespace KMS.Controllers
{
    /// <summary>
    /// Deletes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ApiController]
    [Route("api/[controller]")]
    public class CryptographyController : ControllerBase
    {
        private readonly CryptographyProviderFactory _factory;
        private readonly KeyStoreManager _keyStoreManager;

        public CryptographyController(CryptographyProviderFactory factory, KeyStoreManager keyStoreManager)
        {
            _factory = factory;
            _keyStoreManager = keyStoreManager;
        }

        [HttpGet("list")]
        public IActionResult GetAllKeys()
        {
            var keys = _keyStoreManager.GetAllKeys();
            return Ok(keys);
        }

        public class CreateKeyRequest
        {
            public string Algorithm { get; set; }
            public int KeySize { get; set; }
        }

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

        public class EncryptRequest
        {
            public string Algorithm { get; set; }
            public string KeyId { get; set; }
            public string Data { get; set; } // Change Data property to string
        }

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

        public class DecryptRequest
        {
            public string Algorithm { get; set; }
            public string KeyId { get; set; }
            public string Data { get; set; } // Change Data property to string
        }

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

        public class KeyActionRequest
        {
            public string Algorithm { get; set; }
            public string KeyId { get; set; }
        }

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
    }
}