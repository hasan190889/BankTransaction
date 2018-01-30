
namespace UtilityLibrary
{
    /// <summary>
    /// This is a common response class which to client after processing the request.The response can be in encrypted or plain format.
    /// </summary>
    public class ClientResponse
    {
        /// <summary>
        /// The TMW Error instance should be passed here which contains error code and error message. If there is no error then pass null
        /// </summary>
        public ApplicationError Error { get; set; }

        /// <summary>
        /// Information or data to be sent to client
        /// </summary>        
        public object Data { get; set; }

        /// <summary>
        /// It is IV(initialization vector) which is used in AES encryption and decryption
        /// </summary>
        public string A1 { get; set; } 
    }
}
