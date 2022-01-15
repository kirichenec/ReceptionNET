using Reception.App.Model.Auth;

namespace Reception.App.Service
{
    public static class TokenSectionExtensions
    {
        public static TokenSection CreateTokenSection(this Token value)
        {
            return new TokenSection
            {
                UserId = value?.UserId ?? 0,
                Token = value?.Value
            };
        }

        public static Token ToToken(this TokenSection value)
        {
            return new Token
            {
                UserId = value.UserId,
                Value = value.Token
            };
        }
    }
}