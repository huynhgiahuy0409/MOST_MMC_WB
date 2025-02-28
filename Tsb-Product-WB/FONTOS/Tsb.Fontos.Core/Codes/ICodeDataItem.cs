using System;
namespace Tsb.Fontos.Core.Codes
{
    /// <summary>
    /// Represent Code Data Item
    /// </summary>
    public interface ICodeDataItem : ICloneable
    {
       /// <summary>
        /// Gets or Sets Code
        /// </summary>
        string Code {get; set;}

        /// <summary>
        /// Gets or Sets Code name
        /// </summary>
        string CodeName { get; set; }

        /// <summary>
        /// Gets or Sets Text Value. This property is used for only customized displaying
        /// (ex. concatenated CODE and CodeName with colon like TSB:Total Soft Bank)
        /// </summary>
        string TextValue { get; set; }

        /// <summary>
        /// Gets or Sets Code type
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or Sets Code type name
        /// </summary>
        string TypeName { get; set; }

        /// <summary>
        /// Gets or Sets Default Check Y/N flag
        /// </summary>
        string DefaultChkYN { get; set; }

    }
}
