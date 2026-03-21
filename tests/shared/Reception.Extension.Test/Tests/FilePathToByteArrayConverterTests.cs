using Reception.Extension.Converters;
using Reception.Extension.Test.Fixture;

namespace Reception.Extension.Test.Tests;

public class FilePathToByteArrayConverterTests
{
    [Fact]
    public async Task FilePathToByteArrayConverter_GetFileBytesByPathAsync_FilePathIsNull_ShouldThrow()
    {
        // arrange
        string filePath = null;

        // act
        var act = async () => await filePath.GetFileBytesByPathAsync();

        // assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task FilePathToByteArrayConverter_GetFileBytesByPathAsync_FilePathIsEmptyOrWhiteSpace_ShouldThrow(string filePath)
    {
        // act
        var act = async () => await filePath.GetFileBytesByPathAsync();

        // assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task FilePathToByteArrayConverter_GetFileBytesByPathAsync_FilePathOfNonExistedFile_ShouldThrow()
    {
        // arrange
        string filePath = "Name.jpgxc";

        // act
        var act = async () => await filePath.GetFileBytesByPathAsync();

        // assert
        await act.Should().ThrowAsync<FileNotFoundException>();
    }

    [Fact]
    public async Task FilePathToByteArrayConverter_GetFileBytesByPathAsync_FilePathOfNonExistedFolder_ShouldThrow()
    {
        // arrange
        string filePath = "randomUnsupported\\Name";

        // act
        var act = async () => await filePath.GetFileBytesByPathAsync();

        // assert
        await act.Should().ThrowAsync<DirectoryNotFoundException>();
    }

    [Fact]
    public async Task FilePathToByteArrayConverter_GetFileBytesByPathAsync_IncorrectFilePath_ShouldThrow()
    {
        // arrange
        string filePath = "fileName .jpg";

        // act
        var act = async () => await filePath.GetFileBytesByPathAsync();

        // assert
        await act.Should().ThrowAsync<IOException>();
    }

    [Fact]
    public async Task FilePathToByteArrayConverter_GetFileBytesByPathAsync_ReturnExpected()
    {
        // arrange
        using var tempFile = new TemporaryFile();
        var filePath = tempFile.FilePath;
        File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Temporary);

        // act
        var file = await filePath.GetFileBytesByPathAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        file.Should().BeEquivalentTo(Array.Empty<byte>());
    }
}
