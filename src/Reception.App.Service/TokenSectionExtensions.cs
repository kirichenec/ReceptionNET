using Reception.Model.Interface;

namespace Reception.App.Service
{
    public static class TokenSectionExtensions
    {

        public static TokenSection CreateTokenSection(this IToken value)
        {
            return new TokenSection
            {
                UserId = value?.UserId ?? 0,
                Token = value?.Value
            };
        }

        public static IToken ToIToken(this TokenSection value)
        {
            return new TokenSettings
            {
                UserId = value.UserId,
                Value = value.Token
            };
        }
    }
}