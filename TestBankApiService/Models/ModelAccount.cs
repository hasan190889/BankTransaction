using DataLibrary;
using EntityLayer.Request;
using EntityLayer.Response;
using System;

namespace Service.Models
{
    public class ModelAccount
    {
        private string strMessage = string.Empty;

        #region Deposit

        public AcoountServiceResponse Deposit(AccountDepositOrWithdraw objAccountDepositOrWithdraw)
        {
            AcoountServiceResponse objAcoountServiceResponse = null;

            DLAccount objDLAccount = null;

            if (objAccountDepositOrWithdraw != null && ValidateAccountDepositOrWithdraw(objAccountDepositOrWithdraw, out strMessage))
            {
                try
                {
                    objAcoountServiceResponse = new AcoountServiceResponse();
                    objDLAccount = new DLAccount();
                    objAcoountServiceResponse = objDLAccount.Deposit(objAccountDepositOrWithdraw.AccountNumber, objAccountDepositOrWithdraw.Amount, objAccountDepositOrWithdraw.Currency);
                }
                finally
                {
                    objDLAccount = null;
                }
            }
            else
            {
                throw new Exception(strMessage);
            }
            return objAcoountServiceResponse;
        }

        private bool ValidateAccountDepositOrWithdraw(AccountDepositOrWithdraw objAccountDepositOrWithdraw, out string Message )
        {
            Message = string.Empty;
            bool status = false;
            if(objAccountDepositOrWithdraw.AccountNumber <0 )
            {
                status = false;
                Message = "Please insert valid account no";
            }
            else if (objAccountDepositOrWithdraw.Amount <= 0)
            {
                status = false;
                Message = "Amount should be greate than 0";
            }            
            else
            {
                status = true;
            }
            return status;
        }

        #endregion

        #region Withdraw

        public AcoountServiceResponse Withdraw(AccountDepositOrWithdraw objAccountDepositOrWithdraw)
        {
            AcoountServiceResponse objAcoountServiceResponse = null;
            DLAccount objDLAccount = null;

            if (objAccountDepositOrWithdraw != null && ValidateAccountDepositOrWithdraw(objAccountDepositOrWithdraw, out strMessage))
            {
                try
                {
                    objAcoountServiceResponse = new AcoountServiceResponse();
                    objDLAccount = new DLAccount();
                    objAcoountServiceResponse = objDLAccount.Withdraw(objAccountDepositOrWithdraw.AccountNumber, objAccountDepositOrWithdraw.Amount, objAccountDepositOrWithdraw.Currency);
                }
                finally
                {
                    objDLAccount = null;
                }
            }
            else
            {
                throw new Exception(strMessage);
            }
            return objAcoountServiceResponse;
        }

        #endregion

        #region Balance

        public AcoountServiceResponse Balance(long AccountNumber)
        {
            AcoountServiceResponse objAcoountServiceResponse = null;
            DLAccount objDLAccount = null;

            if (AccountNumber > 0)
            {
                try
                {
                    objAcoountServiceResponse = new AcoountServiceResponse();
                    objDLAccount = new DLAccount();
                    objAcoountServiceResponse = objDLAccount.Balance(AccountNumber);
                }
                finally
                {
                    objDLAccount = null;
                }
            }
            else
            {
                throw new Exception(strMessage);
            }
            return objAcoountServiceResponse;
        }

        #endregion
    }
}