namespace UltrakillRaceGenerator;
internal static class ClipboardUtility
{
    private static void RunInSTAThread(ThreadStart threadStart)
    {
        Thread staThread = new(threadStart);
        staThread.SetApartmentState(ApartmentState.STA);
        staThread.Start();
        staThread.Join();
    }

    public static void SetText(string text)
    {
        RunInSTAThread(() => CopyToClipboard(DataFormats.Text, text));
    }

    private static void CopyToClipboard(string dataFormat, object data)
    {
        try
        {
            Clipboard.SetData(dataFormat, data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to copy text to clipboard: {ex.Message}");
        }
    }
}