namespace Reception.Extension.Test.Fixture;

/// <summary>
/// The class can create a temp file and delete it when disposed
/// </summary>
/// <example>
/// <code>
/// </code>
/// using var tempFile = new FilePathToByteArrayConverterFixture.TemporaryFile();
/// </example>
/// <remarks>
/// Code from <see href="https://stackoverflow.com/a/3241196/7473834">stackoverflow.com</see>
/// </remarks>
internal class TemporaryFile(bool shortLived) : IDisposable
{
    private bool _isDisposed;


    public TemporaryFile() : this(false) { }


    public bool Keep { get; set; }

    public string FilePath { get; private set; } = CreateTemporaryFile(shortLived);


    public static string CreateTemporaryFile(bool shortLived)
    {
        string temporaryFile = Path.GetTempFileName();

        if (shortLived)
        {
            // Set the temporary attribute, meaning the file will live in memory and will not be written to disk
            File.SetAttributes(temporaryFile, File.GetAttributes(temporaryFile) | FileAttributes.Temporary);
        }

        return temporaryFile;
    }

    private void TryDelete()
    {
        try
        {
            File.Delete(FilePath);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // All ok
        }
    }

    #region IDisposable

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            _isDisposed = true;

            if (!Keep)
            {
                TryDelete();
            }
        }
    }

    ~TemporaryFile()
    {
        Dispose(false);
    }

    #endregion IDisposable
}
