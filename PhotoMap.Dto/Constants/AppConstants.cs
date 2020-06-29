namespace PhotoMap.Dto.Constants
{
    public static class AppConstants
    {
        public static string ApiServiceUrl => "https://photomapapi.azurewebsites.net";
        public static string AccountAuthUrl => $"{ApiServiceUrl}/user/authenticate";
        public static string PostPhotoUrl => $"{ApiServiceUrl}/photo";
    }
}