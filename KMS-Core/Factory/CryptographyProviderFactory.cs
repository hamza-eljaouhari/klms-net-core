using System;
using KMS.Contracts;
using KMS.CryptographyProviders;
using Microsoft.Extensions.DependencyInjection;

namespace KMS.Factory
{
    public class CryptographyProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CryptographyProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IKeyLifeCycleManager GetCryptographyProvider(string algorithm)
        {
            switch (algorithm.ToUpper())
            {
                case "AES":
                    return _serviceProvider.GetRequiredService<AESProvider>();
                case "RSA":
                    return _serviceProvider.GetRequiredService<RSAProvider>();
                // Add cases for other algorithms
                default:
                    throw new ArgumentException("Unsupported algorithm.");
            }
        }
    }
}