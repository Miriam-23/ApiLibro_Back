using System.Web.Http;
using System.Web.Http.Controllers;
using ApiLibro.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace ApiLibro.Handlers
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;

            if (!headers.Contains("Authorization"))
                return false;

            var token = headers.GetValues("Authorization")
                               .FirstOrDefault()
                               .Replace("Bearer ", "");

            // VALIDAR JWT
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("LIBREIRAJ4ZM1N+K4R3N+M1RI4M8C2026");

            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false; // token inválido o expirado
            }

            // VALIDAR SESIÓN EN BD
            var dao = new UsuariosDAO();
            var user = dao.GetByToken(token);

            if (user == null)
                return false;

            return user.TokenActivo == token;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request
                .CreateResponse(HttpStatusCode.Unauthorized, "Sesión expirada o no autorizada");
        }
    }
}