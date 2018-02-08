using System;
using System.ComponentModel.DataAnnotations;

namespace CoolBaby.Application.ViewModels.System
{
    /// <summary>
    /// App role view model class
    /// </summary>
    public class AppRoleViewModel
    {
        /// <summary>
        /// Identitifier
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Name { set; get; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Description { set; get; }
    }
}