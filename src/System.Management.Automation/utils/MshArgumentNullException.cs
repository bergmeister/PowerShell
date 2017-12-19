/********************************************************************++
Copyright (c) Microsoft Corporation. All rights reserved.
--********************************************************************/

using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Management.Automation
{
    /// <summary>
    /// This is a wrapper for exception class
    /// <see cref="System.ArgumentNullException"/>
    /// which provides additional information via
    /// <see cref="System.Management.Automation.IContainsErrorRecord"/>.
    /// </summary>
    /// <remarks>
    /// Instances of this exception class are usually generated by the
    /// Monad Engine.  It is unusual for code outside the Monad Engine
    /// to create an instance of this class.
    /// </remarks>
    [Serializable]
    public class PSArgumentNullException
            : ArgumentNullException, IContainsErrorRecord
    {
        #region ctor
        /// <summary>
        /// Initializes a new instance of the PSArgumentNullException class.
        /// </summary>
        /// <returns> constructed object </returns>
        public PSArgumentNullException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PSArgumentNullException class.
        /// </summary>
        /// <param name="paramName">  </param>
        /// <returns> constructed object </returns>
        /// <remarks>
        /// Per MSDN, the parameter is paramName and not message.
        /// I confirm this experimentally as well.
        /// </remarks>
        public PSArgumentNullException(string paramName)
            : base(paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PSArgumentNullException class.
        /// </summary>
        /// <param name="message">  </param>
        /// <param name="innerException">  </param>
        /// <returns> constructed object </returns>
        public PSArgumentNullException(string message, Exception innerException)
            : base(message, innerException)
        {
            _message = message;
        }

        /// <summary>
        /// Initializes a new instance of the PSArgumentNullException class.
        /// </summary>
        /// <param name="paramName">  </param>
        /// <param name="message">  </param>
        /// <returns> constructed object </returns>
        /// <remarks>
        /// ArgumentNullException has this ctor form and we imitate it here.
        /// </remarks>
        public PSArgumentNullException(string paramName, string message)
            : base(paramName, message)
        {
            _message = message;
        }

        #region Serialization
        /// <summary>
        /// Initializes a new instance of the PSArgumentNullException class
        /// using data serialized via
        /// <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info"> serialization information </param>
        /// <param name="context"> streaming context </param>
        /// <returns> constructed object </returns>
        protected PSArgumentNullException(SerializationInfo info,
                           StreamingContext context)
                : base(info, context)
        {
            _errorId = info.GetString("ErrorId");
            _message = info.GetString("PSArgumentNullException_MessageOverride");
        }

        /// <summary>
        /// Serializer for <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info"> serialization information </param>
        /// <param name="context"> streaming context </param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new PSArgumentNullException("info");
            }

            base.GetObjectData(info, context);
            info.AddValue("ErrorId", _errorId);
            info.AddValue("PSArgumentNullException_MessageOverride", _message);
        }
        #endregion Serialization
        #endregion ctor

        /// <summary>
        /// Additional information about the error
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Note that ErrorRecord.Exception is
        /// <see cref="System.Management.Automation.ParentContainsErrorRecordException"/>.
        /// </remarks>
        public ErrorRecord ErrorRecord
        {
            get
            {
                if (null == _errorRecord)
                {
                    _errorRecord = new ErrorRecord(
                        new ParentContainsErrorRecordException(this),
                        _errorId,
                        ErrorCategory.InvalidArgument,
                        null);
                }
                return _errorRecord;
            }
        }
        private ErrorRecord _errorRecord;
        private string _errorId = "ArgumentNull";

        /// <summary>
        /// see <see cref="System.Exception.Message"/>
        /// </summary>
        /// <remarks>
        /// Exception.Message is get-only, but you can effectively
        /// set it in a subclass by overriding this virtual property.
        /// </remarks>
        /// <value></value>
        public override string Message
        {
            get { return String.IsNullOrEmpty(_message) ? base.Message : _message; }
        }
        private string _message;
    } // PSArgumentNullException
} // System.Management.Automation

