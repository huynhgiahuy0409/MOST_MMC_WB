namespace Tsb.Fontos.Core.Codes.Type
{
    /// <summary>
    /// Code Bind Type Enumeration
    /// </summary>
    public enum CodeBindType
    {
        /// <summary>
        /// General case.Code will be binded to CodeDataItem.Code property, 
        /// CodeName will be binded to CodeDataItem.CodeName property
        /// </summary>
        Code_CodeName,

        /// <summary>
        /// Reversed case. Code will be binded to CodeDataItem.CodeName property, 
        /// CodeName will be binded to CodeDataItem.Code property
        /// </summary>
        CodeName_Code,

        /// <summary>
        /// Only Code case. Code will be binded to CodeDataItem.Code property, 
        /// Code will be binded to CodeDataItem.CodeName property
        /// </summary>
        Code_Code,

        /// <summary>
        /// Only CodeNam case CodeName will be binded to CodeDataItem.Code property, 
        /// CodeName will be binded to CodeDataItem.CodeName property
        /// </summary>
        CodeName_CodeName


    }
}
