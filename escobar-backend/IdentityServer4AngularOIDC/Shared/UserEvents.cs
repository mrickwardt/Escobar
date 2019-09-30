namespace Server.Shared
{
    public static class UserEvents
    {
        public static string Login { get; } = "Login";
        public static string AlreadyLogged { get; } = "AlreadyLogged";
        public static string Logout { get; } = "Logout";
    }
}