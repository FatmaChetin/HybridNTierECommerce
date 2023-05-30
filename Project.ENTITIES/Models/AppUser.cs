using Microsoft.AspNetCore.Identity;
using Project.ENTITIES.CoreInterfaces;
using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser:IdentityUser<int>,IEntity
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus DataStatus { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AppUser()
        {
            CreatedDate = DateTime.Now;
            DataStatus = DataStatus.Inserted;
        }
        //relational properties
        public virtual AppUserProfile AppUserProfile { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
