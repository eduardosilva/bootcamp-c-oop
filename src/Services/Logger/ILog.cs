using System;

namespace Exemplo.Services.Logger
{
    public interface ILog
    {
        Action<string> Logger { get; set; }
    }
}