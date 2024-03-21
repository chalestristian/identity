namespace Identity.Application.DTOs.Response
{
    public class UserRegistrationResponseDTO
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }

        public UserRegistrationResponseDTO() =>
            Errors = new List<string>();

        public UserRegistrationResponseDTO(bool success = true) : this() =>
            Success = success;

        public void AddErrors(IEnumerable<string> errors) =>
            Errors.AddRange(errors);
    }
}