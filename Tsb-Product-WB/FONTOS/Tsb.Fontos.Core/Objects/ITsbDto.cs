using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Transaction;

namespace Tsb.Fontos.Core.Objects
{
    /// <summary>
    /// Represents Data Transfer Object
    /// </summary>
    public interface ITsbDto
    {
        TransactionInfo TransactionInfo { get; set; }
    }
}
