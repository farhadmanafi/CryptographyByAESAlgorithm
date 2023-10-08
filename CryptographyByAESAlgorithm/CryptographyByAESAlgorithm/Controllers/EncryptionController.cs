using CryptographyByAESAlgorithm.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CryptographyByAESAlgorithm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {
        [HttpGet("GatEncrypt")]
        public IActionResult Encrypt(string value)
        {
            var encryptValue = EncryptionHelper.Encrypt(value);

            return Ok(encryptValue);
        }
        [HttpGet("GatDecrypt")]
        public IActionResult Decrypt(string encryptValue)
        {
            var decryptValue = EncryptionHelper.Decrypt(encryptValue);

            return Ok(decryptValue);
        }

    }
}
