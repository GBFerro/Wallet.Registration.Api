using MediatR;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Wallet.Registration.CrossCutting.Exceptions.BadRequest;
using Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp;
using Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp.Models;
using Wallet.Registration.Domain.Entities.v1.MongoDB.UserSalt;
using Wallet.Registration.Domain.Enum;
using Wallet.Registration.Domain.Interfaces.v1.MongoDb;

namespace Wallet.Registration.Domain.Command.v1.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpCommandResponse>
    {
        private readonly IUserRegistryRepository _userRegistryRepository;
        private readonly IUserSaltRepository _userSaltRepository;

        public SignUpCommandHandler(
            IUserRegistryRepository userRegistryRepository,
            IUserSaltRepository userSaltRepository
        )
        {
            _userRegistryRepository = userRegistryRepository;
            _userSaltRepository = userSaltRepository;
        }

        public async Task<SignUpCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateUser(request);

                var hashPassword = HashPasword(request.Password, out byte[] salt);
                SignUpMongoEntity newUser = new SignUpMongoEntity()
                {
                    Username = request.Username,
                    Password = hashPassword,
                    User = new User()
                    {
                        BirthDate = request.User.BirthDate,
                        Email = request.User.Email,
                        GovId = request.User.GovId,
                        Name = request.User.Name,
                        Phone = request.User.Phone,
                    },
                    Role = RoleEnum.Free
                };

                UserSaltMongoEntity userSalt = new UserSaltMongoEntity()
                {
                    GovId = request.User.GovId,
                    Salt = Convert.ToHexString(salt)
                };

                RunAsyncTasks(newUser, userSalt);

                return new SignUpCommandResponse()
                {
                    Username = newUser.Username,
                    Email = newUser.User.Email,
                    GovId = request.User.GovId,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RunAsyncTasks(SignUpMongoEntity newUser, UserSaltMongoEntity userSalt)
        {

            var registerUserTask = Task.Run(() =>
            {
                _userRegistryRepository.RegisterUserAsync(newUser);
            });

            var registerSaltTask = Task.Run(() =>
            {
                _userSaltRepository.SaveUserSaltAsync(userSalt);
            });

            var whenTask = Task.WhenAll(
                registerUserTask,
                registerSaltTask
            );
        }

        private async Task ValidateUser(SignUpCommand request)
        {
            var userMongo = await _userRegistryRepository.GetUserAsync(request.User.GovId);

            if (userMongo is not null)
            {
                throw new UserAlreadyRegisteredBadRequestException("UserAlreadyRegistered", HttpStatusCode.Conflict);
            }
        }

        private static byte[] GenerateSalt(int keySize) =>
            RandomNumberGenerator.GetBytes(keySize);

        private static string HashPasword(string password, out byte[] salt)
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            salt = GenerateSalt(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }
    }
}
