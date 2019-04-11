using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class UserDefinedBuilder
    {
        IServiceCollection Services { get; }
        public UserDefinedBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}

