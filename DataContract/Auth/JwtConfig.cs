using System;
using System.Collections.Generic;
using System.Text;

namespace DataContract.Auth
{
    public class JwtSettings
    {
        public string Secret { get; set; }

    }   

    public static class JwtCacheKeys
    {
        public static string Entry { get { return "_Entry"; } }
        public static string CallbackEntry { get { return "_Callback"; } }
        public static string CallbackMessage { get { return "_CallbackMessage"; } }
        public static string Parent { get { return "_Parent"; } }
        public static string Child { get { return "_Child"; } }
        public static string DependentMessage { get { return "_DependentMessage"; } }
        public static string DependentCTS { get { return "_DependentCTS"; } }
        public static string Ticks { get { return "_Ticks"; } }
        public static string CancelMsg { get { return "_CancelMsg"; } }
        public static string CancelTokenSource { get { return "_CancelTokenSource"; } }
    }

    public static class JwtClaimIdentifiers
    {
        public const string Rol = "rol", Id = "id";
    }

    public static class JwtClaims
    {
        public const string ApiAccess = "api_access";
        //public const string Administrator = "Administrator";
    }
}
