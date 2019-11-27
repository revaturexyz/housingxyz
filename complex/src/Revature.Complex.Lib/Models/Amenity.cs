using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This model represents an amenity.
  /// AmenityRoom and AmenityComplex show the sets of amenities that rooms and complexes have.
  /// It only has a type and description.
  /// </summary>
  public class Amenity
  {
    [Required]
    public Guid AmenityId { get; set; }

    [Required, MaxLength(50)]
    public string AmenityType { get; set; }

    [MaxLength(100)]
    public string Description { get; set; }
  }
}
