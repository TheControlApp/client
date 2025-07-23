using ControlApp.Models;

namespace ControlApp.Services
{
    /// <summary>
    /// Manages the state of the currently logged-in user for the application session.
    /// </summary>
    public static class AccountService
    {
        /// <summary>
        /// Gets the account information for the currently logged-in user.
        /// This is null if no user is logged in.
        /// </summary>
        public static UserAccount? CurrentUser { get; private set; }

        /// <summary>
        /// Gets a value indicating whether a user is currently logged in.
        /// </summary>
        public static bool IsLoggedIn => CurrentUser != null;

        /// <summary>
        /// Initializes the service with the logged-in user's account data.
        /// This should be called immediately after a successful login or token validation.
        /// </summary>
        /// <param name="account">The user account data fetched from the server.</param>
        public static void Initialize(UserAccount account)
        {
            CurrentUser = account;
        }

        /// <summary>
        /// Clears the current user's session data.
        /// This should be called on logout.
        /// </summary>
        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}