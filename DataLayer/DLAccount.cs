using EntityLayer.Response;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DataLibrary
{
    public class DLAccount
    {
        #region Public Methods        

        /// <summary>
        /// Deposits the amount
        /// </summary>
        /// <param name="AccountNumber">Account Number</param>
        /// <param name="Amount">Amount</param>
        /// <param name="Currency">Currency</param>
        /// <returns>Response object</returns>
        public AcoountServiceResponse Deposit(long AccountNumber, decimal Amount, string Currency)
        {
            AcoountServiceResponse objAcoountServiceResponse = null;
            using (var objTCommonContext = new CommonContext())
            {
                objAcoountServiceResponse = new AcoountServiceResponse();
                SqlParameter[] parameters ={
                                               (new SqlParameter("@AccountNumber", AccountNumber)),
                                               (new SqlParameter("@Amount", Amount)),
                                               (new SqlParameter("@Currency", Currency))
                                           };
                try
                {
                    string sqlQuery = "exec USP_Deposit  @AccountNumber, @Amount, @Currency ";
                    objAcoountServiceResponse = objTCommonContext.Database.SqlQuery<AcoountServiceResponse>(sqlQuery, parameters).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objAcoountServiceResponse;
        }

        /// <summary>
        /// Withdraw the amount
        /// </summary>
        /// <param name="AccountNumber">Account Number</param>
        /// <param name="Amount">Amount</param>
        /// <param name="Currency">Currency</param>
        /// <returns>Response object</returns>
        public AcoountServiceResponse Withdraw(long AccountNumber, decimal Amount, string Currency)
        {
            AcoountServiceResponse objAcoountServiceResponse = null;
            using (var objTCommonContext = new CommonContext())
            {
                objAcoountServiceResponse = new AcoountServiceResponse();
                SqlParameter[] parameters ={
                                               (new SqlParameter("@AccountNumber", AccountNumber)),
                                               (new SqlParameter("@Amount", Amount)),
                                               (new SqlParameter("@Currency", Currency))
                                           };
                try
                {
                    string sqlQuery = "exec USP_Withdraw  @AccountNumber, @Amount, @Currency ";
                    objAcoountServiceResponse = objTCommonContext.Database.SqlQuery<AcoountServiceResponse>(sqlQuery, parameters).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objAcoountServiceResponse;
        }

        /// <summary>
        /// Checks balance
        /// </summary>
        /// <param name="AccountNumber">Account Number</param>
        /// <returns>Response object</returns>
        public AcoountServiceResponse Balance(long AccountNumber)
        {
            AcoountServiceResponse objAcoountServiceResponse = null;
            using (var objTCommonContext = new CommonContext())
            {
                objAcoountServiceResponse = new AcoountServiceResponse();
                SqlParameter[] parameters ={
                                               (new SqlParameter("@AccountNumber", AccountNumber))
                                           };
                try
                {
                    string sqlQuery = "exec USP_Accountbalance  @AccountNumber ";
                    objAcoountServiceResponse = objTCommonContext.Database.SqlQuery<AcoountServiceResponse>(sqlQuery, parameters).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objAcoountServiceResponse;
        }

        #endregion
    }
}
