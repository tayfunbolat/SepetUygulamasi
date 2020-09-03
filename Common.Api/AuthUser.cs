using System;
using System.Collections.Generic;
using System.Threading;

public class AuthUser
    {

        public DateTime ExpireDate { get; set; }
        public string RequestIp { get; set; }
        private static readonly AsyncLocal<AuthUser> _current = new AsyncLocal<AuthUser>();
        public static AuthUser Current
        {
            get => _current.Value;
            set => _current.Value = value;
        }

    }

