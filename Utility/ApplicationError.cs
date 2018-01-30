namespace UtilityLibrary
{
    /// <summary>
    /// Please refer ApplicationDbUtility for this ApplicationError class
    /// </summary>
    public class ApplicationError
    {
        #region Declaration

        private int errorCode = 0;
        private string errorMessage = string.Empty;

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        public ApplicationError(int errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        /// <summary>Returns the instance of ApplicationError</summary>
        /// <param name="errorCode">Pass a numeric error code</param>
        /// <param name="errorMessage">Pass the error message w.r.t error code</param>
        public static ApplicationError GetApplicationError(int errorCode, string errorMessage)
        {
            return new ApplicationError(errorCode, errorMessage);
        }

        /// <summary>Returns the instance of ApplicationError</summary>
        /// <param name="ex">Pass the instance ApplicationException class</param>
        /// 
        public static ApplicationError GetApplicationError(ABException ex)
        {
            return new ApplicationError(ex.ErrorCode, ex.ErrorMessage);
        }

        /// <summary>
        /// Returns the numeric error code
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return errorCode;
            }
        }

        /// <summary>
        /// Returns the string error message
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
        }

        #endregion
    }
}
