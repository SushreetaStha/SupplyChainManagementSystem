using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SupplyChainManagement.Authorization;
using SupplyChainManagement.Data;
using SupplyChainManagement.Helpers;
using SupplyChainManagement.Models;
using SupplyChainManagement.ViewModels.Users;

namespace SupplyChainManagement.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Authenticate the user while signing in with a username and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The AuthenticateResponse which contains the user information with a token</returns>
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

        /// <summary>
        /// Sign up a new user in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The new signed up user</returns>
        Task<User> SignUp(SignUpRequest model);

        /// <summary>
        /// Get All the user from the database
        /// </summary>
        /// <returns>List of all the users signed up in the database</returns>
        Task<IEnumerable<User>> GetAll();

        /// <summary>
        /// Get a specific user with the id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Users with the given id signed up in the database</returns>
        Task<User> GetById(Guid id);

        /// <summary>
        /// Validates the sign up data
        /// </summary>
        /// <param name="model"></param>
        /// <returns>String containing error descriptions if errors were found else null</returns>
        Task<string> ValidateSignUp(SignUpRequest model);
    }

    public class UserService : IUserService
    {
        private readonly DatabaseContext _db;

        private readonly AppSettings _appSettings;
        private readonly IJwtUtils _jwtUtils;

        public UserService(DatabaseContext db, IOptions<AppSettings> appSetting, IJwtUtils jwtUtils)
        {
            _db = db;
            _appSettings = appSetting.Value;
            _jwtUtils = jwtUtils;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == model.Username);

            // validate
            if (user == null || !PasswordHelper.Validate(user.Password, model.Password))
                return null;

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public async Task<User> SignUp(SignUpRequest model)
        {
            var validate = await ValidateSignUp(model);

            if (validate != null) return null;

            User newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = PasswordHelper.Hash(model.Password),
                Role = model.Role
            };
            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            return newUser;
        }

        public async Task<IEnumerable<User>> GetAll() => await _db.Users.ToListAsync();

        public async Task<User> GetById(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return null;
            return user;
        }

        public async Task<string> ValidateSignUp(SignUpRequest model)
        {
            string errors = string.Empty;
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == model.Username);

            // validate if user with the same username is already signed up
            if (user != null) errors += "User with a same username already signed up.\n";

            // validate if the password and confirm password match
            if (model.Password != model.ConfirmPassword) errors += "Passwords do not match.\n";

            // return null if no errors found
            if (string.IsNullOrEmpty(errors)) return null;

            return errors;
        }
    }
}