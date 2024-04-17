using Dapper;
using HandT_Api_Layer.DomainEntities;
using HandT_Api_Layer.DomainInterface;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.DbContext;
using System.Security.Cryptography;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HandT_Api_Layer.DomainRepository
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationRepo(DapperContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<object>> Register(UserRegistrationRequest request)
        {
            var response = new ServiceResponse<object>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var username = await connection.QueryFirstOrDefaultAsync<User>("select * from User where username=@username", new { username = request.Username });
                if (username != null)
                {
                    response.Success = false;
                    response.Message = "Username already exists!";
                    return response;
                }
                var email = await connection.QueryFirstOrDefaultAsync<User>("select * from User where email=@email", new { email = request.Email });
                if (email != null)
                {
                    response.Success = false;
                    response.Message = "Email address is already registered!";
                    return response;
                }

                CreatePasswordHash(request.Password,
                    out byte[] passwordHash,
                    out byte[] passwordSalt);

                var user = new User
                {
                    Role = "External",
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = Convert.ToHexString(passwordHash),
                    PasswordSalt = Convert.ToHexString(passwordSalt),
                    VerificationToken = CreateRandomToken()
                };

                await connection.ExecuteAsync("insert into User (Role, Username, Email, PasswordHash, PasswordSalt, VerificationToken) values (@Role, @Username, @Email, @PasswordHash, @PasswordSalt, @VerificationToken)", user);
                var emailBody = $"Verify your id following this webaddress http://localhost:3000/reset-password/{user.VerificationToken}";
                SendEmail(request.Email, "Verification Token", emailBody);
                
                response.Data = new
                {
                    roles = new List<string> { user.Role },
                    username = request.Username,
                    token = CreateJWTToken(user)
                };
                response.Success = true;
                response.Message = "User created successfully!";
                return response;
            }
        }

        public async Task<ServiceResponse<object>> Login(UserLoginRequest request)
        {
            var response = new ServiceResponse<object>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("select * from User where username=@username", new { username = request.Username });
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Username doesn't exists!";
                    return response;
                }

                if (user.VerifiedAt == null)
                {
                    response.Success = false;
                    response.Message = "User is not verified!";
                    return response;
                }

                Console.WriteLine("User", user);
                Console.WriteLine("PasswordHash", user.PasswordHash);
                Console.WriteLine("PasswordSalt", user.PasswordSalt);

                if (VerifyPasswordHash(request.Password, System.Text.Encoding.UTF8.GetBytes(user.PasswordHash), System.Text.Encoding.UTF8.GetBytes(user.PasswordSalt)))
                {
                    response.Success = false;
                    response.Message = "Password is incorrect for the username!";
                    return response;
                }

                response.Data = new
                {
                    roles = new List<string> { user.Role },
                    username= request.Username,
                    token = CreateJWTToken(user)
                };
                response.Success = true;
                response.Message = "User sucessfully logged in!";
                return response;
            }
        }

        public async Task<ServiceResponse<string>> Verify(string token)
        {
            var response = new ServiceResponse<string>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("select * from User where verificationtoken=@token", new { token = token });
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User is not verified!";
                    return response;
                }

                var VerifiedAt = DateTime.Now;

                await connection.ExecuteAsync("update User set VerifiedAt=@VerifiedAt where username=@username", new { VerifiedAt = VerifiedAt, username = user.Username });

                response.Data = "User was successfully verified"; // user was verified at message
                response.Success = true;
                response.Message = "User was successfully verified";
                return response;
            }
        }

        public async Task<ServiceResponse<string>> ForgotPassword(UserRequestForgotPassword request)
        {
            var response = new ServiceResponse<string>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("select * from User where email=@email", new { email = request.email });
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Email address cannot be found! Please enter a valid email address!";
                    return response;
                }

                var PasswordResetToken = CreateRandomToken();
                var ResetTokenExpires = DateTime.Now.AddDays(1);

                await connection.ExecuteAsync("update User set PasswordResetToken=@PasswordResetToken, ResetTokenExpires=@ResetTokenExpires where email=@email", new { PasswordResetToken = PasswordResetToken, ResetTokenExpires = ResetTokenExpires, email = request.email });
                var emailBody = $"Verify your id following this webaddress http://localhost:3000/reset-password/{PasswordResetToken}";
                SendEmail(request.email, "Reset Password Token", emailBody);

                response.Data = "Password reset token was sent to your email!";
                response.Success = true;
                response.Message = "Password reset token was sent to your email!";
                return response;
            }
        }

        public async Task<ServiceResponse<string>> ResetPassword(ResetPasswordRequest request)
        {
            var response = new ServiceResponse<string>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("select * from User where PasswordResetToken=@PasswordResetToken", new { PasswordResetToken = request.Token });
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Reset token is invalid! Please enter a valid token";
                    return response;
                }

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                await connection.ExecuteAsync("update User set PasswordHash=@PasswordHash, PasswordSalt=@PasswordSalt, PasswordResetToken=NULL, ResetTokenExpires=NULL where email=@email", new { PasswordHash = Convert.ToHexString(passwordHash), PasswordSalt = Convert.ToHexString(passwordSalt), email = user.Email });

                response.Data = "Password has been reset successfully!";
                response.Success = true;
                response.Message = "Password has been reset successfully!";
                return response;
            }
        }

        public async Task<ServiceResponse<string>> ChangePassword(UserPasswordChangeRequest request, string username)
        {
            var response = new ServiceResponse<string>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("select * from User where username=@username", new { username = username });
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Username doesn't exist!";
                    return response;
                }
                if (VerifyPasswordHash(request.oldpassword, System.Text.Encoding.UTF8.GetBytes(user.PasswordHash), System.Text.Encoding.UTF8.GetBytes(user.PasswordSalt)))
                {
                    response.Success = false;
                    response.Message = "Old Password is incorrect for the username!";
                    return response;
                }

                CreatePasswordHash(request.newpassword, out byte[] passwordHash, out byte[] passwordSalt);

                await connection.ExecuteAsync("update User set PasswordHash=@PasswordHash, PasswordSalt=@PasswordSalt where username=@username", new { PasswordHash = Convert.ToHexString(passwordHash), PasswordSalt = Convert.ToHexString(passwordSalt), username = username });

                response.Data = "Password changed successfully!";
                response.Success = true;
                response.Message = "Password changed successfully!";
                return response;
            }
        }

        public async Task<ServiceResponse<string>> RegisterInternalUser(UserRegistrationRequest request)
        {
            var response = new ServiceResponse<string>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var username = await connection.QueryFirstOrDefaultAsync<User>("select * from User where username=@username", new { username = request.Username });
                if (username != null)
                {
                    response.Success = false;
                    response.Message = "Username already exists!";
                    return response;
                }
                var email = await connection.QueryFirstOrDefaultAsync<User>("select * from User where email=@email", new { email = request.Email });
                if (email != null)
                {
                    response.Success = false;
                    response.Message = "Email address is already registered!";
                    return response;
                }

                CreatePasswordHash(request.Password,
                    out byte[] passwordHash,
                    out byte[] passwordSalt);

                var user = new User
                {
                    Role = "Internal",
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = Convert.ToHexString(passwordHash),
                    PasswordSalt = Convert.ToHexString(passwordSalt),
                    VerificationToken = CreateRandomToken()
                };

                await connection.ExecuteAsync("insert into User (Role, Username, Email, PasswordHash, PasswordSalt, VerificationToken) values (@Role, @Username, @Email, @PasswordHash, @PasswordSalt, @VerificationToken)", user);
                var emailBody = $"Verify your id following this webaddress http://localhost:3000/reset-password/{user.VerificationToken}, your username is {user.Username} and password is {request.Password} , please change your password as soon as possible.";
                SendEmail(request.Email, "Verification Token", emailBody);

                response.Data = "Internal user created successfully!";
                response.Success = true;
                response.Message = "Internal user created successfully!";
                return response;
            }
        }

        public async Task<ServiceResponse<string>> UpdateIndividualUserDetail(IndividualUserProfileUpdateRequest request, IFormFile? pictureFile, string username)
        {
            var response = new ServiceResponse<string>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("select * from User where username=@username", new { username = username });
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Username doesn't exists!";
                    return response;
                }
                if (user.IsIndividual == null)
                {
                    await connection.ExecuteAsync("update User set isIndividual=1 where username=@username", new { username = username });
                    await connection.ExecuteAsync("insert into individual_details (fullname, dob, gender, occupation, address, country, province, city, interest, bio, contact_no, citizenship_no, username) values (@Fullname, @DOB, @Gender, @Occupation, @Address, @Country, @Province, @City, @Interest, @Bio, @Contact_No, @Citizenship_No, @username)",
                        new {
                            Fullname = request.Fullname,
                            DOB = request.DOB,
                            Gender = request.Gender,
                            Occupation = request.Occupation,
                            Address = request.Address,
                            Country = request.Country,
                            Province = request.Province,
                            City = request.City,
                            Interest = request.Interest,
                            Bio = request.Bio,
                            Contact_No = request.Contact_No,
                            Citizenship_No = request.Citizenship_No,
                            username = username
                        });
                } else
                {
                    await connection.ExecuteAsync("update individual_details set fullname=@Fullname, dob=@DOB, gender=@Gender, occupation=@Occupation, address=@Address, country=@Country, province=@Province, city=@City, interest=@Interest, bio=@Bio, contact_no=@Contact_No, citizenship_no=@Citizenship_No where username=@Username",
                            new {
                                Fullname = request.Fullname,
                                DOB = request.DOB,
                                Gender = request.Gender,
                                Occupation = request.Occupation,
                                Address = request.Address,
                                Country = request.Country,
                                Province = request.Province,
                                City = request.City,
                                Interest = request.Interest,
                                Bio = request.Bio,
                                Contact_No = request.Contact_No,
                                Citizenship_No = request.Citizenship_No,
                                username = username
                            });
                }

                if (request.PictureUpdated)
                {
                    if (pictureFile != null && pictureFile.Length > 0)
                    {
                        var fileName = user.Username + Path.GetExtension(pictureFile.FileName);
                        var filePath = Path.Combine("wwwroot", "Upload", fileName);

                        var individualuserdetails = await connection.QueryFirstOrDefaultAsync<IndividualUserDetail>("select * from individual_details where username=@username", new { username = username });
                        if (!(individualuserdetails == null))
                        {
                            if (!string.IsNullOrEmpty(individualuserdetails.Picture))
                            {
                                var existingFilePath = Path.Combine("wwwroot", "Upload", individualuserdetails.Picture.TrimStart('/'));
                                if (File.Exists(existingFilePath))
                                {
                                    File.Delete(existingFilePath);
                                }
                            }
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await pictureFile.CopyToAsync(stream);
                        }

                        await connection.ExecuteAsync("update individual_details set picture=@Picture where username=@username", new { Picture = fileName, username = username });
                    }
                }


                response.Data = "User detail updated successfully!";
                response.Success = true;
                response.Message = "User detail updated successfully!";
                return response;
            }
        }
        
        public async Task<ServiceResponse<string>> UpdateCorporateUserDetail(CorporateUserProfileUpdateRequest request, IFormFile? pictureFile, string username)
        {
            var response = new ServiceResponse<string>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("select * from User where username=@username", new { username = username });
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Username doesn't exists!";
                    return response;
                }
                if (user.IsIndividual == null)
                {
                    await connection.ExecuteAsync("update User set isIndividual=0 where username=@username", new { username = username });
                    await connection.ExecuteAsync("insert into corporate_details (org_name, address, country, province, city, interest, bio, org_website, org_reg_no, org_contact, contact_pers_name, contact_pers_email, contact_pers_no, username) values (@Org_Name, @Address, @Country, @Province, @City, @Interest, @Bio, @Org_Website, @Org_Reg_No, @Org_Contact, @Contact_Pers_Name, @Contact_Pers_Email, @Contact_Pers_No, @username)",
                        new
                        {
                            Org_Name = request.Org_Name,
                            Address = request.Address,
                            Country = request.Country,
                            Province = request.Province,
                            City = request.City,
                            Interest = request.Interest,
                            Bio = request.Bio,
                            Org_Website = request.Org_Website,
                            Org_Reg_No = request.Org_Reg_No,
                            Org_Contact = request.Org_Contact,
                            Contact_Pers_Name = request.Contact_Pers_Name,
                            Contact_Pers_Email = request.Contact_Pers_Email,
                            Contact_Pers_No = request.Contact_Pers_No,
                            username = username
                        });
                }
                else
                {
                    await connection.ExecuteAsync("update corporate_details set org_name=@Org_Name, address=@Address, country=@Country, province=@Province, city=@City, interes=@Interest, bio=@Bio, org_website=@Org_Website, org_reg_no=@Org_Reg_No, org_contact=@Org_Contact, contact_pers_name=@Contact_Pers_Name, contact_pers_email=@Contact_Pers_Email, contact_pers_no=@Contact_Pers_No where username=@Username",
                            new
                            {
                                Org_Name = request.Org_Name,
                                Address = request.Address,
                                Country = request.Country,
                                Province = request.Province,
                                City = request.City,
                                Interest = request.Interest,
                                Bio = request.Bio,
                                Org_Website = request.Org_Website,
                                Org_Reg_No = request.Org_Reg_No,
                                Org_Contact = request.Org_Contact,
                                Contact_Pers_Name = request.Contact_Pers_Name,
                                Contact_Pers_Email = request.Contact_Pers_Email,
                                Contact_Pers_No = request.Contact_Pers_No,
                                username = username
                            });
                }

                if (request.PictureUpdated)
                {
                    if (pictureFile != null && pictureFile.Length > 0)
                    {
                        var fileName = user.Username + Path.GetExtension(pictureFile.FileName);
                        var filePath = Path.Combine("wwwroot", "Upload", fileName);

                        var corporateuserdetails = await connection.QueryFirstOrDefaultAsync<CorporateUserDetail>("select * from corporate_details where username=@username", new { username = username });
                        if (!(corporateuserdetails == null))
                        {
                            if (!string.IsNullOrEmpty(corporateuserdetails.Profile_Picture))
                            {
                                var existingFilePath = Path.Combine("wwwroot", "Upload", corporateuserdetails.Profile_Picture.TrimStart('/'));
                                if (File.Exists(existingFilePath))
                                {
                                    File.Delete(existingFilePath);
                                }
                            }
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await pictureFile.CopyToAsync(stream);
                        }

                        await connection.ExecuteAsync("update corporate_details set profile_picture=@Profile_Picture where username=@username", new { Profile_Picture = fileName, username = username });
                    }
                }


                response.Data = "User detail updated successfully!";
                response.Success = true;
                response.Message = "User detail updated successfully!";
                return response;
            }
        }

        public async Task<ServiceResponse<object>> GetUserDetail(string username)
        {
            var response = new ServiceResponse<object>();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();

                    var query = "SELECT U.*, ID.* FROM User U LEFT JOIN individual_details ID ON U.username = ID.username WHERE U.username = @username ";

                    var result = await connection.QueryFirstOrDefaultAsync<object>(query, new { username });

                    if (result == null)
                    {
                        response.Success = false;
                        response.Message = "User details not found!";
                        return response;
                    }

                    response.Success = true;
                    response.Message = "User details retrieved successfully!";
                    response.Data = result;

                    return response;
                }
            }
            catch (Exception ex)
            {
                
                response.Success = false;
                response.Message = "An error occurred while fetching user details";
                response.Data = null;

                return response;
            }
        }



        //public async Task<ServiceResponse<object>> GetUserDetail(string username)
        //{
        //    var response = new ServiceResponse<object>();

        //    try
        //    {
        //        using (var connection = _context.CreateConnection())
        //        {
        //            connection.Open();


        //                var query = @"
        //                        SELECT U.*, 
        //                        CASE WHEN U.IsIndividual = 1 THEN ID.* ELSE CD.* END AS UserDetails
        //                        FROM User U
        //                        LEFT JOIN individual_details ID ON U.username = ID.username AND U.IsIndividual = 1
        //                        LEFT JOIN corporate_details CD ON U.username = CD.username AND U.IsIndividual = 0
        //                        WHERE U.username = @username";


        //            var result = await connection.QueryFirstOrDefaultAsync<object>(query, new { username });

        //            if (result == null)
        //            {
        //                response.Success = false;
        //                response.Message = "User details not found!";
        //                return response;
        //            }

        //            response.Success = true;
        //            response.Message = "User details retrieved successfully!";
        //            response.Data = result;

        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        response.Success = false;
        //        response.Message = "An error occurred while fetching user details";
        //        response.Data = null;

        //        return response;
        //    }
        //}



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private string CreateJWTToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:JWTToken").Value;
            if (appSettingsToken is null)
            {
                throw new Exception("AppSettings Token is null");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                Console.WriteLine(computedHash.SequenceEqual(passwordHash));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void SendEmail(string reciever, string subject, string body)
        {
            var emailaddress = "thomas.cole@ethereal.email";
            var securitykey = "vZp292r6gFcGGh85Wr";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailaddress));
            email.To.Add(MailboxAddress.Parse(emailaddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailaddress, securitykey);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
