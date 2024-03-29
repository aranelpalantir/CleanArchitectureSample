﻿namespace CleanArchSample.Infrastructure.Security
{
    internal sealed class TokenSettings
    {
        public required string Audience { get; set; }
        public required string Issuer { get; set; }
        public required string Secret { get; set; }
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
}
