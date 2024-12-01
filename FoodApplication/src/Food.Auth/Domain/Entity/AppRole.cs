using Microsoft.AspNetCore.Identity;

namespace Domain.Entity
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
