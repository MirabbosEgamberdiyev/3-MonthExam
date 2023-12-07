﻿

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class BaseEntity
{
    [Key, Required]
    public int Id { get; set; }
}
