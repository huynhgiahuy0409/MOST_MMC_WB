using System;
namespace Tsb.Fontos.Core.Objects
{
    /// <summary>
    /// Represents TSB Base Object
    /// </summary>
    public interface ITsbBaseObject
    {
        string ObjectID { get; set; }
        ObjectType ObjectType { get; }
    }
}
