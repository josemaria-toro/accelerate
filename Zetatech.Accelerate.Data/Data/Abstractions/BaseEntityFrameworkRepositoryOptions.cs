using System;
using Zetatech.Accelerate.Data.Enums;

namespace Zetatech.Accelerate.Data.Abstractions;

public abstract class BaseEntityFrameworkRepositoryOptions
{
    public String ConnectionString { get; set; }
    public SupportedDatabaseEngines Engine { get; set; }
    public Int32 Timeout { get; set; }
}
