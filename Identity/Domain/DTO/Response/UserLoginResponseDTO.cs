using System.Text.Json.Serialization;

namespace Identity.Application.DTOs.Response
{
    public class UserLoginResponseDTO
    {
        public bool Success  => Errors.Count == 0;
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AccessToken { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefreshToken { get; private set; }
        
        public List<string> Errors { get; private set; }

        public UserLoginResponseDTO() =>
            Errors = new List<string>();

        public UserLoginResponseDTO(bool success, string accessToken, string refreshToken) : this()
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public void AddError(string error) =>
            Errors.Add(error);

        public void AddError(IEnumerable<string> errors) =>
            Errors.AddRange(errors);
    }
}