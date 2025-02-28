using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsb.Fontos.Core.Exceptions.Business
{
    /// <summary>
    /// TSB Network Exception that is occured from Business processing flow
    /// </summary>
    [Serializable]
    public class TsbBizMenuException : TsbBizBaseException
    {
        private readonly string _OBJECT_ID = "GNR-FTCO-EXP-TsbBizMenuException";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TsbBizMenuException()
            : base()
        {
            this.ObjectID = this._OBJECT_ID;
        }

        /// <summary>
        /// Initialize a new instance with operation ID, message code and array of string to make message
        /// </summary>
        /// <param name="objectID">The Object ID</param>
        /// <param name="msgCode">A message code to make message</param>
        /// <param name="msgArgs">An array of string to make message</param>
        public TsbBizMenuException(string objectID, string msgCode, params string[] msgArgs)
            : base(objectID, msgCode, msgArgs)
        {
            this.ObjectID = this._OBJECT_ID;
        }

        /// <summary>
        /// Initialize a new instance with a reference to the inner exception that cause this exception,
        /// operation ID, message code and array of string to make message
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        /// <param name="sourceObjectID">The Object ID of source object</param>
        /// <param name="msgCode">A message code to make message</param>
        /// <param name="msgArgs">An array of string to make message</param>
        public TsbBizMenuException(Exception innerException, string sourceObjectID, string msgCode, params string[] msgArgs)
            : base(innerException, sourceObjectID, msgCode, msgArgs)
        {
            this.ObjectID = this._OBJECT_ID;
        }
    }
}
