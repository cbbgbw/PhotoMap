using System;

namespace PhotoMap.Dto.Models
{
    public class UserAuthResponse
    {
        public UserAuthResponse(Guid userRowguid, string firstName, string lastName, string login, string token)
        {
            UserROWGUID = userRowguid;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Token = token;
        }

        public Guid UserROWGUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
    }
}