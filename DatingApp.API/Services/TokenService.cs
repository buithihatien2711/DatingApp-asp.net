using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Database.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                // Gets or sets the output claims to be included in the issued token.
                Subject = new ClaimsIdentity(claims),
                // Thời gian hết hạn của 1 token
                Expires = DateTime.Now.AddDays(1),
                // Gets or sets the credentials that are used to sign the token.
                SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

/*
    JWT(Json Web Token) bao gồm 3 phần, được ngăn cách bởi dấu chấm:
    1. Header
    2. Payload
    3. Signature
    Header: khai báo kiểu chữ ký và thuật toán mã hóa dùng cho token
    Payload (Claims): chứa các nội dung của thông tin (claim)
        + Thông tin truyền đi có thể là mô tả của 1 thực thể hoặ có thể là thông tin bổ sung thêm cho header
        + Chúng được chia làm 3 loại: reserved, public, private
            -Reserved:  là những thông tin đã được quy định ở trong IANA JSON Web Token Claims registry. 
                Chúng bao gồm (Chú ý rằng các khóa của claim đều chỉ dài 3 ký tự vì mục đích giảm kích thước của Token)
                    iss (issuer): tổ chức phát hành token, vd: Nhatkyhoctap
                    sub (subject): chủ đề của token
                    aud (audience): đối tượng sử dụng token, vd: http://nhatkyhoctap.blogspot.com
                    exp (expired time): thời điểm token sẽ hết hạn
                    nbf (not before time): token sẽ chưa hợp lệ trước thời điểm này
                    iat (issued at): thời điểm token được phát hành, tính theo UNIX time
                    jti: JWT ID
            -Private: Phần thông tin thêm dùng để truyền qua giữa các máy thành viên.
                Ví dụ
                    {
                        "sub": "1234567890", 
                        "name": "paduvi",
                        "admin": true
                    }
            Public: Khóa nên được quy định ở trong IANA JSON Web Token Registry hoặc là 1 URI có chứa không gian tên không bị trùng lặp.

    Signature: được tạo ra bằng cách kết hợp Header + Payload
*/