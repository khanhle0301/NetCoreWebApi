using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBaby.Data.Entities
{
    /// <summary>
    /// App Role class
    /// </summary>
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>
    {
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AppRole() : base() { }

        #endregion Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="description">Description</param>
        public AppRole(string name, string description) : base(name)
        {
            Description = description;
        }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [StringLength(250)]
        public string Description { get; set; }
    }
}