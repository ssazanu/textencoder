using System.Buffers.Text;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace TextEncoder;

[ApiController]
[Route("api/[controller]")]
public class EncodersController : Controller
{
    [HttpGet]
    public IActionResult Encode(string unencoded)
    {
        if (string.IsNullOrEmpty(unencoded))
        {
            return Accepted("Please enter a string to encode");
        }

        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(unencoded));
        return Ok(encoded);
    }

    [HttpGet("decode")]
    public IActionResult Decode(string undecoded)
    {
        if (string.IsNullOrEmpty(undecoded))
        {
            return Accepted("Please enter a Base64 string to decode");
        }

        var span = new Span<byte>(new byte[undecoded.Length]);
        if (Convert.TryFromBase64String(undecoded, span, out int parsed))
        {
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(undecoded));
            return Ok(decoded);
        }

        return BadRequest("Please enter a valid Base64 string");
    }
}