using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// A single user account
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account ID
        /// </summary>
        [Key]
        public int AccountID { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Required]
        [StringLength(64)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Required]
        [StringLength(64)]
        public string LastName { get; set; }

        /// <summary>
        /// The username
        /// </summary>
        [Required]
        [StringLength(32)]
        public string Username { get; set; }

        /// <summary>
        /// The passwrod for logging in
        /// </summary>
        [Required]
        [StringLength(256)]
        [DataType(DataType.Password)] // <input type="password">
        public string Password { get; set; }

        /// <summary>
        /// The email to send shipping notices to.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Where to ship the product.
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Address { get; set; }

        /// <summary>
        /// The date of your birth
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
