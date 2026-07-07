using System;
using Zetatech.Accelerate.Data.Enums;

namespace Zetatech.Accelerate.Data;

public sealed class RepositoryOptions
{
    public String ConnectionString { get; set; }
    public DatabaseEngines Engine { get; set; }
    public Int32 Timeout { get; set; }
}