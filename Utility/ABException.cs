using System;

namespace UtilityLibrary
{
    //[Obsolete]
    /// <summary>
    /// Please refer ApplicationDbUtility for this Exception class
    /// </summary>
    public class ABException : ApplicationException
    {
        #region Declaration

        private int errorCode;
        private string errorMessage = "DEFAULT ERROR MESSAGE";
        private string systemException = null;

        #endregion

        #region Public Methods

        public ABException() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        public ABException(int errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        public ABException(int errorCode, bool status)
        {
            string message = string.Empty;
            if (status == true)
            {
                message = "Success";
            }
            else
            {
                message = "Error";
            }
            this.errorCode = errorCode;
            this.errorMessage = message;
        }

        public ABException(int errorCode, string systemException, bool status)
        {
            string message = string.Empty;
            if (status == true)
            {
                message = "Success";
            }
            else
            {
                message = "Error";
            }
            this.errorCode = errorCode;
            this.errorMessage = message;
            this.systemException = systemException;
        }

        public ABException(int errorCode, string errorMessage, string systemException)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
            this.systemException = systemException;
        }

        #endregion

        #region Public Properties

        public int ErrorCode
        {
            get
            {
                return errorCode;
            }
            set
            {
                errorCode = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }

        public string SystemException
        {
            get
            {
                return systemException;
            }
            set
            {
                systemException = value;
            }
        }

        #endregion
    }
}
