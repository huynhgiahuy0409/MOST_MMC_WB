using System;
using System.Collections.Generic;
using System.Text;
using Tsb.Fontos.Core.Codes;

namespace Tsb.Fontos.Core.Caches
{
    /// <summary>
    /// Cache Data Query Delegator
    /// </summary>
    /// <param name="codeType">Code type string</param>
    /// <returns>result object of query</returns>
    public delegate object CodeDataQueryDelegator(CodeDataParam param);
}
