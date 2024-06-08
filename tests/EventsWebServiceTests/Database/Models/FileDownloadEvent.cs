using System;
using System.Collections.Generic;

namespace EventsWebServiceTests.Database.Models;

public partial class FileDownloadEvent
{
    public int Id { get; set; }

    public string? EventId { get; set; }

    public string? Date { get; set; }

    public string? FileName { get; set; }

    public int? FileLenght { get; set; }
}
