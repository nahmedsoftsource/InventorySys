using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedKernal
{
    public abstract class AppService
    {
        public virtual bool IsPermissionGranted(string permissionName)
        {
            ///implementation for check if current user Has Specified Permission
            return true;
        }
    }
}
