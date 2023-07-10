namespace Reception.App.Localization.Test
{
    public class LocalizeExtensionTests
    {
        [Theory]
        [InlineData("testKey")]
        public void LocalizeExtension_Initialization_CheckKey(string testKey)
        {
            // Arrange
            var localizeExtension = new LocalizeExtension(testKey);

            // Assert
            Assert.Equal(testKey, localizeExtension.Key);
        }
    }
}
