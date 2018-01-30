using EntityLayer;
using EntityLayer.Request;
using EntityLayer.Response;
using Service.Models;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UtilityLibrary;

namespace TestBankApiService.Controllers
{
    public class AccountController : System.Web.Http.ApiController
    {
        ApiRequestResponseLog objAPIRequestResponseLog = null;
        Stopwatch stopwatch = null;
        AcoountServiceResponse objAcoountServiceResponse = null;
        ModelAccount objModelAccount = null;

        [HttpPost]
        [Route("api/account/deposit")]
        public ClientResponse Deposit(CommonRequest<AccountDepositOrWithdraw> ObjDecryptRequest)
        {
            ClientResponse ObjClientResponse = null;
            ObjClientResponse = new ClientResponse();
            string type = "INFO";
            objAPIRequestResponseLog = new ApiRequestResponseLog();
            stopwatch = new Stopwatch();
            objAcoountServiceResponse = new AcoountServiceResponse();

            stopwatch.Start();
            try
            {
                if (ObjDecryptRequest.Data != null)
                {
                    objAPIRequestResponseLog.Request = string.Format("AccountNumber:{0},Amount:{1},Currency: {2}", ObjDecryptRequest.Data.AccountNumber, ObjDecryptRequest.Data.Amount, ObjDecryptRequest.Data.Currency);
                    objModelAccount = new ModelAccount();
                    objAcoountServiceResponse = objModelAccount.Deposit(ObjDecryptRequest.Data);
                    ObjClientResponse.Data = objAcoountServiceResponse;
                }
                else
                {
                    throw new ABException(100, false);
                }
            }
            catch (ABException ex)
            {
                type = "ERROR";
                ObjClientResponse = new ClientResponse();
                objAPIRequestResponseLog.Response = string.Format("ErrorCode:{0},ErrorMessage:{1},ErrorSource:{2}", ex.ErrorCode, ex.ErrorMessage, ex.ToString());
            }
            catch (Exception ex)
            {
                type = "ERROR";
                objAPIRequestResponseLog.Response = string.Format("ErrorSource:{0}", ex.ToString());
                ObjClientResponse = new ClientResponse();
            }
            finally
            {
                objAPIRequestResponseLog.ResponseTime = stopwatch.Elapsed.ToString();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string JsonSerializerlog = serializer.Serialize(objAPIRequestResponseLog);
                objAcoountServiceResponse = null;
                objAPIRequestResponseLog = null;
                objModelAccount = null;
            }
            return ObjClientResponse;
        }

        [HttpPost]
        [Route("api/account/withdraw")]
        public ClientResponse Withdraw(CommonRequest<AccountDepositOrWithdraw> ObjDecryptRequest)
        {
            ClientResponse ObjClientResponse = null;
            ObjClientResponse = new ClientResponse();
            string type = "INFO";
            objAPIRequestResponseLog = new ApiRequestResponseLog();
            stopwatch = new Stopwatch();
            objAcoountServiceResponse = new AcoountServiceResponse();

            stopwatch.Start();
            try
            {
                if (ObjDecryptRequest.Data != null)
                {
                    objAPIRequestResponseLog.Request = string.Format("AccountNumber:{0},Amount:{1},Currency: {2}", ObjDecryptRequest.Data.AccountNumber, ObjDecryptRequest.Data.Amount, ObjDecryptRequest.Data.Currency);
                    objModelAccount = new ModelAccount();
                    objAcoountServiceResponse = objModelAccount.Withdraw(ObjDecryptRequest.Data);
                    ObjClientResponse.Data = objAcoountServiceResponse;

                }
                else
                {
                    throw new ABException(100, false);
                }
            }
            catch (ABException ex)
            {
                type = "ERROR";
                ObjClientResponse = new ClientResponse();
                objAPIRequestResponseLog.Response = string.Format("ErrorCode:{0},ErrorMessage:{1},ErrorSource:{2}", ex.ErrorCode, ex.ErrorMessage, ex.ToString());
            }
            catch (Exception ex)
            {
                type = "ERROR";
                objAPIRequestResponseLog.Response = string.Format("ErrorSource:{0}", ex.ToString());
                ObjClientResponse = new ClientResponse();
            }
            finally
            {
                objAPIRequestResponseLog.ResponseTime = stopwatch.Elapsed.ToString();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string JsonSerializerlog = serializer.Serialize(objAPIRequestResponseLog);
                objAcoountServiceResponse = null;
                objAPIRequestResponseLog = null;
                objModelAccount = null;
            }
            return ObjClientResponse;
        }

        [HttpGet]
        [Route("api/account/balance")]
        public ClientResponse Balance(long AccountNumber)
        {
            ClientResponse ObjClientResponse = null;
            ObjClientResponse = new ClientResponse();
            string type = "INFO";
            objAPIRequestResponseLog = new ApiRequestResponseLog();
            stopwatch = new Stopwatch();
            objAcoountServiceResponse = new AcoountServiceResponse();

            stopwatch.Start();
            try
            {
                if (AccountNumber > 0)
                {
                    objAPIRequestResponseLog.Request = string.Format("AccountNumber:{0}", AccountNumber);
                    objModelAccount = new ModelAccount();
                    objAcoountServiceResponse = objModelAccount.Balance(AccountNumber);
                    ObjClientResponse.Data = objAcoountServiceResponse;
                }
                else
                {
                    throw new ABException(100, false);
                }
            }
            catch (ABException ex)
            {
                type = "ERROR";
                ObjClientResponse = new ClientResponse();
                objAPIRequestResponseLog.Response = string.Format("ErrorCode:{0},ErrorMessage:{1},ErrorSource:{2}", ex.ErrorCode, ex.ErrorMessage, ex.ToString());
            }
            catch (Exception ex)
            {
                type = "ERROR";
                objAPIRequestResponseLog.Response = string.Format("ErrorSource:{0}", ex.ToString());
                ObjClientResponse = new ClientResponse();
            }
            finally
            {
                objAPIRequestResponseLog.ResponseTime = stopwatch.Elapsed.ToString();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string JsonSerializerlog = serializer.Serialize(objAPIRequestResponseLog);
                objAcoountServiceResponse = null;
                objAPIRequestResponseLog = null;
                objModelAccount = null;
            }
            return ObjClientResponse;
        }
    }
}