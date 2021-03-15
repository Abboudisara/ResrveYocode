using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserNam { get; set; }
        public bool IsSelected { get; set; }
        public object UserName { get; internal set; }
    }
}
